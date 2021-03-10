using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPGLeaderBoards : MonoBehaviour
{
    public static PlayGamesPlatform platform;

    public bool IsConnectedToPlayServices = false;

    public PlayerBubbleGameData data = null;
   

    private void Start()
    {
        Debug.Log("========================AUTH START===============");
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .RequestIdToken()
                .RequestServerAuthCode(false)
                .Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();


        }
        //else
        //{
        //    Destroy(this.gameObject);
        //}
        ConnectToPlayServices();


     
    }

    public void ConnectToPlayServices()
    {
        PlayGamesPlatform.Instance.Authenticate((f,s)=> {

            Debug.Log("==============" + f);
            Debug.Log("============"+ s);
            IsConnectedToPlayServices = f;
        },true);
      
      
    }

    public void OpenLeaderBoards()
    {
        Debug.Log("===========================OPEN LEADERBOARDS CALLED");
        if (IsConnectedToPlayServices)
        {
            Social.ReportScore(data.totalBubblesPopped, GPGSIds.leaderboard_pushpop_leaderboard, (isSuccess) => {

                if (!isSuccess) Debug.LogError("[GPGLEADERBOARDS]: UNABLE TO POST HIGHSCORE");
              
            });
            Debug.Log("======================OPEN");
            Social.ShowLeaderboardUI();
        }
        else 
        Social.ShowLeaderboardUI();
    }
}
