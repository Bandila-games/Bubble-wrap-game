using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="bubble game config",menuName ="BubbleGame/Config")]
public class BubbleGameConfig : ScriptableObject
{
    [SerializeField] public bool isSoundActive;
    [SerializeField] public bool isVibrateOn;
}
