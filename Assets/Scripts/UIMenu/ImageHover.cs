using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject cardInfoPanel;
    public string CardName; //�o�i�d���W��

    private Image image;

    public bool IsNotCard = false;//�O�_���O��ù�P���d

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
        // ��ܥd���T�����O
        cardInfoPanel.SetActive(true);
    }

    public void HideCardInfo()
    {
        // ���åd���T�����O
        cardInfoPanel.SetActive(false);
    }
}
