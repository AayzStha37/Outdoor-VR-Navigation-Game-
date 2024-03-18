using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/**
This function is currently not in use and is replaced by Task1SurfaceRendererSystem.cs
**/

[System.Serializable]
public class TextureMaterialData
{
    public string key;
    public Material material;

    public Material GetValueForKey(string desiredKey)
    {
        if (key == desiredKey)
        {
            return material;
        }
        return null; 
    }
}

public class Task1TextueControllerSystem : MonoBehaviour
{
    

    private List<Tuple<string, string>> dualTexturePairsList = new List<Tuple<string, string>>();
    private List<string> singleTextureOccurenceList;
    private float timer = 0f;
    private bool isTiming = false;

    private int currentIndex = 0;
    public List<TextureMaterialData> textureMaterialDataList = new List<TextureMaterialData>();
    public List<AK.Wwise.Switch> akSwitches = new List<AK.Wwise.Switch>();

    void Start()
    {  
        if (gameObject.CompareTag("Task 1.1"))
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

    // Update is called once per frame
    void Update()
    {        
        storeTaskCompletiontime();        
    }

    public void NextSubTaskButtonClicked(){
        logTaskCompetionTime();
            //Task 1.1 : Both audio and haptics
            if(gameObject.CompareTag("Task 1.1")){
                currentIndex = (currentIndex + 1) % dualTexturePairsList.Count;
                Tuple<string, string> currentPair = dualTexturePairsList[currentIndex];
                GameObject texture_1 = transform.GetChild(0).gameObject;
                GameObject texture_2 = transform.GetChild(1).gameObject;

                texture_1.tag = currentPair.Item1;
                texture_2.tag = currentPair.Item2;

                texture_1.GetComponentInChildren<MeshRenderer>().material = getMaterialData(currentPair.Item1);
                texture_2.GetComponentInChildren<MeshRenderer>().material = getMaterialData(currentPair.Item2);

                SetAudioSwitchByName(texture_1,checkForSwitchType(currentPair.Item1));
                SetAudioSwitchByName(texture_2,checkForSwitchType(currentPair.Item2));
            }
            //Task 1.2 : No haptics and only audio
            else if(gameObject.CompareTag("Task 1.2")){
                currentIndex = (currentIndex + 1) % singleTextureOccurenceList.Count;
                GameObject texture = transform.GetChild(0).gameObject;

                texture.tag = singleTextureOccurenceList[currentIndex];

                texture.GetComponentInChildren<MeshRenderer>().material = getMaterialData(singleTextureOccurenceList[currentIndex]);

                SetAudioSwitchByName(texture,checkForSwitchType(singleTextureOccurenceList[currentIndex]));
            }
            //Task 1.3 : No audio and only haptics
            else if(gameObject.CompareTag("Task 1.3")){
                currentIndex = (currentIndex + 1) % singleTextureOccurenceList.Count;
                GameObject texture = transform.GetChild(0).gameObject;
                
                texture.tag = singleTextureOccurenceList[currentIndex];

                texture.GetComponentInChildren<MeshRenderer>().material = getMaterialData(singleTextureOccurenceList[currentIndex]);
            }
    }

    private void logTaskCompetionTime()
    {
        if(!isTiming){
            isTiming = true;
            Debug.Log("GAMELOG: New subtask started");
            Debug.Log("GAMELOG: Timer started.");
        }else{
            isTiming = false;
            Debug.Log("GAMELOG: Timer stopped. Task Completion time: " + timer.ToString("F2") + " seconds");
            timer = 0f;
            logTaskCompetionTime();
        }
    }

    private void storeTaskCompletiontime()
    {
        if (isTiming)
        {
            timer += Time.deltaTime;
        }
    }

   

    private AK.Wwise.Switch checkForSwitchType(string texture_item)
    {
        string switchName;
        switch(texture_item){
            case Constants.TEXTURE_GRASS:
                switchName = "GroundType_Sweep / Grass";
                break;
            case Constants.TEXTURE_ASPHALT:
                switchName = "GroundType_Sweep / Road";
                break;
            case Constants.TEXTURE_SIDEWALK:
                switchName = "GroundType_Sweep / Sidewalk";
                break;
            case Constants.TEXTURE_METAL:
            case Constants.TEXTURE_GRAVEL:
            default:
                switchName = "GroundType_Sweep / Null";
                break;
        }

        foreach(AK.Wwise.Switch akswitch in akSwitches){
            if (akswitch != null && akswitch.Name == switchName )
            {
                return akswitch;
            }
        }
        return null;
    }

    private Material getMaterialData(String texture)
    {
        foreach (var item in textureMaterialDataList)
        {
            Material material = item.GetValueForKey(texture);
            if (material != null) 
            {
                return material; 
            }
        }
        
        return null; 
    }

    private void SetAudioSwitchByName(GameObject childGameObject, AK.Wwise.Switch switchToSet)
    {
        //akSwitches.Find(x => x.ToString() == switchToSet.ToString()).SetValue(childGameObject);
        // foreach(AK.Wwise.Switch akSwitch in akSwitches){
        //     if (akSwitch != null && akSwitch.Name == switchToSet.Name )
        //     {
                
        //         break;
        //     }
        // }
        AK.Wwise.Switch[] switchComponents = childGameObject.GetComponents<AK.Wwise.Switch>();

        foreach(AK.Wwise.Switch akSwitch in switchComponents){
            if (akSwitch != null && akSwitch.Name != switchToSet.Name)
                akSwitch.SetValue(null);
        }
    }
}
