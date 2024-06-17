using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentCal : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public ChooseWeapon chooseWeapon;
    public PlayerInfo playerInfo;
    public TabMenu tabMenu;

    [Header("傷害倍率")] public float DamageMultiplying = 1f; //傷害倍率 預設為1
    [Header("傷害加成")] public int DamagePlus = 0; //傷害加成
    [HideInInspector]public int FinalDamage; //輸出結果
    [HideInInspector]public float LoverDamageMultiplying = 0f; //戀人卡傷害倍率:50%，從playerinfo判斷是否未受傷害五秒
    [HideInInspector] public float StrengthDamageMultiplying = 1f; //力量卡傷害倍率，預設為1(乘以1等於沒加成)，拿到卡且觸發效果後變2
    float JusticeDamageMultiplying; //正義卡傷害倍率，預設為1
    [HideInInspector] public float SunDamageMultiplying = 1f; //太陽卡傷害倍率，預設為1

    [Header("爆擊倍率")] public float CritRateMultiplying = 1f; //爆擊機率倍率 預設為1
    [Header("爆擊機率加成")] public int CritRatePlus = 0; //爆擊機率加成
    [HideInInspector] public float FinalCritRate; //輸出結果

    [Header("爆擊傷害倍率")] public float CritDamageMultiplying = 1f; //爆擊傷害倍率 預設為1
    [Header("爆擊傷害加成")] public int CritDamagePlus = 0; //爆擊傷害加成
    [HideInInspector] public float FinalCritDamage;//輸出結果

    [Header("攻擊冷卻秒數減免")] public float atkcooldownMinus = 0;
    float currentAtkcoolDown;
    [HideInInspector] public float Finalatkcooldown; //輸出結果

    int DefaultEnergyNumber = 6; //玩家的基礎能量數
    [Header("額外能量")] public int EnergyNumberPlus = 0; //增加的能量數
    [HideInInspector] public int FinalEnergyNumber; //輸出結果

    [Header("能量回復冷卻減少時間")]public float MinusEnergyRechargeTime = 0f; //玩家的能量回復冷卻減少時間

    [Header("額外玩家生命值")]public int ExtraPlayerLifePoint; //額外玩家生命值

    [Header("衝刺冷卻減免")] public float MinusDashCoolDown;
    float DefaultDashCoolDownLimit;


    void Start()
    {
        DefaultDashCoolDownLimit = playerMovement.DashcooldownLimit;

        StrengthDamageMultiplying = 1;
    }

    // Update is called once per frame
    void Update()
    {
        JusticeDamageMultiplying = CardSystem.JusticeDamageMultiplying;

        if(chooseWeapon.currentWeapon!=null) //攻擊冷卻計算
        {
            currentAtkcoolDown = chooseWeapon.currentWeapon.GetComponent<WeaponShoot>().AtkCooldown;
            Finalatkcooldown = currentAtkcoolDown - atkcooldownMinus;
            playerMovement.AtkCoolDown = Finalatkcooldown;
        }

        if(playerInfo!=null) //能量數量加成計算
        {

            if(EnergyNumberPlus>8)
            {
                EnergyNumberPlus = 8;
            }
            FinalEnergyNumber = DefaultEnergyNumber + EnergyNumberPlus;
            playerInfo.maxEnergy = FinalEnergyNumber;
        }
        if (playerInfo != null) //能量回復冷卻時間減免
        {
            playerInfo.MinusEnergyRechargeTime = MinusEnergyRechargeTime;
        }
        if (playerInfo != null) //額外玩家生命值
        {
            //playerInfo.MaxHealth = ExtraPlayerLifePoint + playerInfo.DefaultHealth;
            playerInfo.MaxHealth = System.Convert.ToInt16(playerInfo.DefaultHealth * ExtraPlayerLifePoint * 0.01f + playerInfo.DefaultHealth);
        }
        if (playerMovement != null) //衝刺冷卻減免
        {
            playerMovement.DashcooldownLimit = DefaultDashCoolDownLimit - MinusDashCoolDown;
        }

        ///////////////////////////////////////下面為測試用
        // 獲得卡牌
        if (tabMenu.currentPanelNumber==1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            { CardSystem.AcquireCard("魔術師"); }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            { CardSystem.AcquireCard("女祭司"); }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            { CardSystem.AcquireCard("惡魔"); }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            { CardSystem.AcquireCard("皇后"); }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            { CardSystem.AcquireCard("皇帝"); }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            { CardSystem.AcquireCard("教皇"); }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            { CardSystem.AcquireCard("戀人"); }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            { CardSystem.AcquireCard("戰車"); }
        }
        else if(tabMenu.currentPanelNumber == 2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            { CardSystem.AcquireCard("力量"); }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            { CardSystem.AcquireCard("隱者"); }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            { CardSystem.AcquireCard("命運之輪"); }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            { CardSystem.AcquireCard("正義"); }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            { CardSystem.AcquireCard("倒吊人"); }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            { CardSystem.AcquireCard("死神"); }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            { CardSystem.AcquireCard("節制"); }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            { CardSystem.AcquireCard("塔"); }
        }
        else if(tabMenu.currentPanelNumber == 3)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            { CardSystem.AcquireCard("星星"); }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            { CardSystem.AcquireCard("月亮"); }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            { CardSystem.AcquireCard("太陽"); }
        }

    }
    public void DamageCal(int Damage, float CritRate, float CritDamage) //傷害計算程式
    {
        Damage += DamagePlus; //加基礎傷害
        if (!CardSystem.HasCard("戀人"))
        {
            Damage = System.Convert.ToInt16(Damage * DamageMultiplying * StrengthDamageMultiplying * JusticeDamageMultiplying * SunDamageMultiplying);//傷害=傷害x傷害倍率之後四捨五入
        }
        else
        {
            Damage = System.Convert.ToInt16(Damage * (DamageMultiplying + LoverDamageMultiplying) * StrengthDamageMultiplying * JusticeDamageMultiplying * SunDamageMultiplying);//傷害=傷害x(傷害倍率加上戀人卡傷害倍率)之後四捨五入
        }
            
        FinalDamage = Damage; //將計算結果存在FinalDamage上，方便bulletScript讀取

        CritRate += CritRatePlus;
        CritRate = CritRate * CritRateMultiplying;
        FinalCritRate = CritRate;

        CritDamage += CritDamagePlus;
        CritDamage = CritDamage * CritDamageMultiplying;
        FinalCritDamage = CritDamage;

    }
}
