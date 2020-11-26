using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    [SerializeField] Bubble bubblePrefab;

    [SerializeField] List<Bubble> bubbles;
    [SerializeField] GameConfig config;
    [SerializeField] PlayerBubbleGameData data;
    [SerializeField] public BubbleUIController uiController;
    [SerializeField] public float waitTime = 0.02f;

    private int bubblePopCountr = 0;
    private int bubbleTotal = 0;
    private int levelCounter = 0;
    private const int MAX_AD_LVL_COUNT = 3;

    public IEnumerator InitializeController()
    {
        if(uiController == null) { uiController = GameObject.Find("BubbleGameUI").GetComponent<BubbleUIController>(); }
       // if(data == null) { data = }

        data.currentBubblePopped = 0;
        uiController.SetTapCounterTxt("0");
        uiController.AddListenerToResetButton(ResetBubble);

        CreateBubbles();
       // Debug.Log("YEYE");
        yield return null; 
    }

    private void CreateBubbles()
    {

        bool isPadded = false;
        float padding = 0;
        bubbleTotal = 0;
        bubbles = new List<Bubble>();
        for (int y = 0; y < config.yValue; y++)
        {
            isPadded = !isPadded;
            if (isPadded)
            {
                // padding = 0.435f;
                padding = 0;
            }
            else
            {
                padding = 0;
            }
            for (int x = 0; x < config.xValue; x++)
            {
                var buble = Instantiate(bubblePrefab, new Vector3(((x + padding) * config.xSpaceMultiplier),
                    y * config.ySpaceMultiplier,
                    0), Quaternion.identity);
                buble.InitializeBubble(AddPoint, config);
                //LeanTween.rotateZ(buble.gameObject, Random.Range(0, 360), 0);
                bubbles.Add(buble);
                bubbleTotal++;
            }
        }
    }

    private void Update()
    {
        foreach(Bubble b in bubbles)
        {
            b.refresh();
        }
    }

    public void ResetBubble()
    {
        foreach(Bubble b in bubbles)
        {
            b.ResetBubble();
        }
      
    }
    
    public void PauseBubbles(bool isActive)
    {
        foreach(Bubble b in bubbles)
        {
            b.Setactive(isActive);
        }
    }

    public void AddPoint()
    {
        data.currentBubblePopped += 1;
        data.totalBubblesPopped += 1;
        bubblePopCountr++;
        PlayerPrefs.SetInt(DataNames.TOTAL_TAP_COUNT.ToString(), (int)data.totalBubblesPopped);

        if (data.currentBubblePopped%10 == 0 )
        {
            uiController.ShowMileStoneanimation();
        }

        Debug.Log("HEHE");
        uiController.SetTapCounterTxt(data.currentBubblePopped.ToString());

        if(bubblePopCountr >= bubbleTotal)
        {
            
            StartCoroutine(ResetBubbleGame());
        }
    }

    private IEnumerator ResetBubbleGame()
    {
        yield return new WaitForSeconds(0.25f);
        levelCounter++;
        while (true)
        {
            foreach (Bubble b in bubbles)
            {
                b.Setactive(false);
            }
            break;
        }

        while (true)
        {
            //foreach (Bubble b in bubbles)
            //{
            //  StartCoroutine(b.EnumeratorResetBubble());
            //    yield return null;
            //}
            for(int x= bubbles.Count-1; x>=0; x--)
            {
                bubbles[x].ResetBubble();
                yield return new WaitForSeconds(waitTime);
            }
            break;
        }
        bubblePopCountr = 0;
        while (true)
        {
            foreach (Bubble b in bubbles)
            {
                b.Setactive(true);
            }
            break;
        }

        if(levelCounter >= MAX_AD_LVL_COUNT)
        {
            AdmobAds.instance.ShowInterstitialAd();
            Debug.Log("SHOW ADS");
            levelCounter = 0;
        }

        yield return null;
    }

   
}
