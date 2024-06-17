using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using TMPro;
using UnityEngine;

public class thunderballObj : MonoBehaviour
{
    public float existTime = 3f;
    public int Damage = 10;

    public GameObject FloatingTextPrefeb;

    public float critRate;
    public float critDamage;

    void Start()
    {
        Destroy(gameObject, existTime);
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            float A = UnityEngine.Random.Range(0.0f, 1.0f);  // 0.0 ~ 1.0
            if (A <= critRate * 0.01) //觸發爆擊
            {
                Damage += System.Convert.ToInt16(Damage * critDamage * 0.01f);

                if (collision.GetComponent<EnemyInfo>() != null)
                {
                    collision.GetComponent<EnemyInfo>().TakeDamage(Damage);
                }
                if (collision.GetComponent<BossInfo>() != null)
                {
                    collision.GetComponent<BossInfo>().TakeDamage(Damage);
                }
            }
            else //沒爆擊
            {
                if (collision.GetComponent<EnemyInfo>() != null)
                {
                    collision.GetComponent<EnemyInfo>().TakeDamage(Damage);
                }
                if (collision.GetComponent<BossInfo>() != null)
                {
                    collision.GetComponent<BossInfo>().TakeDamage(Damage);
                }
            }
            if (CardSystem.HasCard("星星"))
            {
                float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                if (randomNum <= CardSystem.StarTriggerProbability) //10%觸發機率
                {
                    int HealNum = Convert.ToInt16((float)Damage * CardSystem.StarHealPercentage);
                    GameObject.FindWithTag("Player").GetComponent<PlayerInfo>().Heal(HealNum);
                }
            }
            if (CardSystem.HasCard("塔"))
            {
                float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                if (randomNum <= CardSystem.TowerTriggerProbability) //3%觸發機率
                {
                    var TowerExplodePrefab = Resources.Load<GameObject>("Prefab/TowerExplodeObj");
                    Instantiate(TowerExplodePrefab, transform.position, Quaternion.identity);
                }
            }


            DamageNumberEft.ShowFloatingTextEft(FloatingTextPrefeb, collision, transform, Damage); //生成傷害數值

        }
    }
}
