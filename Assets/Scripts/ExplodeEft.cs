using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEft : MonoBehaviour
{
    public int Damage;
    public float existTime;
    public GameObject FloatingTextPrefeb;
    void Start()
    {
        Destroy(gameObject,existTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (collision.GetComponent<EnemyInfo>() != null)
            {
                collision.GetComponent<EnemyInfo>().TakeDamage(Damage);
            }
            if (collision.GetComponent<BossInfo>() != null)
            {
                collision.GetComponent<BossInfo>().TakeDamage(Damage);
            }
            DamageNumberEft.ShowFloatingTextEft(FloatingTextPrefeb, collision, transform, Damage); //¥Í¦¨¶Ë®`¼Æ­È
        }
    }
}
