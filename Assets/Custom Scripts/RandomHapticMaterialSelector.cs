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
    public List<HapticMaterial> metalHapticMaterials;
    public List<HapticMaterial> gravelHapticMaterials;
    
    private void Start() {
        
    }
    
    public HapticMaterial GetRandomHapticFile(string textureInContact){
        switch(textureInContact){
            case Constants.TEXTURE_GRASS:            
                Debug.Log("Chosen Haptic Material : Grass");
                return PickRandomHapticFile(grassHapticMaterials);
            case Constants.TEXTURE_ASPHALT:            
                Debug.Log("Chosen Haptic Material : Asphalt");
                return PickRandomHapticFile(roadHapticMaterials);
            case Constants.TEXTURE_SIDEWALK:
                Debug.Log("Chosen Haptic Material : Sidewalk");
                return PickRandomHapticFile(sidewalkHapticMaterials);
            case Constants.TEXTURE_METAL:
                Debug.Log("Chosen Haptic Material : Metal");
                return PickRandomHapticFile(metalHapticMaterials);
            case Constants.TEXTURE_GRAVEL:
                Debug.Log("Chosen Haptic Material : Gravel");
                return PickRandomHapticFile(gravelHapticMaterials);
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
