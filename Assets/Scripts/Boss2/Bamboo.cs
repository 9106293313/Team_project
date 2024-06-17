using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bamboo : MonoBehaviour
{
    public float delayTime = 0.6f;
    public BoxCollider2D Newcollider2D;
    void Start()
    {
        StartCoroutine(OpenColider());
    }


    IEnumerator OpenColider()
    {
        yield return new WaitForSeconds(delayTime);
        Newcollider2D.enabled = true;
    }
}
