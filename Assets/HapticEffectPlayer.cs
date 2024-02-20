using UnityEngine;
using Interhaptics.Core;
using Interhaptics;
using Interhaptics.HapticBodyMapping;

public class HapticEffectPlayer : MonoBehaviour
{
    public HapticMaterial myHapticMaterial; 
    public double customIntensity;
    void Start()
    {
        PlayMyHapticEffect();
    }
    void PlayMyHapticEffect()
    {
        if (myHapticMaterial != null)
        {
            HAR.PlayHapticEffect(myHapticMaterial,HAR.GetGlobalIntensity(),10,LateralFlag.Right);
            Debug.Log("Haptic effect played successfully!");
        }
        else
        {
            Debug.LogError("Haptic material is not assigned!");
        }
    }
    void Loop(){
        HAR.SetGlobalIntensity(customIntensity);
    }
}