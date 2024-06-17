using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;

public class SummonBulletScript : MonoBehaviour
{
    public float existTime = 3f;
    public int bulletDamage = 10; //子彈的基礎傷害
    public int Damage; //子彈的最終傷害(計算後)

    public GameObject FloatingTextPrefeb;

    public float critRate; //子彈的基礎爆率
    public float critDamage; //子彈的基礎爆傷

    EquipmentCal equipmentCal;

    public bool ShootThrough = false;
    public bool IgnoreGrid = false;
    void Start()
    {
        equipmentCal = GameObject.FindWithTag("Player").GetComponent<EquipmentCal>();
        DamageCal(); //啟用傷害計算
        Destroy(gameObject, existTime);
    }

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


            DamageNumberEft.ShowFloatingTextEft(FloatingTextPrefeb, collision, transform, Damage); //生成傷害數值


            if (ShootThrough == false)
            { Destroy(gameObject); }

        }
        if (collision.tag == "Grid")
        {
            if (IgnoreGrid == false)
            {
                Destroy(gameObject);
            }
        }
    }
    public void DamageCal() //傷害計算程式
    {
        float StrengthDamageMultiplying = equipmentCal.StrengthDamageMultiplying;
        float JusticeDamageMultiplying = CardSystem.JusticeDamageMultiplying;
        float SunDamageMultiplying = equipmentCal.SunDamageMultiplying;

        Damage = bulletDamage + equipmentCal.DamagePlus;

        if (!CardSystem.HasCard("戀人"))
        {
            Damage = Convert.ToInt16(Damage * equipmentCal.DamageMultiplying * StrengthDamageMultiplying * JusticeDamageMultiplying * SunDamageMultiplying);
        }
        else
        {
            Damage = Convert.ToInt16((Damage * equipmentCal.DamageMultiplying + equipmentCal.LoverDamageMultiplying) * StrengthDamageMultiplying * JusticeDamageMultiplying * SunDamageMultiplying);
        }

        critRate += equipmentCal.CritRatePlus;
        critRate = critRate * equipmentCal.CritRateMultiplying;

        critDamage += equipmentCal.CritDamagePlus;
        critDamage = critDamage * equipmentCal.CritDamageMultiplying;

        Debug.Log("Damage=" + Damage);
    }
}
