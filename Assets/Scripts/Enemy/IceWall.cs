using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class IceWall : MonoBehaviour
{
    public float existTime = 30f;
    public GameObject FloatingTextPrefeb;
    public GameObject childObj;
    public AudioSource audioSource;

    bool Isbounce = false;

    public GameObject Hitbox;
    public CircleCollider2D CollisonCollider;
    void Start()
    {
        Destroy(gameObject,existTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Grid") || collision.CompareTag("Wall"))
        {
            if(!Isbounce)
            {
                StartCoroutine(bounceCoolDown());
                Atk();
            }
        }
    }
    IEnumerator bounceCoolDown()
    {
        Isbounce = true;
        yield return new WaitForSeconds(0.3f);
        Isbounce = false;
    }
    void Atk()
    {
        float A = gameObject.transform.localScale.x * 10f;
        NormalAtk(childObj, 10f*A/60 + A, Convert.ToInt16(A)*6 , 0, 360);
        gameObject.transform.localScale = gameObject.transform.localScale * 0.85f;
        if(gameObject.transform.localScale.x < 0.5f )
        {
            CollisonCollider.enabled = false;
            Hitbox.SetActive(false);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            Destroy(gameObject,1f);
        }
    }
    void NormalAtk(GameObject BulletType, float BulletSpeed, int Radius, int MinRadius, int MaxRadius) //朝周圍扇形攻擊，可設定角度和子彈
    {
        audioSource.Play();

        for (int i = 0; i < MaxRadius; i += Radius)
        {
            GameObject bullet = Instantiate(BulletType, gameObject.transform.position, Quaternion.Euler(0, 0, transform.rotation.z + MinRadius+i));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * BulletSpeed, ForceMode2D.Impulse);
        }
    }

}
