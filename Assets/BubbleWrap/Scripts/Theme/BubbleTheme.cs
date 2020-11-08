using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Bubble theme",menuName = "themes/Bubble")]
public class BubbleTheme : ScriptableObject
{
    [SerializeField] public Sprite unPoppedSprite;
    [SerializeField] public Sprite poppedSprite; 
}
