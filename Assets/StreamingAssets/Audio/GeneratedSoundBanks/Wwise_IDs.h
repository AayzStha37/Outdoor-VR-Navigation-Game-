/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID AMBIENTSOUNDEVENT = 3307224408U;
        static const AkUniqueID FOOTSTEPSPLAYEVENT = 654585276U;
        static const AkUniqueID GROUNDSWEEPEVENT = 2424624112U;
        static const AkUniqueID LEVELCOMPLETESOUNDEVENT = 1944751995U;
        static const AkUniqueID PLAYPOLICESIRENEVENT = 882516764U;
        static const AkUniqueID STARTSCENETRAFFICSOUNDEVENT = 275942671U;
        static const AkUniqueID TRAFFICLIGHTWAITSOUNDEVENT = 3187816352U;
        static const AkUniqueID TRAFFICLIGHTWALKSOUNDEVENT = 3123712896U;
        static const AkUniqueID VEHICLEINMOTIONEVENT = 3804784588U;
        static const AkUniqueID VEHICLESTAIONARYEVENT = 3013206709U;
    } // namespace EVENTS

    namespace SWITCHES
    {
        namespace AMBIENT_SOUNDS_SWITCH
        {
            static const AkUniqueID GROUP = 1077759249U;

            namespace SWITCH
            {
                static const AkUniqueID BUS_STOP_CONVO = 3633441448U;
                static const AkUniqueID CHILDREN_PALYING = 2364011221U;
                static const AkUniqueID CONSTRUCTION = 2634775690U;
                static const AkUniqueID CONSTRUCTION_SITE_CONVO = 2070892264U;
                static const AkUniqueID CROWD_AMBIENT_NOISE = 1073138942U;
                static const AkUniqueID CROWD_CHEERING_DANCING = 2449584239U;
                static const AkUniqueID HOSPTIAL_AMBULANCE = 492490026U;
                static const AkUniqueID HOTDOG_CONVO_PHONE = 1349895145U;
                static const AkUniqueID HOTDOG_STAND = 2409619181U;
                static const AkUniqueID PARK_ENTRANCE_CONVO = 2591134982U;
            } // namespace SWITCH
        } // namespace AMBIENT_SOUNDS_SWITCH

        namespace FOOTSTEPS_WALK
        {
            static const AkUniqueID GROUP = 1780308710U;

            namespace SWITCH
            {
                static const AkUniqueID GRAVEL = 2185786256U;
                static const AkUniqueID NULL = 784127654U;
                static const AkUniqueID ROAD = 2110808655U;
                static const AkUniqueID SIDEWALK = 3137249879U;
            } // namespace SWITCH
        } // namespace FOOTSTEPS_WALK

        namespace GROUNDTYPE_SWEEP
        {
            static const AkUniqueID GROUP = 1983642705U;

            namespace SWITCH
            {
                static const AkUniqueID GRASS = 4248645337U;
                static const AkUniqueID NULL = 784127654U;
                static const AkUniqueID ROAD = 2110808655U;
                static const AkUniqueID SIDEWALK = 3137249879U;
            } // namespace SWITCH
        } // namespace GROUNDTYPE_SWEEP

        namespace VEHICLE_MOTION_GAMESYNC
        {
            static const AkUniqueID GROUP = 1515766668U;

            namespace SWITCH
            {
                static const AkUniqueID BUS = 714721605U;
                static const AkUniqueID CAR = 983016381U;
            } // namespace SWITCH
        } // namespace VEHICLE_MOTION_GAMESYNC

        namespace VEHICLE_STATIONARY_GAMESYNC
        {
            static const AkUniqueID GROUP = 2268314992U;

            namespace SWITCH
            {
                static const AkUniqueID BUS = 714721605U;
                static const AkUniqueID CAR = 983016381U;
            } // namespace SWITCH
        } // namespace VEHICLE_STATIONARY_GAMESYNC

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID CANEINTERACTIONPITCH = 1410364352U;
        static const AkUniqueID CANEINTERACTIONVOLUME = 2349282220U;
        static const AkUniqueID CHARACTERNAVSPEED = 3094130262U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID AMBIENT_SOUNDS = 2141388522U;
        static const AkUniqueID CHARACTER_FOOTSTEPS = 2775932802U;
        static const AkUniqueID LEVEL_COMPLETE = 3736098925U;
        static const AkUniqueID POLICE_SIREN_BANK = 2551573942U;
        static const AkUniqueID START_SCENE_TRAFFIC = 1659826720U;
        static const AkUniqueID TRAFFICLIGHT_WAIT = 3736590696U;
        static const AkUniqueID TRAFFICLIGHT_WALK = 3686257672U;
        static const AkUniqueID VEHICLE_MOTION_BANK = 663403457U;
        static const AkUniqueID VEHICLE_STATIONARY_BANK = 564684285U;
        static const AkUniqueID WHITECANE_INTERACTION = 3669902090U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
