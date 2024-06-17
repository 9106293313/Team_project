using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnMainMenu : MonoBehaviour
{
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            Destroy(gameObject);
        }
    }
}
