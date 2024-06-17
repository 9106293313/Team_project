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

    public GameObject ShieldObj; //���a�@��
    bool IsShield; //���@�ޫO�@�����A
    [HideInInspector]public bool CanShield = true; //�O�_��}���@��
    float ShieldCoolDownTimer;
    [HideInInspector]public bool IsShieldAnimation=false; //�O�_���b���@�ޯ}���ʵe�A��PlayerMovement�P�_��
    public AudioSource ShieldBreakSound;
    float ShieldCoolDownTime = 20f; //�@�ު��N�o�ɶ�

    GameObject EftUI;

    public GameObject WheelEftTextObj; //��ܩR�B�����ĪG�s������r
    float WheelTimer = 9f;
    float WheelTime = 10f;//�C10��Ĳ�o�R�B����1��
    int WheelLastNum = 0;//�R�B�����W��Ĳ�o���ĪG�s��
    int WheelCurNum = 0;//�R�B�����{�b���ĪG�s��
    bool infiniteEnergy = false;//�L����q

    float JusticeDamageMultiplying;

    float HangedManTriggerProbability = 0.1f; //�˦Q�H��Ĳ�o���v

    float CurHealthPercentage = 1; //��e��q���ʤ���
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
        if (GameObject.FindWithTag("CardSelectPanel") != null) //�p�G�������t��CardSelectPanel��tag������A�{�����~�����
        {
            return;
        }

        CurHealthPercentage = (float)curHealth / maxHealth;

        if (CardSystem.HasCard("��G"))
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
        if (CardSystem.HasCard("�Ӷ�"))
        {
            /*float DamagePercentage = 1 / CurHealthPercentage;
            if(DamagePercentage < 10) // ����̤j�[���A10��������
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


        JusticeDamageMultiplying = CardSystem.JusticeGetHitDamageMultiplying; //���q�d���ĪG�p���

        if (CardSystem.HasCard("�R�B����"))
        {
            WheelTimer += Time.deltaTime;
            if(WheelTimer>=WheelTime)
            {
                //�ͦ��ϥ�
                if (EftUI.transform.Find("Wheel(Clone)") == null)
                {
                    var Wheel = Resources.Load<GameObject>("Effect/Wheel");
                    GameObject WheelEft = Instantiate(Wheel);
                    WheelEft.transform.SetParent(EftUI.transform);
                }

                //�b���a���W����ʵe
                GameObject.FindWithTag("Player").transform.Find("Wheel").gameObject.SetActive(true);
                GameObject.FindWithTag("Player").transform.Find("Wheel").GetComponent<Animator>().SetTrigger("Rotate");
                GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayerScript>().PlayWheelDingSound();
                GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayerScript>().PlayWheelClockSound();

                WheelTimer = 0;

                if(WheelLastNum == 0)
                {
                    WheelCurNum = UnityEngine.Random.Range(1, 6);//1~5
                }
                //�M���W�����ĪG
                else if(WheelLastNum == 1)
                {
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().CritRatePlus -= 1000; //�� 1000% �z���v
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().CritDamagePlus -= 100; //�� 100% �z���ˮ`
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
                    //�����w�z���B�z���ˮ`+100%
                    GameObject WheelText = Instantiate(WheelEftTextObj, transform.position + new Vector3(0,1,0), Quaternion.identity);
                    WheelText.GetComponent<TextMeshPro>().text = "<color=#00EAFF>��</color>";

                    WheelLastNum = 1;
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().CritRatePlus += 1000; //�[ 1000% �z���v
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().CritDamagePlus += 100; //�[ 100% �z���ˮ`
                    EftUI.transform.Find("Wheel(Clone)").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "�R�B����:<color=#00EAFF>���w�z��</color>�B�z���ˮ`+<color=yellow>50%</color>";
                }
                if (WheelCurNum == 2)
                {
                    //����֧����N�o�ɶ�0.2��A�B�L����q
                    GameObject WheelText = Instantiate(WheelEftTextObj, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    WheelText.GetComponent<TextMeshPro>().text = "<color=#00EAFF>��</color>";

                    WheelLastNum = 2;
                    infiniteEnergy = true;
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().atkcooldownMinus += 0.2f;
                    EftUI.transform.Find("Wheel(Clone)").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "�R�B����:��֧����N�o�ɶ�<color=yellow>0.2</color>��B�L����q";
                }
                if (WheelCurNum == 3)
                {
                    //����ֽĨ�N�o�ɶ�0.4��
                    GameObject WheelText = Instantiate(WheelEftTextObj, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    WheelText.GetComponent<TextMeshPro>().text = "<color=#00EAFF>��</color>";

                    WheelLastNum = 3;
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().MinusDashCoolDown += 0.4f;
                    EftUI.transform.Find("Wheel(Clone)").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "�R�B����:��ֽĨ�N�o�ɶ�<color=yellow>0.4</color>��";
                }
                if (WheelCurNum == 4)
                {
                    //���L�k�z��
                    GameObject WheelText = Instantiate(WheelEftTextObj, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    WheelText.GetComponent<TextMeshPro>().text = "<color=#FF0003>��</color>";

                    WheelLastNum = 4;
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().CritRatePlus -= 1000;
                    EftUI.transform.Find("Wheel(Clone)").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "�R�B����:<color=red>�L�k�z��</color>";
                }
                if (WheelCurNum == 5)
                {
                    //���W�[�����N�o�ɶ�0.4��
                    GameObject WheelText = Instantiate(WheelEftTextObj, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    WheelText.GetComponent<TextMeshPro>().text = "<color=#FF0003>��</color>";

                    WheelLastNum = 5;
                    GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().atkcooldownMinus -= 0.4f;
                    EftUI.transform.Find("Wheel(Clone)").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "�R�B����:�W�[�����N�o�ɶ�<color=red>0.4</color>��";
                }
            }
        }
        else
        {
            //�����ϥ�
            if (EftUI.transform.Find("Wheel(Clone)") != null)
            {
                Destroy(EftUI.transform.Find("Wheel(Clone)").gameObject);
            }

            GameObject.FindWithTag("Player").transform.Find("Wheel").gameObject.SetActive(false);
        }

        if (CardSystem.HasCard("����"))
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
            if(ShieldCoolDownTimer > 0f) //�p���@�ާN�o
            {
                ShieldCoolDownTimer -= Time.deltaTime;
            }
            else
            {
                CanShield = true;
            }
        }

        if (TakeDamageTime >= 0)  //�Q������p��5��A����ù�P�p���
        {
            TakeDamageTime -= Time.deltaTime;
        }

        if (CardSystem.HasCard("�k���q")) // �P�_���L�k���q�d�P
        {
            if (TakeDamageTime < 0)  //�����������W�L5��~�|�}�l�^��
            {
                if (EftUI.transform.Find("Priestess(Clone)") == null) //�ͦ��ϥ�
                {
                    var Priestess = Resources.Load<GameObject>("Effect/Priestess");
                    GameObject PriestessEft = Instantiate(Priestess);
                    PriestessEft.transform.SetParent(EftUI.transform);
                }
                priestessTimer += Time.deltaTime;
                if (priestessTimer > 1f && curHealth < maxHealth) //�C1��^1�I��
                {
                    curHealth += 1;
                    healthBar.SetHealth2(curHealth);
                    priestessTimer = 0;
                }
            }
            else if (EftUI.transform.Find("Priestess(Clone)") != null) //�����ϥ�
            {
                Destroy(EftUI.transform.Find("Priestess(Clone)").gameObject);
            }
        }
        if (CardSystem.HasCard("�ʤH"))
        {
            if (TakeDamageTime < 0) //�����������W�L5��
            {
                GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().LoverDamageMultiplying = 0.35f; //�ˮ`���v�[�W35%
            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().LoverDamageMultiplying = 0f; //�ˮ`���v�[�W0%
            }
        }


        if(infiniteEnergy==true) //�p�G�{�b�O�L����q���A
        {
            curEnergy=maxEnergy; //��q�û�����̤j��
        }
        if (AttackTime >= 0)  //��������W�L0.5��~�|�}�l�^��q
        {
            AttackTime -= Time.deltaTime;
        }
        else
        {
            if (CardSystem.HasCard("�]�N�v")) // �P�_���L�]�N�v�d�P
            {
                EnergyRecharge(2, DefaultEnergyRechargeTime - MinusEnergyRechargeTime);//�^�_2��q
            }
            else
            {
                EnergyRecharge(1, DefaultEnergyRechargeTime - MinusEnergyRechargeTime);
            }
            
        }

        ////////
        if(GameObject.FindWithTag("HealthNumberDisplay")!=null) //��ܷ�e��q���Ʀr
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
            /*if (Input.GetKey(KeyCode.Z)) //���ե�
            {
                money += 100;
            }*/

        }
        
        if(curHealth>maxHealth)
        {
            curHealth = maxHealth;
        }
        if(curHealth<=0) //���`
        {
            if (CardSystem.HasCard("����") && CardSystem.Deathbool==false)
            {
                GetComponent<EquipmentCal>().ExtraPlayerLifePoint -= 50;
                curHealth = maxHealth;
                healthBar.SetHealth(curHealth);
                ReviveSound.Play();
                StartCoroutine(InvincibilityTime(2f));//�_���ᦳ2���L�Įɶ�
                StartCoroutine(SpriteFlash()); //�ϹϹ��{�{
                StartCoroutine(PlayReviveEft()); //����S��

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

        if(PoisonTimer > 0) //���r���A
        {
            PoisonSkullEft.SetActive(true);

            PoisonTimer -= Time.deltaTime;
            PoisonTimer2 += Time.deltaTime;
            if(PoisonTimer2 >= 0.5f)
            {
                PoisonTimer2 = 0;

                if (CardSystem.HasCard("�˦Q�H"))
                {
                    float randomNum = UnityEngine.Random.Range(0.0f,1.0f);
                    if(randomNum <= HangedManTriggerProbability) //10%Ĳ�o���v
                    {
                        curHealth += Convert.ToInt32(maxHealth * 0.01f * JusticeDamageMultiplying);
                        InvalidAttackText();//��ܤ�r�i�����aĲ�o�F�˦Q�H
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

            if (EftUI.transform.Find("Poison(Clone)") == null)//���r�ɥͦ����r�ϥ�
            {
                var Poison = Resources.Load<GameObject>("Effect/Poison");
                GameObject PoisonEft = Instantiate(Poison);
                PoisonEft.transform.SetParent(EftUI.transform);
            }
            if (EftUI.transform.Find("Poison(Clone)") != null)//��ܤ��r���
            {
                EftUI.transform.Find("Poison(Clone)").transform.Find("Panel").GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToInt16(PoisonTimer).ToString();
            }
        }
        else
        {
            PoisonSkullEft.SetActive(false);

            if (EftUI.transform.Find("Poison(Clone)") != null)//����r�ɧR�����r�ϥ�
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

            if(ShieldObj.activeInHierarchy && IsShieldAnimation == false) //�p�G�@�ެO�Ұʪ��A�A�B�S���b����}���ʵe
            {
                IsShield=true;
                StartCoroutine(ShieldBreak());
            }
            if(IsShield) //�����a�����ˮ`
            {
                return;
            }

            if (CardSystem.HasCard("�˦Q�H"))
            {
                float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                if (randomNum <= HangedManTriggerProbability) //10%Ĳ�o���v
                {
                    curHealth += Convert.ToInt16(damage * JusticeDamageMultiplying);
                    healthBar.SetHealth(curHealth);
                    StartCoroutine(InvincibilityTime(1f));//�L�ħ����ᦳ1���L�Įɶ�
                    InvalidAttackText();//��ܤ�r�i�����aĲ�o�F�˦Q�H
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

                    TakeDamageTime = 5f;//�Q������p��5��A����ù�P�p���

                    if (curHealth > 0) //������˰ʵe�M�������a�L�Įɶ�
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

                TakeDamageTime = 5f;//�Q������p��5��A����ù�P�p���

                if (curHealth > 0) //������˰ʵe�M�������a�L�Įɶ�
                {
                    StartCoroutine(TakingDamage());
                }
            }
        }
    }
    public void TakeSpecialDamage(int damage) //�S��������|�����L�Įɶ�
    {
        if (CanTakeDamage)
        {
            if (ShieldObj.activeInHierarchy && IsShieldAnimation == false) //�p�G�@�ެO�Ұʪ��A�A�B�S���b����}���ʵe
            {
                IsShield = true;
                StartCoroutine(ShieldBreak());
            }
            if (IsShield) //�����a�����ˮ`
            {
                return;
            }

            if (CardSystem.HasCard("�˦Q�H"))
            {
                float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                if (randomNum <= HangedManTriggerProbability) //10%Ĳ�o���v
                {
                    curHealth += Convert.ToInt16(damage * JusticeDamageMultiplying);
                    healthBar.SetHealth(curHealth);
                    InvalidAttackText();//��ܤ�r�i�����aĲ�o�F�˦Q�H
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

                    TakeDamageTime = 5f;//�Q������p��5��A����ù�P�p���
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

                TakeDamageTime = 5f;//�Q������p��5��A����ù�P�p���
            }
        }
    }
    public void TakeSpecialDamage2(int damage,float time) //�S������ۭq�L�Įɶ����u
    {
        if (CanTakeDamage)
        {
            if (ShieldObj.activeInHierarchy && IsShieldAnimation == false) //�p�G�@�ެO�Ұʪ��A�A�B�S���b����}���ʵe
            {
                IsShield = true;
                StartCoroutine(ShieldBreak());
            }
            if (IsShield) //�����a�����ˮ`
            {
                return;
            }

            if (CardSystem.HasCard("�˦Q�H"))
            {
                float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
                if (randomNum <= HangedManTriggerProbability) //10%Ĳ�o���v
                {
                    curHealth += Convert.ToInt16(damage * JusticeDamageMultiplying);
                    healthBar.SetHealth(curHealth);
                    StartCoroutine(InvincibilityTime(time));//�L�ħ����ᦳ1���L�Įɶ�
                    InvalidAttackText();//��ܤ�r�i�����aĲ�o�F�˦Q�H
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

                    TakeDamageTime = 5f;//�Q������p��5��A����ù�P�p���

                    if (curHealth > 0) //������˰ʵe�M�������a�L�Įɶ�
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

                TakeDamageTime = 5f;//�Q������p��5��A����ù�P�p���

                if (curHealth > 0) //������˰ʵe�M�������a�L�Įɶ�
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
        AttackTime = 0.5f; //��������W�L0.5��~�|�}�l�^��q
    }
    public void Jumping()
    {
        JumpTime = 0.5f;
    }

    public void AddEnergyWhenBulletHit() //���u��[��q
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

    ////////�D�����/////////
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
    ////////�D�����/////////
}
