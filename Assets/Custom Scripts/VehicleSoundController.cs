    using System;
    using UnityEngine;
using UnityEngine.Video;

public class VehicleSoundController : MonoBehaviour
    {
        private uint vehicleMotionSound = 0;
        private uint vehicleStationarySound = 0;
        private enum VehicleState{moving,stationary,undefined}
        VehicleState currentState;
        VehicleState previousState;
        private VehicleWayPointFollower wayPointFollower;

        void Start()
        {
            wayPointFollower = GetComponent<VehicleWayPointFollower>();
            checkForPoliceVehicle();
            currentState = getCurentVehicleState();
            playAudio(currentState);
        }

    private VehicleState getCurentVehicleState()
    {
        if(gameObject.tag.Equals(Constants.VEHCILE_TAG))
            return checkVehicleState();
        else if (gameObject.tag.Equals(Constants.VEHCILE_DUMMY_TAG))
            return VehicleState.stationary;
        
        return VehicleState.undefined;
    }

    void Update()
        {
            previousState = currentState;            
            currentState = getCurentVehicleState();

            if(currentState.Equals(VehicleState.undefined)){
                Debug.LogError("Vehicle gameobject is in undefine state");
            }else if(compareVehicleState())
                playAudio(currentState);               
        }

        private void playAudio(VehicleState state){
            if(currentState.Equals(VehicleState.stationary)){
                //play staionary sound
                if(vehicleMotionSound>0){
                    AkSoundEngine.StopPlayingID(vehicleMotionSound);
                    vehicleMotionSound = 0;
                }
                vehicleStationarySound = AkSoundEngine.PostEvent(Constants.VEHICLE_STATIONARY_SOUND_EVENT,gameObject);
                
            }else if(currentState.Equals(VehicleState.moving)){
                //play in motion sound            
                if(vehicleStationarySound>0){
                    AkSoundEngine.StopPlayingID(vehicleStationarySound);
                    vehicleStationarySound = 0;
                }
                vehicleMotionSound = AkSoundEngine.PostEvent(Constants.VEHICLE_IN_MOTION_SOUND_EVENT, gameObject);
            }
        }

        private bool compareVehicleState()
        {
            if(previousState.Equals(currentState))
                return false;
            else 
                return true;
        }

        private VehicleState checkVehicleState()
        {
            if(isInMotion())
                return VehicleState.moving;
            else   
                return VehicleState.stationary;
        }

        private bool isInMotion(){
            return wayPointFollower.shouldMove;
        }
        
        private void checkForPoliceVehicle()
        {
           if(gameObject.name.Contains(Constants.POLICE_VEHICLE_PREFAB_NAME))
                AkSoundEngine.PostEvent(Constants.POLICE_SIREN_EVENT,gameObject);
        }

    }
