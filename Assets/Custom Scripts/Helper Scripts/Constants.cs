using System.Collections.Generic;

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

    public static List<string> COLLIDER_TAG_LIST = new List<string>{TEXTURE_GRASS, TEXTURE_ASPHALT, TEXTURE_GRAVEL, TEXTURE_SIDEWALK, TEXTURE_METAL};

}