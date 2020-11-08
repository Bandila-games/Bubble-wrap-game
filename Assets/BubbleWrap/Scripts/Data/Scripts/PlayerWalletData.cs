using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Wallet Data",menuName ="Wallet/Player wallet data")]
public class PlayerWalletData : ScriptableObject
{
    public uint currentGoldAmount;
    public uint totalGoldAccumulated;
    public uint totalGoldSpent;
}
