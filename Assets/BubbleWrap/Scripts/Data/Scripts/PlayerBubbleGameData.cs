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
}

