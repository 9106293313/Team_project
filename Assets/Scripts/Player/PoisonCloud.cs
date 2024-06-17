using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    public GameObject PoisonCloudObj;

    public float existTime = 3.5f;
    public int Damage;
    public GameObject FloatingTextPrefeb;
    public float critRate;
    public float critDamage;

    float AtkCoolDown = 0.5f;
    float Timer = 0f;
    bool CanAtk = true;

    int FinalCritDamage;

    private void Start()
    {
        Destroy(PoisonCloudObj, existTime);
        FinalCritDamage = Damage + System.Convert.ToInt16(Damage * critDamage * 0.01f);
    }
    private void Update()
    {
        if(CanAtk==false)
        {
            Timer += Time.deltaTime;
        }
        
        if(Timer >= AtkCoolDown)
        {
            CanAtk = true;
            Timer = 0f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && CanAtk)
        {
            float A = UnityEngine.Random.Range(0.0f, 1.0f);  // 0.0 ~ 1.0
            if (A <= critRate * 0.01) //Ĳ�o�z��
            {
                if(collision.GetComponent<EnemyInfo>()!=null)
                {
                    collision.GetComponent<EnemyInfo>().TakeDamage(FinalCritDamage);
                }
                if (collision.GetComponent<BossInfo>() != null)
                {
                    collision.GetComponent<BossInfo>().TakeDamage(FinalCritDamage);
                }

            }
            else //�S�z��
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

            if (CardSystem.HasCard("�P�P"))
            {
                float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                if (randomNum <= CardSystem.StarTriggerProbability) //10%Ĳ�o���v
                {
                    int HealNum = Convert.ToInt16((float)Damage * CardSystem.StarHealPercentage);
                    GameObject.FindWithTag("Player").GetComponent<PlayerInfo>().Heal(HealNum);
                }
            }
            if (CardSystem.HasCard("��"))
            {
                float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                if (randomNum <= CardSystem.TowerTriggerProbability) //3%Ĳ�o���v
                {
                    var TowerExplodePrefab = Resources.Load<GameObject>("Prefab/TowerExplodeObj");
                    Instantiate(TowerExplodePrefab, transform.position, Quaternion.identity);
                }
            }


            DamageNumberEft.ShowFloatingTextEft(FloatingTextPrefeb, collision, transform, Damage); //�ͦ��ˮ`�ƭ�

            CanAtk = false;
        }
    }
}
