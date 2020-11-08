using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BubbleUIController : MonoBehaviour
{
    [SerializeField] public Text tapCounterLbl = null;
    [SerializeField] public Text totalPoppedLbl = null;

    [SerializeField] public Button playButton = null;
    [SerializeField] public Button resetButton = null;

    [SerializeField] public RectTransform menu = null;
    [SerializeField] public RectTransform gameUI = null;


    //TEST

    [SerializeField] MileStoneAnimation animationPrefab;

    public void SetTapCounterTxt(string txt)
    {
        tapCounterLbl.text = txt;
    }

    public void SetTotalCounterTxt(string txt)
    {
        totalPoppedLbl.text = txt;
    }

    public void AddListenerToPlayButton(UnityAction action)
    {
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(action);
    }

    public void AddListenerToResetButton(UnityAction action)
    {
        resetButton.onClick.RemoveAllListeners();
        resetButton.onClick.AddListener(action);
    }
    public void AddListenerToMenuButton(UnityAction action)
    {
        Debug.Log("Not yet implemented");
    }

    public void HideMenu(bool isActive)
    {
        menu.gameObject.SetActive(isActive);
    }

    public void HideGameElements(bool isActive)
    {
        gameUI.gameObject.SetActive(isActive);
    }

    public void ShowMileStoneanimation()
    {
        var prefab = Instantiate(animationPrefab, gameUI.transform);
        StartCoroutine(prefab.Playanimation());
    }
}
