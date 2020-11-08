using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Collectable : MonoBehaviour
{
    [SerializeField] public Sprite collectableSprite;
    [SerializeField] public uint price;
    [SerializeField] public bool isLocked = false;
    [SerializeField] public bool isBought = false;
}
