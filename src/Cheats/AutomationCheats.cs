using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MalumMenu;
public static class AutomationCheats
{
    private static float botTimer = 0f;
    private static float aiTimer = 0f;
    private static bool isAutoPlaying = false;
    private static Vector3 lastPosition;
    private static float stuckTimer = 0f;

    public static void autoPlayCheat()
    {
        if (!CheatToggles.autoPlay || !Utils.isPlayer) return;

        try
        {
            if (!isAutoPlaying)
            {
                isAutoPlaying = true;
                lastPosition = PlayerControl.LocalPlayer.transform.position;
            }

            // Check if player is stuck
            if (Vector3.Distance(PlayerControl.LocalPlayer.transform.position, lastPosition) < 0.1f)
            {
                stuckTimer += Time.deltaTime;
                if (stuckTimer > 2f) // If stuck for 2 seconds, teleport randomly
                {
                    Vector3 randomPos = GetRandomSafePosition();
                    PlayerControl.LocalPlayer.NetTransform.RpcSnapTo(randomPos);
                    stuckTimer = 0f;
                }
            }
            else
            {
                stuckTimer = 0f;
                lastPosition = PlayerControl.LocalPlayer.transform.position;
            }

            // Auto complete tasks
            AutoCompleteTasks();

            // Auto avoid other players if impostor nearby
            AvoidImpostors();

            // Auto move to tasks
            MoveToNearestTask();
        }
        catch { }
    }

    public static void botModeCheat()
    {
        if (!CheatToggles.botMode || !Utils.isPlayer) return;

        try
        {
            botTimer += Time.deltaTime;
            
            if (botTimer > 1f) // Update bot behavior every second
            {
                if (Utils.isMeeting)
                {
                    BotVotingBehavior();
                }
                else
                {
                    BotGameplayBehavior();
                }
                botTimer = 0f;
            }
        }
        catch { }
    }

    public static void aiAssistCheat()
    {
        if (!CheatToggles.aiAssist || !Utils.isPlayer) return;

        try
        {
            aiTimer += Time.deltaTime;
            
            if (aiTimer > 0.5f) // Update AI assistance every 0.5 seconds
            {
                // Warn about nearby impostors
                WarnAboutImpostors();
                
                // Suggest optimal task routes
                SuggestTaskRoute();
                
                // Auto report bodies
                AutoReportBodies();
                
                aiTimer = 0f;
            }
        }
        catch { }
    }

    public static void autoWinCheat()
    {
        if (!CheatToggles.autoWin || !Utils.isPlayer) return;

        try
        {
            if (PlayerControl.LocalPlayer.Data.Role.IsImpostor)
            {
                // Impostor auto-win strategy
                AutoKillStrategy();
                AutoSabotageStrategy();
            }
            else
            {
                // Crewmate auto-win strategy
                AutoCompleteAllTasks();
                AutoCallEmergencyMeeting();
            }
        }
        catch { }
    }

    public static void smartPlayCheat()
    {
        if (!CheatToggles.smartPlay || !Utils.isPlayer) return;

        try
        {
            // Analyze game state and make smart decisions
            AnalyzeGameState();
            
            // Optimize movement patterns
            OptimizeMovement();
            
            // Smart voting in meetings
            if (Utils.isMeeting)
            {
                SmartVoting();
            }
        }
        catch { }
    }

    private static void AutoCompleteTasks()
    {
        try
        {
            foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks)
            {
                if (!task.IsComplete && Vector3.Distance(PlayerControl.LocalPlayer.transform.position, task.transform.position) < 1f)
                {
                    task.Complete();
                }
            }
        }
        catch { }
    }

    private static void AvoidImpostors()
    {
        try
        {
            PlayerControl[] players = Object.FindObjectsOfType<PlayerControl>();
            foreach (PlayerControl player in players)
            {
                if (player != PlayerControl.LocalPlayer && player.Data.Role.IsImpostor)
                {
                    float distance = Utils.getDistanceFrom(player);
                    if (distance < 3f) // If impostor is too close
                    {
                        Vector3 escapeDirection = (PlayerControl.LocalPlayer.transform.position - player.transform.position).normalized;
                        PlayerControl.LocalPlayer.transform.position += escapeDirection * 2f * Time.deltaTime;
                    }
                }
            }
        }
        catch { }
    }

    private static void MoveToNearestTask()
    {
        try
        {
            PlayerTask nearestTask = PlayerControl.LocalPlayer.myTasks
                .Where(task => !task.IsComplete)
                .OrderBy(task => Vector3.Distance(PlayerControl.LocalPlayer.transform.position, task.transform.position))
                .FirstOrDefault();

            if (nearestTask != null)
            {
                Vector3 direction = (nearestTask.transform.position - PlayerControl.LocalPlayer.transform.position).normalized;
                PlayerControl.LocalPlayer.transform.position += direction * 1.5f * Time.deltaTime;
            }
        }
        catch { }
    }

    private static void BotVotingBehavior()
    {
        try
        {
            if (MeetingHud.Instance != null && Utils.isMeetingVoting)
            {
                // Simple voting logic - vote for suspicious players or skip
                var playerStates = MeetingHud.Instance.playerStates;
                if (playerStates != null && playerStates.Length > 0)
                {
                    // Vote for a random player with low probability, otherwise skip
                    if (UnityEngine.Random.Range(0f, 1f) < 0.3f)
                    {
                        int randomIndex = UnityEngine.Random.Range(0, playerStates.Length);
                        MeetingHud.Instance.CastVote(PlayerControl.LocalPlayer.PlayerId, playerStates[randomIndex].TargetPlayerId);
                    }
                    else
                    {
                        MeetingHud.Instance.CastVote(PlayerControl.LocalPlayer.PlayerId, 253); // Skip vote
                    }
                }
            }
        }
        catch { }
    }

    private static void BotGameplayBehavior()
    {
        try
        {
            // Random movement and task completion
            if (UnityEngine.Random.Range(0f, 1f) < 0.1f) // 10% chance to move randomly
            {
                Vector3 randomDirection = new Vector3(
                    UnityEngine.Random.Range(-1f, 1f),
                    UnityEngine.Random.Range(-1f, 1f),
                    0
                ).normalized;
                
                PlayerControl.LocalPlayer.transform.position += randomDirection * 1f * Time.deltaTime;
            }
        }
        catch { }
    }

    private static void WarnAboutImpostors()
    {
        try
        {
            PlayerControl[] players = Object.FindObjectsOfType<PlayerControl>();
            foreach (PlayerControl player in players)
            {
                if (player != PlayerControl.LocalPlayer && player.Data.Role.IsImpostor)
                {
                    float distance = Utils.getDistanceFrom(player);
                    if (distance < 5f)
                    {
                        // Visual warning could be implemented here
                        Debug.Log($"Warning: Impostor {player.Data.PlayerName} nearby!");
                    }
                }
            }
        }
        catch { }
    }

    private static void SuggestTaskRoute()
    {
        try
        {
            // Calculate optimal task completion route
            var incompleteTasks = PlayerControl.LocalPlayer.myTasks.Where(task => !task.IsComplete).ToList();
            if (incompleteTasks.Count > 0)
            {
                // Simple nearest-neighbor algorithm for task routing
                var sortedTasks = incompleteTasks.OrderBy(task => 
                    Vector3.Distance(PlayerControl.LocalPlayer.transform.position, task.transform.position)).ToList();
                
                // Move towards the nearest task
                if (sortedTasks.Count > 0)
                {
                    Vector3 direction = (sortedTasks[0].transform.position - PlayerControl.LocalPlayer.transform.position).normalized;
                    // This could be used to show a path or guide the player
                }
            }
        }
        catch { }
    }

    private static void AutoReportBodies()
    {
        try
        {
            GameObject[] bodyObjects = GameObject.FindGameObjectsWithTag("DeadBody");
            foreach (GameObject bodyObject in bodyObjects)
            {
                DeadBody deadBody = bodyObject.GetComponent<DeadBody>();
                if (deadBody && !deadBody.Reported)
                {
                    float distance = Vector3.Distance(PlayerControl.LocalPlayer.transform.position, deadBody.transform.position);
                    if (distance < 2f) // Auto report if close enough
                    {
                        NetworkedPlayerInfo playerData = GameData.Instance.GetPlayerById(deadBody.ParentId);
                        Utils.reportDeadBody(playerData);
                    }
                }
            }
        }
        catch { }
    }

    private static void AutoKillStrategy()
    {
        try
        {
            if (PlayerControl.LocalPlayer.Data.Role.IsImpostor && PlayerControl.LocalPlayer.killTimer <= 0f)
            {
                PlayerControl[] players = Object.FindObjectsOfType<PlayerControl>();
                PlayerControl nearestCrewmate = players
                    .Where(p => p != PlayerControl.LocalPlayer && !p.Data.Role.IsImpostor && !p.Data.IsDead)
                    .OrderBy(p => Utils.getDistanceFrom(p))
                    .FirstOrDefault();

                if (nearestCrewmate != null && Utils.getDistanceFrom(nearestCrewmate) < 2f)
                {
                    PlayerControl.LocalPlayer.CmdCheckMurder(nearestCrewmate);
                }
            }
        }
        catch { }
    }

    private static void AutoSabotageStrategy()
    {
        try
        {
            if (PlayerControl.LocalPlayer.Data.Role.IsImpostor && UnityEngine.Random.Range(0f, 1f) < 0.01f)
            {
                // Randomly trigger sabotages
                int sabotageType = UnityEngine.Random.Range(0, 4);
                switch (sabotageType)
                {
                    case 0:
                        CheatToggles.reactorSab = true;
                        break;
                    case 1:
                        CheatToggles.oxygenSab = true;
                        break;
                    case 2:
                        CheatToggles.elecSab = true;
                        break;
                    case 3:
                        CheatToggles.commsSab = true;
                        break;
                }
            }
        }
        catch { }
    }

    private static void AutoCompleteAllTasks()
    {
        try
        {
            foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks)
            {
                if (!task.IsComplete)
                {
                    task.Complete();
                }
            }
        }
        catch { }
    }

    private static void AutoCallEmergencyMeeting()
    {
        try
        {
            // Call emergency meeting if suspicious activity detected
            PlayerControl[] players = Object.FindObjectsOfType<PlayerControl>();
            int alivePlayers = players.Count(p => !p.Data.IsDead);
            int aliveImpostors = players.Count(p => !p.Data.IsDead && p.Data.Role.IsImpostor);
            
            if (aliveImpostors >= alivePlayers / 2 && ShipStatus.Instance.EmergencyCooldown <= 0f)
            {
                // Emergency situation - call meeting
                PlayerControl.LocalPlayer.CmdReportDeadBody(null);
            }
        }
        catch { }
    }

    private static void AnalyzeGameState()
    {
        try
        {
            // Analyze current game state for optimal decisions
            PlayerControl[] players = Object.FindObjectsOfType<PlayerControl>();
            int totalPlayers = players.Length;
            int alivePlayers = players.Count(p => !p.Data.IsDead);
            int deadPlayers = totalPlayers - alivePlayers;
            int impostors = players.Count(p => p.Data.Role.IsImpostor && !p.Data.IsDead);
            
            // Make decisions based on game state analysis
            if (impostors >= alivePlayers / 2)
            {
                // Critical situation - prioritize survival
                CheatToggles.godMode = true;
            }
        }
        catch { }
    }

    private static void OptimizeMovement()
    {
        try
        {
            // Optimize movement patterns to avoid wasted time
            if (PlayerControl.LocalPlayer.myTasks.All(task => task.IsComplete))
            {
                // All tasks complete - move to common areas or follow other players
                PlayerControl[] players = Object.FindObjectsOfType<PlayerControl>();
                PlayerControl nearestPlayer = players
                    .Where(p => p != PlayerControl.LocalPlayer && !p.Data.IsDead)
                    .OrderBy(p => Utils.getDistanceFrom(p))
                    .FirstOrDefault();

                if (nearestPlayer != null)
                {
                    Vector3 direction = (nearestPlayer.transform.position - PlayerControl.LocalPlayer.transform.position).normalized;
                    PlayerControl.LocalPlayer.transform.position += direction * 1f * Time.deltaTime;
                }
            }
        }
        catch { }
    }

    private static void SmartVoting()
    {
        try
        {
            if (MeetingHud.Instance != null && Utils.isMeetingVoting)
            {
                // Implement smart voting logic based on game analysis
                var playerStates = MeetingHud.Instance.playerStates;
                if (playerStates != null && playerStates.Length > 0)
                {
                    // Vote for the most suspicious player or skip if uncertain
                    // This is a simplified version - real implementation would be more complex
                    MeetingHud.Instance.CastVote(PlayerControl.LocalPlayer.PlayerId, 253); // Skip for now
                }
            }
        }
        catch { }
    }

    private static Vector3 GetRandomSafePosition()
    {
        try
        {
            // Get a random safe position on the map
            return new Vector3(
                UnityEngine.Random.Range(-10f, 10f),
                UnityEngine.Random.Range(-10f, 10f),
                0
            );
        }
        catch
        {
            return Vector3.zero;
        }
    }
}