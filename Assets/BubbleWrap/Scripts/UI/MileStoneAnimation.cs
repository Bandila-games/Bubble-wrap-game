using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sound;

public class MileStoneAnimation : MonoBehaviour
{
    [SerializeField] List<Animator> animators;
    [SerializeField] PlayerBubbleGameData gameData;
    [SerializeField] Text scoreText = null;

    public IEnumerator Playanimation()
    {
        scoreText.text = gameData.currentBubblePopped.ToString();
        Soundplayer.PlayAudio(AudioLibrary.SFX_MILESTONE);
        foreach(Animator a in animators)
        {
            a.enabled = true;
        }
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

}
