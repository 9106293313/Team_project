using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreAltar : MonoBehaviour
{
    public enum StoreType  //�w�q�ө�����
    {
        ��¦����,
        ��q,
        �z�����v,
        �z���ˮ`,
        �����N�o��K,
        �B�~��q
    }

    public GameObject FButton;
    public AudioSource TriggerAltarSound;
    public GameObject TriggerEffect;
    public GameObject FloatImage;

    public StoreType storetype; //�ө�����
    public int MaxBuffNum, MinBuffNum;
    int BuffNum;
    public int MaxCostMoney, MinCostMoney;
    int CostMoney;
    public TextMeshProUGUI SellingTextUI;
    public TextMeshProUGUI CostMoneyTextUI;

    bool IsTrigger=false;//�O�_�w�QĲ�o�A�ɯťu���ʶR1���AĲ�o�ᥲ�������\��

    bool NoMoneyTextBool=false;
    public AudioSource NoMoneySound;
    public GameObject NoMoneyTextObj;
    void Start()
    {
        FButton.SetActive(false);

        BuffNum = UnityEngine.Random.Range(MinBuffNum, MaxBuffNum+1);
        CostMoney = UnityEngine.Random.Range(MinCostMoney, MaxCostMoney + 1);

        if (storetype == StoreType.��¦����)
        {
            SellingTextUI.text = "���ɰ�¦�����O:" + BuffNum;
        }
        else if(storetype == StoreType.��q)
        {
            SellingTextUI.text = "�����`��q:" + BuffNum;
        }
        else if (storetype == StoreType.�z�����v)
        {
            SellingTextUI.text = "�����z�����v:" + BuffNum;
        }
        else if (storetype == StoreType.�z���ˮ`)
        {
            SellingTextUI.text = "�����z���ˮ`:" + BuffNum;
        }
        else if (storetype == StoreType.�����N�o��K)
        {
            SellingTextUI.text = "�����N�o��K:" + (float)BuffNum / 100 + "��";
        }
        else if (storetype == StoreType.�B�~��q)
        {
            SellingTextUI.text = "�B�~��q:" + BuffNum;
        }

        CostMoneyTextUI.text = "$:"+ CostMoney;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && IsTrigger == false)
        {
            FButton.SetActive(true);

            if (Input.GetKey(KeyCode.F))
            {

                if(collision.GetComponent<PlayerInfo>().money >= CostMoney) //�����R
                {
                    collision.GetComponent<PlayerInfo>().money -= CostMoney;
                    IsTrigger = true;
                    FButton.SetActive(false);
                    FloatImage.SetActive(false);

                    GiveBuff(collision.gameObject);
                    
                    TriggerAltarSound.Play();
                    StartCoroutine(StartTriggetEffect());
                }
                else //�S���R
                {
                    NoMoneyText();
                }
                
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FButton.SetActive(false);
        }
    }

    IEnumerator StartTriggetEffect()
    {
        TriggerEffect.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        TriggerEffect.SetActive(false);
    }

    public void NoMoneyText()
    {
        if (NoMoneyTextBool == false)
        {
            StartCoroutine(NoMoneyTextDelay());
        }
    }
    IEnumerator NoMoneyTextDelay()
    {
        NoMoneyTextBool = true;
        NoMoneySound.Play();
        Instantiate(NoMoneyTextObj, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        NoMoneyTextBool = false;
    }
    void GiveBuff(GameObject Player)
    {
        if(storetype == StoreType.��¦����)
        {
            Player.GetComponent<EquipmentCal>().DamagePlus += BuffNum;
        }
        else if(storetype == StoreType.��q)
        {
            StartCoroutine(AddHealth(Player));
        }
        else if (storetype == StoreType.�z�����v)
        {
            Player.GetComponent<EquipmentCal>().CritRatePlus += BuffNum;
        }
        else if (storetype == StoreType.�z���ˮ`)
        {
            Player.GetComponent<EquipmentCal>().CritDamagePlus += BuffNum;
        }
        else if (storetype == StoreType.�����N�o��K)
        {
            Player.GetComponent<EquipmentCal>().atkcooldownMinus += (float)BuffNum /100;
        }
        else if (storetype == StoreType.�B�~��q)
        {
            Player.GetComponent<EquipmentCal>().EnergyNumberPlus += BuffNum;
        }

    }
    IEnumerator AddHealth(GameObject Player)
    {
        Player.GetComponent<EquipmentCal>().ExtraPlayerLifePoint += BuffNum;
        yield return new WaitForSeconds(0.1f);
        Player.GetComponent<PlayerInfo>().curHealth += BuffNum;
        Player.GetComponent<PlayerInfo>().healthBar.SetHealth2(Player.GetComponent<PlayerInfo>().curHealth);
    }
}
