﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sound;
using UnityEngine.Events;

public class Bubble : MonoBehaviour
{
    [SerializeField] public SpriteRenderer spriteRenderer = null;
    [SerializeField] private BubbleTheme bubbleThemeConfig;
    [SerializeField] private Collider collider;

    bool isPopped = false;
    bool isDecompressing = false;
    bool isTapped = false;
    UnityAction AddPointAction;
    GameConfig config;
    public BubbleGameConfig gameconfig;

    public void InitializeBubble(UnityAction action, GameConfig config)
    {
        isPopped = false;
        isTapped = false;
        
        spriteRenderer.sprite = bubbleThemeConfig.unPoppedSprite;
        AddPointAction = action;
        this.config = config;
    }

    public void ResetBubble()
    {
        isPopped = false;
        isTapped = false;
        transform.localScale = new Vector3(config.minSize, config.minSize); 
        spriteRenderer.sprite = bubbleThemeConfig.unPoppedSprite;
    }

    public IEnumerator EnumeratorResetBubble()
    {
        isPopped = false;
        isTapped = false;
        transform.localScale = new Vector3(config.minSize, config.minSize);
         LeanTween.color(this.gameObject, new Color(255, 255, 255, 125), 1f).setOnComplete(()=> {
             LeanTween.color(this.gameObject, new Color(255, 255, 255, 255), 0.25f);
         });              
        spriteRenderer.sprite = bubbleThemeConfig.unPoppedSprite;
        yield return null;
    }

    public void refresh()
    {
        if (transform.localScale.y >= config.maxSize && isPopped == false)
        {
            AddPointAction?.Invoke();
            if (gameconfig.isSoundActive)
            { Soundplayer.PlayAudio((AudioLibrary)Random.Range(1, 4)); }
            spriteRenderer.sprite = bubbleThemeConfig.poppedSprite;
            isDecompressing = true;
            StartCoroutine(decompressAnimation());
            //transform.localScale = new Vector3(1, 1, 1);

            

            if(gameconfig.isVibrateOn) Handheld.Vibrate();
          
            isPopped = true;
        }
        else if (isPopped == false)
        {

            if (isTapped)
            {

                if (transform.localScale.y <= config.maxSize && transform.localScale.x <= config.maxSize)
                {
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
        else if(isPopped && !isDecompressing)
        {
            if (isTapped )
            {

                if (transform.localScale.y <= config.maxSize && transform.localScale.x <= config.maxSize)
                {
                  
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

    private IEnumerator decompressAnimation()
    {      
        while(transform.localScale.y > config.minSize && transform.localScale.x > config.minSize)
        {

            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f) * Time.deltaTime * config.popSpeedMultiplier;
            yield return null;
        }

        isDecompressing = false;
        yield return null;
    }

    /*
    private void OnMouseDown()
    {
        isTapped = true;
    }

    private void OnMouseUp()
    {
        isTapped = false;
    }
    */

    private void OnMouseEnter()
    {
        isTapped = true;
    }

    private void OnMouseExit()
    {
        isTapped = false;
    }

    public void Setactive(bool isActive)
    {
        isTapped = false;
        collider.enabled = isActive;
    }

}
