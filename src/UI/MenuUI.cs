using UnityEngine;
using System.Collections.Generic;

namespace MalumMenu;
public class MenuUI : MonoBehaviour
{

    public List<GroupInfo> groups = new List<GroupInfo>();
    private bool isDragging = false;
    private Rect windowRect = new Rect(10, 10, 320, 600);
    private bool isGUIActive = false;
    private GUIStyle submenuButtonStyle;
    private Vector2 scrollPosition = Vector2.zero;

    // Create all groups (buttons) and their toggles on start
    private void Start()
    {
        groups.Add(new GroupInfo("Player", false, new List<ToggleInfo>() {
            new ToggleInfo(" NoClip", () => CheatToggles.noClip, x => CheatToggles.noClip = x),
            new ToggleInfo(" SpeedHack", () => CheatToggles.speedBoost, x => CheatToggles.speedBoost = x),
            new ToggleInfo(" Fly Mode", () => CheatToggles.flyMode, x => CheatToggles.flyMode = x),
            new ToggleInfo(" Infinite Stamina", () => CheatToggles.infiniteStamina, x => CheatToggles.infiniteStamina = x),
            new ToggleInfo(" Auto Run", () => CheatToggles.autoRun, x => CheatToggles.autoRun = x),
            new ToggleInfo(" Wall Hack", () => CheatToggles.wallHack, x => CheatToggles.wallHack = x),
            new ToggleInfo(" Anti Kick", () => CheatToggles.antiKick, x => CheatToggles.antiKick = x),
            new ToggleInfo(" God Mode", () => CheatToggles.godMode, x => CheatToggles.godMode = x),
            }, new List<SubmenuInfo> {
            new SubmenuInfo("Teleport", false, new List<ToggleInfo>() {
                new ToggleInfo(" to Cursor", () => CheatToggles.teleportCursor, x => CheatToggles.teleportCursor = x),
                new ToggleInfo(" to Player", () => CheatToggles.teleportPlayer, x => CheatToggles.teleportPlayer = x),
            }),
        }));

        groups.Add(new GroupInfo("ESP", false, new List<ToggleInfo>() {
            new ToggleInfo(" See Roles", () => CheatToggles.seeRoles, x => CheatToggles.seeRoles = x),
            new ToggleInfo(" See Ghosts", () => CheatToggles.seeGhosts, x => CheatToggles.seeGhosts = x),
            new ToggleInfo(" No Shadows", () => CheatToggles.fullBright, x => CheatToggles.fullBright = x),
            new ToggleInfo(" Reveal Votes", () => CheatToggles.revealVotes, x => CheatToggles.revealVotes = x),
            new ToggleInfo(" X-Ray Vision", () => CheatToggles.xrayVision, x => CheatToggles.xrayVision = x),
            new ToggleInfo(" Player ESP", () => CheatToggles.playerESP, x => CheatToggles.playerESP = x),
            new ToggleInfo(" Task ESP", () => CheatToggles.taskESP, x => CheatToggles.taskESP = x),
            new ToggleInfo(" Vent ESP", () => CheatToggles.ventESP, x => CheatToggles.ventESP = x),
            new ToggleInfo(" Door ESP", () => CheatToggles.doorESP, x => CheatToggles.doorESP = x),
            new ToggleInfo(" Item ESP", () => CheatToggles.itemESP, x => CheatToggles.itemESP = x),
            new ToggleInfo(" Show Distance", () => CheatToggles.showPlayerDistance, x => CheatToggles.showPlayerDistance = x),
            new ToggleInfo(" Show Health", () => CheatToggles.showPlayerHealth, x => CheatToggles.showPlayerHealth = x),
            new ToggleInfo(" Show Stats", () => CheatToggles.showPlayerStats, x => CheatToggles.showPlayerStats = x),
        }, new List<SubmenuInfo> {
            new SubmenuInfo("Camera", false, new List<ToggleInfo>() {
                new ToggleInfo(" Zoom Out", () => CheatToggles.zoomOut, x => CheatToggles.zoomOut = x),
                new ToggleInfo(" Spectate", () => CheatToggles.spectate, x => CheatToggles.spectate = x),
                new ToggleInfo(" Freecam", () => CheatToggles.freecam, x => CheatToggles.freecam = x),
                new ToggleInfo(" Night Vision", () => CheatToggles.nightVision, x => CheatToggles.nightVision = x),
                new ToggleInfo(" Thermal Vision", () => CheatToggles.thermalVision, x => CheatToggles.thermalVision = x),
                new ToggleInfo(" Camera Shake", () => CheatToggles.cameraShake, x => CheatToggles.cameraShake = x),
                new ToggleInfo(" Smooth Camera", () => CheatToggles.smoothCamera, x => CheatToggles.smoothCamera = x)
            }),
            new SubmenuInfo("Tracers", false, new List<ToggleInfo>() {
                new ToggleInfo(" Crewmates", () => CheatToggles.tracersCrew, x => CheatToggles.tracersCrew = x),
                new ToggleInfo(" Impostors", () => CheatToggles.tracersImps, x => CheatToggles.tracersImps = x),
                new ToggleInfo(" Ghosts", () => CheatToggles.tracersGhosts, x => CheatToggles.tracersGhosts = x),
                new ToggleInfo(" Dead Bodies", () => CheatToggles.tracersBodies, x => CheatToggles.tracersBodies = x),
                new ToggleInfo(" Color-based", () => CheatToggles.colorBasedTracers, x => CheatToggles.colorBasedTracers = x),
                new ToggleInfo(" Vents", () => CheatToggles.tracersVents, x => CheatToggles.tracersVents = x),
                new ToggleInfo(" Tasks", () => CheatToggles.tracersTasks, x => CheatToggles.tracersTasks = x),
                new ToggleInfo(" Items", () => CheatToggles.tracersItems, x => CheatToggles.tracersItems = x),
            }),
            new SubmenuInfo("Minimap", false, new List<ToggleInfo>() {
                new ToggleInfo(" Crewmates", () => CheatToggles.mapCrew, x => CheatToggles.mapCrew = x),
                new ToggleInfo(" Impostors", () => CheatToggles.mapImps, x => CheatToggles.mapImps = x),
                new ToggleInfo(" Ghosts", () => CheatToggles.mapGhosts, x => CheatToggles.mapGhosts = x),
                new ToggleInfo(" Color-based", () => CheatToggles.colorBasedMap, x => CheatToggles.colorBasedMap = x),
                new ToggleInfo(" Show Tasks", () => CheatToggles.showMapTasks, x => CheatToggles.showMapTasks = x),
                new ToggleInfo(" Show Vents", () => CheatToggles.showMapVents, x => CheatToggles.showMapVents = x),
                new ToggleInfo(" Show Doors", () => CheatToggles.showMapDoors, x => CheatToggles.showMapDoors = x),
                new ToggleInfo(" Radar Mode", () => CheatToggles.radarMode, x => CheatToggles.radarMode = x)
            }),
        }));

        groups.Add(new GroupInfo("Roles", false, new List<ToggleInfo>() {
            new ToggleInfo(" Set Fake Role", () => CheatToggles.changeRole, x => CheatToggles.changeRole = x),
            new ToggleInfo(" Auto Complete Tasks", () => CheatToggles.autoCompleteTask, x => CheatToggles.autoCompleteTask = x),
            new ToggleInfo(" Instant Kill", () => CheatToggles.instantKill, x => CheatToggles.instantKill = x),
            new ToggleInfo(" Kill Through Walls", () => CheatToggles.killThroughWalls, x => CheatToggles.killThroughWalls = x),
            new ToggleInfo(" Multi Kill", () => CheatToggles.multiKill, x => CheatToggles.multiKill = x),
        }, 
            new List<SubmenuInfo> {
                new SubmenuInfo("Impostor", false, new List<ToggleInfo>() {
                    new ToggleInfo(" Kill Reach", () => CheatToggles.killReach, x => CheatToggles.killReach = x),
                    new ToggleInfo(" Kill Anyone", () => CheatToggles.killAnyone, x => CheatToggles.killAnyone = x),
                    new ToggleInfo(" No Kill Cooldown", () => CheatToggles.zeroKillCd, x => CheatToggles.zeroKillCd = x),
                }),
                new SubmenuInfo("Shapeshifter", false, new List<ToggleInfo>() {
                    new ToggleInfo(" No Ss Animation", () => CheatToggles.noShapeshiftAnim, x => CheatToggles.noShapeshiftAnim = x),
                    new ToggleInfo(" Endless Ss Duration", () => CheatToggles.endlessSsDuration, x => CheatToggles.endlessSsDuration = x),
                }),
                new SubmenuInfo("Crewmate", false, new List<ToggleInfo>() {
                    new ToggleInfo(" Complete My Tasks", () => CheatToggles.completeMyTasks, x => CheatToggles.completeMyTasks = x)
                }),
                new SubmenuInfo("Tracker", false, new List<ToggleInfo>() {
                    new ToggleInfo(" Endless Tracking", () => CheatToggles.endlessTracking, x => CheatToggles.endlessTracking = x),
                    new ToggleInfo(" No Track Delay", () => CheatToggles.noTrackingDelay, x => CheatToggles.noTrackingDelay = x),
                    new ToggleInfo(" No Track Cooldown", () => CheatToggles.noTrackingCooldown, x => CheatToggles.noTrackingCooldown = x),
                }),
                new SubmenuInfo("Engineer", false, new List<ToggleInfo>() {
                    new ToggleInfo(" Endless Vent Time", () => CheatToggles.endlessVentTime, x => CheatToggles.endlessVentTime = x),
                    new ToggleInfo(" No Vent Cooldown", () => CheatToggles.noVentCooldown, x => CheatToggles.noVentCooldown = x),
                }),
                new SubmenuInfo("Scientist", false, new List<ToggleInfo>() {
                    new ToggleInfo(" Endless Battery", () => CheatToggles.endlessBattery, x => CheatToggles.endlessBattery = x),
                    new ToggleInfo(" No Vitals Cooldown", () => CheatToggles.noVitalsCooldown, x => CheatToggles.noVitalsCooldown = x),
                }),
            }));

        groups.Add(new GroupInfo("Ship", false, new List<ToggleInfo> {
            new ToggleInfo(" Unfixable Lights", () => CheatToggles.unfixableLights, x => CheatToggles.unfixableLights = x),
            new ToggleInfo(" Report Body", () => CheatToggles.reportBody, x => CheatToggles.reportBody = x),
            new ToggleInfo(" Close Meeting", () => CheatToggles.closeMeeting, x => CheatToggles.closeMeeting = x),
            new ToggleInfo(" Auto Sabotage", () => CheatToggles.autoSabotage, x => CheatToggles.autoSabotage = x),
            new ToggleInfo(" Prevent Sabotage", () => CheatToggles.preventSabotage, x => CheatToggles.preventSabotage = x),
            new ToggleInfo(" Instant Repair", () => CheatToggles.instantRepair, x => CheatToggles.instantRepair = x),
            new ToggleInfo(" Auto Repair", () => CheatToggles.autoRepair, x => CheatToggles.autoRepair = x),
        }, new List<SubmenuInfo> {
            new SubmenuInfo("Sabotage", false, new List<ToggleInfo>() {
                new ToggleInfo(" Reactor", () => CheatToggles.reactorSab, x => CheatToggles.reactorSab = x),
                new ToggleInfo(" Oxygen", () => CheatToggles.oxygenSab, x => CheatToggles.oxygenSab = x),
                new ToggleInfo(" Lights", () => CheatToggles.elecSab, x => CheatToggles.elecSab = x),
                new ToggleInfo(" Comms", () => CheatToggles.commsSab, x => CheatToggles.commsSab = x),
                new ToggleInfo(" Doors", () => CheatToggles.doorsSab, x => CheatToggles.doorsSab = x),
                new ToggleInfo(" MushroomMixup", () => CheatToggles.mushSab, x => CheatToggles.mushSab = x),
            }),
            new SubmenuInfo("Vents", false, new List<ToggleInfo>() {
                new ToggleInfo(" Unlock Vents", () => CheatToggles.useVents, x => CheatToggles.useVents = x),
                new ToggleInfo(" Kick All From Vents", () => CheatToggles.kickVents, x => CheatToggles.kickVents = x),
                new ToggleInfo(" Walk In Vents", () => CheatToggles.walkVent, x => CheatToggles.walkVent = x),
                new ToggleInfo(" Vent Teleport", () => CheatToggles.ventTeleport, x => CheatToggles.ventTeleport = x),
                new ToggleInfo(" Vent Speed", () => CheatToggles.ventSpeed, x => CheatToggles.ventSpeed = x),
                new ToggleInfo(" Vent Invisible", () => CheatToggles.ventInvisible, x => CheatToggles.ventInvisible = x)
            }),
        }));

        groups.Add(new GroupInfo("Chat", false, new List<ToggleInfo>() {
            new ToggleInfo(" Enable Chat", () => CheatToggles.alwaysChat, x => CheatToggles.alwaysChat = x),
            new ToggleInfo(" Unlock Textbox", () => CheatToggles.chatJailbreak, x => CheatToggles.chatJailbreak = x),
            new ToggleInfo(" Spam Chat", () => CheatToggles.spamChat, x => CheatToggles.spamChat = x),
            new ToggleInfo(" Auto Chat", () => CheatToggles.autoChat, x => CheatToggles.autoChat = x),
            new ToggleInfo(" Chat Bot", () => CheatToggles.chatBot, x => CheatToggles.chatBot = x),
            new ToggleInfo(" Colored Chat", () => CheatToggles.coloredChat, x => CheatToggles.coloredChat = x),
            new ToggleInfo(" Big Text", () => CheatToggles.bigText, x => CheatToggles.bigText = x),
            new ToggleInfo(" Invisible Text", () => CheatToggles.invisibleText, x => CheatToggles.invisibleText = x)
        }, new List<SubmenuInfo>()));

        groups.Add(new GroupInfo("Meeting", false, new List<ToggleInfo>() {
            new ToggleInfo(" Auto Vote", () => CheatToggles.autoVote, x => CheatToggles.autoVote = x),
            new ToggleInfo(" Vote Anyone", () => CheatToggles.voteAnyone, x => CheatToggles.voteAnyone = x),
            new ToggleInfo(" Skip Vote", () => CheatToggles.skipVote, x => CheatToggles.skipVote = x),
            new ToggleInfo(" Force Vote", () => CheatToggles.forceVote, x => CheatToggles.forceVote = x),
            new ToggleInfo(" Meeting Spam", () => CheatToggles.meetingSpam, x => CheatToggles.meetingSpam = x),
            new ToggleInfo(" Instant Meeting", () => CheatToggles.instantMeeting, x => CheatToggles.instantMeeting = x),
            new ToggleInfo(" Prevent Meeting", () => CheatToggles.preventMeeting, x => CheatToggles.preventMeeting = x)
        }, new List<SubmenuInfo>()));

        groups.Add(new GroupInfo("Game Control", false, new List<ToggleInfo>() {
            new ToggleInfo(" Force Start", () => CheatToggles.forceStart, x => CheatToggles.forceStart = x),
            new ToggleInfo(" Force End", () => CheatToggles.forceEnd, x => CheatToggles.forceEnd = x),
            new ToggleInfo(" Change Settings", () => CheatToggles.changeSettings, x => CheatToggles.changeSettings = x),
            new ToggleInfo(" Kick Players", () => CheatToggles.kickPlayers, x => CheatToggles.kickPlayers = x),
            new ToggleInfo(" Ban Players", () => CheatToggles.banPlayers, x => CheatToggles.banPlayers = x),
            new ToggleInfo(" Control Host", () => CheatToggles.controlHost, x => CheatToggles.controlHost = x),
            new ToggleInfo(" Game Speed", () => CheatToggles.gameSpeed, x => CheatToggles.gameSpeed = x),
            new ToggleInfo(" Time Control", () => CheatToggles.timeControl, x => CheatToggles.timeControl = x)
        }, new List<SubmenuInfo>()));

        groups.Add(new GroupInfo("Visual", false, new List<ToggleInfo>() {
            new ToggleInfo(" Custom Colors", () => CheatToggles.customColors, x => CheatToggles.customColors = x),
            new ToggleInfo(" Rainbow Mode", () => CheatToggles.rainbowMode, x => CheatToggles.rainbowMode = x),
            new ToggleInfo(" Glow Effect", () => CheatToggles.glowEffect, x => CheatToggles.glowEffect = x),
            new ToggleInfo(" Trail Effect", () => CheatToggles.trailEffect, x => CheatToggles.trailEffect = x),
            new ToggleInfo(" Particle Effects", () => CheatToggles.particleEffects, x => CheatToggles.particleEffects = x),
            new ToggleInfo(" Custom Skins", () => CheatToggles.customSkins, x => CheatToggles.customSkins = x),
            new ToggleInfo(" Big Player", () => CheatToggles.bigPlayer, x => CheatToggles.bigPlayer = x),
            new ToggleInfo(" Small Player", () => CheatToggles.smallPlayer, x => CheatToggles.smallPlayer = x),
            new ToggleInfo(" Invisible Player", () => CheatToggles.invisiblePlayer, x => CheatToggles.invisiblePlayer = x)
        }, new List<SubmenuInfo>()));

        groups.Add(new GroupInfo("Audio", false, new List<ToggleInfo>() {
            new ToggleInfo(" Mute All", () => CheatToggles.muteAll, x => CheatToggles.muteAll = x),
            new ToggleInfo(" Custom Sounds", () => CheatToggles.customSounds, x => CheatToggles.customSounds = x),
            new ToggleInfo(" Sound Spam", () => CheatToggles.soundSpam, x => CheatToggles.soundSpam = x),
            new ToggleInfo(" Voice Changer", () => CheatToggles.voiceChanger, x => CheatToggles.voiceChanger = x),
            new ToggleInfo(" Music Player", () => CheatToggles.musicPlayer, x => CheatToggles.musicPlayer = x)
        }, new List<SubmenuInfo>()));

        groups.Add(new GroupInfo("Network", false, new List<ToggleInfo>() {
            new ToggleInfo(" Lag Switch", () => CheatToggles.lagSwitch, x => CheatToggles.lagSwitch = x),
            new ToggleInfo(" Packet Loss", () => CheatToggles.packetLoss, x => CheatToggles.packetLoss = x),
            new ToggleInfo(" Ping Spoof", () => CheatToggles.pingSpoof, x => CheatToggles.pingSpoof = x),
            new ToggleInfo(" Region Spoof", () => CheatToggles.regionSpoof, x => CheatToggles.regionSpoof = x),
            new ToggleInfo(" Server Control", () => CheatToggles.serverControl, x => CheatToggles.serverControl = x)
        }, new List<SubmenuInfo>()));

        groups.Add(new GroupInfo("Automation", false, new List<ToggleInfo>() {
            new ToggleInfo(" Auto Play", () => CheatToggles.autoPlay, x => CheatToggles.autoPlay = x),
            new ToggleInfo(" Bot Mode", () => CheatToggles.botMode, x => CheatToggles.botMode = x),
            new ToggleInfo(" AI Assist", () => CheatToggles.aiAssist, x => CheatToggles.aiAssist = x),
            new ToggleInfo(" Auto Win", () => CheatToggles.autoWin, x => CheatToggles.autoWin = x),
            new ToggleInfo(" Smart Play", () => CheatToggles.smartPlay, x => CheatToggles.smartPlay = x)
        }, new List<SubmenuInfo>()));

        groups.Add(new GroupInfo("Trolling", false, new List<ToggleInfo>() {
            new ToggleInfo(" Confuse Mode", () => CheatToggles.confuseMode, x => CheatToggles.confuseMode = x),
            new ToggleInfo(" Chaos Mode", () => CheatToggles.chaosMode, x => CheatToggles.chaosMode = x),
            new ToggleInfo(" Prank Mode", () => CheatToggles.prankMode, x => CheatToggles.prankMode = x),
            new ToggleInfo(" Annoy Mode", () => CheatToggles.annoyMode, x => CheatToggles.annoyMode = x),
            new ToggleInfo(" Disrupt Mode", () => CheatToggles.disruptMode, x => CheatToggles.disruptMode = x)
        }, new List<SubmenuInfo>()));

        groups.Add(new GroupInfo("Security", false, new List<ToggleInfo>() {
            new ToggleInfo(" Anti Cheat", () => CheatToggles.antiCheat, x => CheatToggles.antiCheat = x),
            new ToggleInfo(" Hide Cheat", () => CheatToggles.hideCheat, x => CheatToggles.hideCheat = x),
            new ToggleInfo(" Stealth Mode", () => CheatToggles.stealthMode, x => CheatToggles.stealthMode = x),
            new ToggleInfo(" Bypass Detection", () => CheatToggles.bypassDetection, x => CheatToggles.bypassDetection = x)
        }, new List<SubmenuInfo>()));

        groups.Add(new GroupInfo("Host-Only", false, 
        new List<ToggleInfo>{
            new ToggleInfo(" Kill While Vanished", () => CheatToggles.killVanished, x => CheatToggles.killVanished = x),
        },
        new List<SubmenuInfo>{
            new SubmenuInfo("Murder", false, new List<ToggleInfo>() {
                new ToggleInfo(" Kill Player", () => CheatToggles.killPlayer, x => CheatToggles.killPlayer = x),
                new ToggleInfo(" Kill All Crewmates", () => CheatToggles.killAllCrew, x => CheatToggles.killAllCrew = x),
                new ToggleInfo(" Kill All Impostors", () => CheatToggles.killAllImps, x => CheatToggles.killAllImps = x),
                new ToggleInfo(" Kill Everyone", () => CheatToggles.killAll, x => CheatToggles.killAll = x),
            }),
        }));

        groups.Add(new GroupInfo("Passive", false, new List<ToggleInfo>() {
            new ToggleInfo(" Free Cosmetics", () => CheatToggles.freeCosmetics, x => CheatToggles.freeCosmetics = x),
            new ToggleInfo(" Avoid Penalties", () => CheatToggles.avoidBans, x => CheatToggles.avoidBans = x),
            new ToggleInfo(" Unlock Extra Features", () => CheatToggles.unlockFeatures, x => CheatToggles.unlockFeatures = x),
        }, new List<SubmenuInfo>()));
    }

    private void Update(){

        if (Input.GetKeyDown(Utils.stringToKeycode(MalumMenu.menuKeybind.Value)))
        {
            //Enable-disable GUI with DELETE key
            isGUIActive = !isGUIActive;

            //Also teleport the window to the mouse for immediate use
            Vector2 mousePosition = Input.mousePosition;
            windowRect.position = new Vector2(mousePosition.x, Screen.height - mousePosition.y);
        }

        //Passive cheats are always on to avoid problems
        CheatToggles.unlockFeatures = CheatToggles.freeCosmetics = CheatToggles.avoidBans = true;

        if(!Utils.isPlayer){
            CheatToggles.changeRole = CheatToggles.killAll = CheatToggles.telekillPlayer = CheatToggles.killAllCrew = CheatToggles.killAllImps = CheatToggles.teleportCursor = CheatToggles.teleportPlayer = CheatToggles.spectate = CheatToggles.freecam = CheatToggles.killPlayer = false;
        }

        if(!Utils.isHost && !Utils.isFreePlay){
            CheatToggles.killAll = CheatToggles.telekillPlayer = CheatToggles.killAllCrew = CheatToggles.killAllImps = CheatToggles.killPlayer = CheatToggles.zeroKillCd = CheatToggles.killAnyone = CheatToggles.killVanished = false;
        }

        //Some cheats only work if the ship is present, so they are turned off if it is not
        if(!Utils.isShip){
            CheatToggles.unfixableLights = CheatToggles.completeMyTasks = CheatToggles.kickVents = CheatToggles.reportBody = CheatToggles.closeMeeting = CheatToggles.reactorSab = CheatToggles.oxygenSab = CheatToggles.commsSab = CheatToggles.elecSab = CheatToggles.mushSab = CheatToggles.doorsSab = false;
        }
    }

    public void OnGUI()
    {

        if (!isGUIActive) return;

        if (submenuButtonStyle == null)
        {
            submenuButtonStyle = new GUIStyle(GUI.skin.button);

            submenuButtonStyle.normal.textColor = Color.white;

            submenuButtonStyle.fontSize = 16;
            GUI.skin.toggle.fontSize = GUI.skin.button.fontSize = 18;

            submenuButtonStyle.normal.background = Texture2D.grayTexture;
            submenuButtonStyle.normal.background.Apply();
        }

        //Only change the window height while the user is not dragging it
        //Or else dragging breaks
        if (!isDragging)
        {
            int windowHeight = CalculateWindowHeight();
            windowRect.height = Mathf.Min(windowHeight, Screen.height - 50); // Limit max height
        }

        Color uiColor;

        string configHtmlColor = MalumMenu.menuHtmlColor.Value;

        if (!ColorUtility.TryParseHtmlString(configHtmlColor, out uiColor))
        {
            if (!configHtmlColor.StartsWith("#"))
            {
                if (ColorUtility.TryParseHtmlString("#" + configHtmlColor, out uiColor))
                {
                    GUI.backgroundColor = uiColor;
                }
            }
        }
        else
        {
            GUI.backgroundColor = uiColor;
        }

        windowRect = GUI.Window(0, windowRect, (GUI.WindowFunction)WindowFunction, "MalumMenu v" + MalumMenu.malumVersion + " - Enhanced");
    }

    public void WindowFunction(int windowID)
    {
        int groupSpacing = 45;
        int toggleSpacing = 35;
        int submenuSpacing = 35;
        int currentYPosition = 20;

        // Add scroll view for better navigation
        scrollPosition = GUI.BeginScrollView(new Rect(5, 20, 310, windowRect.height - 25), scrollPosition, new Rect(0, 0, 290, CalculateContentHeight()));

        for (int groupId = 0; groupId < groups.Count; groupId++)
        {
            GroupInfo group = groups[groupId];

            if (GUI.Button(new Rect(10, currentYPosition, 270, 35), group.name))
            {
                group.isExpanded = !group.isExpanded;
                groups[groupId] = group;
                CloseAllGroupsExcept(groupId); // Close all other groups when one is expanded
            }
            currentYPosition += groupSpacing;

            if (group.isExpanded)
            {
                // Render direct toggles for the group
                foreach (var toggle in group.toggles)
                {
                    bool currentState = toggle.getState();
                    bool newState = GUI.Toggle(new Rect(20, currentYPosition, 250, 25), currentState, toggle.label);
                    if (newState != currentState)
                    {
                        toggle.setState(newState);
                    }
                    currentYPosition += toggleSpacing;
                }

                for (int submenuId = 0; submenuId < group.submenus.Count; submenuId++)
                {
                    var submenu = group.submenus[submenuId];

                    // Add a button for each submenu and toggle its expansion state when clicked
                    if (GUI.Button(new Rect(20, currentYPosition, 250, 25), submenu.name, submenuButtonStyle))
                    {
                        submenu.isExpanded = !submenu.isExpanded;
                        group.submenus[submenuId] = submenu;
                        if (submenu.isExpanded)
                        {
                            CloseAllSubmenusExcept(group, submenuId);
                        }
                    }
                    currentYPosition += submenuSpacing;

                    if (submenu.isExpanded)
                    {
                        // Show all the toggles in the expanded submenu
                        foreach (var toggle in submenu.toggles)
                        {
                            bool currentState = toggle.getState();
                            bool newState = GUI.Toggle(new Rect(30, currentYPosition, 240, 25), currentState, toggle.label);
                            if (newState != currentState)
                            {
                                toggle.setState(newState);
                            }
                            currentYPosition += toggleSpacing;
                        }
                    }
                }
            }
        }

        GUI.EndScrollView();

        if (Event.current.type == EventType.MouseDrag)
        {
            isDragging = true;
        }

        if (Event.current.type == EventType.MouseUp)
        {
            isDragging = false;
        }

        GUI.DragWindow(); //Allows dragging the GUI window with mouse
    }

    // Calculate content height for scroll view
    private int CalculateContentHeight()
    {
        int totalHeight = 50; // Base height
        int groupHeight = 45; // Height for each group title
        int toggleHeight = 35; // Height for each toggle
        int submenuHeight = 35; // Height for each submenu title

        foreach (GroupInfo group in groups)
        {
            totalHeight += groupHeight; // Always add height for the group title

            if (group.isExpanded)
            {
                totalHeight += group.toggles.Count * toggleHeight; // Add height for toggles in the group

                foreach (SubmenuInfo submenu in group.submenus)
                {
                    totalHeight += submenuHeight; // Always add height for the submenu title

                    if (submenu.isExpanded)
                    {
                        totalHeight += submenu.toggles.Count * toggleHeight; // Add height for toggles in the expanded submenu
                    }
                }
            }
        }

        return totalHeight;
    }

    // Dynamically calculate the window's height depending on
    // The number of toggles & group expansion
    private int CalculateWindowHeight()
    {
        return Mathf.Min(CalculateContentHeight() + 50, 700); // Limit window height
    }

    // Closes all expanded groups other than indexToKeepOpen
    private void CloseAllGroupsExcept(int indexToKeepOpen)
    {
        for (int i = 0; i < groups.Count; i++)
        {
            if (i != indexToKeepOpen)
            {
                GroupInfo group = groups[i];
                group.isExpanded = false;
                groups[i] = group;
            }
        }
    }

    private void CloseAllSubmenusExcept(GroupInfo group, int submenuIndexToKeepOpen)
    {
        for (int i = 0; i < group.submenus.Count; i++)
        {
            if (i != submenuIndexToKeepOpen)
            {
                var submenu = group.submenus[i];
                submenu.isExpanded = false;
                group.submenus[i] = submenu;
            }
        }
    }
}