using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sound;
using UnityEngine.Events;

public class Bubble : MonoBehaviour
{
    [SerializeField] public SpriteRenderer spriteRenderer = null;
    [SerializeField] private BubbleTheme bubbleThemeConfig;

    bool isPopped = false;
    bool isTapped = false;
    UnityAction AddPointAction;
    GameConfig config;

    public void InitializeBubble(UnityAction action, GameConfig config)
    {
        isPopped = false;
        isTapped = false;
        
        spriteRenderer.sprite = bubbleThemeConfig.unPoppedSprite;
        AddPointAction = action;
        this.config = config;
    }

    public void refresh()
    {
        if (transform.localScale.y >= config.maxSize && isPopped == false)
        {
            AddPointAction?.Invoke();
            Soundplayer.PlayAudio((AudioLibrary)Random.Range(0,4));
            spriteRenderer.sprite = bubbleThemeConfig.poppedSprite;
            transform.localScale = new Vector3(1, 1, 1);
            // Handheld.Vibrate();

            isPopped = true;
        }
        else if (isPopped == false)
        {

            if (isTapped)
            {
                Debug.Log("On");
                if (transform.localScale.y <= config.maxSize && transform.localScale.x <= config.maxSize)
                {
                    Debug.Log("Expand");
                    transform.localScale += new Vector3(0.05f, 0.05f, 0.05f) * Time.deltaTime * config.popSpeedMultiplier;
                }

            }
            else
            {

                if (transform.localScale.y > config.minSize && transform.localScale.x > config.minSize)
                {
                    transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f) * Time.deltaTime * config.popSpeedMultiplier;
                }
            }

        }
    }

    private void OnMouseDown()
    {
        isTapped = true;
    }

    private void OnMouseUp()
    {
        isTapped = false;
    }


}
