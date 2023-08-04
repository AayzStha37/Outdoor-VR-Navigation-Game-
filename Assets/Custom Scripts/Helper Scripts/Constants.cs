using System.Collections.Generic;

public static class Constants
    {
        
    public const string RoadHapticsProfileTag = "Road";
    public const string GrassHapticsProfileTag = "Grass";
    public const string SidewalkHapticsProfileTag = "Sidewalk";
    public const string GravelHapticsProfileTag = "Gravel";
    public const string WhiteCaneTipTag = "Cane";
    public const string DefaultPlaneTag = "DefaultPlane";
    public const string StartHapticsFlag = "startHaptics";
    public const string StopHapticsFlag = "stopHaptics";
    public const string RTPC_CharacterNavSpeed = "CharacterNavSpeed";
    public const string RTPC_CaneInteractionPitch = "CaneInteractionPitch";
    public const string RTPC_CaneInteractionVolume = "CaneInteractionVolume";
    public const float stationaryTolerance = 0.005f;

    public static List<string> colliderTagList = new List<string>{RoadHapticsProfileTag, GrassHapticsProfileTag, SidewalkHapticsProfileTag, GravelHapticsProfileTag};

}