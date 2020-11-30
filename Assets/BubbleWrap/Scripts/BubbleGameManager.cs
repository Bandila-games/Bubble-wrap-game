using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bunity
{
    public class BubbleGameManager : MonoBehaviour
    {
        [SerializeField] public BubbleController bubbleController = null;
        [SerializeField] public BubbleDataController dataController = null;
        [SerializeField] private BubbleUIController uiController;

        //Test
        [SerializeField] public PlayerBubbleGameData data;

       // [SerializeField] public Canvas debugcanvas = null;
       // [SerializeField] public Transform debugImage = null;
       // [SerializeField] public Text debugText = null;
       

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
         //   debugcanvas.sortingOrder = -1;
         //   debugImage.gameObject.SetActive(true);

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

        //    AdmobAds.instance.OnAdFailedToLoad += Instance_OnAdFailedToLoad;
        //    AdmobAds.instance.OnAdLoaded += Instance_OnAdLoaded;
        //    AdmobAds.instance.OnAdStarted += Instance_OnAdStarted;
       //     AdmobAds.instance.OnAdLoad += Instance_OnAdLoad;

            StartCoroutine(loadAds());
            // Sceneloader.Instance.Test();
        }

        private void Instance_OnAdLoad(object sender, AdLoadEventArgs e)
        {
            StartCoroutine(debugAds(e.Message));
        }

        private void Instance_OnAdStarted(object sender, System.EventArgs e)
        {
            StartCoroutine(debugAds("Add started succesfully"));
        }

        private void Instance_OnAdLoaded(object sender, System.EventArgs e)
        {
            StartCoroutine(debugAds("Ad loaded succesfully"));
        }

        private void Instance_OnAdFailedToLoad(object sender, GoogleMobileAds.Api.AdFailedToLoadEventArgs e)
        {
            StartCoroutine(debugAds(e.Message));
        }

        public void ShowDebug(bool isShow)
        {
           // debugcanvas.sortingOrder = isShow ? 3 : -1;
        }

        IEnumerator debugAds(string message)
        {

          //  Text mt = Instantiate(debugText, debugImage);
           // mt.text = message;
            yield return null;
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

        public IEnumerator loadAds()
        {
         
            while(true)
            {

                yield return new WaitForSeconds(30);
                AdmobAds.instance.requestInterstital();
                AdmobAds.instance.loadRewardVideo();
                AdmobAds.instance.reqBannerAd();                

            }
        }



        public void ShowTestInts()
        {
            AdmobAds.instance.ShowInterstitialAd();
        }
        public void ShowTestRewardVid()
        {
            AdmobAds.instance.showVideoAd();
        }
    }
}

public enum DataNames
{
    TOTAL_TAP_COUNT
}