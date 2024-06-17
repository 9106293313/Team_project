using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet4 : MonoBehaviour
{
    public int Damage = 10;
    public float existTime = 3f;
    float Timer = 0;
    public float PoisonTime = 3f;
    public GameObject FloatingTextPrefeb;

    public GameObject PoisonCloud;

    public bool CreatePoisonCloud=true;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            collision.GetComponent<PlayerInfo>().TakeDamage(Damage);
            collision.GetComponent<PlayerInfo>().TakePoisonDamage(PoisonTime);

            if(CreatePoisonCloud)
            {
                Instantiate(PoisonCloud, transform.position, Quaternion.identity);
            }
            
            Destroy(gameObject);
        }
        if(collision.tag=="Grid")
        {
            if (CreatePoisonCloud)
            {
                Instantiate(PoisonCloud, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > existTime)
        {
            Destroy(gameObject);
        }
    }
}
