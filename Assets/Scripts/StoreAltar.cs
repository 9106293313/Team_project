using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreAltar : MonoBehaviour
{
    public enum StoreType  //定義商店類型
    {
        基礎攻擊,
        血量,
        爆擊機率,
        爆擊傷害,
        攻擊冷卻減免,
        額外能量
    }

    public GameObject FButton;
    public AudioSource TriggerAltarSound;
    public GameObject TriggerEffect;
    public GameObject FloatImage;

    public StoreType storetype; //商店類型
    public int MaxBuffNum, MinBuffNum;
    int BuffNum;
    public int MaxCostMoney, MinCostMoney;
    int CostMoney;
    public TextMeshProUGUI SellingTextUI;
    public TextMeshProUGUI CostMoneyTextUI;

    bool IsTrigger=false;//是否已被觸發，升級只能購買1次，觸發後必須關閉功能

    bool NoMoneyTextBool=false;
    public AudioSource NoMoneySound;
    public GameObject NoMoneyTextObj;
    void Start()
    {
        FButton.SetActive(false);

        BuffNum = UnityEngine.Random.Range(MinBuffNum, MaxBuffNum+1);
        CostMoney = UnityEngine.Random.Range(MinCostMoney, MaxCostMoney + 1);

        if (storetype == StoreType.基礎攻擊)
        {
            SellingTextUI.text = "提升基礎攻擊力:" + BuffNum;
        }
        else if(storetype == StoreType.血量)
        {
            SellingTextUI.text = "提升總血量:" + BuffNum;
        }
        else if (storetype == StoreType.爆擊機率)
        {
            SellingTextUI.text = "提升爆擊機率:" + BuffNum;
        }
        else if (storetype == StoreType.爆擊傷害)
        {
            SellingTextUI.text = "提升爆擊傷害:" + BuffNum;
        }
        else if (storetype == StoreType.攻擊冷卻減免)
        {
            SellingTextUI.text = "攻擊冷卻減免:" + (float)BuffNum / 100 + "秒";
        }
        else if (storetype == StoreType.額外能量)
        {
            SellingTextUI.text = "額外能量:" + BuffNum;
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

                if(collision.GetComponent<PlayerInfo>().money >= CostMoney) //有錢買
                {
                    collision.GetComponent<PlayerInfo>().money -= CostMoney;
                    IsTrigger = true;
                    FButton.SetActive(false);
                    FloatImage.SetActive(false);

                    GiveBuff(collision.gameObject);
                    
                    TriggerAltarSound.Play();
                    StartCoroutine(StartTriggetEffect());
                }
                else //沒錢買
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
        if(storetype == StoreType.基礎攻擊)
        {
            Player.GetComponent<EquipmentCal>().DamagePlus += BuffNum;
        }
        else if(storetype == StoreType.血量)
        {
            StartCoroutine(AddHealth(Player));
        }
        else if (storetype == StoreType.爆擊機率)
        {
            Player.GetComponent<EquipmentCal>().CritRatePlus += BuffNum;
        }
        else if (storetype == StoreType.爆擊傷害)
        {
            Player.GetComponent<EquipmentCal>().CritDamagePlus += BuffNum;
        }
        else if (storetype == StoreType.攻擊冷卻減免)
        {
            Player.GetComponent<EquipmentCal>().atkcooldownMinus += (float)BuffNum /100;
        }
        else if (storetype == StoreType.額外能量)
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
