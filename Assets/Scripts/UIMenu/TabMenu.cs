using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabMenu : MonoBehaviour
{
    public EquipmentCal equipmentCal;
    public ChooseWeapon WeaponChoose;

    public GameObject CardInfoPanel;
    public GameObject CardPanel1;
    public GameObject CardPanel2;
    public GameObject CardPanel3;
    [HideInInspector]public int currentPanelNumber = 1;

    public TextMeshProUGUI DamageMultiplying;
    public TextMeshProUGUI DamagePlus;
    public TextMeshProUGUI CritRate;
    public TextMeshProUGUI CritDamage;
    public TextMeshProUGUI atkcooldownMinus;
    public TextMeshProUGUI EnergyNumberPlus;
    public TextMeshProUGUI MinusEnergyRechargeTime;
    public TextMeshProUGUI ExtraPlayerLifePoint;
    public TextMeshProUGUI MinusDashCoolDown;

    public GameObject ThunderSummonImage, IceSummonImage, FireSummonImage, PoisonSummonImage;
    void Update()
    {
        float StrengthDamageMultiplying = equipmentCal.StrengthDamageMultiplying;
        float JusticeDamageMultiplying = CardSystem.JusticeDamageMultiplying;
        float SunDamageMultiplying = equipmentCal.SunDamageMultiplying;

        if (!CardSystem.HasCard("≈ §H"))
        {
            DamageMultiplying.text = Convert.ToInt16(equipmentCal.DamageMultiplying * StrengthDamageMultiplying * JusticeDamageMultiplying * SunDamageMultiplying * 100f).ToString() + "%";
        }
        else
        {
            DamageMultiplying.text = Convert.ToInt16((equipmentCal.DamageMultiplying + equipmentCal.LoverDamageMultiplying) * StrengthDamageMultiplying * JusticeDamageMultiplying * SunDamageMultiplying * 100f).ToString() + "%";
        }
        
        DamagePlus.text = (equipmentCal.DamagePlus + WeaponChoose.GetComponentInChildren<WeaponShoot>().weaponBaseDamage) .ToString();
        CritRate.text = ((WeaponChoose.GetComponentInChildren<WeaponShoot>().critRate + equipmentCal.CritRatePlus) * equipmentCal.CritRateMultiplying).ToString() + "%";
        CritDamage.text = ((WeaponChoose.GetComponentInChildren<WeaponShoot>().critDamage + equipmentCal.CritDamagePlus) * equipmentCal.CritDamageMultiplying).ToString() + "%";
        atkcooldownMinus.text = equipmentCal.atkcooldownMinus.ToString();
        EnergyNumberPlus.text = equipmentCal.EnergyNumberPlus.ToString();
        MinusEnergyRechargeTime.text = equipmentCal.MinusEnergyRechargeTime.ToString();
        ExtraPlayerLifePoint.text = equipmentCal.ExtraPlayerLifePoint.ToString();
        MinusDashCoolDown.text = equipmentCal.MinusDashCoolDown.ToString();

        if(currentPanelNumber>=4)
        {
            currentPanelNumber = 1;
        }
        if(currentPanelNumber<=0)
        {
            currentPanelNumber = 3;
        }
        if(currentPanelNumber == 1)
        {
            CardPanel1.SetActive(true);
            CardPanel2.SetActive(false);
            CardPanel3.SetActive(false);
        }
        if(currentPanelNumber == 2)
        {
            CardPanel1.SetActive(false);
            CardPanel2.SetActive(true);
            CardPanel3.SetActive(false);
        }
        if (currentPanelNumber == 3)
        {
            CardPanel1.SetActive(false);
            CardPanel2.SetActive(false);
            CardPanel3.SetActive(true);
        }

        if(GameObject.FindWithTag("Player").GetComponent<PlayerInfo>().HasThunderSummon)
        {
            ThunderSummonImage.SetActive(true);
        }
        else
        {
            ThunderSummonImage.SetActive(false);
        }
        if (GameObject.FindWithTag("Player").GetComponent<PlayerInfo>().HasIceSummon)
        {
            IceSummonImage.SetActive(true);
        }
        else
        {
            IceSummonImage.SetActive(false);
        }
        if (GameObject.FindWithTag("Player").GetComponent<PlayerInfo>().HasFireSummon)
        {
            FireSummonImage.SetActive(true);
        }
        else
        {
            FireSummonImage.SetActive(false);
        }
        if (GameObject.FindWithTag("Player").GetComponent<PlayerInfo>().HasPoisonSummon)
        {
            PoisonSummonImage.SetActive(true);
        }
        else
        {
            PoisonSummonImage.SetActive(false);
        }

    }
    public void CloseCardInfoPanel()
    {
        CardInfoPanel.SetActive(false);
    }
    public void ChangeCardPanelL()
    {
        currentPanelNumber--;
    }
    public void ChangeCardPanelR()
    {
        currentPanelNumber++;
    }
}
