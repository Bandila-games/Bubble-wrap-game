using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPGAuth : MonoBehaviour
{
    public static PlayGamesPlatform platform;

    private void Start()
    {
        Debug.Log("========================AUTH START===============");
        if(platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .RequestIdToken()
                .RequestServerAuthCode(false)
                .Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
           platform = PlayGamesPlatform.Activate();

      
        }

        //Social.Active.localUser.Authenticate(SignInInteractivity.CanPromptOnce,(succes) => {

        //    if (succes)
        //    {
        //        Debug.Log("[GPGPAuth]: Success");
        //    }
        //    else
        //    {
        //        Debug.Log("[GPGAuth]: false");
        //    }
        //});

        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (resultt) => {

            Debug.Log("=========MESAGE: "+resultt.ToString());
            
        });
    }


}
