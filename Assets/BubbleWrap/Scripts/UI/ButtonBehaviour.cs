using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


namespace Bunity
{

    [RequireComponent(typeof(Button))]
    public class ButtonBehaviour : MonoBehaviour
    {
        private bool isEnabled = false;

        public Button button = null;
        public Image buttonImage = null;

        public Color disabledColor;
        public Color enabledColor;

        public Sprite disabledBtnImage;
        public Sprite enabledBtnImage;


        private void Awake()
        {
            if(button == null)
            {
                button = GetComponent<Button>();
            }
        }

        public void SetButtonEnable(bool isEnabled = false)
        {
            button.interactable = isEnabled;
            if(isEnabled)
            {
                button.GetComponent<Image>().color = enabledColor;
                buttonImage.sprite = enabledBtnImage;
            }
            else
            {
                button.GetComponent<Image>().color = disabledColor;
                buttonImage.sprite = disabledBtnImage;
            }
        }

        public void AddListenerToButton(UnityAction action)
        {

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(action);

        }

         
    }
}