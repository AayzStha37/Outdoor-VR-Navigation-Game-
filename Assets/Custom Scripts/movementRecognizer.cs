using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using PDollarGestureRecognizer;
using System.IO;
using UnityEngine.Events;
using Newtonsoft.Json;
using System;

public class movementRecognizer : MonoBehaviour
{   
    public XRNode inputSource;
    public UnityEngine.XR.Interaction.Toolkit.InputHelpers.Button inputButton;
    public float inputThreshold = 0.1f;
    public Transform movementSource;
    //Distance to be maintained (5cm) between the current position and the last postion
    public float newPostionthresholdDistance = 0.05f;
    private bool isMoving = false;
    private List<Vector3> positionsList = new List<Vector3>();
    public GameObject debugCube;
    //parameter to differentitate between creating a new gesture or recognize it using the PDollar algo
    public bool creationMode =  true;
    public string newGestureName;
    public List<Gesture> trainingSet = new List<Gesture>();

    public float recognitionThreshold = 0.09f;

    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string>{}
    public UnityStringEvent onRecognized;

    public GameObject primaryCollsionObject;
    public GameObject secondaryCollsionObject;

    private static string palneInfoJsonPath; 
    // Start is called before the first frame update
    void Start()
    {
        string[] gestureFiles =  Directory.GetFiles(Application.persistentDataPath, "*.xml");
        palneInfoJsonPath = Application.persistentDataPath+"/PlanesInfoJSONS";
        foreach(var item in gestureFiles){
            trainingSet.Add(GestureIO.ReadGestureFromFile(item));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //getting the prompt that the input button has been pressed in the input source
        //UnityEngine.XR.Interaction.Toolkit.InputHelpers.isColliding(InputDevices.GetDeviceAtXRNode(inputSource),inputButton, out bool isColliding, inputThreshold);

        bool isColliding = CollisionDetectionCustomScript.IsTouching(primaryCollsionObject,secondaryCollsionObject);

        //Starting the movement
        if(!isMoving && isColliding){
            startMovement();
        }
        //Ending the movement
        else if(isMoving && !isColliding){
            endMovement();
        }
        //Updating the movement
        else if(isMoving && isColliding){
            updateMovement();
        }
    }

    void startMovement(){
        isMoving=true;
        Debug.Log("Movement has been initiated");

        if(debugCube){
            //Instantiate gameobject of type - cube at the current position of the input source
            //Destroy the instantiated game object after 3 seconds
            Destroy(Instantiate(debugCube,movementSource.position,Quaternion.identity),3);
        }
            
        positionsList.Clear();
        positionsList.Add(movementSource.position);
    }


    void updateMovement(){
        Debug.Log("Movement being updated");
        Vector3 lastPostion = positionsList[positionsList.Count-1];
        if(Vector3.Distance(movementSource.position,lastPostion) > newPostionthresholdDistance){
            positionsList.Add(movementSource.position);
        }
        if(debugCube)   
            Destroy(Instantiate(debugCube,movementSource.position,Quaternion.identity),3);
    }


    void endMovement(){
        isMoving=false;
        Debug.Log("Movement has been terminated");
        Point[] pointsArray = new Point[positionsList.Count];

        for(int i =0; i< positionsList.Count; i++){
            //projecting 3D points to the 2D plane
           Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionsList[i]);
           pointsArray[i] = new Point(screenPoint.x,screenPoint.y, 0);
        }

        Gesture gesture = new Gesture(pointsArray);
        //Adding a new gesture to the training set
        if(creationMode){
            gesture.Name = newGestureName;
            trainingSet.Add(gesture);
            string traningFileName = Application.persistentDataPath + "/" + newGestureName + ".xml";
            GestureIO.WriteGesture(pointsArray,newGestureName,traningFileName);
            Debug.Log("Gesture CREATED - "+ gesture.Name);
        }
        //recognizing the gesture using the Pdollar algo
        else{
            Result result = PointCloudRecognizer.Classify(gesture, trainingSet.ToArray());
            if(result.Score>=recognitionThreshold){
                onRecognized.Invoke(result.GestureClass); 
                Debug.Log("Gesture RECOGNIZED - "+ result.GestureClass + result.Score);
                textureRecognizer(secondaryCollsionObject, result.GestureClass);
            }
        }
    }

    private void textureRecognizer(GameObject secondaryCollsionObject, string motionType)
    {
        string hapticInfo;
        string soundInfo;
        
        SFXManager sFXManager = new SFXManager();

        string[] jsonFileContent = Directory.GetFiles(palneInfoJsonPath,secondaryCollsionObject.tag+".json");
        dynamic DynamicData = JsonConvert.DeserializeObject(System.IO.File.ReadAllText(jsonFileContent[0]));
        
        if(motionType.Equals("pointMotion")){
            hapticInfo = DynamicData.pointMotion.haptic;
            soundInfo = DynamicData.pointMotion.sound;
            
            sFXManager.playGestureAudio(soundInfo+".mp3");
            
            Debug.Log("<color=red>HAPTIC - </color>"+hapticInfo);
            Debug.Log("<color=red>SOUND - </color>"+soundInfo);
        }else if(motionType.Equals("sweepMotion")){
            hapticInfo = DynamicData.sweepMotion.haptic;
            soundInfo = DynamicData.sweepMotion.sound;

            sFXManager.playGestureAudio(soundInfo+".mp3");

            Debug.Log("<color=red>HAPTIC - </color>"+hapticInfo);
            Debug.Log("<color=red>SOUND - </color>"+soundInfo);
        }

       
    }
}
