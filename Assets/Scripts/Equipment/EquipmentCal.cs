using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentCal : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public ChooseWeapon chooseWeapon;
    public PlayerInfo playerInfo;
    public TabMenu tabMenu;

    [Header("�ˮ`���v")] public float DamageMultiplying = 1f; //�ˮ`���v �w�]��1
    [Header("�ˮ`�[��")] public int DamagePlus = 0; //�ˮ`�[��
    [HideInInspector]public int FinalDamage; //��X���G
    [HideInInspector]public float LoverDamageMultiplying = 0f; //�ʤH�d�ˮ`���v:50%�A�qplayerinfo�P�_�O�_�����ˮ`����
    [HideInInspector] public float StrengthDamageMultiplying = 1f; //�O�q�d�ˮ`���v�A�w�]��1(���H1����S�[��)�A����d�BĲ�o�ĪG����2
    float JusticeDamageMultiplying; //���q�d�ˮ`���v�A�w�]��1
    [HideInInspector] public float SunDamageMultiplying = 1f; //�Ӷ��d�ˮ`���v�A�w�]��1

    [Header("�z�����v")] public float CritRateMultiplying = 1f; //�z�����v���v �w�]��1
    [Header("�z�����v�[��")] public int CritRatePlus = 0; //�z�����v�[��
    [HideInInspector] public float FinalCritRate; //��X���G

    [Header("�z���ˮ`���v")] public float CritDamageMultiplying = 1f; //�z���ˮ`���v �w�]��1
    [Header("�z���ˮ`�[��")] public int CritDamagePlus = 0; //�z���ˮ`�[��
    [HideInInspector] public float FinalCritDamage;//��X���G

    [Header("�����N�o��ƴ�K")] public float atkcooldownMinus = 0;
    float currentAtkcoolDown;
    [HideInInspector] public float Finalatkcooldown; //��X���G

    int DefaultEnergyNumber = 6; //���a����¦��q��
    [Header("�B�~��q")] public int EnergyNumberPlus = 0; //�W�[����q��
    [HideInInspector] public int FinalEnergyNumber; //��X���G

    [Header("��q�^�_�N�o��֮ɶ�")]public float MinusEnergyRechargeTime = 0f; //���a����q�^�_�N�o��֮ɶ�

    [Header("�B�~���a�ͩR��")]public int ExtraPlayerLifePoint; //�B�~���a�ͩR��

    [Header("�Ĩ�N�o��K")] public float MinusDashCoolDown;
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

        if(chooseWeapon.currentWeapon!=null) //�����N�o�p��
        {
            currentAtkcoolDown = chooseWeapon.currentWeapon.GetComponent<WeaponShoot>().AtkCooldown;
            Finalatkcooldown = currentAtkcoolDown - atkcooldownMinus;
            playerMovement.AtkCoolDown = Finalatkcooldown;
        }

        if(playerInfo!=null) //��q�ƶq�[���p��
        {

            if(EnergyNumberPlus>8)
            {
                EnergyNumberPlus = 8;
            }
            FinalEnergyNumber = DefaultEnergyNumber + EnergyNumberPlus;
            playerInfo.maxEnergy = FinalEnergyNumber;
        }
        if (playerInfo != null) //��q�^�_�N�o�ɶ���K
        {
            playerInfo.MinusEnergyRechargeTime = MinusEnergyRechargeTime;
        }
        if (playerInfo != null) //�B�~���a�ͩR��
        {
            //playerInfo.MaxHealth = ExtraPlayerLifePoint + playerInfo.DefaultHealth;
            playerInfo.MaxHealth = System.Convert.ToInt16(playerInfo.DefaultHealth * ExtraPlayerLifePoint * 0.01f + playerInfo.DefaultHealth);
        }
        if (playerMovement != null) //�Ĩ�N�o��K
        {
            playerMovement.DashcooldownLimit = DefaultDashCoolDownLimit - MinusDashCoolDown;
        }

        ///////////////////////////////////////�U�������ե�
        // ��o�d�P
        if (tabMenu.currentPanelNumber==1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            { CardSystem.AcquireCard("�]�N�v"); }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            { CardSystem.AcquireCard("�k���q"); }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            { CardSystem.AcquireCard("�c�]"); }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            { CardSystem.AcquireCard("�ӦZ"); }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            { CardSystem.AcquireCard("�ӫ�"); }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            { CardSystem.AcquireCard("�Ь�"); }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            { CardSystem.AcquireCard("�ʤH"); }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            { CardSystem.AcquireCard("�Ԩ�"); }
        }
        else if(tabMenu.currentPanelNumber == 2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            { CardSystem.AcquireCard("�O�q"); }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            { CardSystem.AcquireCard("����"); }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            { CardSystem.AcquireCard("�R�B����"); }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            { CardSystem.AcquireCard("���q"); }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            { CardSystem.AcquireCard("�˦Q�H"); }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            { CardSystem.AcquireCard("����"); }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            { CardSystem.AcquireCard("�`��"); }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            { CardSystem.AcquireCard("��"); }
        }
        else if(tabMenu.currentPanelNumber == 3)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            { CardSystem.AcquireCard("�P�P"); }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            { CardSystem.AcquireCard("��G"); }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            { CardSystem.AcquireCard("�Ӷ�"); }
        }

    }
    public void DamageCal(int Damage, float CritRate, float CritDamage) //�ˮ`�p��{��
    {
        Damage += DamagePlus; //�[��¦�ˮ`
        if (!CardSystem.HasCard("�ʤH"))
        {
            Damage = System.Convert.ToInt16(Damage * DamageMultiplying * StrengthDamageMultiplying * JusticeDamageMultiplying * SunDamageMultiplying);//�ˮ`=�ˮ`x�ˮ`���v����|�ˤ��J
        }
        else
        {
            Damage = System.Convert.ToInt16(Damage * (DamageMultiplying + LoverDamageMultiplying) * StrengthDamageMultiplying * JusticeDamageMultiplying * SunDamageMultiplying);//�ˮ`=�ˮ`x(�ˮ`���v�[�W�ʤH�d�ˮ`���v)����|�ˤ��J
        }
            
        FinalDamage = Damage; //�N�p�⵲�G�s�bFinalDamage�W�A��KbulletScriptŪ��

        CritRate += CritRatePlus;
        CritRate = CritRate * CritRateMultiplying;
        FinalCritRate = CritRate;

        CritDamage += CritDamagePlus;
        CritDamage = CritDamage * CritDamageMultiplying;
        FinalCritDamage = CritDamage;

    }
}
