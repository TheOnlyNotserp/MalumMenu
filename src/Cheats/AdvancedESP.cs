using UnityEngine;
using System.Collections.Generic;

namespace MalumMenu;
public static class AdvancedESP
{
    private static Dictionary<GameObject, GameObject> espObjects = new Dictionary<GameObject, GameObject>();
    private static Dictionary<PlayerControl, GameObject> distanceLabels = new Dictionary<PlayerControl, GameObject>();

    public static void playerESPCheat()
    {
        if (!CheatToggles.playerESP) return;

        try
        {
            PlayerControl[] players = Object.FindObjectsOfType<PlayerControl>();
            foreach (PlayerControl player in players)
            {
                if (player != PlayerControl.LocalPlayer)
                {
                    CreateESPBox(player.gameObject, Color.cyan);
                }
            }
        }
        catch { }
    }

    public static void taskESPCheat()
    {
        if (!CheatToggles.taskESP) return;

        try
        {
            PlayerTask[] tasks = Object.FindObjectsOfType<PlayerTask>();
            foreach (PlayerTask task in tasks)
            {
                if (!task.IsComplete)
                {
                    CreateESPBox(task.gameObject, Color.yellow);
                }
            }
        }
        catch { }
    }

    public static void ventESPCheat()
    {
        if (!CheatToggles.ventESP) return;

        try
        {
            Vent[] vents = Object.FindObjectsOfType<Vent>();
            foreach (Vent vent in vents)
            {
                CreateESPBox(vent.gameObject, Color.magenta);
            }
        }
        catch { }
    }

    public static void doorESPCheat()
    {
        if (!CheatToggles.doorESP) return;

        try
        {
            OpenableDoor[] doors = Object.FindObjectsOfType<OpenableDoor>();
            foreach (OpenableDoor door in doors)
            {
                Color doorColor = door.Open ? Color.green : Color.red;
                CreateESPBox(door.gameObject, doorColor);
            }
        }
        catch { }
    }

    public static void showPlayerDistanceCheat()
    {
        if (!CheatToggles.showPlayerDistance || !Utils.isPlayer) return;

        try
        {
            PlayerControl[] players = Object.FindObjectsOfType<PlayerControl>();
            foreach (PlayerControl player in players)
            {
                if (player != PlayerControl.LocalPlayer)
                {
                    float distance = Utils.getDistanceFrom(player);
                    CreateDistanceLabel(player, $"{distance:F1}m");
                }
            }
        }
        catch { }
    }

    public static void showPlayerHealthCheat()
    {
        if (!CheatToggles.showPlayerHealth) return;

        try
        {
            PlayerControl[] players = Object.FindObjectsOfType<PlayerControl>();
            foreach (PlayerControl player in players)
            {
                if (player != PlayerControl.LocalPlayer)
                {
                    string healthStatus = player.Data.IsDead ? "DEAD" : "ALIVE";
                    Color healthColor = player.Data.IsDead ? Color.red : Color.green;
                    CreateHealthLabel(player, healthStatus, healthColor);
                }
            }
        }
        catch { }
    }

    public static void showPlayerStatsCheat()
    {
        if (!CheatToggles.showPlayerStats) return;

        try
        {
            PlayerControl[] players = Object.FindObjectsOfType<PlayerControl>();
            foreach (PlayerControl player in players)
            {
                if (player != PlayerControl.LocalPlayer)
                {
                    string stats = $"Tasks: {GetCompletedTasks(player)}/{GetTotalTasks(player)}";
                    CreateStatsLabel(player, stats);
                }
            }
        }
        catch { }
    }

    private static void CreateESPBox(GameObject target, Color color)
    {
        if (espObjects.ContainsKey(target)) return;

        try
        {
            GameObject espBox = new GameObject("ESPBox");
            LineRenderer lineRenderer = espBox.AddComponent<LineRenderer>();
            
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.color = color;
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.positionCount = 5;
            
            // Create box around target
            Bounds bounds = GetObjectBounds(target);
            Vector3[] positions = new Vector3[5]
            {
                new Vector3(bounds.min.x, bounds.min.y, -1),
                new Vector3(bounds.max.x, bounds.min.y, -1),
                new Vector3(bounds.max.x, bounds.max.y, -1),
                new Vector3(bounds.min.x, bounds.max.y, -1),
                new Vector3(bounds.min.x, bounds.min.y, -1)
            };
            
            lineRenderer.SetPositions(positions);
            espObjects[target] = espBox;
        }
        catch { }
    }

    private static void CreateDistanceLabel(PlayerControl player, string text)
    {
        try
        {
            if (!distanceLabels.ContainsKey(player))
            {
                GameObject labelObj = new GameObject("DistanceLabel");
                TextMesh textMesh = labelObj.AddComponent<TextMesh>();
                textMesh.text = text;
                textMesh.fontSize = 20;
                textMesh.color = Color.white;
                textMesh.anchor = TextAnchor.MiddleCenter;
                
                labelObj.transform.position = player.transform.position + Vector3.up * 1.5f;
                distanceLabels[player] = labelObj;
            }
            else
            {
                distanceLabels[player].GetComponent<TextMesh>().text = text;
                distanceLabels[player].transform.position = player.transform.position + Vector3.up * 1.5f;
            }
        }
        catch { }
    }

    private static void CreateHealthLabel(PlayerControl player, string text, Color color)
    {
        try
        {
            GameObject labelObj = new GameObject("HealthLabel");
            TextMesh textMesh = labelObj.AddComponent<TextMesh>();
            textMesh.text = text;
            textMesh.fontSize = 15;
            textMesh.color = color;
            textMesh.anchor = TextAnchor.MiddleCenter;
            
            labelObj.transform.position = player.transform.position + Vector3.up * 2f;
        }
        catch { }
    }

    private static void CreateStatsLabel(PlayerControl player, string text)
    {
        try
        {
            GameObject labelObj = new GameObject("StatsLabel");
            TextMesh textMesh = labelObj.AddComponent<TextMesh>();
            textMesh.text = text;
            textMesh.fontSize = 12;
            textMesh.color = Color.cyan;
            textMesh.anchor = TextAnchor.MiddleCenter;
            
            labelObj.transform.position = player.transform.position + Vector3.up * 2.5f;
        }
        catch { }
    }

    private static Bounds GetObjectBounds(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            return renderer.bounds;
        }
        
        Collider2D collider = obj.GetComponent<Collider2D>();
        if (collider != null)
        {
            return collider.bounds;
        }
        
        return new Bounds(obj.transform.position, Vector3.one);
    }

    private static int GetCompletedTasks(PlayerControl player)
    {
        try
        {
            return player.myTasks.Count(task => task.IsComplete);
        }
        catch
        {
            return 0;
        }
    }

    private static int GetTotalTasks(PlayerControl player)
    {
        try
        {
            return player.myTasks.Count;
        }
        catch
        {
            return 0;
        }
    }

    public static void CleanupESP()
    {
        foreach (var espObj in espObjects.Values)
        {
            if (espObj != null)
            {
                Object.Destroy(espObj);
            }
        }
        espObjects.Clear();

        foreach (var label in distanceLabels.Values)
        {
            if (label != null)
            {
                Object.Destroy(label);
            }
        }
        distanceLabels.Clear();
    }
}