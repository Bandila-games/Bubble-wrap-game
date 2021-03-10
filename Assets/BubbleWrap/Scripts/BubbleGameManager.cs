using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

namespace Bunity
{
    public class BubbleGameManager : MonoBehaviour
    {
        [SerializeField] public BubbleController bubbleController = null;
        [SerializeField] public BubbleDataController dataController = null;
        [SerializeField] private BubbleUIController uiController;

        //Test
        [SerializeField] public PlayerBubbleGameData data;
        [SerializeField] public BubbleGameConfig gameConfig;
        //[SerializeField] public GPGLeaderBoards leaderboards;

       // [SerializeField] public Canvas debugcanvas = null;
       // [SerializeField] public Transform debugImage = null;
       // [SerializeField] public Text debugText = null;
       

        private static BubbleGameManager _gameManager;
        private static BubbleGameManager gameManager
        {
            get { return _gameManager; }
            set { _gameManager = value; }
        }

        public static PlayGamesPlatform platform;
        public bool IsConnectedToPlayServices = false;

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
            uiController.scoreButton.SetButtonEnable(true);

            uiController.scoreButton.AddListenerToButton(() => {

                OpenLeaderBoards();
            });

            if (gameManager == null)
            {
                gameManager = this;
                                                 
            }
            else
            {
                Destroy(this.gameObject);
            }

            data.On_100_Popped += SetAchievements;
            data.On_500_Popped += SetAchievements;
            data.On_1000_Popped += SetAchievements;
            data.On_3000_Popped += SetAchievements;
            data.On_5000_Popped += SetAchievements;
            data.On_10000_Popped += SetAchievements;
         
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                       .RequestIdToken()
                       .RequestServerAuthCode(false)
                       .Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();

            ConnectToPlayServices();

            StartCoroutine(loadAds());
            Sound.Soundplayer.PlayAudio(AudioLibrary.BGM_NORMAL,Sound.SoundSourceType.BGM);
            Sound.Soundplayer.SetSourceVolume(Sound.SoundSourceType.BGM, 0.6f);
            // Sceneloader.Instance.Test();
        }

        #region GPG
        public void ConnectToPlayServices()
        {
            PlayGamesPlatform.Instance.Authenticate((f, s) => {

                Debug.Log("==============" + f);
                Debug.Log("============" + s);


                uiController.scoreButton.SetButtonEnable(f);

                IsConnectedToPlayServices = f;

                if (IsConnectedToPlayServices)
                {
                    Social.ReportScore(data.totalBubblesPopped, GPGSIds.leaderboard_pushpop_leaderboard, (isSuccess) =>
                    {
                        if (!isSuccess) Debug.LogError("[GPGLEADERBOARDS]: UNABLE TO POST HIGHSCORE");
                         
                    });
                    SetAchievements();
                    Debug.Log("======================OPEN");                   
                }

                else
                {
                    PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (isLoggedIn) => {
                        Debug.Log("==============again" + isLoggedIn);
                       


                        uiController.scoreButton.SetButtonEnable(isLoggedIn == SignInStatus.Success);
                        IsConnectedToPlayServices = isLoggedIn == SignInStatus.Success;

                        if (IsConnectedToPlayServices)
                        {
                            Social.ReportScore(data.totalBubblesPopped, GPGSIds.leaderboard_pushpop_leaderboard, (isSuccess) =>
                            {
                                if (!isSuccess) Debug.LogError("[GPGLEADERBOARDS]: UNABLE TO POST HIGHSCORE");

                            });

                            SetAchievements();
                        
                        }
                    });
                 
                }

            }, true);


        }

       
        //TODO MOVE TO ANOTHER CLASS
        public void SetAchievements()
        {
            if (PlayerPrefs.HasKey(GPGSIds.achievement_popper_starter) == false)
            {
                PlayerPrefs.SetInt(GPGSIds.achievement_popper_starter, 0);
                PlayerPrefs.SetInt(GPGSIds.achievement_junior_popper, 0);
                PlayerPrefs.SetInt(GPGSIds.achievement_casual_popper, 0);
                PlayerPrefs.SetInt(GPGSIds.achievement_regular_popper, 0);
                PlayerPrefs.SetInt(GPGSIds.achievement_senior_popper, 0);
                PlayerPrefs.SetInt(GPGSIds.achievement_hardcore_popper, 0);
            }

            bool popperStarterAlreadyUnlocked = PlayerPrefs.GetInt(GPGSIds.achievement_popper_starter) == 1;
            if (!popperStarterAlreadyUnlocked)
            {   if(data.totalBubblesPopped >= 100)
                {
                    Social.ReportProgress(GPGSIds.achievement_popper_starter,
                                     100.0f,
                                     (isSuccess) =>
                                     {
                                         PlayerPrefs.SetInt(GPGSIds.achievement_popper_starter, 1);
                                     });
                }               
            }
            bool popperJuniorAlreadyUnlocked = PlayerPrefs.GetInt(GPGSIds.achievement_junior_popper) == 1;
            if (!popperJuniorAlreadyUnlocked)
            {
                if (data.totalBubblesPopped >= 500)
                {
                    Social.ReportProgress(GPGSIds.achievement_junior_popper,
                                     100.0f,
                                     (isSuccess) =>
                                     {
                                         PlayerPrefs.SetInt(GPGSIds.achievement_junior_popper, 1);
                                     });
                }
            }
            bool poppercasualAlreadyUnlocked = PlayerPrefs.GetInt(GPGSIds.achievement_casual_popper) == 1;
            if (!poppercasualAlreadyUnlocked)
            {
                if (data.totalBubblesPopped >= 1000)
                {
                    Social.ReportProgress(GPGSIds.achievement_casual_popper,
                                     100.0f,
                                     (isSuccess) =>
                                     {
                                         PlayerPrefs.SetInt(GPGSIds.achievement_casual_popper, 1);
                                     });
                }
            }
            bool popperRegularAlreadyUnlocked = PlayerPrefs.GetInt(GPGSIds.achievement_regular_popper) == 1;
            if (!popperRegularAlreadyUnlocked)
            {
                if (data.totalBubblesPopped >= 3000)
                {
                    Social.ReportProgress(GPGSIds.achievement_regular_popper,
                                     100.0f,
                                     (isSuccess) =>
                                     {
                                         PlayerPrefs.SetInt(GPGSIds.achievement_regular_popper, 1);
                                     });
                }
            }
            bool popperSeniourAlreadyUnlocked = PlayerPrefs.GetInt(GPGSIds.achievement_senior_popper) == 1;
            if (!popperSeniourAlreadyUnlocked)
            {
                if (data.totalBubblesPopped >= 5000)
                {
                    Social.ReportProgress(GPGSIds.achievement_senior_popper,
                                     100.0f,
                                     (isSuccess) =>
                                     {
                                         PlayerPrefs.SetInt(GPGSIds.achievement_senior_popper,1);
                                     });
                }
            }
            bool popperHardcoreAlreadyUnlocked = PlayerPrefs.GetInt(GPGSIds.achievement_hardcore_popper) == 1;
            if (!popperStarterAlreadyUnlocked)
            {
                if (data.totalBubblesPopped >= 10000)
                {
                    Social.ReportProgress(GPGSIds.achievement_hardcore_popper,
                                     100.0f,
                                     (isSuccess) =>
                                     {
                                         PlayerPrefs.SetInt(GPGSIds.achievement_hardcore_popper, 1);
                                     });
                }
            }
        }

        public void OpenLeaderBoards()
        {
            Debug.Log("===========================OPEN LEADERBOARDS CALLED");

            if (IsConnectedToPlayServices)
            {
                Social.ReportScore(data.totalBubblesPopped, GPGSIds.leaderboard_pushpop_leaderboard, (isSuccess) =>
                {
                    if (!isSuccess) Debug.LogError("[GPGLEADERBOARDS]: UNABLE TO POST HIGHSCORE");

                });
                Debug.Log("======================OPEN");
            }
            Social.ShowLeaderboardUI();
        }
        #endregion


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

                yield return new WaitForSeconds(20);
               // AdmobAds.instance.requestInterstital();
                //AdmobAds.instance.loadRewardVideo();
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

        public void ClickSoundPlay()
        {
          if(gameConfig.isSoundActive) Sound.Soundplayer.PlayAudio(AudioLibrary.SFX_BUTTON_CLICK);
        }
    }
}

public enum DataNames
{
    TOTAL_TAP_COUNT
}