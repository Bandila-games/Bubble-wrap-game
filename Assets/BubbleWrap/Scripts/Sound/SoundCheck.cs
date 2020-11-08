using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;


public class SoundCheck : MonoBehaviour
{
    [SerializeField] Toggle bgm;
    [SerializeField] Toggle sfx;
    [SerializeField] Toggle Loop;
    [SerializeField] Toggle Oneshot;

    [SerializeField] InputField delayValue;

    
    /*
    public void PlayNormal()
    {
        Soundplayer.PlayAudio(AudioLibrary.SFX_POKERWIN2);
    }

    public void PlayWithSelect()
    {
        if(bgm.isOn)
        {
            Soundplayer.PlayAudio(AudioLibrary.SFX_POKERWIN2, SoundSourceType.BGM,PlayType.Oneshot);
        }
        if(sfx.isOn)
        {
            Soundplayer.PlayAudio(AudioLibrary.SFX_POKERWIN2, SoundSourceType.SFX);
        }
    }
    public void PlayWithPlayType()
    {
        if (Loop.isOn)
        {
            Soundplayer.PlayAudio(AudioLibrary.SFX_POKERWIN2, SoundSourceType.SFX,PlayType.Loop);
        }
        if (Oneshot.isOn)
        {
            Soundplayer.PlayAudio(AudioLibrary.SFX_POKERWIN2, SoundSourceType.SFX,PlayType.Oneshot);
        }
    }

    public void StopPlayLoop()
    {
        Soundplayer.StopAudio(AudioLibrary.SFX_POKERWIN2, SoundSourceType.SFX);
    }

    public void PlayWithDelay()
    {
        Soundplayer.PlayAudio(AudioLibrary.SFX_BOX_JUMP, SoundSourceType.SFX, ulong.Parse(delayValue.text));
    }

    public void PlayWithCallback()
    {
        Soundplayer.PlayAudioWithCallBack(AudioLibrary.SFX_BETCLICK,()=>{
            Debug.Log("HEHE, CALLBACK");
        }); 
    }

    public void StopBGSong()
    {
        Soundplayer.StopAudio(AudioLibrary.BGM_FISH_2, SoundSourceType.BGM);
    }

    public void PlayBGSong()
    {
        Soundplayer.PlayAudio(AudioLibrary.BGM_FISH_2, SoundSourceType.BGM, PlayType.Loop);
    }
    */
  
}
