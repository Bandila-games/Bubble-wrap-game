using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Player Data Cache", menuName = "BubbleGame/Player Data Cache")]
public class PlayerBubbleGameData : ScriptableObject
{
    public string player_name;
    public uint uid;
    public uint totalBubblesPopped;
    public uint currentBubblePopped = 0;

    public event UnityAction On_100_Popped;
    public event UnityAction On_500_Popped;
    public event UnityAction On_1000_Popped;
    public event UnityAction On_3000_Popped;
    public event UnityAction On_5000_Popped;
    public event UnityAction On_10000_Popped;

    public void AddTotal()
    {

        totalBubblesPopped++;
        PlayerPrefs.SetInt(DataNames.TOTAL_TAP_COUNT.ToString(), (int)totalBubblesPopped);

        if (totalBubblesPopped >= 100&& PlayerPrefs.GetInt(GPGSIds.achievement_popper_starter) != 1) { On_100_Popped?.Invoke(); PlayerPrefs.SetInt(GPGSIds.achievement_popper_starter, 1); }
        if (totalBubblesPopped >= 500 && PlayerPrefs.GetInt(GPGSIds.achievement_junior_popper) != 1) { On_500_Popped?.Invoke(); PlayerPrefs.SetInt(GPGSIds.achievement_junior_popper, 1); }
        if (totalBubblesPopped >= 1000 && PlayerPrefs.GetInt(GPGSIds.achievement_casual_popper) != 1) { On_1000_Popped?.Invoke(); PlayerPrefs.SetInt(GPGSIds.achievement_casual_popper, 1); }
        if (totalBubblesPopped >= 3000 && PlayerPrefs.GetInt(GPGSIds.achievement_regular_popper) != 1) { On_3000_Popped?.Invoke(); PlayerPrefs.SetInt(GPGSIds.achievement_regular_popper, 1); }
        if (totalBubblesPopped >= 5000 && PlayerPrefs.GetInt(GPGSIds.achievement_senior_popper) != 1) { On_5000_Popped?.Invoke(); PlayerPrefs.SetInt(GPGSIds.achievement_senior_popper, 1); }
        if (totalBubblesPopped >= 10000 && PlayerPrefs.GetInt(GPGSIds.achievement_hardcore_popper) != 1) { On_10000_Popped?.Invoke(); PlayerPrefs.SetInt(GPGSIds.achievement_hardcore_popper, 1); }

    }

    [ContextMenu("RESET")]
    public void ResetCache()
    {
        PlayerPrefs.SetInt(DataNames.TOTAL_TAP_COUNT.ToString(), 0);
    }
}

