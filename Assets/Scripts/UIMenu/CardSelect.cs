using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardSelect : MonoBehaviour
{
    private string targetSceneName;//��ܧ��d�P�����쪺����

    public AudioSource CardFlip, CardHover, CardLeave;
    GameObject FadeObj;
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;

    public Sprite �]�N�v, �k���q, �c�], �ӦZ, �ӫ�, �Ь�, �ʤH, �Ԩ�, �O�q, ����, �R�B����, ���q, �˦Q�H, ����, �`��, ��, �P�P, ��G, �Ӷ�;
    public Sprite CardBack;

    public List<string> RemoveCards = new List<string>();
    Vector2 TPpos;
    void Start()
    {
        FadeObj = GameObject.FindWithTag("FadeObj").transform.Find("FadeObj").gameObject;

        RandomizeCards();
        CardFlip.Play();

        GameObject.FindWithTag("GameManager").GetComponent<Canvas>().enabled = false; //���ê��a��UI
    }
    private void Update()
    {
        targetSceneName = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().targetSceneName;
        TPpos = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().TPpos;
    }

    // �H���]�m�d�P�Ϥ�
    public void RandomizeCards()
    {
        List<string> availableCards = new List<string>();

        // �N�Ҧ�����o���d�P�K�[�� availableCards �C��
        foreach (string card in CardSystem.GetAllCards())
        {
            if (!CardSystem.HasCard(card))
            {
                availableCards.Add(card);
            }
        }

        availableCards.RemoveAll(card => RemoveCards.Contains(card)); //����RemoveCards�C���]�t���d��

        // �M�� panel �����T�� image �l����
        for (int i = 0; i < 3; i++)
        {
            // �T�O�٦��i�Ϊ��d�P
            if (availableCards.Count > 0)
            {
                // �ͦ��@���H���Ƨ@������
                int randomIndex = Random.Range(0, availableCards.Count);

                // ��ܤ@�i�d�P�ñq availableCards �C���R��
                string selectedCard = availableCards[randomIndex];
                availableCards.RemoveAt(randomIndex);

                // �ھگ��޳]�m panel �������� image �l���󪺹Ϥ��M��r
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
                // �p�G�i�Υd�P�����A�i�H�]�m�@�ӹw�]�Ϥ��αĨ���L�B�z�覡
                Debug.Log("Not enough available cards.");
                break;
            }
        }
    }

    private Sprite GetCardSprite(string cardName)
    {
        switch (cardName)
        {
            case "�]�N�v":
                return �]�N�v;
            case "�k���q":
                return �k���q;
            case "�c�]":
                return �c�];
            case "�ӦZ":
                return �ӦZ;
            case "�ӫ�":
                return �ӫ�;
            case "�Ь�":
                return �Ь�;
            case "�ʤH":
                return �ʤH;
            case "�Ԩ�":
                return �Ԩ�;
            case "�O�q":
                return �O�q;
            case "����":
                return ����;
            case "�R�B����":
                return �R�B����;
            case "���q":
                return ���q;
            case "�˦Q�H":
                return �˦Q�H;
            case "����":
                return ����;
            case "�`��":
                return �`��;
            case "��":
                return ��;
            case "�P�P":
                return �P�P;
            case "��G":
                return ��G;
            case "�Ӷ�":
                return �Ӷ�;
            default:
                return CardBack; // �Y�S��������sprite�A�h��^Cardback
        }
    }

    private string GetCardIntro(string cardName)
    {
        switch (cardName)
        {
            case "�]�N�v":
                return "�W�[ <color=yellow>100%</color> ��q��_�q";
            case "�k���q":
                return "<color=yellow>5</color>������������h\r\n�C���_ <color=yellow>1</color> �I�ͩR";
            case "�c�]":
                return "�W�[ <color=yellow>15%</color> �z���v \r\n<color=yellow>30%</color> �z���ˮ`";
            case "�ӦZ":
                return "�����O�W��<color=yellow>25%</color>";
            case "�ӫ�":
                return "�ݩʮĪG�W�j\r\n<b><color=#1CFFC0>��</color></b>:�����ĤH��g�X<color=#1CFFC0>3</color>�o�p�b��\r\n<b><color=red>��</color></b>:�ˮ`<color=red>1.5��</color>\r\n<b><color=#ED5AFF>�r</color></b>:�r���W�j<color=#ED5AFF>1</color>���A�ˮ`������\r\n<b><color=yellow>�p</color></b>:�p���d��W�[�A�ƶq�W�[�A�ˮ`������\r\n<b><color=#42C1FF>�B</color></b>:������<color=#42C1FF>2</color>��\r\n";
            case "�Ь�":
                return "�W�[ <color=yellow>5</color> �I�̤j��q";
            case "�ʤH":
                return "<color=yellow>5</color>������������h�W�[ <color=yellow>35%</color> �ˮ`";
            case "�Ԩ�":
                return "�����l�[<color=yellow>1</color>�o�B�~�b��";
            case "�O�q":
                return "�����ֿn�p�ơA�F��<color=yellow>3</color>�h��\r\n�U�������ˮ`<color=yellow>2</color>��";
            case "����":
                return "�W�O�ɥͦ��@�h�@�n\r\n�i�H���<color=yellow>1</color>������\r\n�Q�}�a��i�J�N�o<color=yellow>20</color>��";
            case "�R�B����":
                return "�C10��q�H�U�ĪG���ܤ@\r\n<color=#00EAFF>���w�z��</color>�B�z���ˮ`+<color=yellow>50%</color>\r\n��֧����N�o�ɶ�<color=yellow>0.4</color>��B�L����q\r\n��ֽĨ�N�o�ɶ�<color=yellow>0.4</color>��\r\n<color=red>�L�k�z��</color>\r\n�W�[�����N�o�ɶ�<color=red>0.4</color>��";
            case "���q":
                return "�����O�ܬ�<color=yellow>1.5</color>��\r\n���O���쪺�ˮ`�ܬ�<color=yellow>2</color>��";
            case "�˦Q�H":
                return "����ˮ`��<color=yellow>10%</color>���vĲ�o <color=#00EAFF>\"����\"\r\n</color>\r\n�N�ˮ`�L�Ĩæ^�_���P��ˮ`�q����q";
            case "����":
                return "��o1���_�����|\r\n�_�����`��q�|�ܬ��@�b";
            case "�`��":
                return "������<color=yellow>30%</color>���v�����ӯ�q";
            case "��":
                return "��ĤH�y���ˮ`�ɦ�<color=yellow>3%</color>���v�z��\r\n�y��<color=yellow>5%</color>�`�ͩR�Ȫ��ˮ`�q\r\n(�z���]�|������ۤv)";
            case "�P�P":
                return "��ĤH�y���ˮ`��\r\n��<color=yellow> 10%</color>���v�^�_�ۤv�ͩR��\r\n(�^�_�q���y���ˮ`��<color=yellow>3%</color>)";
            case "��G":
                return "��q�V��\r\n���쪺�ˮ`�V�C";
            case "�Ӷ�":
                return "��q�V��\r\n�����O���ɶV�h\r\n(��q�ʤ���p��10%��\r\n���|�A�~�򴣤ɧ����O)";
            default:
                return "null"; // �Y�S�������������A�h��^null
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
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().TotalSceneNum += 1;//���������+1
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
