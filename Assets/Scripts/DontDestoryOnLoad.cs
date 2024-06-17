using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestoryOnLoad : MonoBehaviour
{
    public bool DontDestroyOnMenu = false;
    public bool DontDestroyOnGameWinScene = false;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" && DontDestroyOnMenu==false)
        {
            Destroy(gameObject);
        }
        if (SceneManager.GetActiveScene().name == "GameWinScene" && DontDestroyOnGameWinScene == false)
        {
            Destroy(gameObject);
        }
    }
}
