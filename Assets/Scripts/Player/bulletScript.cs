using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UIElements;

public class bulletScript : MonoBehaviour
{
    public float existTime = 3f;
    public int Damage = 10;

    public GameObject HitEft;

    public GameObject FloatingTextPrefeb;

    public float critRate;
    public float critDamage;

    public EquipmentCal equipmentCal;

    public bool IsLongBow=false;
    public int LongBowBulletLevelNum = 0;

    public bool ShootThrough = false;
    public bool IsPoison = false;
    public bool IgnoreGrid = false;
    public bool IsThunder = false;
    public bool WindSplit =false;

    public GameObject smallWindArrow;
    void Start()
    {
        equipmentCal = GameObject.FindWithTag("Player").GetComponent<EquipmentCal>();

        Destroy(gameObject,existTime);

        PlayerItemsEffect();

        if(IsLongBow)
        {
            LongBowBulletLevel(LongBowBulletLevelNum);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Enemy")
        {
            float A = UnityEngine.Random.Range(0.0f, 1.0f);  // 0.0 ~ 1.0
            if (A <= critRate*0.01) //觸發爆擊
            {
                Damage += System.Convert.ToInt16(Damage * critDamage *0.01f); 

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
            
            
            GameObject Eft = Instantiate(HitEft,transform.position, Quaternion.identity);
            Eft.GetComponent<Transform>().localScale = this.transform.localScale;

            DamageNumberEft.ShowFloatingTextEft(FloatingTextPrefeb,collision,transform,Damage); //生成傷害數值
            
            
            if(IsPoison && GetComponent<CreatePoisonCloud>()!=null)
            {
                this.GetComponent<CreatePoisonCloud>().CreatePoisonCloundObj();
            }

            if (IsThunder == true)
            {
                GetComponent<Collider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                Eft.GetComponent<spawnThunderball>().Damage = Damage;
                Eft.GetComponent<spawnThunderball>().critDamage = critDamage;
                Eft.GetComponent<spawnThunderball>().critRate = critRate;
            }

            if(WindSplit == true && CardSystem.HasCard("皇帝"))
            {
                for (int i = 0; i < 3; i++)
                {
                    GameObject SmallWindArrow = Instantiate(smallWindArrow, GameObject.FindWithTag("WeaponChoose").transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), 1f), Quaternion.Euler(0f, 0f, GameObject.FindWithTag("WeaponChoose").transform.rotation.eulerAngles.z + 90f));
                    SmallWindArrow.GetComponent<bulletScript>().Damage = Convert.ToInt16(Damage * 0.2f);
                    SmallWindArrow.GetComponent<bulletScript>().critDamage = critDamage;
                    SmallWindArrow.GetComponent<bulletScript>().critRate = critRate;
                }
                
            }

            if(CardSystem.HasCard("星星"))
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
                    Instantiate(TowerExplodePrefab,transform.position,Quaternion.identity);
                }
            }

            if (ShootThrough==false)
            { Destroy(gameObject); }
            
            
        }
        if (collision.tag == "Grid")
        {
            if (IsPoison && GetComponent<CreatePoisonCloud>() != null)
            {
                this.GetComponent<CreatePoisonCloud>().CreatePoisonCloundObj();
            }

            if (IgnoreGrid==false)
            {
                GameObject Eft = Instantiate(HitEft, transform.position, transform.rotation);
                Eft.GetComponent<Transform>().localScale = this.transform.localScale;
                Eft.GetComponent<Light2D>().pointLightOuterRadius = 2 * this.transform.localScale.x;

                if (IsThunder == true)
                {
                    GetComponent<Collider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                    Eft.GetComponent<spawnThunderball>().Damage = Damage;
                    Eft.GetComponent<spawnThunderball>().critDamage = critDamage;
                    Eft.GetComponent<spawnThunderball>().critRate = critRate;
                }

                Destroy(gameObject);
            }


        }
    }

    void PlayerItemsEffect()
    {
        equipmentCal.DamageCal(Damage,critRate,critDamage) ; //執行傷害計算 (Damage,CritRate,CritDamage)
        Damage = equipmentCal.FinalDamage; //套用計算出的傷害
        critRate = equipmentCal.FinalCritRate;
        critDamage = equipmentCal.FinalCritDamage;
    }
    
    public void LongBowBulletLevel(int Level)
    {
        Damage += Convert.ToInt16(Damage * Level * 1.4f);
    }

}
