using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MonoBbTotalTxt : MonoBehaviour
{
    [SerializeField] PlayerBubbleGameData data;
    [SerializeField] Text totalText => GetComponent<Text>();

    // Update is called once per frame
    void Update()
    {
        totalText.text = "All Time Total: "+ data.totalBubblesPopped;
    }
}
