using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BubbleGameSettings : MonoBehaviour
{
    [SerializeField] public Toggle soundToggle = null;
    [SerializeField] public Toggle vibrationToggle = null;
    [SerializeField] public BubbleGameConfig gameConfig = null;

    private void OnEnable()
    {
        soundToggle.isOn = gameConfig.isSoundActive;
        vibrationToggle.isOn = gameConfig.isVibrateOn;
    }

    public void SaveSettings()
    {
        //foreach()
        gameConfig.isSoundActive = soundToggle.isOn;
        gameConfig.isVibrateOn = vibrationToggle.isOn;

    }
}
