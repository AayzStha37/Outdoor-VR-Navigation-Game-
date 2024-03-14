using System.Collections.Generic;
using System.Linq.Expressions;

public static class Constants
    {
    public const string TEXTURE_GRASS = "Tex_Grass";
    public const string TEXTURE_ASPHALT = "Tex_Asphalt";
    public const string TEXTURE_GRAVEL = "Tex_Gravel";
    public const string TEXTURE_SIDEWALK = "Tex_Sidewalk";
    public const string TEXTURE_METAL = "Tex_Metal";
    public const string WhiteCaneTipTag = "Cane";
    public const string DefaultPlaneTag = "DefaultPlane";
    public const string StartHapticsFlag = "startHaptics";
    public const string StopHapticsFlag = "stopHaptics";
    public const string RTPC_CharacterNavSpeed = "CharacterNavSpeed";
    public const string RTPC_CaneInteractionPitch = "CaneInteractionPitch";
    public const string RTPC_CaneInteractionVolume = "CaneInteractionVolume";
    public const float stationaryTolerance = 0.01f;

    public const string WaitSound = "waitSound";
    public const string WalkSound = "walkSound";
    public const string COLLISION_ON = "ON";
    public const string COLLISION_OFF = "OFF";
    public const string XR_ORIGIN_MAIN_CHARACTER = "XR origin (Main character)";
    public const string VEHCILE_TAG = "Vehicle";
    public const string VEHCILE_DUMMY_TAG = "Dummy_vehicle";
    public const string MAIN_CHARACTER_BODY = "Main Character First Person Body";
    public static List<string> COLLIDER_TAG_LIST = new List<string>{TEXTURE_GRASS, TEXTURE_ASPHALT, TEXTURE_GRAVEL, TEXTURE_SIDEWALK, TEXTURE_METAL};
    public const string POLICE_VEHICLE_PREFAB_NAME= "Police";
    //Scene names constant
    public const string TASK1_SCENE_NAME = "Task 1 scene";    
    public const string TASK2_SCENE_NAME = "Task 2 scene";    
    public const string TASK3_SCENE_NAME = "Task 3 scene";    

    //Task specific constants
    public const string TASK1_FIRST_TASK_OBJECT_TAG = "Task 1.1";
    public const string TASK1_SECOND_TASK_OBJECT_TAG = "Task 1.2";
    public const string TASK1_THIRD_TASK_OBJECT_TAG = "Task 1.3";
    public const string TASK3_PATHWAY_1 = "Task 3.1 Pathway";
    public const string TASK3_PATHWAY_2 = "Task 3.2 Pathway";
    public const string TASK3_INTERMEDIATE_TARGET = "Intermediate";
    public const string TASK3_DESTINATION_TARGET = "Destination";
    public const string TASK2_COMPLETION_COLLIDER = "Completion collider";

    //Main device specific constant
    public const string LOG_PATH = @"C:\Users\Aayush Shrestha\Desktop\GONVI\Outdoor-VR-Navigation-Game-\Game Logs";
    //Server specfic constants
    public const string ENDPOINT_COLLISON_REGISTER = "collisonRegister";
    public const string ENDPOINT_DFT321_PREDCICT = "predict";
    public const string FLASK_SERVER_URL = "http://127.0.0.1:5000/";


    //wwise audio events
    public const string FOOTSTEPS_PLAY_EVENT= "FootstepsPlayEvent";
    public const string AMBIENT_SOUND_EVENT= "AmbientSoundEvent";
    public const string TRAFFIC_LIGHT_WALK_SOUND_EVENT= "TrafficLightWalkSoundEvent";
    public const string TRAFFIC_LIGHT_WAIT_SOUND_EVENT= "TrafficLightWaitSoundEvent";
    public const string LEVEL_COMPLETE_SOUND_EVENT= "LevelCompleteSoundEvent";
    public const string VEHICLE_IN_MOTION_SOUND_EVENT= "VehicleInMotionEvent";
    public const string VEHICLE_STATIONARY_SOUND_EVENT= "VehicleStaionaryEvent";
    public const string GROUND_SWEEP_EVENT= "GroundSweepEvent";
    public const string POLICE_SIREN_EVENT= "PlayPoliceSirenEvent";

    //folder paths
    public const string HAPTICS_RESOURCE_FOLDER_PATH = @"D:\GEM Lab\GONVI\Outdoor-VR-Navigation-Game-\Assets\Resources\Haptics";
    public const string SIDEWALK_FOLDER_PATH = @"\Sidewalk";
    public const string GRASS_FOLDER_PATH = @"\Grass";    
    public const string ROAD_FOLDER_PATH = @"\Road";

     public const string PEDESTRIAN_CONVERSATION_TAG = "Pedestrain_conversation";
     public const string LOOP_SOUND_TAG = "Loop_Sound";


}