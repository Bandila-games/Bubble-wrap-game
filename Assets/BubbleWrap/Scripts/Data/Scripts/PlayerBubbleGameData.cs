using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data Cache", menuName = "BubbleGame/Player Data Cache")]
public class PlayerBubbleGameData : ScriptableObject
{
    public string player_name;
    public uint uid;
    public uint totalBubblesPopped;
    public uint currentBubblePopped = 0;

    [ContextMenu("RESET")]
    public void ResetCache()
    {
        PlayerPrefs.SetInt(DataNames.TOTAL_TAP_COUNT.ToString(), 0);
    }
}

