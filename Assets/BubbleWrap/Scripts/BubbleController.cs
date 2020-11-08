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

    public IEnumerator InitializeController()
    {
        if(uiController == null) { uiController = GameObject.Find("BubbleGameUI").GetComponent<BubbleUIController>(); }
       // if(data == null) { data = }

        data.currentBubblePopped = 0;
        uiController.AddListenerToResetButton(ResetBubble);

        CreateBubbles();
       // Debug.Log("YEYE");
        yield return null; 
    }

    private void CreateBubbles()
    {
        bool isPadded = false;
        float padding = 0;
        bubbles = new List<Bubble>();
        for (int y = 0; y < config.yValue; y++)
        {
            isPadded = !isPadded;
            if (isPadded)
            {
                padding = 0.435f;
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
                buble.InitializeBubble(AddPoint,config);
                LeanTween.rotateZ(buble.gameObject, Random.Range(0, 360), 0);
                bubbles.Add(buble);
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

    public void AddPoint()
    {
        data.currentBubblePopped += 1;
        data.totalBubblesPopped += 1;
        if(data.currentBubblePopped%10 == 0 )
        {
            uiController.ShowMileStoneanimation();
        }

        uiController.SetTapCounterTxt(data.currentBubblePopped.ToString());
    }
}
