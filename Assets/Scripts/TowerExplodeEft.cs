using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerExplodeEft : MonoBehaviour
{
    public float existTime;
    public GameObject FloatingTextPrefeb;
    void Start()
    {
        Destroy(gameObject, existTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<EnemyInfo>() != null)
            {
                int DamageNum = Convert.ToInt16((float)collision.gameObject.GetComponent<EnemyInfo>().maxHealth * CardSystem.TowerDamagePercentage * CardSystem.JusticeDamageMultiplying);
                collision.GetComponent<EnemyInfo>().TakeDamage(DamageNum);
                DamageNumberEft.ShowFloatingTextEft(FloatingTextPrefeb, collision, transform, DamageNum); //生成傷害數值
            }
            if (collision.GetComponent<BossInfo>() != null)
            {
                int DamageNum = Convert.ToInt16((float)collision.gameObject.GetComponent<BossInfo>().maxHealth * CardSystem.TowerDamagePercentage * CardSystem.JusticeDamageMultiplying);
                collision.GetComponent<BossInfo>().TakeDamage(DamageNum);
                DamageNumberEft.ShowFloatingTextEft(FloatingTextPrefeb, collision, transform, DamageNum); //生成傷害數值
            }
        }
        if(collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerInfo>() != null)
            {
                int DamageNum = Convert.ToInt16((float)collision.gameObject.GetComponent<PlayerInfo>().maxHealth * CardSystem.TowerDamagePercentage);
                collision.GetComponent<PlayerInfo>().TakeDamage(DamageNum);
            }
        }

    }
}
