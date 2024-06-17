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

    public int weaponBaseDamage = 10; //�Z����¦�ˮ`
    public float bulletSpeed = 10f; //�l�u�t��
    int extraDamage = 0; //�B�~�ˮ`=��¦�ˮ`*�l�u����*1.4��������
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

    float LongBowBaseMultiply = 1f; //���}����¦�ˮ`���q��1
    float ShortBowBaseMultiply = 0.5f; //�u�}����¦�ˮ`���q��0.5
    float CrossBowBaseMultiply = 0.5f; //������¦�ˮ`���q��0.5

    float FireArrowSpeedMultiply = 1.5f; //���K�b�ڪ��t�׬�1.5��
    float FireArrowDamageMultiply = 1.5f; //���K�b�ڪ��ˮ`��1.5��
    float PoisonArrowSpeedMultiply = 0.5f; //�r�b�ڪ��t�׬�0.5��
    float PoisonArrowDamageMultiply = 0.75f; //�r�b�ڪ��ˮ`��0.75��
    float ThunderArrowSpeedMultiply = 2f; //�p�q�b�ڪ��t�׬�2��
    float ThunderArrowDamageMultiply = 0.8f; //�p�q�b�ڪ��ˮ`��0.8��
    float IceArrowDamageMultiply = 0.8f; //�B�b�ڪ��ˮ`��0.8��
    float WindArrowDamageMultiply = 1f; //���b�ڪ��ˮ`��1��

    public ElementsType.Elements elements; //�Z������������

    public GameObject BulletTypeFire, BulletTypePoison, BulletTypeThunder, BulletTypeIce, BulletTypeWind;

    int strengthCount = 0; //�O�q�d�������p��
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

        

        if (CardSystem.HasCard("�ӫ�"))//�p�G���ӫҥd�A���K�b�ڶˮ`�W�[
        {
            FireArrowDamageMultiply = 2.25f;
        }
        if(CardSystem.HasCard("�O�q"))
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
        GameObject[] iceSummonObjects = GameObject.FindGameObjectsWithTag("IceSummon");//�B�l�ꪫ������
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
            extraDamage = Convert.ToInt16(weaponBaseDamage * bulletLevel * 1.4f); //�|�ˤ��J
            energyRequire = 1;
            bulletSize = 1.3f;
            //
            bulletAngleCal = 0.7f;
        }
        else if (bulletLevel == 2)
        {
            extraDamage = Convert.ToInt16(weaponBaseDamage * bulletLevel * 1.4f); //�|�ˤ��J
            energyRequire = 2;
            bulletSize = 1.6f;
            //
            bulletAngleCal = 0.4f;
        }
        else if (bulletLevel == 3)
        {
            extraDamage = Convert.ToInt16(weaponBaseDamage * bulletLevel * 1.4f); //�|�ˤ��J
            energyRequire = 3;
            bulletSize = 1.9f;
            //
            bulletAngleCal = 0.1f;
        }
        else if (bulletLevel == 4)
        {
            extraDamage = Convert.ToInt16(weaponBaseDamage * bulletLevel * 1.4f); //�|�ˤ��J
            energyRequire = 4;
            bulletSize = 2.2f;
        }
        else if (bulletLevel == 5)
        {
            extraDamage = Convert.ToInt16(weaponBaseDamage * bulletLevel * 1.4f); //�|�ˤ��J
            energyRequire = 5;
            bulletSize = 2.6f;
        }

        if (CardSystem.HasCard("�O�q"))
        {
            if (strengthCount>=3)
            {
                //GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().StrengthDamageMultiplying = 2f;
                this.GetComponent<AudioSource>().pitch = 1.5f; //�վ�������Ī�pitch�A�Ϩ�ť�_�Ӥ��P
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

        if(CardSystem.HasCard("�Ԩ�"))
        {
            StartCoroutine(StartChariotShoot());
        }

        if(chooseWeapon.weaponNum==1)
        {

            PlayerInfo.Attacking(); //�������ɤ��|�^�_��q
            if (!CardSystem.HasCard("�`��"))
            {
                PlayerInfo.curEnergy -= energyRequire;
            }
            else
            {
                float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                if (randomNum <= CardSystem.TemperanceTriggerProbability) //30%Ĳ�o���v
                {
                    //�����ӯ�q
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
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * FireArrowSpeedMultiply, ForceMode2D.Impulse); //���K�b�ڳt�׬�1.5��

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

            if (!CardSystem.HasCard("�`��"))
            {
                PlayerInfo.curEnergy -= energyRequire;
            }
            else
            {
                float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                if (randomNum <= CardSystem.TemperanceTriggerProbability) //30%Ĳ�o���v
                {
                    //�����ӯ�q
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
                    
                    if (!CardSystem.HasCard("�`��"))
                    {
                        PlayerInfo.curEnergy -= 1;
                    }
                    else
                    {
                        float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                        if (randomNum <= CardSystem.TemperanceTriggerProbability) //30%Ĳ�o���v
                        {
                            //�����ӯ�q
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
                if (!CardSystem.HasCard("�`��"))
                {
                    PlayerInfo.curEnergy -= 1;
                }
                else
                {
                    float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                    if (randomNum <= CardSystem.TemperanceTriggerProbability) //30%Ĳ�o���v
                    {
                        //�����ӯ�q
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
                bulletObj.GetComponent<Rigidbody2D>().AddForce(ArrowPivot.transform.up * bulletSpeed * FireArrowSpeedMultiply, ForceMode2D.Impulse); //���K�b�ڳt�׬�1.5��

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
