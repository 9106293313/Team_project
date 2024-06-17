using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableInCardScene : MonoBehaviour
{
    public RectTransform Rect;
    Vector3 DefaultScale;
    void Start()
    {
        DefaultScale = Rect.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "CardSceneA" || SceneManager.GetActiveScene().name == "CardSceneB")
        {
            Rect.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            Rect.localScale = DefaultScale;
        }
    }
}
