using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardSelect : MonoBehaviour
{
    private string targetSceneName;//選擇完卡牌後跳轉到的場景

    public AudioSource CardFlip, CardHover, CardLeave;
    GameObject FadeObj;
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;

    public Sprite 魔術師, 女祭司, 惡魔, 皇后, 皇帝, 教皇, 戀人, 戰車, 力量, 隱者, 命運之輪, 正義, 倒吊人, 死神, 節制, 塔, 星星, 月亮, 太陽;
    public Sprite CardBack;

    public List<string> RemoveCards = new List<string>();
    Vector2 TPpos;
    void Start()
    {
        FadeObj = GameObject.FindWithTag("FadeObj").transform.Find("FadeObj").gameObject;

        RandomizeCards();
        CardFlip.Play();

        GameObject.FindWithTag("GameManager").GetComponent<Canvas>().enabled = false; //隱藏玩家的UI
    }
    private void Update()
    {
        targetSceneName = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().targetSceneName;
        TPpos = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().TPpos;
    }

    // 隨機設置卡牌圖片
    public void RandomizeCards()
    {
        List<string> availableCards = new List<string>();

        // 將所有未獲得的卡牌添加到 availableCards 列表中
        foreach (string card in CardSystem.GetAllCards())
        {
            if (!CardSystem.HasCard(card))
            {
                availableCards.Add(card);
            }
        }

        availableCards.RemoveAll(card => RemoveCards.Contains(card)); //移除RemoveCards列表中包含的卡片

        // 遍歷 panel 中的三個 image 子物件
        for (int i = 0; i < 3; i++)
        {
            // 確保還有可用的卡牌
            if (availableCards.Count > 0)
            {
                // 生成一個隨機數作為索引
                int randomIndex = Random.Range(0, availableCards.Count);

                // 選擇一張卡牌並從 availableCards 列表中刪除
                string selectedCard = availableCards[randomIndex];
                availableCards.RemoveAt(randomIndex);

                // 根據索引設置 panel 中對應的 image 子物件的圖片和文字
                switch (i)
                {
                    case 0:
                        card1.transform.Find("Image").GetComponent<Image>().sprite = GetCardSprite(selectedCard);
                        card1.transform.Find("CardName").GetComponent<TextMeshProUGUI>().text = selectedCard;
                        card1.transform.Find("Intro").GetComponent<TextMeshProUGUI>().text = GetCardIntro(selectedCard);
                        card1.transform.Find("Image").GetComponent<FlipImage>().frontSprite = GetCardSprite(selectedCard);
                        break;
                    case 1:
                        card2.transform.Find("Image").GetComponent<Image>().sprite = GetCardSprite(selectedCard);
                        card2.transform.Find("CardName").GetComponent<TextMeshProUGUI>().text = selectedCard;
                        card2.transform.Find("Intro").GetComponent<TextMeshProUGUI>().text = GetCardIntro(selectedCard);
                        card2.transform.Find("Image").GetComponent<FlipImage>().frontSprite = GetCardSprite(selectedCard);
                        break;
                    case 2:
                        card3.transform.Find("Image").GetComponent<Image>().sprite = GetCardSprite(selectedCard);
                        card3.transform.Find("CardName").GetComponent<TextMeshProUGUI>().text = selectedCard;
                        card3.transform.Find("Intro").GetComponent<TextMeshProUGUI>().text = GetCardIntro(selectedCard);
                        card3.transform.Find("Image").GetComponent<FlipImage>().frontSprite = GetCardSprite(selectedCard);
                        break;
                }
            }
            else
            {
                // 如果可用卡牌不足，可以設置一個預設圖片或採取其他處理方式
                Debug.Log("Not enough available cards.");
                break;
            }
        }
    }

    private Sprite GetCardSprite(string cardName)
    {
        switch (cardName)
        {
            case "魔術師":
                return 魔術師;
            case "女祭司":
                return 女祭司;
            case "惡魔":
                return 惡魔;
            case "皇后":
                return 皇后;
            case "皇帝":
                return 皇帝;
            case "教皇":
                return 教皇;
            case "戀人":
                return 戀人;
            case "戰車":
                return 戰車;
            case "力量":
                return 力量;
            case "隱者":
                return 隱者;
            case "命運之輪":
                return 命運之輪;
            case "正義":
                return 正義;
            case "倒吊人":
                return 倒吊人;
            case "死神":
                return 死神;
            case "節制":
                return 節制;
            case "塔":
                return 塔;
            case "星星":
                return 星星;
            case "月亮":
                return 月亮;
            case "太陽":
                return 太陽;
            default:
                return CardBack; // 若沒有對應的sprite，則返回Cardback
        }
    }

    private string GetCardIntro(string cardName)
    {
        switch (cardName)
        {
            case "魔術師":
                return "增加 <color=yellow>100%</color> 能量恢復量";
            case "女祭司":
                return "<color=yellow>5</color>秒內未受到攻擊則\r\n每秒恢復 <color=yellow>1</color> 點生命";
            case "惡魔":
                return "增加 <color=yellow>15%</color> 爆擊率 \r\n<color=yellow>30%</color> 爆擊傷害";
            case "皇后":
                return "攻擊力上升<color=yellow>25%</color>";
            case "皇帝":
                return "屬性效果增強\r\n<b><color=#1CFFC0>風</color></b>:擊中敵人後射出<color=#1CFFC0>3</color>發小箭矢\r\n<b><color=red>火</color></b>:傷害<color=red>1.5倍</color>\r\n<b><color=#ED5AFF>毒</color></b>:毒雲增大<color=#ED5AFF>1</color>倍，傷害略提升\r\n<b><color=yellow>雷</color></b>:雷擊範圍增加，數量增加，傷害略提升\r\n<b><color=#42C1FF>冰</color></b>:分裂數<color=#42C1FF>2</color>倍\r\n";
            case "教皇":
                return "增加 <color=yellow>5</color> 點最大能量";
            case "戀人":
                return "<color=yellow>5</color>秒內未受到攻擊則增加 <color=yellow>35%</color> 傷害";
            case "戰車":
                return "攻擊追加<color=yellow>1</color>發額外箭矢";
            case "力量":
                return "攻擊累積計數，達到<color=yellow>3</color>層時\r\n下次攻擊傷害<color=yellow>2</color>倍";
            case "隱者":
                return "蓄力時生成一層護罩\r\n可以抵擋<color=yellow>1</color>次攻擊\r\n被破壞後進入冷卻<color=yellow>20</color>秒";
            case "命運之輪":
                return "每10秒從以下效果中擇一\r\n<color=#00EAFF>必定爆擊</color>且爆擊傷害+<color=yellow>50%</color>\r\n減少攻擊冷卻時間<color=yellow>0.4</color>秒且無限能量\r\n減少衝刺冷卻時間<color=yellow>0.4</color>秒\r\n<color=red>無法爆擊</color>\r\n增加攻擊冷卻時間<color=red>0.4</color>秒";
            case "正義":
                return "攻擊力變為<color=yellow>1.5</color>倍\r\n但是受到的傷害變為<color=yellow>2</color>倍";
            case "倒吊人":
                return "受到傷害時<color=yellow>10%</color>機率觸發 <color=#00EAFF>\"反轉\"\r\n</color>\r\n將傷害無效並回復等同於傷害量的血量";
            case "死神":
                return "獲得1次復活機會\r\n復活後總血量會變為一半";
            case "節制":
                return "攻擊時<color=yellow>30%</color>機率不消耗能量";
            case "塔":
                return "對敵人造成傷害時有<color=yellow>3%</color>機率爆炸\r\n造成<color=yellow>5%</color>總生命值的傷害量\r\n(爆炸也會攻擊到自己)";
            case "星星":
                return "對敵人造成傷害時\r\n有<color=yellow> 10%</color>機率回復自己生命值\r\n(回復量為造成傷害的<color=yellow>3%</color>)";
            case "月亮":
                return "血量越少\r\n受到的傷害越低";
            case "太陽":
                return "血量越少\r\n攻擊力提升越多\r\n(血量百分比小於10%後\r\n不會再繼續提升攻擊力)";
            default:
                return "null"; // 若沒有對應的說明，則返回null
        }
    }
    public void OnCard1Click()
    {
        string cardName = card1.transform.Find("CardName").GetComponent<TextMeshProUGUI>().text;
        CardSystem.AcquireCard(cardName);
        StartCoroutine(ChangeScene());
    }
    public void OnCard2Click()
    {
        string cardName = card2.transform.Find("CardName").GetComponent<TextMeshProUGUI>().text;
        CardSystem.AcquireCard(cardName);
        StartCoroutine(ChangeScene());
    }
    public void OnCard3Click()
    {
        string cardName = card3.transform.Find("CardName").GetComponent<TextMeshProUGUI>().text;
        CardSystem.AcquireCard(cardName);
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        CardLeave.Play();
        FadeObj.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        FadeObj.GetComponent<Animator>().SetTrigger("Out");
        yield return new WaitForSeconds(0.5f);
        FadeObj.GetComponent<Animator>().SetTrigger("In");
        SceneManager.LoadScene(targetSceneName);
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().TotalSceneNum += 1;//跳轉場景時+1
        GameObject.FindWithTag("GameManager").GetComponent<Canvas>().enabled = true;
        GameObject.FindWithTag("Player").transform.position = new Vector3(TPpos.x, TPpos.y, 0);
        GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;

    }

    public void Card1PointerHover()
    {
        card1.GetComponent<Animator>().SetBool("Hover",true);
        CardHover.Play();
        card1.GetComponent<Animator>().SetBool("Leave", false);
    }
    public void Card1PointerLeave()
    {
        card1.GetComponent<Animator>().SetBool("Leave", true);
        card1.GetComponent<Animator>().SetBool("Hover", false);
    }
    public void Card2PointerHover()
    {
        card2.GetComponent<Animator>().SetBool("Hover", true);
        CardHover.Play();
        card2.GetComponent<Animator>().SetBool("Leave", false);
    }
    public void Card2PointerLeave()
    {
        card2.GetComponent<Animator>().SetBool("Leave", true);
        card2.GetComponent<Animator>().SetBool("Hover", false);
    }
    public void Card3PointerHover()
    {
        card3.GetComponent<Animator>().SetBool("Hover", true);
        CardHover.Play();
        card3.GetComponent<Animator>().SetBool("Leave", false);
    }
    public void Card3PointerLeave()
    {
        card3.GetComponent<Animator>().SetBool("Leave", true);
        card3.GetComponent<Animator>().SetBool("Hover", false);
    }

}
