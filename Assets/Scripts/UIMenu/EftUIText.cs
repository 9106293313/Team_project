using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EftUIText : MonoBehaviour
{
    public GameObject EftIntro;

    public void MouseHover()
    {
        EftIntro.SetActive(true);
    }
    public void MouseLeave()
    {
        EftIntro.SetActive(false);
    }
}
