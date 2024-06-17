using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet2 : MonoBehaviour
{
    public float existTime = 3f;
    float Timer = 0;
    public int Damage = 10;
    public float PoisonTime = 3f;
    public GameObject FloatingTextPrefeb;
    public bool ShootThrough = false;
    public GameObject PoisonCloudObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerInfo>().TakeDamage(Damage);
            collision.GetComponent<PlayerInfo>().TakePoisonDamage(PoisonTime);
            Instantiate(PoisonCloudObj,transform.position,Quaternion.identity);
            if (ShootThrough == false)
            {
                GetComponent<Animator>().SetTrigger("Destroy");
            }
        }
        if(collision.tag == "Grid")
        {
            GetComponent<Animator>().SetTrigger("Destroy");
            Instantiate(PoisonCloudObj, transform.position, Quaternion.identity);
        }
    }
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer>existTime)
        {
            GetComponent<Animator>().SetTrigger("Destroy");
        }
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }
    
}
