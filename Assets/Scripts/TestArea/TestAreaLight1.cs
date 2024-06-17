using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAreaLight1 : MonoBehaviour
{
    public TestAreaLightControl testAreaLightControl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            testAreaLightControl.UseLightControl2();
        }
    }
}
