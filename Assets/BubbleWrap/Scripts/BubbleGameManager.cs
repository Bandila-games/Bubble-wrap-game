using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Bunity
{
    public class BubbleGameManager : MonoBehaviour
    {
        [SerializeField] public BubbleController bubbleController = null;
        [SerializeField] public BubbleDataController dataController = null;
        [SerializeField] private BubbleUIController uiController;

        private void Awake()
        {
          
        }

        private void Start()
        {
            if (uiController == null) { GameObject.Find("BubbleGameUI").GetComponent<BubbleUIController>(); }


            uiController.AddListenerToPlayButton(() => { 
                StartCoroutine(InitializeGame(() => { })
                );
            });
           // Sceneloader.Instance.Test();
        }

        private IEnumerator InitializeGame(UnityAction action)
        {
           yield return Sceneloader.Instance.ChangeScene((int)BubbleGameScenes.MAIN,0,0,()=> {

               if(bubbleController == null)
               {
                   bubbleController = GameObject.Find("Controller").GetComponent<BubbleController>();
               }
               StartCoroutine(bubbleController.InitializeController());

            },()=> { uiController.HideMenu(false); });
            


            action?.Invoke();
            yield return null;
        }
    }
}