using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Sceneloader : MonoBehaviour
{
    [SerializeField] public float totalFadeTime = 0.25f;
    [SerializeField] public Image blackOverlay = null;
    [SerializeField] public Canvas overlayCanvas = null;

    private static Sceneloader instance;
    public static Sceneloader Instance { get { return instance; } set { instance = value; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void Test()
    {
        Debug.Log("TEST");
    }

    public IEnumerator ChangeScene(int sceneID,float fadeInTime = 0, float fadeOutTime = 0, UnityAction action = null, UnityAction middleAction = null)
    {
        if(fadeInTime == 0) { fadeInTime = totalFadeTime; }
        if(fadeOutTime == 0) { fadeOutTime = totalFadeTime; }

        LeanTween.alpha(blackOverlay.rectTransform, 1, fadeOutTime).setOnComplete(() => {
            middleAction?.Invoke();
            StartCoroutine(LoadScene(sceneID, () => {
                LeanTween.alpha(blackOverlay.rectTransform, 0, fadeInTime).setOnComplete(()=> { action?.Invoke(); });
            }));
        });

        yield return null;
    }

    private IEnumerator LoadScene(int sceneId,UnityAction action)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneId);
        while(!asyncLoad.isDone)
        {
            yield return null;
        }
        action?.Invoke();
        yield return null;
    }

}
