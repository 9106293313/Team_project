using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;

public class SummonBulletScript : MonoBehaviour
{
    public float existTime = 3f;
    public int bulletDamage = 10; //�l�u����¦�ˮ`
    public int Damage; //�l�u���̲׶ˮ`(�p���)

    public GameObject FloatingTextPrefeb;

    public float critRate; //�l�u����¦�z�v
    public float critDamage; //�l�u����¦�z��

    EquipmentCal equipmentCal;

    public bool ShootThrough = false;
    public bool IgnoreGrid = false;
    void Start()
    {
        equipmentCal = GameObject.FindWithTag("Player").GetComponent<EquipmentCal>();
        DamageCal(); //�ҥζˮ`�p��
        Destroy(gameObject, existTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            float A = UnityEngine.Random.Range(0.0f, 1.0f);  // 0.0 ~ 1.0
            if (A <= critRate * 0.01) //Ĳ�o�z��
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


            DamageNumberEft.ShowFloatingTextEft(FloatingTextPrefeb, collision, transform, Damage); //�ͦ��ˮ`�ƭ�


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
    public void DamageCal() //�ˮ`�p��{��
    {
        float StrengthDamageMultiplying = equipmentCal.StrengthDamageMultiplying;
        float JusticeDamageMultiplying = CardSystem.JusticeDamageMultiplying;
        float SunDamageMultiplying = equipmentCal.SunDamageMultiplying;

        Damage = bulletDamage + equipmentCal.DamagePlus;

        if (!CardSystem.HasCard("�ʤH"))
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
