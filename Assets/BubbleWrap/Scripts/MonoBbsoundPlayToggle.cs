using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MonoBbsoundPlayToggle : MonoBehaviour
{
    [SerializeField] Button button => GetComponent<Button>();
    [SerializeField] bool stopBg = false;

    public void Awake()
    {
        button.onClick.RemoveListener(ToggleSound);
        button.onClick.AddListener(ToggleSound);
    }


    public void ToggleSound()
    {
        Debug.Log("HEHE");
        if (stopBg)
        {
            Debug.Log("HEHE");
            Sound.Soundplayer.StopAudio(AudioLibrary.BGM_NORMAL, Sound.SoundSourceType.BGM);
        }
        else
        {
            Sound.Soundplayer.PlayAudio(AudioLibrary.BGM_NORMAL, Sound.SoundSourceType.BGM);
        }
    }

}
