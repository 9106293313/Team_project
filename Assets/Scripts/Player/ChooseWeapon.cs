using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseWeapon : MonoBehaviour
{
    public GameObject currentWeapon;
    public GameObject longBow;
    public GameObject shortBow;
    public GameObject crossBow;
    public int weaponNum = 1;
    public Image longBowImage;
    public Image ShortBowImage;
    public Image CrossBowImage;
    void Update()
    {
        if(weaponNum == 1)
        {
            UseLongbow();
        }
        if (weaponNum == 2)
        {
            UseShortbow();
        }
        if (weaponNum == 3)
        {
            UseCrossbow();
        }

    }


    public void Attack()
    {
        currentWeapon.GetComponent<Animator>().SetTrigger("Shoot");
    }
    public void BulletLevel(int level)
    {
        currentWeapon.GetComponent<WeaponShoot>().bulletLevel = level;
    }

    public void UseLongbow()
    {
        currentWeapon = longBow;
        longBow.SetActive(true);
        shortBow.SetActive(false);
        crossBow.SetActive(false);
        weaponNum = 1;
        longBowImage.enabled = true;
        ShortBowImage.enabled = false;
        CrossBowImage.enabled = false;
    }
    public void UseShortbow()
    {
        currentWeapon = shortBow;
        longBow.SetActive(false);
        shortBow.SetActive(true);
        crossBow.SetActive(false);
        weaponNum = 2;
        longBowImage.enabled = false;
        ShortBowImage.enabled = true;
        CrossBowImage.enabled = false;
    }
    public void UseCrossbow()
    {
        currentWeapon = crossBow;
        longBow.SetActive(false);
        shortBow.SetActive(false);
        crossBow.SetActive(true);
        weaponNum = 3;
        longBowImage.enabled = false;
        ShortBowImage.enabled = false;
        CrossBowImage.enabled = true;
    }
}
