using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MonoBbExitApplication : MonoBehaviour
{
    Button exitButton => GetComponent<Button>();
    // Start is called before the first frame update
    void Awake()
    {
        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }

    
}
