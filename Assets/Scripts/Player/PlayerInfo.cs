using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerInfo : MonoBehaviour
{
    bool IsDead=false;
    public int curHealth;

    public int maxHealth;
    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            if (value != maxHealth)
            {
                maxHealth = value;

                healthBar.slider.maxValue = maxHealth;
            }
        }
    }
    public int DefaultHealth = 100;

    public HealthBarScript healthBar;

    public int curEnergy;
    public int maxEnergy = 6;

    public int curStamina;
    public int maxStamina = 10;

    public int money = 0;

    float energyTimer = 0;
    float staminaTimer = 0;

    float AttackTime = 0;
    float JumpTime = 0;

    public AudioSource OutOfEnergySound;
    public AudioSource InvalidAttackSound;

    public GameObject playerSprite;
    public GameObject weaponChoose;

    public bool CanTakeDamage = true;

    float PoisonTimer = 0;
    float PoisonTimer2 = 0;
    public GameObject PoisonSkullEft;

    public GameObject NoEnergyText;
    bool NoEnergyTextBool = false;

    public GameObject InvalidAttackTextObj;
    bool InvalidAttackTextBool = false;

    public AudioSource EnergyAddSound;

    float DefaultEnergyRechargeTime = 1f;
    [HideInInspector]public float MinusEnergyRechargeTime = 0;

    float priestessTimer = 0;
    float TakeDamageTime = 0;

    public GameObject ShieldObj; //玩家護盾
    bool IsShield; //有護盾保護的狀態
    [HideInInspector]public bool CanShield = true; //是否能開啟護盾
    float ShieldCoolDownTimer;
    [HideInInspector]public bool IsShieldAnimation=false; //是否正在播護盾破裂動畫，給PlayerMovement判斷用
    public AudioSource ShieldBreakSound;
    float ShieldCoolDownTime = 20f; //護盾的冷卻時間

    GameObject EftUI;

    public GameObject WheelEftTextObj; //顯示命運之輪效果編號的文字
    float WheelTimer = 9f;
    float WheelTime = 10f;//每10秒觸發命運之輪1次
    int WheelLastNum = 0;//命運之輪上次觸發的效果編號
    int WheelCurNum = 0;//命運之輪現在的效果編號
    bool infiniteEnergy = false;//無限能量

    float JusticeDamageMultiplying;

    float HangedManTriggerProbability = 0.1f; //倒吊人的觸發機率

    float CurHealthPercentage = 1; //當前血量的百分比
    float MoonPercentage = 1;

    Material DefaultMaterial;
    public Material WhiteMaterial;
    public AudioSource ReviveSound;
    public GameObject ReviveEft;

    [HideInInspector] public bool HasThunderSummon = false, HasIceSummon = false, HasFireSummon = false, HasPoisonSummon = false;


    void Start()
    {

        maxHealth = DefaultHealth;

        curHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        curEnergy = maxEnergy;
        curStamina = maxStamina;

        ShieldCoolDownTimer = ShieldCoolDownTime;

        EftUI = GameObject.FindWithTag("EftUI");

        DefaultMaterial = playerSprite.GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("CardSelectPanel") != null) //如果場景中含有CardSelectPanel的tag的物件，程式不繼續執行
        {
            return;
        }

        CurHealthPercentage = (float)curHealth / maxHealth;

        if (CardSystem.HasCard("月亮"))
        {
            //MoonPercentage = CurHealthPercentage;
            if (CurHealthPercentage >= 0.9f)
            {
                MoonPercentage = 1f;
            }
            else if (CurHealthPercentage >= 0.75f)
            {
                MoonPercentage = 0.8f;
            }
            else if (CurHealthPercentage >= 0.5f)
            {
                MoonPercentage = 0.6f;
            }
            else if (CurHealthPercentage >= 0.25f)
            {
                MoonPercentage = 0.4f;
            }
            else
            {
                MoonPercentage = 0.3f;
            }
        }
        else
        {
            MoonPercentage = 1;
        }
        if (CardSystem.HasCard("太陽"))
        {
            /*float DamagePercentage = 1 / CurHealthPercentage;
            if(DamagePercentage < 10) // 限制最大加成，10倍為極限
            {
                GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().SunDamageMultiplying = DamagePercentage;
            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().SunDamageMultiplying = 10;
            }*/
            float DamagePercentage;
            if (CurHealthPercentage>=0.75f)
            {
                DamagePercentage = 1.25f;
                GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().SunDamageMultiplying = DamagePercentage;
            }
            else if(CurHealthPercentage >= 0.5f)
            {
                DamagePercentage = 1.5f;
                GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().SunDamageMultiplying = DamagePercentage;
            }
            else if (CurHealthPercentage >= 0.25f)
            {
                DamagePercentage = 1.75f;
                GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().SunDamageMultiplying = DamagePercentage;
            }
            else
            {
                DamagePercentage = 2f;
                GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().SunDamageMultiplying = DamagePercentage;
            }

        }
        else
        {
            GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().SunDamageMultiplying = 1;
        }


        JusticeDamageMultiplying = CardSystem.JusticeGetHitDamageMultiplying; //正義卡的效果計算用

        if (CardSystem.HasCard("命運之輪"))
        {
            WheelTimer += Time.deltaTime;
            if(WheelTimer>=WheelTime)
            {
                //生成圖示
                if (EftUI.transform.Find("Wheel(Clone)") == null)
                {
                    var Wheel = Resources.Load<GameObject>("Effect/Wheel");
                    GameObject WheelEft = Instantiate(Wheel);
                    WheelEft.transform.SetParent(EftUI.transform);
                }

                //在玩家身上播放動畫
                GameObject.FindWithTag("Player").transform.Find("Wheel").gameObject.SetActive(true);
                GameObject.FindWithTag("Player").transform.Find("Wheel").GetComponent<Animator>().SetTrigger("Rotate");
                GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayerScript>().PlayWheelDingSound();
                GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayerScript>().PlayWheelClockSound();

                WheelTimer = 0;

                if(WheelLastNum == 0)
                {
                    WheelCurNum = UnityEngine.Random.Range(1, 6);//1~5
                }
                //清除上次的效果
                else if(WheelLastNum == 1)
                {
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().CritRatePlus -= 1000; //減 1000% 爆擊率
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().CritDamagePlus -= 100; //減 100% 爆擊傷害
                    WheelCurNum = UnityEngine.Random.Range(2, 6);//2~5
                }
                else if (WheelLastNum == 2)
                {
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().atkcooldownMinus -= 0.2f;
                    infiniteEnergy = false;

                    int[] numbers = { 1, 3, 4, 5 };
                    int randomIndex = UnityEngine.Random.Range(0, numbers.Length);
                    WheelCurNum = numbers[randomIndex];
                }
                else if (WheelLastNum == 3)
                {
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().MinusDashCoolDown -= 0.4f;

                    int[] numbers = { 1, 2, 4, 5 };
                    int randomIndex = UnityEngine.Random.Range(0, numbers.Length);
                    WheelCurNum = numbers[randomIndex];
                }
                else if (WheelLastNum == 4)
                {
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().CritRatePlus += 1000;

                    int[] numbers = { 1, 2, 3, 5 };
                    int randomIndex = UnityEngine.Random.Range(0, numbers.Length);
                    WheelCurNum = numbers[randomIndex];
                }
                else if (WheelLastNum == 5)
                {
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().atkcooldownMinus += 0.4f;

                    int[] numbers = { 1, 2, 3, 4 };
                    int randomIndex = UnityEngine.Random.Range(0, numbers.Length);
                    WheelCurNum = numbers[randomIndex];
                }
                
                if (WheelCurNum == 1)
                {
                    //Ⅰ必定爆擊且爆擊傷害+100%
                    GameObject WheelText = Instantiate(WheelEftTextObj, transform.position + new Vector3(0,1,0), Quaternion.identity);
                    WheelText.GetComponent<TextMeshPro>().text = "<color=#00EAFF>Ⅰ</color>";

                    WheelLastNum = 1;
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().CritRatePlus += 1000; //加 1000% 爆擊率
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().CritDamagePlus += 100; //加 100% 爆擊傷害
                    EftUI.transform.Find("Wheel(Clone)").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "命運之輪:<color=#00EAFF>必定爆擊</color>且爆擊傷害+<color=yellow>50%</color>";
                }
                if (WheelCurNum == 2)
                {
                    //Ⅱ減少攻擊冷卻時間0.2秒，且無限能量
                    GameObject WheelText = Instantiate(WheelEftTextObj, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    WheelText.GetComponent<TextMeshPro>().text = "<color=#00EAFF>Ⅱ</color>";

                    WheelLastNum = 2;
                    infiniteEnergy = true;
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().atkcooldownMinus += 0.2f;
                    EftUI.transform.Find("Wheel(Clone)").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "命運之輪:減少攻擊冷卻時間<color=yellow>0.2</color>秒且無限能量";
                }
                if (WheelCurNum == 3)
                {
                    //Ⅲ減少衝刺冷卻時間0.4秒
                    GameObject WheelText = Instantiate(WheelEftTextObj, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    WheelText.GetComponent<TextMeshPro>().text = "<color=#00EAFF>Ⅲ</color>";

                    WheelLastNum = 3;
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().MinusDashCoolDown += 0.4f;
                    EftUI.transform.Find("Wheel(Clone)").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "命運之輪:減少衝刺冷卻時間<color=yellow>0.4</color>秒";
                }
                if (WheelCurNum == 4)
                {
                    //Ⅳ無法爆擊
                    GameObject WheelText = Instantiate(WheelEftTextObj, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    WheelText.GetComponent<TextMeshPro>().text = "<color=#FF0003>Ⅳ</color>";

                    WheelLastNum = 4;
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().CritRatePlus -= 1000;
                    EftUI.transform.Find("Wheel(Clone)").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "命運之輪:<color=red>無法爆擊</color>";
                }
                if (WheelCurNum == 5)
                {
                    //Ⅴ增加攻擊冷卻時間0.4秒
                    GameObject WheelText = Instantiate(WheelEftTextObj, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    WheelText.GetComponent<TextMeshPro>().text = "<color=#FF0003>Ⅴ</color>";

                    WheelLastNum = 5;
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().atkcooldownMinus -= 0.4f;
                    EftUI.transform.Find("Wheel(Clone)").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "命運之輪:增加攻擊冷卻時間<color=red>0.4</color>秒";
                }
            }
        }
        else
        {
            //移除圖示
            if (EftUI.transform.Find("Wheel(Clone)") != null)
            {
                Destroy(EftUI.transform.Find("Wheel(Clone)").gameObject);
            }

            GameObject.FindWithTag("Player").transform.Find("Wheel").gameObject.SetActive(false);
        }

        if (CardSystem.HasCard("隱者"))
        {
            if (CanShield)
            {
                if(EftUI.transform.Find("Hermit(Clone)") == null)
                {
                    var Hermit = Resources.Load<GameObject>("Effect/Hermit");
                    GameObject HermitEft = Instantiate(Hermit);
                    HermitEft.transform.SetParent(EftUI.transform);
                }
            }
            else
            {
                if(EftUI.transform.Find("Hermit(Clone)") !=null)
                {
                    Destroy(EftUI.transform.Find("Hermit(Clone)").gameObject);
                }
            }
        }

        if (CanShield==false)
        {
            if(ShieldCoolDownTimer > 0f) //計算護盾冷卻
            {
                ShieldCoolDownTimer -= Time.deltaTime;
            }
            else
            {
                CanShield = true;
            }
        }

        if (TakeDamageTime >= 0)  //被攻擊後計算5秒，給塔羅牌計算用
        {
            TakeDamageTime -= Time.deltaTime;
        }

        if (CardSystem.HasCard("女祭司")) // 判斷有無女祭司卡牌
        {
            if (TakeDamageTime < 0)  //停止受到攻擊超過5秒才會開始回血
            {
                if (EftUI.transform.Find("Priestess(Clone)") == null) //生成圖示
                {
                    var Priestess = Resources.Load<GameObject>("Effect/Priestess");
                    GameObject PriestessEft = Instantiate(Priestess);
                    PriestessEft.transform.SetParent(EftUI.transform);
                }
                priestessTimer += Time.deltaTime;
                if (priestessTimer > 1f && curHealth < maxHealth) //每1秒回1點血
                {
                    curHealth += 1;
                    healthBar.SetHealth2(curHealth);
                    priestessTimer = 0;
                }
            }
            else if (EftUI.transform.Find("Priestess(Clone)") != null) //移除圖示
            {
                Destroy(EftUI.transform.Find("Priestess(Clone)").gameObject);
            }
        }
        if (CardSystem.HasCard("戀人"))
        {
            if (TakeDamageTime < 0) //停止受到攻擊超過5秒
            {
                GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().LoverDamageMultiplying = 0.35f; //傷害倍率加上35%
            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().LoverDamageMultiplying = 0f; //傷害倍率加上0%
            }
        }


        if(infiniteEnergy==true) //如果現在是無限能量狀態
        {
            curEnergy=maxEnergy; //能量永遠等於最大值
        }
        if (AttackTime >= 0)  //停止攻擊超過0.5秒才會開始回能量
        {
            AttackTime -= Time.deltaTime;
        }
        else
        {
            if (CardSystem.HasCard("魔術師")) // 判斷有無魔術師卡牌
            {
                EnergyRecharge(2, DefaultEnergyRechargeTime - MinusEnergyRechargeTime);//回復2能量
            }
            else
            {
                EnergyRecharge(1, DefaultEnergyRechargeTime - MinusEnergyRechargeTime);
            }
            
        }

        ////////
        if(GameObject.FindWithTag("HealthNumberDisplay")!=null) //顯示當前血量的數字
        {
            GameObject.FindWithTag("HealthNumberDisplay").transform.Find("MaxHealthNum").GetComponent<TextMeshProUGUI>().text = maxHealth.ToString();
            GameObject.FindWithTag("HealthNumberDisplay").transform.Find("CurHealthNum").GetComponent<TextMeshProUGUI>().text = curHealth.ToString();
        }
        ////////

        if(JumpTime >= 0)
        {
            JumpTime -= Time.deltaTime;
        }
        else
        {
            StaminaRecharge(1, 1f);
        }
        

        if (GameObject.FindWithTag("MoneyNumber")!=null)
        {
            GameObject.FindWithTag("MoneyNumber").GetComponent<TextMeshProUGUI>().text = money.ToString();
            /*if (Input.GetKey(KeyCode.Z)) //測試用
            {
                money += 100;
            }*/

        }
        
        if(curHealth>maxHealth)
        {
            curHealth = maxHealth;
        }
        if(curHealth<=0) //死亡
        {
            if (CardSystem.HasCard("死神") && CardSystem.Deathbool==false)
            {
                GetComponent<EquipmentCal>().ExtraPlayerLifePoint -= 50;
                curHealth = maxHealth;
                healthBar.SetHealth(curHealth);
                ReviveSound.Play();
                StartCoroutine(InvincibilityTime(2f));//復活後有2秒的無敵時間
                StartCoroutine(SpriteFlash()); //使圖像閃爍
                StartCoroutine(PlayReviveEft()); //播放特效

                CardSystem.Deathbool = true;
            }
            else
            {
                curHealth = 0;
                if (IsDead == false)
                {
                    GameOver();
                }
            }    
        }
        if(curEnergy>maxEnergy)
        {
            curEnergy = maxEnergy;
        }

        if(PoisonTimer > 0) //中毒狀態
        {
            PoisonSkullEft.SetActive(true);

            PoisonTimer -= Time.deltaTime;
            PoisonTimer2 += Time.deltaTime;
            if(PoisonTimer2 >= 0.5f)
            {
                PoisonTimer2 = 0;

                if (CardSystem.HasCard("倒吊人"))
                {
                    float randomNum = UnityEngine.Random.Range(0.0f,1.0f);
                    if(randomNum <= HangedManTriggerProbability) //10%觸發機率
                    {
                        curHealth += Convert.ToInt32(maxHealth * 0.01f * JusticeDamageMultiplying);
                        InvalidAttackText();//顯示文字告知玩家觸發了倒吊人
                    }
                    else
                    {
                        int DamageNum = Convert.ToInt32(maxHealth * 0.01f * JusticeDamageMultiplying * MoonPercentage);
                        if(DamageNum >= 1)
                        {
                            curHealth -= DamageNum;
                        }
                        else
                        {
                            curHealth --;
                        }
                    }
                }
                else
                {
                    int DamageNum = Convert.ToInt32(maxHealth * 0.01f * JusticeDamageMultiplying * MoonPercentage);
                    if (DamageNum >= 1)
                    {
                        curHealth -= DamageNum;
                    }
                    else
                    {
                        curHealth--;
                    }
                }

                healthBar.SetHealth(curHealth);
            }

            if (EftUI.transform.Find("Poison(Clone)") == null)//中毒時生成中毒圖示
            {
                var Poison = Resources.Load<GameObject>("Effect/Poison");
                GameObject PoisonEft = Instantiate(Poison);
                PoisonEft.transform.SetParent(EftUI.transform);
            }
            if (EftUI.transform.Find("Poison(Clone)") != null)//顯示中毒秒數
            {
                EftUI.transform.Find("Poison(Clone)").transform.Find("Panel").GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToInt16(PoisonTimer).ToString();
            }
        }
        else
        {
            PoisonSkullEft.SetActive(false);

            if (EftUI.transform.Find("Poison(Clone)") != null)//停止中毒時刪除中毒圖示
            {
                Destroy(EftUI.transform.Find("Poison(Clone)").gameObject);
            }
        }

    }
    public void Heal(int damage)
    {
        curHealth += damage;
        healthBar.SetHealth2(curHealth);
    }
    public void TakeDamage(int damage)
    {
        if(CanTakeDamage)
        {

            if(ShieldObj.activeInHierarchy && IsShieldAnimation == false) //如果護盾是啟動狀態，且沒有在執行破裂動畫
            {
                IsShield=true;
                StartCoroutine(ShieldBreak());
            }
            if(IsShield) //讓玩家不受傷害
            {
                return;
            }

            if (CardSystem.HasCard("倒吊人"))
            {
                float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                if (randomNum <= HangedManTriggerProbability) //10%觸發機率
                {
                    curHealth += Convert.ToInt16(damage * JusticeDamageMultiplying);
                    healthBar.SetHealth(curHealth);
                    StartCoroutine(InvincibilityTime(1f));//無效攻擊後有1秒的無敵時間
                    InvalidAttackText();//顯示文字告知玩家觸發了倒吊人
                }
                else
                {
                    CanTakeDamage = false;

                    int DamageNum = Convert.ToInt16(damage * JusticeDamageMultiplying * MoonPercentage);
                    if (DamageNum >= 1)
                    {
                        curHealth -= DamageNum;
                    }
                    else
                    {
                        curHealth--;
                    }

                    healthBar.SetHealth(curHealth);

                    GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayerScript>().PlayPlayerHurtSound();

                    TakeDamageTime = 5f;//被攻擊後計算5秒，給塔羅牌計算用

                    if (curHealth > 0) //播放受傷動畫和給予玩家無敵時間
                    {
                        StartCoroutine(TakingDamage());
                    }
                }
            }
            else
            {
                CanTakeDamage = false;

                int DamageNum = Convert.ToInt16(damage * JusticeDamageMultiplying * MoonPercentage);
                if (DamageNum >= 1)
                {
                    curHealth -= DamageNum;
                }
                else
                {
                    curHealth--;
                }

                healthBar.SetHealth(curHealth);

                GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayerScript>().PlayPlayerHurtSound();

                TakeDamageTime = 5f;//被攻擊後計算5秒，給塔羅牌計算用

                if (curHealth > 0) //播放受傷動畫和給予玩家無敵時間
                {
                    StartCoroutine(TakingDamage());
                }
            }
        }
    }
    public void TakeSpecialDamage(int damage) //特殊攻擊不會給予無敵時間
    {
        if (CanTakeDamage)
        {
            if (ShieldObj.activeInHierarchy && IsShieldAnimation == false) //如果護盾是啟動狀態，且沒有在執行破裂動畫
            {
                IsShield = true;
                StartCoroutine(ShieldBreak());
            }
            if (IsShield) //讓玩家不受傷害
            {
                return;
            }

            if (CardSystem.HasCard("倒吊人"))
            {
                float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                if (randomNum <= HangedManTriggerProbability) //10%觸發機率
                {
                    curHealth += Convert.ToInt16(damage * JusticeDamageMultiplying);
                    healthBar.SetHealth(curHealth);
                    InvalidAttackText();//顯示文字告知玩家觸發了倒吊人
                }
                else
                {
                    int DamageNum = Convert.ToInt16(damage * JusticeDamageMultiplying * MoonPercentage);
                    if (DamageNum >= 1)
                    {
                        curHealth -= DamageNum;
                    }
                    else
                    {
                        curHealth--;
                    }
                    healthBar.SetHealth(curHealth);

                    GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayerScript>().PlayPlayerHurtSound();

                    TakeDamageTime = 5f;//被攻擊後計算5秒，給塔羅牌計算用
                }
            }
            else
            {
                int DamageNum = Convert.ToInt16(damage * JusticeDamageMultiplying * MoonPercentage);
                if (DamageNum >= 1)
                {
                    curHealth -= DamageNum;
                }
                else
                {
                    curHealth--;
                }
                healthBar.SetHealth(curHealth);

                GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayerScript>().PlayPlayerHurtSound();

                TakeDamageTime = 5f;//被攻擊後計算5秒，給塔羅牌計算用
            }
        }
    }
    public void TakeSpecialDamage2(int damage,float time) //特殊攻擊自訂無敵時間長短
    {
        if (CanTakeDamage)
        {
            if (ShieldObj.activeInHierarchy && IsShieldAnimation == false) //如果護盾是啟動狀態，且沒有在執行破裂動畫
            {
                IsShield = true;
                StartCoroutine(ShieldBreak());
            }
            if (IsShield) //讓玩家不受傷害
            {
                return;
            }

            if (CardSystem.HasCard("倒吊人"))
            {
                float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                if (randomNum <= HangedManTriggerProbability) //10%觸發機率
                {
                    curHealth += Convert.ToInt16(damage * JusticeDamageMultiplying);
                    healthBar.SetHealth(curHealth);
                    StartCoroutine(InvincibilityTime(time));//無效攻擊後有1秒的無敵時間
                    InvalidAttackText();//顯示文字告知玩家觸發了倒吊人
                }
                else
                {
                    CanTakeDamage = false;
                    int DamageNum = Convert.ToInt16(damage * JusticeDamageMultiplying * MoonPercentage);
                    if (DamageNum >= 1)
                    {
                        curHealth -= DamageNum;
                    }
                    else
                    {
                        curHealth--;
                    }
                    healthBar.SetHealth(curHealth);

                    GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayerScript>().PlayPlayerHurtSound();

                    TakeDamageTime = 5f;//被攻擊後計算5秒，給塔羅牌計算用

                    if (curHealth > 0) //播放受傷動畫和給予玩家無敵時間
                    {
                        StartCoroutine(TakingDamageSpecial2(time));
                    }
                }
            }
            else
            {
                CanTakeDamage = false;
                int DamageNum = Convert.ToInt16(damage * JusticeDamageMultiplying * MoonPercentage);
                if (DamageNum >= 1)
                {
                    curHealth -= DamageNum;
                }
                else
                {
                    curHealth--;
                }
                healthBar.SetHealth(curHealth);

                GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayerScript>().PlayPlayerHurtSound();

                TakeDamageTime = 5f;//被攻擊後計算5秒，給塔羅牌計算用

                if (curHealth > 0) //播放受傷動畫和給予玩家無敵時間
                {
                    StartCoroutine(TakingDamageSpecial2(time));
                }
            }
        }
    }

    IEnumerator ShieldBreak()
    {
        ShieldBreakSound.Play();
        IsShieldAnimation = true;
        ShieldObj.GetComponent<Animator>().SetTrigger("ShieldBreak");
        yield return new WaitForSeconds(0.6f);
        ShieldObj.SetActive(false);
        ShieldCoolDownTimer = ShieldCoolDownTime;
        CanShield = false;
        IsShield = false;
        IsShieldAnimation = false;
    }
    IEnumerator TakingDamage()
    {
        playerSprite.GetComponent<Animator>().SetTrigger("Hurt");
        yield return new WaitForSeconds(1f);
        CanTakeDamage = true;
    }
    IEnumerator TakingDamageSpecial2(float time)
    {
        playerSprite.GetComponent<Animator>().SetTrigger("Hurt");
        yield return new WaitForSeconds(time);
        CanTakeDamage = true;
    }
    IEnumerator InvincibilityTime(float time)
    {
        CanTakeDamage = false;
        yield return new WaitForSeconds(time);
        CanTakeDamage = true;
    }
    public void TakePoisonDamage(float time)
    {
        if(PoisonTimer < time)
        {
            PoisonTimer = time;
        }
        
    }
    void EnergyRecharge(int EnergyNum, float RechargeTime)
    {
        if(curEnergy<maxEnergy)
        {
            energyTimer += Time.deltaTime;
            if (energyTimer >= RechargeTime)
            {
                curEnergy += EnergyNum;
                energyTimer = 0;
            }
        }
    }
    void StaminaRecharge(int StaminaNum, float RechargeTime)
    {
        if (curStamina < maxStamina)
        {
            staminaTimer += Time.deltaTime;
            if (staminaTimer >= RechargeTime)
            {
                curStamina += StaminaNum;
                staminaTimer = 0;
            }
        }
    }

    public void Attacking()
    {
        AttackTime = 0.5f; //停止攻擊超過0.5秒才會開始回能量
    }
    public void Jumping()
    {
        JumpTime = 0.5f;
    }

    public void AddEnergyWhenBulletHit() //擦彈後加能量
    {
        if(curEnergy < maxEnergy)
        {
            curEnergy++;
        }
        EnergyAddSound.Play();
    }
    public void UseNoEnergyText()
    {
        if(NoEnergyTextBool==false)
        {
            StartCoroutine(UseNoEnergyTextDelay());
        }
    }
    IEnumerator UseNoEnergyTextDelay()
    {
        NoEnergyTextBool = true;
        OutOfEnergySound.Play();
        Instantiate(NoEnergyText, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        NoEnergyTextBool = false;
    }
    public void InvalidAttackText()
    {
        if (InvalidAttackTextBool == false)
        {
            StartCoroutine(InvalidAttackTextDelay());
        }
    }
    IEnumerator InvalidAttackTextDelay()
    {
        InvalidAttackTextBool = true;
        InvalidAttackSound.Play();
        Instantiate(InvalidAttackTextObj, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        InvalidAttackTextBool = false;
    }

    IEnumerator SpriteFlash()
    {
        for (int i = 0; i < 8; i++)
        {
            playerSprite.GetComponent<SpriteRenderer>().material = WhiteMaterial;
            yield return new WaitForSeconds(0.12f);
            playerSprite.GetComponent<SpriteRenderer>().material = DefaultMaterial;
            yield return new WaitForSeconds(0.12f);
        }
    }
    IEnumerator PlayReviveEft()
    {
        ReviveEft.SetActive(true);
        yield return new WaitForSeconds(2f);
        ReviveEft.SetActive(false);
    }

    void GameOver()
    {
        IsDead = true;
        GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayerScript>().PlayPlayerDeadSound();
        playerSprite.GetComponent<Animator>().SetTrigger("Dead");
        Destroy(weaponChoose);
        Destroy(GameObject.FindWithTag("Player"), 1f);

        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().GameOver();
    }

    ////////道具相關/////////
    public void AddMaxHealth(int num)
    {
        maxHealth += num;
        curHealth += num;
        healthBar.AddMaxHealth(num);
    }
    public void AddMaxEnergy(int num)
    {
        maxEnergy += num;
        curEnergy += num;
        if(maxEnergy > 14)
        {
            maxEnergy = 14;
        }
        if(curEnergy > 14)
        {
            curEnergy = 14;
        }
    }
    public void AddMaxStamina(int num)
    {
        maxStamina += num;
        curStamina += num;
        if(maxStamina > 21)
        {
            maxStamina = 21;
        }
        if(curStamina > 21)
        {
            curStamina = 21;
        }
    }
    ////////道具相關/////////
}
