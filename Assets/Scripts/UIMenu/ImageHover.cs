using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject cardInfoPanel;
    public string CardName; //這張卡的名稱

    private Image image;

    public bool IsNotCard = false;//是否不是塔羅牌的卡

    private void Start()
    {
        image = GetComponent<Image>(); 

        HideCardInfo();
    }

    void Update()
    {
        if(IsNotCard == false)
        {
            if (!CardSystem.HasCard(CardName))
            {
                Color color = image.color;
                color.a = 0.3f;
                image.color = color;
            }
            else
            {
                Color color = image.color;
                color.a = 1f;
                image.color = color;
            }
        }
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(IsNotCard == false)
        {
            if (CardSystem.HasCard(CardName))
            {
                ShowCardInfo();
            }
        }
        else
        {
            ShowCardInfo();
        }
         
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideCardInfo();
    }

    public void ShowCardInfo()
    {
        // 顯示卡片訊息面板
        cardInfoPanel.SetActive(true);
    }

    public void HideCardInfo()
    {
        // 隱藏卡片訊息面板
        cardInfoPanel.SetActive(false);
    }
}
