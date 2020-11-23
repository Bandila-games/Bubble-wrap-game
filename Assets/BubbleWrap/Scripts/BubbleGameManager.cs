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

        //Test
        [SerializeField] public PlayerBubbleGameData data;

        private static BubbleGameManager _gameManager;
        private static BubbleGameManager gameManager
        {
            get { return _gameManager; }
            set { _gameManager = value; }
        }

        private void Awake()
        {
          
        }

        private void Start()
        {
           

            if (uiController == null) {uiController = GameObject.Find("BubbleGameUI").GetComponent<BubbleUIController>(); }
            uiController.HideGameElements(false);

            data.totalBubblesPopped = (uint)PlayerPrefs.GetInt(DataNames.TOTAL_TAP_COUNT.ToString());
            uiController.SetTotalCounterTxt(data.totalBubblesPopped.ToString());

            uiController.AddListenerToPlayButton(() => { 
                StartCoroutine(InitializeGame(() => { })
                );
            });

            uiController.AddListenerToHomeButton(()=> {
                StartCoroutine(ReturnToMenu(()=> {
                    data.totalBubblesPopped = (uint)PlayerPrefs.GetInt(DataNames.TOTAL_TAP_COUNT.ToString());
                    uiController.SetTotalCounterTxt(data.totalBubblesPopped.ToString());
                }));
            });

            if (gameManager == null)
            {
                gameManager = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

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

            },()=> { uiController.HideMenu(false);
                uiController.HideGameElements(true);
            });
            
            action?.Invoke();
            yield return null;
        }

        private IEnumerator ReturnToMenu(UnityAction action)
        {
            yield return Sceneloader.Instance.ChangeScene((int)BubbleGameScenes.INIT, 0, 0, () => {

               /* if (bubbleController == null)
                {
                    bubbleController = GameObject.Find("Controller").GetComponent<BubbleController>();
                }
                StartCoroutine(bubbleController.InitializeController());
               */
            }, () => {
                uiController.HideMenu(true);
                uiController.HideGameElements(false);
            });



            action?.Invoke();
            yield return null;
        }

        public void toggleBubbleActive(bool isActive)
        {
            bubbleController.PauseBubbles(isActive);
        }

        public void OnApplicationQuit()
        {
            
        }
    }
}

public enum DataNames
{
    TOTAL_TAP_COUNT
}