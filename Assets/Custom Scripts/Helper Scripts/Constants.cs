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
    public const string MAIN_CHARACTER_BODY = "Character body";

    //Scene names constant
    public const string TASK1_SCENE_NAME = "Task 1 scene";    
    public const string TASK2_SCENE_NAME = "Task 2 scene";    
    public const string TASK3_SCENE_NAME = "Task 3 scene";    
    public const string TASK1_FIRST_TASK_OBJECT_TAG = "Task 1.1";
    public const string TASK1_SECOND_TASK_OBJECT_TAG = "Task 1.2";
    public const string TASK1_THIRD_TASK_OBJECT_TAG = "Task 1.3";
    public const string TASK3_PATHWAY_1 = "Task 3.1 Pathway";
    public const string TASK3_PATHWAY_2 = "Task 3.2 Pathway";
    public const string TASK3_INTERMEDIATE_TARGET = "Intermediate";
    public const string TASK3_DESTINATION_TARGET = "Destination";
    public const string TASK2_COMPLETION_COLLIDER = "Completion collider";
    public const string LOG_PATH = @"D:\GEM Lab\GEMLabHCIResearch\Unity Logs";
    public const string ENDPOINT_COLLISON_REGISTER = "collisonRegister";
    public const string ENDPOINT_DFT321_PREDCICT = "predict";
    public const string FLASK_SERVER_URL = "http://127.0.0.1:5000/";
    public static List<string> COLLIDER_TAG_LIST = new List<string>{TEXTURE_GRASS, TEXTURE_ASPHALT, TEXTURE_GRAVEL, TEXTURE_SIDEWALK, TEXTURE_METAL};

}