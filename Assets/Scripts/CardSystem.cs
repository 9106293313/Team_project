using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardSystem
{
    public static float JusticeDamageMultiplying = 1f; //正義卡傷害倍率，預設為1
    public static float JusticeGetHitDamageMultiplying = 1f; //正義卡受擊傷害倍率，預設為1
    public static float StarTriggerProbability = 0.1f; //星星卡的觸發機率10%
    public static float StarHealPercentage = 0.03f; //星星卡的回復量百分比3%
    public static float TowerTriggerProbability = 0.03f; //塔卡的觸發機率3%
    public static float TowerDamagePercentage = 0.05f; //塔卡的爆炸傷害為5%
    public static bool Deathbool = false; //死神卡是否已經被觸發(復活)過了
    public static float TemperanceTriggerProbability = 0.3f; //節制卡的觸發機率30%

    public static void ResetCardInfo()
    {
        JusticeDamageMultiplying = 1f; //正義卡傷害倍率，預設為1
        JusticeGetHitDamageMultiplying = 1f; //正義卡受擊傷害倍率，預設為1
        StarTriggerProbability = 0.1f; //星星卡的觸發機率10%
        StarHealPercentage = 0.03f; //星星卡的回復量百分比3%
        TowerTriggerProbability = 0.03f; //塔卡的觸發機率3%
        TowerDamagePercentage = 0.05f; //塔卡的爆炸傷害為5%
        Deathbool = false; //死神卡是否已經被觸發(復活)過了
        TemperanceTriggerProbability = 0.3f; //節制卡的觸發機率30%
    }

    public static List<string> acquiredCards = new List<string>(); // 存儲已獲得的卡牌名稱的列表
    private static List<string> allCards = new List<string>() 
    { 
        "魔術師", 
        "女祭司", 
        "惡魔", 
        "皇后", 
        "皇帝", 
        "教皇", 
        "戀人", 
        "戰車", 
        "力量", 
        "隱者", 
        "命運之輪", 
        "正義", 
        "倒吊人", 
        "死神", 
        "節制", 
        "塔", 
        "星星", 
        "月亮", 
        "太陽" 
    }; // 包含所有卡牌的列表

    // 獲得卡牌
    public static void AcquireCard(string cardName)
    {
        if (!acquiredCards.Contains(cardName))
        {
            acquiredCards.Add(cardName);
            CardEffect(cardName);
        }
    }

    // 判斷玩家是否擁有指定的卡牌
    public static bool HasCard(string cardName)
    {
        return acquiredCards.Contains(cardName);
    }

    // 獲取所有卡牌列表
    public static List<string> GetAllCards()
    {
        return allCards;
    }

    public static void CardEffect(string cardName) //套用卡牌效果
    {
        if(cardName == "魔術師")
        {
            //增加 100% 能量恢復量，寫在playerInfo內
        }
        if (cardName == "女祭司")
        {
            //每秒恢復 1 點生命，寫在playerInfo內
        }
        if (cardName == "惡魔")
        {
            GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().CritRatePlus += 15; //加 15% 爆擊率
            GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().CritDamagePlus += 30; //加 30% 爆擊傷害
        }
        if (cardName == "皇后")
        {
            GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().DamageMultiplying += 0.25f; //加25%攻擊力
        }
        if (cardName == "皇帝")
        {
            /*
             屬性效果增強
            風:擊中敵人後射出3發小箭矢
            火:傷害1.5倍
            毒:毒雲增大1倍，傷害略提升
            雷:雷擊範圍增加，數量增加 傷害略提升
            冰:分裂數2倍
            //寫在bulletScript和箭矢物件還有weaponShoot裡
             */
        }
        if (cardName == "教皇")
        {
            GameObject.FindWithTag("Player").GetComponent<EquipmentCal>().EnergyNumberPlus += 5; //加5點最大能量
        }
        if (cardName == "戀人")
        {
            //在5秒內沒被攻擊的話，增加 50 % 傷害
            //寫在playerinfo和equimentCal裡
        }
        if (cardName == "戰車")
        {
            //追加一發額外箭矢，寫在weaponshoot裡
        }
        if (cardName == "力量")
        {
            //射擊累積計數，達到三層時，下次攻擊傷害兩倍，寫在weaponshoot裡

        }
        if (cardName == "隱者")
        {
            //蓄力時生成一層全方位的護罩，可以抵擋一次攻擊，破壞後進入冷卻20秒
            //寫在PlayerInfo和PlayerMovement
        }
        if (cardName == "命運之輪")
        {
            //每10秒換1次效果，正面效果負面效果
            //Ⅰ必定爆擊且爆擊傷害+100%
            //Ⅱ減少攻擊冷卻時間0.2秒，且無限能量
            //Ⅲ減少衝刺冷卻時間0.4秒
            //Ⅳ無法爆擊
            //Ⅴ增加攻擊冷卻時間0.4秒
            //寫在PlayerInfo內
        }
        if (cardName == "正義")
        {
            //攻擊力變為1.5倍，但是受到的傷害變為2倍
            //寫在playerinfo和equimentCal裡
            JusticeDamageMultiplying = 1.5f; //調整攻擊力
            JusticeGetHitDamageMultiplying = 2f; //調整受擊傷害
        }
        if (cardName == "倒吊人")
        {
            //受到傷害時有10%機率將傷害無效並回復等同於該傷害量的血量
            //寫在playerinfo
        }
        if (cardName == "死神")
        {
            //獲得1次復活機會，但是復活後總血量會變為一半
            //寫在playerinfo
        }
        if (cardName == "節制")
        {
            //攻擊時30%機率不消耗能量
            //寫在WeaponShoot
        }
        if (cardName == "塔")
        {
            //攻擊命中敵人時有3%機率爆炸，造成5%總生命值的傷害量，但是爆炸也會攻擊到自己
            //寫在各種玩家子彈上
        }
        if (cardName == "星星")
        {
            //攻擊命中敵人時有10 % 機率回復自己生命值(回復量為造成傷害的3 %)
            //寫在bulletScript
        }
        if (cardName == "月亮")
        {
            //血量越少，受到的傷害越低
            //寫在playerinfo
        }
        if (cardName == "太陽")
        {
            //血量越少，攻擊力提升越多
            //寫在playerinfo
        }

    }
}
