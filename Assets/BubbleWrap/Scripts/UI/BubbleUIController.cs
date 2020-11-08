using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BubbleUIController : MonoBehaviour
{
    [SerializeField] public Text tapCounterLbl = null;

    [SerializeField] public Button playButton = null;

    [SerializeField] public RectTransform menu = null;

    public void SetTapCounterTxt(string txt)
    {
        tapCounterLbl.text = txt;
    }

    public void AddListenerToPlayButton(UnityAction action)
    {
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(action);
    }

    public void HideMenu(bool isActive)
    {
        menu.gameObject.SetActive(isActive);
    }
}
