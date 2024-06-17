using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardSystem
{
    public static float JusticeDamageMultiplying = 1f; //���q�d�ˮ`���v�A�w�]��1
    public static float JusticeGetHitDamageMultiplying = 1f; //���q�d�����ˮ`���v�A�w�]��1
    public static float StarTriggerProbability = 0.1f; //�P�P�d��Ĳ�o���v10%
    public static float StarHealPercentage = 0.03f; //�P�P�d���^�_�q�ʤ���3%
    public static float TowerTriggerProbability = 0.03f; //��d��Ĳ�o���v3%
    public static float TowerDamagePercentage = 0.05f; //��d���z���ˮ`��5%
    public static bool Deathbool = false; //�����d�O�_�w�g�QĲ�o(�_��)�L�F
    public static float TemperanceTriggerProbability = 0.3f; //�`��d��Ĳ�o���v30%

    public static void ResetCardInfo()
    {
        JusticeDamageMultiplying = 1f; //���q�d�ˮ`���v�A�w�]��1
        JusticeGetHitDamageMultiplying = 1f; //���q�d�����ˮ`���v�A�w�]��1
        StarTriggerProbability = 0.1f; //�P�P�d��Ĳ�o���v10%
        StarHealPercentage = 0.03f; //�P�P�d���^�_�q�ʤ���3%
        TowerTriggerProbability = 0.03f; //��d��Ĳ�o���v3%
        TowerDamagePercentage = 0.05f; //��d���z���ˮ`��5%
        Deathbool = false; //�����d�O�_�w�g�QĲ�o(�_��)�L�F
        TemperanceTriggerProbability = 0.3f; //�`��d��Ĳ�o���v30%
    }

    public static List<string> acquiredCards = new List<string>(); // �s�x�w��o���d�P�W�٪��C��
    private static List<string> allCards = new List<string>() 
    { 
        "�]�N�v", 
        "�k���q", 
        "�c�]", 
        "�ӦZ", 
        "�ӫ�", 
        "�Ь�", 
        "�ʤH", 
        "�Ԩ�", 
        "�O�q", 
        "����", 
        "�R�B����", 
        "���q", 
        "�˦Q�H", 
        "����", 
        "�`��", 
        "��", 
        "�P�P", 
        "��G", 
        "�Ӷ�" 
    }; // �]�t�Ҧ��d�P���C��

    // ��o�d�P
    public static void AcquireCard(string cardName)
    {
        if (!acquiredCards.Contains(cardName))
        {
            acquiredCards.Add(cardName);
            CardEffect(cardName);
        }
    }

    // �P�_���a�O�_�֦����w���d�P
    public static bool HasCard(string cardName)
    {
        return acquiredCards.Contains(cardName);
    }

    // ����Ҧ��d�P�C��
    public static List<string> GetAllCards()
    {
        return allCards;
    }

    public static void CardEffect(string cardName) //�M�Υd�P�ĪG
    {
        if(cardName == "�]�N�v")
        {
            //�W�[ 100% ��q��_�q�A�g�bplayerInfo��
        }
        if (cardName == "�k���q")
        {
            //�C���_ 1 �I�ͩR�A�g�bplayerInfo��
        }
        if (cardName == "�c�]")
        {
            GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().CritRatePlus += 15; //�[ 15% �z���v
            GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().CritDamagePlus += 30; //�[ 30% �z���ˮ`
        }
        if (cardName == "�ӦZ")
        {
            GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().DamageMultiplying += 0.25f; //�[25%�����O
        }
        if (cardName == "�ӫ�")
        {
            /*
             �ݩʮĪG�W�j
            ��:�����ĤH��g�X3�o�p�b��
            ��:�ˮ`1.5��
            �r:�r���W�j1���A�ˮ`������
            �p:�p���d��W�[�A�ƶq�W�[ �ˮ`������
            �B:������2��
            //�g�bbulletScript�M�b�ڪ����٦�weaponShoot��
             */
        }
        if (cardName == "�Ь�")
        {
            GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().EnergyNumberPlus += 5; //�[5�I�̤j��q
        }
        if (cardName == "�ʤH")
        {
            //�b5���S�Q�������ܡA�W�[ 50 % �ˮ`
            //�g�bplayerinfo�MequimentCal��
        }
        if (cardName == "�Ԩ�")
        {
            //�l�[�@�o�B�~�b�ڡA�g�bweaponshoot��
        }
        if (cardName == "�O�q")
        {
            //�g���ֿn�p�ơA�F��T�h�ɡA�U�������ˮ`�⭿�A�g�bweaponshoot��

        }
        if (cardName == "����")
        {
            //�W�O�ɥͦ��@�h����쪺�@�n�A�i�H��פ@�������A�}�a��i�J�N�o20��
            //�g�bPlayerInfo�MPlayerMovement
        }
        if (cardName == "�R�B����")
        {
            //�C10��1���ĪG�A�����ĪG�t���ĪG
            //�����w�z���B�z���ˮ`+100%
            //����֧����N�o�ɶ�0.2��A�B�L����q
            //����ֽĨ�N�o�ɶ�0.4��
            //���L�k�z��
            //���W�[�����N�o�ɶ�0.4��
            //�g�bPlayerInfo��
        }
        if (cardName == "���q")
        {
            //�����O�ܬ�1.5���A���O���쪺�ˮ`�ܬ�2��
            //�g�bplayerinfo�MequimentCal��
            JusticeDamageMultiplying = 1.5f; //�վ�����O
            JusticeGetHitDamageMultiplying = 2f; //�վ�����ˮ`
        }
        if (cardName == "�˦Q�H")
        {
            //����ˮ`�ɦ�10%���v�N�ˮ`�L�Ĩæ^�_���P��Ӷˮ`�q����q
            //�g�bplayerinfo
        }
        if (cardName == "����")
        {
            //��o1���_�����|�A���O�_�����`��q�|�ܬ��@�b
            //�g�bplayerinfo
        }
        if (cardName == "�`��")
        {
            //������30%���v�����ӯ�q
            //�g�bWeaponShoot
        }
        if (cardName == "��")
        {
            //�����R���ĤH�ɦ�3%���v�z���A�y��5%�`�ͩR�Ȫ��ˮ`�q�A���O�z���]�|������ۤv
            //�g�b�U�ت��a�l�u�W
        }
        if (cardName == "�P�P")
        {
            //�����R���ĤH�ɦ�10 % ���v�^�_�ۤv�ͩR��(�^�_�q���y���ˮ`��3 %)
            //�g�bbulletScript
        }
        if (cardName == "��G")
        {
            //��q�V�֡A���쪺�ˮ`�V�C
            //�g�bplayerinfo
        }
        if (cardName == "�Ӷ�")
        {
            //��q�V�֡A�����O���ɶV�h
            //�g�bplayerinfo
        }

    }
}
