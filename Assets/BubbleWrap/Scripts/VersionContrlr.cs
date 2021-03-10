using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class VersionContrlr : MonoBehaviour
{
    Text versionTxt => GetComponent<Text>();
    // Start is called before the first frame update
    void Start()
    {
        string version = "V."+Application.version;
        versionTxt.text = version;
    }

   
}
