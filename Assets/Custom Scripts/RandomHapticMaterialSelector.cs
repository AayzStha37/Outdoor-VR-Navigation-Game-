using System;
using System.Collections;
using System.Collections.Generic;
using Interhaptics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomHapticMaterialSelector : MonoBehaviour
{
    public List<HapticMaterial> grassHapticMaterials;
    public List<HapticMaterial> sidewalkHapticMaterials;
    public List<HapticMaterial> roadHapticMaterials;
    private void Start() {
        
    }
    
    public HapticMaterial GetRandomHapticFile(string textureInContact){
        switch(textureInContact){
            case Constants.TEXTURE_GRASS:
                return PickRandomHapticFile(grassHapticMaterials);
            case Constants.TEXTURE_ASPHALT:            
                return PickRandomHapticFile(roadHapticMaterials);
            case Constants.TEXTURE_SIDEWALK:
                return PickRandomHapticFile(sidewalkHapticMaterials);
            case Constants.TEXTURE_METAL:
            case Constants.TEXTURE_GRAVEL:
            default:
                return null;
        }
    }

    private HapticMaterial PickRandomHapticFile(List<HapticMaterial> hapticMatrialFileList)
    {
        if (hapticMatrialFileList != null && hapticMatrialFileList.Count > 0)
        {
            int randomIndex = Random.Range(0, hapticMatrialFileList.Count);
            return hapticMatrialFileList[randomIndex];
        }
        return null;
    }
}
