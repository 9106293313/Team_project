using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;

public class TestAreaLightControl : MonoBehaviour
{
    public GameObject Enemy1, Enemy2;
    public GameObject TileMap2;
    public GameObject GlobalLight,light1,light2, light3;
    public GameObject Text2, Text3, Text4;
    public AudioSource lightOffSound, lightOnSound, goalClearSound, shortBuzzSound, BuzzLoopSound;
    bool Enemy1Dead=false, Enemy2Dead=false;
    void Start()
    {
        StartCoroutine(LightControl1());
    }

    void Update()
    {
        if(light2.activeInHierarchy)
        {
            if(Enemy1 == null && Enemy1Dead == false)
            {
                StartCoroutine(StartEnemy1Dead());
            }
            if(Enemy2 == null && Enemy1Dead == true)
            {
                if(Enemy2Dead == false)
                {
                    goalClearSound.Play();
                    TileMap2.SetActive(false);
                    light2.SetActive(false);
                    lightOffSound.Play();
                    BuzzLoopSound.Stop();
                    Text3.SetActive(false);
                    Text4.SetActive(true);
                    Enemy2Dead = true;
                }
            }
        }
    }
    IEnumerator LightControl1()
    {
        yield return new WaitForSeconds(1f);
        lightOffSound.Play();
        GlobalLight.GetComponent<Light2D>().intensity = 0.1f;
        GameObject.FindWithTag("PlayerLight").GetComponent<Light2D>().intensity = 0.8f;
        yield return new WaitForSeconds(1.5f);
        lightOnSound.Play();
        shortBuzzSound.Play();
        BuzzLoopSound.Play();
        light1.SetActive(true);
    }

    public void UseLightControl2()
    {
        StartCoroutine(LightControl2());
    }
    IEnumerator LightControl2()
    {
        goalClearSound.Play();
        yield return new WaitForSeconds(0.5f);
        lightOffSound.Play();
        BuzzLoopSound.Stop();
        light1.SetActive(false);
        yield return new WaitForSeconds(1f);
        lightOnSound.Play();
        shortBuzzSound.Play();
        BuzzLoopSound.Play();
        light2.SetActive(true);
        Text2.SetActive(true);
    }

    IEnumerator StartEnemy1Dead()
    {
        Enemy1Dead = true;
        goalClearSound.Play();
        Text2.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        Enemy2.SetActive(true);
        Text3.SetActive(true);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        lightOnSound.Play();
        shortBuzzSound.Play();
        GlobalLight.GetComponent<Light2D>().intensity = 0.4f;
        GameObject.FindWithTag("PlayerLight").GetComponent<Light2D>().intensity = 0f;
        GetComponent<BoxCollider2D>().enabled = false;
        light3.SetActive(true);
    }
}
