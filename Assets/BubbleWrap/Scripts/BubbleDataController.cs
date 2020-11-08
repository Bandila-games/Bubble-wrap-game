using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BubbleDataController : MonoBehaviour
{
    [SerializeField] public PlayerBubbleGameData playerData;
    [SerializeField] private PlayerWalletData playerWalletData;
    [SerializeField] private PlayerCollectableData playerCollectableData;


    public IEnumerator EnumeratorInitializeData(UnityAction action)
    {


        action?.Invoke();
        yield return null;
    }

    public DATA_RESPONSE checkData()
    {
        var response = DATA_RESPONSE.RECENT;

        //Check data from database
        //Check which one is most recent
        

        return response;
    }
}


public enum DATA_RESPONSE
{
    RECENT,
    NEW,
    CONFLICTING
}
