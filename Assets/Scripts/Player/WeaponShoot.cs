using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShoot : MonoBehaviour
{
    public ChooseWeapon chooseWeapon;

    public GameObject bullet;
    public GameObject ArrowPivot;

    public int weaponBaseDamage = 10; //武器基礎傷害
    public float bulletSpeed = 10f; //子彈速度
    int extraDamage = 0; //額外傷害=基礎傷害*子彈等級*1.4之後取整數
    public float critRate = 10f;
    public float critDamage = 10f;
    public float AtkCooldown = 0.3f;
    public float defaultAtkCooldown;
    float ChageAtkCooldown =0.05f;

    public PlayerInfo PlayerInfo;
    int energyRequire = 1;

    public int bulletLevel = 0;

    float bulletSize = 1;

    float bulletAngleCal = 1;

    int crossbowShootNum = 0;

    float LongBowBaseMultiply = 1f; //長弓的基礎傷害乘量為1
    float ShortBowBaseMultiply = 0.5f; //短弓的基礎傷害乘量為0.5
    float CrossBowBaseMultiply = 0.5f; //弩的基礎傷害乘量為0.5

    float FireArrowSpeedMultiply = 1.5f; //火焰箭矢的速度為1.5倍
    float FireArrowDamageMultiply = 1.5f; //火焰箭矢的傷害為1.5倍
    float PoisonArrowSpeedMultiply = 0.5f; //毒箭矢的速度為0.5倍
    float PoisonArrowDamageMultiply = 0.75f; //毒箭矢的傷害為0.75倍
    float ThunderArrowSpeedMultiply = 2f; //雷電箭矢的速度為2倍
    float ThunderArrowDamageMultiply = 0.8f; //雷電箭矢的傷害為0.8倍
    float IceArrowDamageMultiply = 0.8f; //冰箭矢的傷害為0.8倍
    float WindArrowDamageMultiply = 1f; //風箭矢的傷害為1倍

    public ElementsType.Elements elements; //武器的元素類型

    public GameObject BulletTypeFire, BulletTypePoison, BulletTypeThunder, BulletTypeIce, BulletTypeWind;

    int strengthCount = 0; //力量卡的攻擊計數
    public GameObject UIstrengthCount;
    public Image strengthCountImage1, strengthCountImage2, strengthCountImage3;

    void Start()
    {
        defaultAtkCooldown = AtkCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (elements== ElementsType.Elements.Fire)
        { bullet = BulletTypeFire; }
        if (elements == ElementsType.Elements.Poison)
        { bullet = BulletTypePoison; }
        if (elements == ElementsType.Elements.Thunder)
        { bullet = BulletTypeThunder; }
        if (elements == ElementsType.Elements.Ice)
        { bullet = BulletTypeIce; }
        if (elements == ElementsType.Elements.wind)
        { bullet = BulletTypeWind; }

        

        if (CardSystem.HasCard("皇帝"))//如果有皇帝卡，火焰箭矢傷害增加
        {
            FireArrowDamageMultiply = 2.25f;
        }
        if(CardSystem.HasCard("力量"))
        {
            UIstrengthCount.SetActive(true);
            if(strengthCount>=1)
            {
                strengthCountImage1.enabled = true;
            }
            else
            {
                strengthCountImage1.enabled = false;
            }
            if (strengthCount >= 2)
            {
                strengthCountImage2.enabled = true;
            }
            else
            {
                strengthCountImage2.enabled = false;
            }
            if (strengthCount >= 3)
            {
                strengthCountImage3.enabled = true;
                GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().StrengthDamageMultiplying = 2f;
            }
            else
            {
                strengthCountImage3.enabled = false;
                GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().StrengthDamageMultiplying = 1f;
            }
        }
        else
        {
            UIstrengthCount.SetActive(false);
        }

    }
    public void Shoot()
    {
        GameObject[] iceSummonObjects = GameObject.FindGameObjectsWithTag("IceSummon");//冰召喚物的控制
        foreach (GameObject iceSummonObject in iceSummonObjects)
        {
            IceSummon iceSummonComponent = iceSummonObject.GetComponent<IceSummon>();
            if (iceSummonComponent != null)
            {
                iceSummonComponent.Atk();
            }
        }

        if (bulletLevel == 0)
        {
            extraDamage = 0;
            energyRequire = 1;
            bulletSize = 1;

            bulletAngleCal = 1;

            AtkCooldown = defaultAtkCooldown;

        }
        else if (bulletLevel == 1)
        {
            extraDamage = Convert.ToInt16(weaponBaseDamage * bulletLevel * 1.4f); //四捨五入
            energyRequire = 1;
            bulletSize = 1.3f;
            //
            bulletAngleCal = 0.7f;
        }
        else if (bulletLevel == 2)
        {
            extraDamage = Convert.ToInt16(weaponBaseDamage * bulletLevel * 1.4f); //四捨五入
            energyRequire = 2;
            bulletSize = 1.6f;
            //
            bulletAngleCal = 0.4f;
        }
        else if (bulletLevel == 3)
        {
            extraDamage = Convert.ToInt16(weaponBaseDamage * bulletLevel * 1.4f); //四捨五入
            energyRequire = 3;
            bulletSize = 1.9f;
            //
            bulletAngleCal = 0.1f;
        }
        else if (bulletLevel == 4)
        {
            extraDamage = Convert.ToInt16(weaponBaseDamage * bulletLevel * 1.4f); //四捨五入
            energyRequire = 4;
            bulletSize = 2.2f;
        }
        else if (bulletLevel == 5)
        {
            extraDamage = Convert.ToInt16(weaponBaseDamage * bulletLevel * 1.4f); //四捨五入
            energyRequire = 5;
            bulletSize = 2.6f;
        }

        if (CardSystem.HasCard("力量"))
        {
            if (strengthCount>=3)
            {
                //GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().StrengthDamageMultiplying = 2f;
                this.GetComponent<AudioSource>().pitch = 1.5f; //調整攻擊音效的pitch，使其聽起來不同
                strengthCount = 0;
            }
            else
            {
                //GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().StrengthDamageMultiplying = 1f;
                this.GetComponent<AudioSource>().pitch = 1f;
                strengthCount++;
            }
            Debug.Log(strengthCount);
        }

        if(CardSystem.HasCard("戰車"))
        {
            StartCoroutine(StartChariotShoot());
        }

        if(chooseWeapon.weaponNum==1)
        {

            PlayerInfo.Attacking(); //讓攻擊時不會回復能量
            if (!CardSystem.HasCard("節制"))
            {
                PlayerInfo.curEnergy -= energyRequire;
            }
            else
            {
                float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                if (randomNum <= CardSystem.TemperanceTriggerProbability) //30%觸發機率
                {
                    //不消耗能量
                }
                else
                {
                    PlayerInfo.curEnergy -= energyRequire;
                }
            }


            GameObject bulletObj = Instantiate(bullet, transform.position, transform.rotation);

            if(bullet == BulletTypeWind)
            {
                bulletObj.GetComponent<HomingAtk>().speed = bulletSpeed;

                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * LongBowBaseMultiply * WindArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().IsLongBow = true;
                bulletObj.GetComponent<bulletScript>().LongBowBulletLevelNum = bulletLevel;
                bulletObj.GetComponent<Transform>().localScale = new Vector3(bulletSize, bulletSize, 1);

                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;

            }
            if(bullet == BulletTypeFire)
            {
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * FireArrowSpeedMultiply, ForceMode2D.Impulse); //火焰箭矢速度為1.5倍

                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * LongBowBaseMultiply * FireArrowDamageMultiply); 
                bulletObj.GetComponent<bulletScript>().IsLongBow = true;
                bulletObj.GetComponent<bulletScript>().LongBowBulletLevelNum = bulletLevel;
                bulletObj.GetComponent<Transform>().localScale = new Vector3(bulletSize, bulletSize, 1);

                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
            }
            if(bullet == BulletTypePoison)
            {
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * PoisonArrowSpeedMultiply, ForceMode2D.Impulse);

                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * LongBowBaseMultiply * PoisonArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().IsLongBow = true;
                bulletObj.GetComponent<bulletScript>().LongBowBulletLevelNum = bulletLevel;
                bulletObj.GetComponent<Transform>().localScale = new Vector3(bulletSize, bulletSize, 1);

                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
            }
            if (bullet == BulletTypeThunder)
            {
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * ThunderArrowSpeedMultiply, ForceMode2D.Impulse);

                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * LongBowBaseMultiply * ThunderArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().IsLongBow = true;
                bulletObj.GetComponent<bulletScript>().LongBowBulletLevelNum = bulletLevel;
                bulletObj.GetComponent<Transform>().localScale = new Vector3(bulletSize, bulletSize, 1);

                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
            }
            if (bullet == BulletTypeIce)
            {
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed, ForceMode2D.Impulse);

                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * LongBowBaseMultiply * IceArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().IsLongBow = true;
                bulletObj.GetComponent<bulletScript>().LongBowBulletLevelNum = bulletLevel;
                bulletObj.GetComponent<Transform>().localScale = new Vector3(bulletSize, bulletSize, 1);

                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
            }


        }
        if(chooseWeapon.weaponNum==2)
        {
            PlayerInfo.Attacking();

            if (!CardSystem.HasCard("節制"))
            {
                PlayerInfo.curEnergy -= energyRequire;
            }
            else
            {
                float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                if (randomNum <= CardSystem.TemperanceTriggerProbability) //30%觸發機率
                {
                    //不消耗能量
                }
                else
                {
                    PlayerInfo.curEnergy -= energyRequire;
                }
            }
            GameObject bulletObj = Instantiate(bullet, transform.position, transform.rotation);
            GameObject bulletObj2 = Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 25f * bulletAngleCal));
            GameObject bulletObj3 = Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, -25f * bulletAngleCal));

            if (bullet == BulletTypeWind)
            {
                bulletObj.GetComponent<HomingAtk>().speed = bulletSpeed;
                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * WindArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj2.GetComponent<HomingAtk>().speed = bulletSpeed;
                bulletObj2.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * WindArrowDamageMultiply);
                bulletObj2.GetComponent<bulletScript>().critRate = critRate;
                bulletObj2.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj3.GetComponent<HomingAtk>().speed = bulletSpeed;
                bulletObj3.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * WindArrowDamageMultiply);
                bulletObj3.GetComponent<bulletScript>().critRate = critRate;
                bulletObj3.GetComponent<bulletScript>().critDamage = critDamage;
            }
            if (bullet == BulletTypeFire)
            {
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * FireArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * FireArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj2.GetComponent<Rigidbody2D>().AddForce(bulletObj2.transform.right * bulletSpeed * FireArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj2.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * FireArrowDamageMultiply);
                bulletObj2.GetComponent<bulletScript>().critRate = critRate;
                bulletObj2.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj3.GetComponent<Rigidbody2D>().AddForce(bulletObj3.transform.right * bulletSpeed * FireArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj3.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * FireArrowDamageMultiply);
                bulletObj3.GetComponent<bulletScript>().critRate = critRate;
                bulletObj3.GetComponent<bulletScript>().critDamage = critDamage;
            }
            if (bullet == BulletTypePoison)
            {
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * PoisonArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * PoisonArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj2.GetComponent<Rigidbody2D>().AddForce(bulletObj2.transform.right * bulletSpeed * PoisonArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj2.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * PoisonArrowDamageMultiply);
                bulletObj2.GetComponent<bulletScript>().critRate = critRate;
                bulletObj2.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj3.GetComponent<Rigidbody2D>().AddForce(bulletObj3.transform.right * bulletSpeed * PoisonArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj3.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * PoisonArrowDamageMultiply);
                bulletObj3.GetComponent<bulletScript>().critRate = critRate;
                bulletObj3.GetComponent<bulletScript>().critDamage = critDamage;
            }
            if (bullet == BulletTypeThunder)
            {
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * ThunderArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * ThunderArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj2.GetComponent<Rigidbody2D>().AddForce(bulletObj2.transform.right * bulletSpeed * ThunderArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj2.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * ThunderArrowDamageMultiply);
                bulletObj2.GetComponent<bulletScript>().critRate = critRate;
                bulletObj2.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj3.GetComponent<Rigidbody2D>().AddForce(bulletObj3.transform.right * bulletSpeed * ThunderArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj3.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * ThunderArrowDamageMultiply);
                bulletObj3.GetComponent<bulletScript>().critRate = critRate;
                bulletObj3.GetComponent<bulletScript>().critDamage = critDamage;
            }
            if (bullet == BulletTypeIce)
            {
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed, ForceMode2D.Impulse);
                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * IceArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj2.GetComponent<Rigidbody2D>().AddForce(bulletObj2.transform.right * bulletSpeed, ForceMode2D.Impulse);
                bulletObj2.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * IceArrowDamageMultiply);
                bulletObj2.GetComponent<bulletScript>().critRate = critRate;
                bulletObj2.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj3.GetComponent<Rigidbody2D>().AddForce(bulletObj3.transform.right * bulletSpeed, ForceMode2D.Impulse);
                bulletObj3.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * IceArrowDamageMultiply);
                bulletObj3.GetComponent<bulletScript>().critRate = critRate;
                bulletObj3.GetComponent<bulletScript>().critDamage = critDamage;
            }
        }
        if (chooseWeapon.weaponNum == 3)
        {
            PlayerInfo.Attacking();

            
            if (bulletLevel == 31)
            {
                if(AtkCooldown > 0.1f)
                {
                    AtkCooldown -= ChageAtkCooldown;
                }
                if(AtkCooldown<0.1f)
                {
                    AtkCooldown = 0.1f;
                }
                GameObject bulletObj = Instantiate(bullet, transform.position, transform.rotation);
               
                if (bullet == BulletTypeWind)
                {
                    bulletObj.GetComponent<HomingAtk>().speed = bulletSpeed;
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * WindArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
                if(bullet == BulletTypeFire)
                {
                    bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * FireArrowSpeedMultiply, ForceMode2D.Impulse);
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * FireArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
                if (bullet == BulletTypePoison)
                {
                    bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * PoisonArrowSpeedMultiply, ForceMode2D.Impulse);
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * PoisonArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
                if (bullet == BulletTypeThunder)
                {
                    bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * ThunderArrowSpeedMultiply, ForceMode2D.Impulse);
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * ThunderArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
                if (bullet == BulletTypeIce)
                {
                    bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed, ForceMode2D.Impulse);
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * IceArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }

                crossbowShootNum++;
                if(crossbowShootNum>=3)
                {
                    crossbowShootNum = 0;
                    
                    if (!CardSystem.HasCard("節制"))
                    {
                        PlayerInfo.curEnergy -= 1;
                    }
                    else
                    {
                        float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                        if (randomNum <= CardSystem.TemperanceTriggerProbability) //30%觸發機率
                        {
                            //不消耗能量
                        }
                        else
                        {
                            PlayerInfo.curEnergy -= 1;
                        }
                    }
                }

            }
            else
            {
                if (!CardSystem.HasCard("節制"))
                {
                    PlayerInfo.curEnergy -= 1;
                }
                else
                {
                    float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                    if (randomNum <= CardSystem.TemperanceTriggerProbability) //30%觸發機率
                    {
                        //不消耗能量
                    }
                    else
                    {
                        PlayerInfo.curEnergy -= 1;
                    }
                }
                GameObject bulletObj = Instantiate(bullet, transform.position, transform.rotation);

                if (bullet == BulletTypeWind)
                {
                    bulletObj.GetComponent<HomingAtk>().speed = bulletSpeed;
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * WindArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
                if(bullet == BulletTypeFire)
                {
                    bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * FireArrowSpeedMultiply, ForceMode2D.Impulse);
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * FireArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
                if (bullet == BulletTypePoison)
                {
                    bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * PoisonArrowSpeedMultiply, ForceMode2D.Impulse);
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * PoisonArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
                if (bullet == BulletTypeThunder)
                {
                    bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * ThunderArrowSpeedMultiply, ForceMode2D.Impulse);
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * ThunderArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
                if (bullet == BulletTypeIce)
                {
                    bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed, ForceMode2D.Impulse);
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * IceArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
            }


        }


    }


    IEnumerator StartChariotShoot()
    {
        yield return new WaitForSeconds(0.1f);
        ChariotShoot();
    }
    public void ChariotShoot()
    {

        if (chooseWeapon.weaponNum == 1)
        {
            GameObject bulletObj = Instantiate(bullet, transform.position, transform.rotation);

            if (bullet == BulletTypeWind)
            {
                bulletObj.GetComponent<HomingAtk>().speed = bulletSpeed;

                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * LongBowBaseMultiply * WindArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().IsLongBow = true;
                bulletObj.GetComponent<bulletScript>().LongBowBulletLevelNum = bulletLevel;
                bulletObj.GetComponent<Transform>().localScale = new Vector3(bulletSize, bulletSize, 1);

                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;

            }
            if (bullet == BulletTypeFire)
            {
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * FireArrowSpeedMultiply, ForceMode2D.Impulse); //火焰箭矢速度為1.5倍

                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * LongBowBaseMultiply * FireArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().IsLongBow = true;
                bulletObj.GetComponent<bulletScript>().LongBowBulletLevelNum = bulletLevel;
                bulletObj.GetComponent<Transform>().localScale = new Vector3(bulletSize, bulletSize, 1);

                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
            }
            if (bullet == BulletTypePoison)
            {
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * PoisonArrowSpeedMultiply, ForceMode2D.Impulse);

                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * LongBowBaseMultiply * PoisonArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().IsLongBow = true;
                bulletObj.GetComponent<bulletScript>().LongBowBulletLevelNum = bulletLevel;
                bulletObj.GetComponent<Transform>().localScale = new Vector3(bulletSize, bulletSize, 1);

                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
            }
            if (bullet == BulletTypeThunder)
            {
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * ThunderArrowSpeedMultiply, ForceMode2D.Impulse);

                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * LongBowBaseMultiply * ThunderArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().IsLongBow = true;
                bulletObj.GetComponent<bulletScript>().LongBowBulletLevelNum = bulletLevel;
                bulletObj.GetComponent<Transform>().localScale = new Vector3(bulletSize, bulletSize, 1);

                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
            }
            if (bullet == BulletTypeIce)
            {
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed, ForceMode2D.Impulse);

                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * LongBowBaseMultiply * IceArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().IsLongBow = true;
                bulletObj.GetComponent<bulletScript>().LongBowBulletLevelNum = bulletLevel;
                bulletObj.GetComponent<Transform>().localScale = new Vector3(bulletSize, bulletSize, 1);

                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
            }


        }
        if (chooseWeapon.weaponNum == 2)
        {
            GameObject bulletObj = Instantiate(bullet, transform.position, transform.rotation);
            GameObject bulletObj2 = Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 25f * bulletAngleCal));
            GameObject bulletObj3 = Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, -25f * bulletAngleCal));

            if (bullet == BulletTypeWind)
            {
                bulletObj.GetComponent<HomingAtk>().speed = bulletSpeed;
                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * WindArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj2.GetComponent<HomingAtk>().speed = bulletSpeed;
                bulletObj2.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * WindArrowDamageMultiply);
                bulletObj2.GetComponent<bulletScript>().critRate = critRate;
                bulletObj2.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj3.GetComponent<HomingAtk>().speed = bulletSpeed;
                bulletObj3.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * WindArrowDamageMultiply);
                bulletObj3.GetComponent<bulletScript>().critRate = critRate;
                bulletObj3.GetComponent<bulletScript>().critDamage = critDamage;
            }
            if (bullet == BulletTypeFire)
            {
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * FireArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * FireArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj2.GetComponent<Rigidbody2D>().AddForce(bulletObj2.transform.right * bulletSpeed * FireArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj2.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * FireArrowDamageMultiply);
                bulletObj2.GetComponent<bulletScript>().critRate = critRate;
                bulletObj2.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj3.GetComponent<Rigidbody2D>().AddForce(bulletObj3.transform.right * bulletSpeed * FireArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj3.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * FireArrowDamageMultiply);
                bulletObj3.GetComponent<bulletScript>().critRate = critRate;
                bulletObj3.GetComponent<bulletScript>().critDamage = critDamage;
            }
            if (bullet == BulletTypePoison)
            {
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * PoisonArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * PoisonArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj2.GetComponent<Rigidbody2D>().AddForce(bulletObj2.transform.right * bulletSpeed * PoisonArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj2.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * PoisonArrowDamageMultiply);
                bulletObj2.GetComponent<bulletScript>().critRate = critRate;
                bulletObj2.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj3.GetComponent<Rigidbody2D>().AddForce(bulletObj3.transform.right * bulletSpeed * PoisonArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj3.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * PoisonArrowDamageMultiply);
                bulletObj3.GetComponent<bulletScript>().critRate = critRate;
                bulletObj3.GetComponent<bulletScript>().critDamage = critDamage;
            }
            if (bullet == BulletTypeThunder)
            {
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * ThunderArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * ThunderArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj2.GetComponent<Rigidbody2D>().AddForce(bulletObj2.transform.right * bulletSpeed * ThunderArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj2.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * ThunderArrowDamageMultiply);
                bulletObj2.GetComponent<bulletScript>().critRate = critRate;
                bulletObj2.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj3.GetComponent<Rigidbody2D>().AddForce(bulletObj3.transform.right * bulletSpeed * ThunderArrowSpeedMultiply, ForceMode2D.Impulse);
                bulletObj3.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * ThunderArrowDamageMultiply);
                bulletObj3.GetComponent<bulletScript>().critRate = critRate;
                bulletObj3.GetComponent<bulletScript>().critDamage = critDamage;
            }
            if (bullet == BulletTypeIce)
            {
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed, ForceMode2D.Impulse);
                bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * IceArrowDamageMultiply);
                bulletObj.GetComponent<bulletScript>().critRate = critRate;
                bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj2.GetComponent<Rigidbody2D>().AddForce(bulletObj2.transform.right * bulletSpeed, ForceMode2D.Impulse);
                bulletObj2.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * IceArrowDamageMultiply);
                bulletObj2.GetComponent<bulletScript>().critRate = critRate;
                bulletObj2.GetComponent<bulletScript>().critDamage = critDamage;
                bulletObj3.GetComponent<Rigidbody2D>().AddForce(bulletObj3.transform.right * bulletSpeed, ForceMode2D.Impulse);
                bulletObj3.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * ShortBowBaseMultiply * IceArrowDamageMultiply);
                bulletObj3.GetComponent<bulletScript>().critRate = critRate;
                bulletObj3.GetComponent<bulletScript>().critDamage = critDamage;
            }
        }
        if (chooseWeapon.weaponNum == 3)
        {
            if (bulletLevel == 31)
            {
                if (AtkCooldown > 0.1f)
                {
                    AtkCooldown -= ChageAtkCooldown;
                }
                if (AtkCooldown < 0.1f)
                {
                    AtkCooldown = 0.1f;
                }
                GameObject bulletObj = Instantiate(bullet, transform.position, transform.rotation);

                if (bullet == BulletTypeWind)
                {
                    bulletObj.GetComponent<HomingAtk>().speed = bulletSpeed;
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * WindArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
                if (bullet == BulletTypeFire)
                {
                    bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * FireArrowSpeedMultiply, ForceMode2D.Impulse);
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * FireArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
                if (bullet == BulletTypePoison)
                {
                    bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * PoisonArrowSpeedMultiply, ForceMode2D.Impulse);
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * PoisonArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
                if (bullet == BulletTypeThunder)
                {
                    bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * ThunderArrowSpeedMultiply, ForceMode2D.Impulse);
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * ThunderArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
                if (bullet == BulletTypeIce)
                {
                    bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed, ForceMode2D.Impulse);
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * IceArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }

            }
            else
            {
                GameObject bulletObj = Instantiate(bullet, transform.position, transform.rotation);

                if (bullet == BulletTypeWind)
                {
                    bulletObj.GetComponent<HomingAtk>().speed = bulletSpeed;
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * WindArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
                if (bullet == BulletTypeFire)
                {
                    bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * FireArrowSpeedMultiply, ForceMode2D.Impulse);
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * FireArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
                if (bullet == BulletTypePoison)
                {
                    bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * PoisonArrowSpeedMultiply, ForceMode2D.Impulse);
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * PoisonArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
                if (bullet == BulletTypeThunder)
                {
                    bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * ThunderArrowSpeedMultiply, ForceMode2D.Impulse);
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * ThunderArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
                if (bullet == BulletTypeIce)
                {
                    bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed, ForceMode2D.Impulse);
                    bulletObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(weaponBaseDamage * CrossBowBaseMultiply * IceArrowDamageMultiply);
                    bulletObj.GetComponent<bulletScript>().critRate = critRate;
                    bulletObj.GetComponent<bulletScript>().critDamage = critDamage;
                }
            }


        }


    }
}
