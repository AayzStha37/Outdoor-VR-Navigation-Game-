using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Task1SurfaceRendererSystem : MonoBehaviour
{
    private List<Tuple<string, string>> dualTexturePairsList = new List<Tuple<string, string>>();
    private List<string> singleTextureOccurenceList;
    private float timer = 0f;
    private bool isTiming = false;

    private int currentIndex = 0;
    public List<GameObject> toRenderSurfaceTextureList = new List<GameObject>();

    private  Vector3 firstTaskFirstSurfacePosition = new Vector3(-0.154296875f,0,-3.18000031f);
    private  Vector3 firstTaskSecondSurfacePosition = new Vector3(-0.179992676f,0,0.0599975586f);
    private  Vector3 firstTaskSurfaceScale = new Vector3(65.2907257f,161.39765f,100);
    private  Vector3 secondTaskSurfacePosition = new Vector3(0.627984107f,-0.0399999619f,-2.45160246f);
    private  Vector3 secondTaskSurfaceScale = new Vector3(185.28389f,502.727997f,100.000008f);
    private  Vector3 surfaceRotation = new Vector3(270f,0f,0f);
    private int spcaeBarPressCount = 0;

    private List<GameObject> instantiatedGameObjectList = new List<GameObject>();
    void Start()
    {  
        InitTextureList();
    }

    void Update()
    {        
        storeTaskCompletiontime();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spcaeBarPressCount++;
            LogTaskCompetionTime();
            DestroyInstantiatedObjects();
            RenderTaskBasedSurfaces(spcaeBarPressCount);
        }            
    }

    public void NextSubTaskButtonClicked(){
        LogTaskCompetionTime();
            
    }

    
    private void InitTextureList()
    {
        if (gameObject.CompareTag(Constants.TASK1_FIRST_TASK_OBJECT_TAG))
        {
            dualTexturePairsList.Add(new Tuple<string, string>(Constants.TEXTURE_GRASS, Constants.TEXTURE_SIDEWALK));        
            dualTexturePairsList.Add(new Tuple<string, string>(Constants.TEXTURE_GRAVEL, Constants.TEXTURE_GRASS));    
            dualTexturePairsList.Add(new Tuple<string, string>(Constants.TEXTURE_ASPHALT, Constants.TEXTURE_METAL));    
            dualTexturePairsList.Add(new Tuple<string, string>(Constants.TEXTURE_SIDEWALK, Constants.TEXTURE_ASPHALT));    
            dualTexturePairsList.Add(new Tuple<string, string>(Constants.TEXTURE_METAL, Constants.TEXTURE_GRAVEL));
        }
        else {
            singleTextureOccurenceList = new List<string> { Constants.TEXTURE_GRASS, Constants.TEXTURE_ASPHALT, Constants.TEXTURE_GRAVEL, Constants.TEXTURE_SIDEWALK};
        }
    }

    private void RenderTaskBasedSurfaces(int spcaeBarPressCount)
    {
        if (gameObject.CompareTag(Constants.TASK1_FIRST_TASK_OBJECT_TAG) && spcaeBarPressCount<=5) 
            //Task 1.1 : Both audio and haptics
            GenerateSurfacePair(dualTexturePairsList, currentIndex, firstTaskFirstSurfacePosition, firstTaskSecondSurfacePosition, firstTaskSurfaceScale);

        else if ((gameObject.CompareTag(Constants.TASK1_SECOND_TASK_OBJECT_TAG) || gameObject.CompareTag(Constants.TASK1_THIRD_TASK_OBJECT_TAG)) && spcaeBarPressCount<=4) 
            //Task 1.2 : No haptics and only audio
            //Task 1.3 : No audio and only haptics
            GenerateSingleSurface(singleTextureOccurenceList, currentIndex, secondTaskSurfacePosition, secondTaskSurfaceScale);
    }

    void GenerateSurfacePair(List<Tuple<string, string>> texturePairsList, int index, Vector3 position1, Vector3 position2, Vector3 scale) {
        currentIndex = (currentIndex + 1) % texturePairsList.Count;
        Tuple<string, string> currentPair = texturePairsList[currentIndex];

        InstantiateSurface(currentPair.Item1, position1, scale);
        InstantiateSurface(currentPair.Item2, position2, scale);
    }

    void GenerateSingleSurface(List<string> textureOccurenceList, int index, Vector3 position, Vector3 scale) {
        currentIndex = (currentIndex + 1) % textureOccurenceList.Count;
        InstantiateSurface(textureOccurenceList[currentIndex], position, scale);
    }

    void InstantiateSurface(string textureName, Vector3 position, Vector3 scale) {
        GameObject surface = Instantiate(toRenderSurfaceTextureList?.Find(x => x.name == textureName));
        surface.transform.SetParent(transform);
        surface.transform.localPosition = position;
        surface.transform.localRotation = Quaternion.Euler(surfaceRotation);
        surface.transform.localScale = scale;
        instantiatedGameObjectList.Add(surface);
    }   

    private void LogTaskCompetionTime()
    {
        if(!isTiming){
            isTiming = true;
            Debug.Log("GAMELOG: New subtask started");
            Debug.Log("GAMELOG: Timer started.");
        }else{
            isTiming = false;
            Debug.Log("GAMELOG: Timer stopped. Task Completion time: " + timer.ToString("F2") + " seconds");
            timer = 0f;
            LogTaskCompetionTime();
        }
    }

    private void storeTaskCompletiontime()
    {
        if (isTiming)
        {
            timer += Time.deltaTime;
        }
    }

      private void DestroyInstantiatedObjects()
    {
        if(instantiatedGameObjectList.Any())
        {    
            foreach(GameObject gameObject in instantiatedGameObjectList){
                Destroy(gameObject);
            }
            instantiatedGameObjectList.Clear();
        }
    }

}
