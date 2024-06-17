using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonLeaf : MonoBehaviour
{
    public float existTime = 3f;
    public int Damage = 10;
    public float PoisonTime = 3f;
    public GameObject FloatingTextPrefeb;
    float Timer=0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerInfo>().TakeDamage(Damage);
            collision.GetComponent<PlayerInfo>().TakePoisonDamage(PoisonTime);
            
        }
        if (collision.tag == "Grid")
        {
            Destroy(gameObject);   
        }
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if(Timer>=existTime)
        {
            Destroy(gameObject);
        }
    }
}
