namespace MalumMenu
{
    public struct CheatToggles
    {
        //Player
        public static bool noClip;
        public static bool speedBoost;
        public static bool teleportPlayer;
        public static bool teleportCursor;
        public static bool reportBody;
        public static bool killPlayer;
        public static bool telekillPlayer;
        public static bool killAll;
        public static bool killAllCrew;
        public static bool killAllImps;
        public static bool flyMode;
        public static bool infiniteStamina;
        public static bool autoRun;
        public static bool wallHack;
        public static bool antiKick;
        public static bool godMode;

        //Roles
        public static bool changeRole;
        public static bool zeroKillCd;
        public static bool completeMyTasks;
        public static bool killReach;
        public static bool killAnyone;
        public static bool endlessSsDuration;
        public static bool endlessBattery;
        public static bool endlessTracking;
        public static bool noTrackingCooldown;
        public static bool noTrackingDelay;
        public static bool noVitalsCooldown;
        public static bool noVentCooldown;
        public static bool endlessVentTime;
        public static bool endlessVanish;
        public static bool killVanished;
        public static bool noVanishAnim;
        public static bool noShapeshiftAnim;
        public static bool autoCompleteTask;
        public static bool instantKill;
        public static bool killThroughWalls;
        public static bool multiKill;

        //ESP
        public static bool fullBright;
        public static bool seeGhosts;
        public static bool seeRoles;
        public static bool seeDisguises;
        public static bool revealVotes;
        public static bool xrayVision;
        public static bool playerESP;
        public static bool taskESP;
        public static bool ventESP;
        public static bool doorESP;
        public static bool itemESP;
        public static bool showPlayerDistance;
        public static bool showPlayerHealth;
        public static bool showPlayerStats;

        //Camera
        public static bool spectate;
        public static bool zoomOut;
        public static bool freecam;
        public static bool nightVision;
        public static bool thermalVision;
        public static bool cameraShake;
        public static bool smoothCamera;

        //Minimap
        public static bool mapCrew;
        public static bool mapImps;
        public static bool mapGhosts;
        public static bool colorBasedMap;
        public static bool showMapTasks;
        public static bool showMapVents;
        public static bool showMapDoors;
        public static bool radarMode;

        //Tracers
        public static bool tracersImps;
        public static bool tracersCrew;
        public static bool tracersGhosts;
        public static bool tracersBodies;
        public static bool colorBasedTracers;
        public static bool distanceBasedTracers;
        public static bool tracersVents;
        public static bool tracersTasks;
        public static bool tracersItems;

        //Chat
        public static bool alwaysChat;
        public static bool chatJailbreak;
        public static bool spamChat;
        public static bool autoChat;
        public static bool chatBot;
        public static bool coloredChat;
        public static bool bigText;
        public static bool invisibleText;
        
        //Ship
        public static bool closeMeeting;
        public static bool doorsSab;
        public static bool unfixableLights;
        public static bool commsSab;
        public static bool elecSab;
        public static bool reactorSab;
        public static bool oxygenSab;
        public static bool mushSab;
        public static bool autoSabotage;
        public static bool preventSabotage;
        public static bool instantRepair;
        public static bool autoRepair;

        //Vents
        public static bool useVents;
        public static bool walkVent;
        public static bool kickVents;
        public static bool ventTeleport;
        public static bool ventSpeed;
        public static bool ventInvisible;

        //Meeting
        public static bool autoVote;
        public static bool voteAnyone;
        public static bool skipVote;
        public static bool forceVote;
        public static bool meetingSpam;
        public static bool instantMeeting;
        public static bool preventMeeting;

        //Game Control
        public static bool forceStart;
        public static bool forceEnd;
        public static bool changeSettings;
        public static bool kickPlayers;
        public static bool banPlayers;
        public static bool controlHost;
        public static bool gameSpeed;
        public static bool timeControl;

        //Visual
        public static bool customColors;
        public static bool rainbowMode;
        public static bool glowEffect;
        public static bool trailEffect;
        public static bool particleEffects;
        public static bool customSkins;
        public static bool bigPlayer;
        public static bool smallPlayer;
        public static bool invisiblePlayer;

        //Audio
        public static bool muteAll;
        public static bool customSounds;
        public static bool soundSpam;
        public static bool voiceChanger;
        public static bool musicPlayer;

        //Network
        public static bool lagSwitch;
        public static bool packetLoss;
        public static bool pingSpoof;
        public static bool regionSpoof;
        public static bool serverControl;

        //Automation
        public static bool autoPlay;
        public static bool botMode;
        public static bool aiAssist;
        public static bool autoWin;
        public static bool smartPlay;

        //Trolling
        public static bool confuseMode;
        public static bool chaosMode;
        public static bool prankMode;
        public static bool annoyMode;
        public static bool disruptMode;

        //Security
        public static bool antiCheat;
        public static bool hideCheat;
        public static bool stealthMode;
        public static bool bypassDetection;

        //Passive
        public static bool unlockFeatures = true;
        public static bool freeCosmetics = true;
        public static bool avoidBans = true;

        public static void DisablePPMCheats(string variableToKeep)
        {
            reportBody = variableToKeep != "reportBody" ? false : reportBody;
            killPlayer = variableToKeep != "killPlayer" ? false : killPlayer;
            telekillPlayer = variableToKeep != "telekillPlayer" ? false : telekillPlayer;
            spectate = variableToKeep != "spectate" ? false : spectate;
            changeRole = variableToKeep != "changeRole" ? false : changeRole;
            teleportPlayer = variableToKeep != "teleportPlayer" ? false : teleportPlayer;
        }

        public static bool shouldPPMClose(){
            return !changeRole && !reportBody && !telekillPlayer && !killPlayer && !spectate && !teleportPlayer;
        }
    }
}