using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MalumMenu;
public static class EnhancedCheats
{
    private static float flySpeed = 5f;
    private static bool isFlying = false;
    private static Vector3 flyVelocity = Vector3.zero;
    private static float rainbowHue = 0f;
    private static Dictionary<PlayerControl, TrailRenderer> playerTrails = new Dictionary<PlayerControl, TrailRenderer>();
    private static float autoRunTimer = 0f;
    private static List<string> chatMessages = new List<string> { "Hello!", "How are you?", "Nice game!", "Good luck!", "Well played!" };
    private static float chatBotTimer = 0f;
    private static float gameSpeedMultiplier = 1f;

    public static void flyModeCheat()
    {
        if (!CheatToggles.flyMode || !Utils.isPlayer) return;

        try
        {
            PlayerControl player = PlayerControl.LocalPlayer;
            
            // Get input for flying
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            if (horizontal != 0 || vertical != 0)
            {
                flyVelocity = new Vector3(horizontal, vertical, 0) * flySpeed;
                isFlying = true;
            }
            else
            {
                flyVelocity = Vector3.Lerp(flyVelocity, Vector3.zero, Time.deltaTime * 5f);
                if (flyVelocity.magnitude < 0.1f)
                {
                    flyVelocity = Vector3.zero;
                    isFlying = false;
                }
            }

            if (isFlying || flyVelocity != Vector3.zero)
            {
                player.transform.position += flyVelocity * Time.deltaTime;
                player.MyPhysics.body.velocity = Vector2.zero;
            }
        }
        catch { }
    }

    public static void infiniteStaminaCheat()
    {
        if (!CheatToggles.infiniteStamina || !Utils.isPlayer) return;

        try
        {
            // Keep stamina at maximum
            if (PlayerControl.LocalPlayer.MyPhysics != null)
            {
                // Reset any stamina-related limitations
                PlayerControl.LocalPlayer.MyPhysics.Speed = CheatToggles.speedBoost ? 5f : 2.5f;
            }
        }
        catch { }
    }

    public static void autoRunCheat()
    {
        if (!CheatToggles.autoRun || !Utils.isPlayer) return;

        try
        {
            autoRunTimer += Time.deltaTime;
            if (autoRunTimer > 0.1f) // Update every 0.1 seconds
            {
                // Simple AI movement - move towards nearest task or random direction
                Vector3 randomDirection = new Vector3(
                    UnityEngine.Random.Range(-1f, 1f),
                    UnityEngine.Random.Range(-1f, 1f),
                    0
                ).normalized;

                PlayerControl.LocalPlayer.transform.position += randomDirection * 2f * Time.deltaTime;
                autoRunTimer = 0f;
            }
        }
        catch { }
    }

    public static void wallHackCheat()
    {
        if (!CheatToggles.wallHack) return;

        try
        {
            // Make walls semi-transparent or disable collision
            GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
            foreach (GameObject wall in walls)
            {
                Renderer renderer = wall.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Color color = renderer.material.color;
                    color.a = 0.3f; // Make semi-transparent
                    renderer.material.color = color;
                }
            }
        }
        catch { }
    }

    public static void godModeCheat()
    {
        if (!CheatToggles.godMode || !Utils.isPlayer) return;

        try
        {
            // Prevent death and make invulnerable
            PlayerControl.LocalPlayer.protectedByGuardianId = PlayerControl.LocalPlayer.PlayerId;
        }
        catch { }
    }

    public static void xrayVisionCheat()
    {
        if (!CheatToggles.xrayVision) return;

        try
        {
            // Make all objects visible through walls
            PlayerControl[] players = Object.FindObjectsOfType<PlayerControl>();
            foreach (PlayerControl player in players)
            {
                if (player != PlayerControl.LocalPlayer)
                {
                    player.gameObject.layer = 8; // UI layer to render on top
                }
            }
        }
        catch { }
    }

    public static void nightVisionCheat()
    {
        if (!CheatToggles.nightVision) return;

        try
        {
            // Increase ambient lighting
            RenderSettings.ambientLight = Color.white;
            Camera.main.backgroundColor = Color.gray;
        }
        catch { }
    }

    public static void thermalVisionCheat()
    {
        if (!CheatToggles.thermalVision) return;

        try
        {
            // Apply thermal-like color effects
            PlayerControl[] players = Object.FindObjectsOfType<PlayerControl>();
            foreach (PlayerControl player in players)
            {
                SpriteRenderer renderer = player.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    renderer.color = player.Data.IsDead ? Color.blue : Color.red;
                }
            }
        }
        catch { }
    }

    public static void rainbowModeCheat()
    {
        if (!CheatToggles.rainbowMode || !Utils.isPlayer) return;

        try
        {
            rainbowHue += Time.deltaTime * 0.5f;
            if (rainbowHue > 1f) rainbowHue = 0f;

            Color rainbowColor = Color.HSVToRGB(rainbowHue, 1f, 1f);
            PlayerControl.LocalPlayer.GetComponent<SpriteRenderer>().color = rainbowColor;
        }
        catch { }
    }

    public static void glowEffectCheat()
    {
        if (!CheatToggles.glowEffect || !Utils.isPlayer) return;

        try
        {
            // Add glow effect to local player
            SpriteRenderer renderer = PlayerControl.LocalPlayer.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.material.SetFloat("_OutlineWidth", 0.1f);
                renderer.material.SetColor("_OutlineColor", Color.yellow);
            }
        }
        catch { }
    }

    public static void trailEffectCheat()
    {
        if (!CheatToggles.trailEffect || !Utils.isPlayer) return;

        try
        {
            PlayerControl player = PlayerControl.LocalPlayer;
            if (!playerTrails.ContainsKey(player))
            {
                GameObject trailObj = new GameObject("PlayerTrail");
                TrailRenderer trail = trailObj.AddComponent<TrailRenderer>();
                trail.material = new Material(Shader.Find("Sprites/Default"));
                trail.color = Color.cyan;
                trail.time = 2f;
                trail.startWidth = 0.5f;
                trail.endWidth = 0.1f;
                trailObj.transform.SetParent(player.transform);
                trailObj.transform.localPosition = Vector3.zero;
                playerTrails[player] = trail;
            }
        }
        catch { }
    }

    public static void bigPlayerCheat()
    {
        if (!CheatToggles.bigPlayer || !Utils.isPlayer) return;

        try
        {
            PlayerControl.LocalPlayer.transform.localScale = Vector3.one * 1.5f;
        }
        catch { }
    }

    public static void smallPlayerCheat()
    {
        if (!CheatToggles.smallPlayer || !Utils.isPlayer) return;

        try
        {
            PlayerControl.LocalPlayer.transform.localScale = Vector3.one * 0.5f;
        }
        catch { }
    }

    public static void invisiblePlayerCheat()
    {
        if (!CheatToggles.invisiblePlayer || !Utils.isPlayer) return;

        try
        {
            SpriteRenderer renderer = PlayerControl.LocalPlayer.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                Color color = renderer.color;
                color.a = 0.3f;
                renderer.color = color;
            }
        }
        catch { }
    }

    public static void autoCompleteTaskCheat()
    {
        if (!CheatToggles.autoCompleteTask || !Utils.isPlayer) return;

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

    public static void spamChatCheat()
    {
        if (!CheatToggles.spamChat) return;

        try
        {
            if (Time.time % 2f < 0.1f) // Spam every 2 seconds
            {
                PlayerControl.LocalPlayer.RpcSendChat("MalumMenu Spam!");
            }
        }
        catch { }
    }

    public static void chatBotCheat()
    {
        if (!CheatToggles.chatBot) return;

        try
        {
            chatBotTimer += Time.deltaTime;
            if (chatBotTimer > 10f) // Send message every 10 seconds
            {
                string randomMessage = chatMessages[UnityEngine.Random.Range(0, chatMessages.Count)];
                PlayerControl.LocalPlayer.RpcSendChat(randomMessage);
                chatBotTimer = 0f;
            }
        }
        catch { }
    }

    public static void autoVoteCheat()
    {
        if (!CheatToggles.autoVote || !Utils.isMeeting) return;

        try
        {
            if (MeetingHud.Instance != null && Utils.isMeetingVoting)
            {
                // Auto vote for a random player or skip
                var playerStates = MeetingHud.Instance.playerStates;
                if (playerStates != null && playerStates.Length > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, playerStates.Length + 1);
                    if (randomIndex < playerStates.Length)
                    {
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

    public static void gameSpeedCheat()
    {
        if (!CheatToggles.gameSpeed) return;

        try
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                gameSpeedMultiplier = 2f;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                gameSpeedMultiplier = 0.5f;
            }
            else
            {
                gameSpeedMultiplier = 1f;
            }

            Time.timeScale = gameSpeedMultiplier;
        }
        catch { }
    }

    public static void confuseModeCheat()
    {
        if (!CheatToggles.confuseMode) return;

        try
        {
            // Randomly teleport other players slightly
            PlayerControl[] players = Object.FindObjectsOfType<PlayerControl>();
            foreach (PlayerControl player in players)
            {
                if (player != PlayerControl.LocalPlayer && UnityEngine.Random.Range(0f, 1f) < 0.01f)
                {
                    Vector3 randomOffset = new Vector3(
                        UnityEngine.Random.Range(-0.5f, 0.5f),
                        UnityEngine.Random.Range(-0.5f, 0.5f),
                        0
                    );
                    player.transform.position += randomOffset;
                }
            }
        }
        catch { }
    }

    public static void stealthModeCheat()
    {
        if (!CheatToggles.stealthMode) return;

        try
        {
            // Hide cheat indicators and make actions less obvious
            PlayerControl.LocalPlayer.nameText.text = PlayerControl.LocalPlayer.Data.PlayerName; // Reset name
        }
        catch { }
    }

    public static void resetPlayerScale()
    {
        if (!CheatToggles.bigPlayer && !CheatToggles.smallPlayer && Utils.isPlayer)
        {
            try
            {
                PlayerControl.LocalPlayer.transform.localScale = Vector3.one;
            }
            catch { }
        }
    }

    public static void resetPlayerVisibility()
    {
        if (!CheatToggles.invisiblePlayer && Utils.isPlayer)
        {
            try
            {
                SpriteRenderer renderer = PlayerControl.LocalPlayer.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    Color color = renderer.color;
                    color.a = 1f;
                    renderer.color = color;
                }
            }
            catch { }
        }
    }

    public static void resetTimeScale()
    {
        if (!CheatToggles.gameSpeed)
        {
            Time.timeScale = 1f;
        }
    }
}