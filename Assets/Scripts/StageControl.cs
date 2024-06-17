using TMPro;
using UnityEngine;

public class StageControl : MonoBehaviour
{
    public GameObject[] events; // �U�Өƥ�
    float SpawnTimer = 0;
    public GameObject SceneDoor;

    public GameObject floatingText;
    bool floatingTextBool=false;
    public GameObject MiniBossfloatingText;
    public enum StageType
    {
        �p�����d,
        �pboss���d
    }
    public StageType stageType;
    
    [Header("����ɶ�(�p�����d�~�ݭn)")]public float MaxTime = 30f;
    float timer;
    public TextMeshProUGUI TimerTextObj;
    bool StageClear=false;

    void Start()
    {
        if (events.Length > 0)
        {
            int randomIndex = Random.Range(0, events.Length); // �H����ܤ@�Өƥ�

            // �ҥο�ܪ��ƥ�
            events[randomIndex].SetActive(true);
        }
        if(stageType == StageType.�p�����d)
        {
            GameObject FT = Instantiate(floatingText); //�ͦ����ܦr
            FT.GetComponentInChildren<TextMeshProUGUI>().text = "�b�ɶ������ѼĤH��o�B�~���y";
        }

        timer = MaxTime;
    }

    void Update()
    {
        if(stageType == StageType.�p�����d)
        {
            if (StageClear == false)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else if (floatingTextBool == false)
                {
                    GameObject FT = Instantiate(floatingText); //�ͦ����ܦr
                    FT.GetComponentInChildren<TextMeshProUGUI>().text = "�D�ԥ���";
                    floatingTextBool = true;
                }
            }
            else
            {
                TimerTextObj.enabled = false;
                if (timer > 0 && floatingTextBool == false)
                {
                    GameObject FT = Instantiate(floatingText); //�ͦ����ܦr
                    FT.GetComponentInChildren<TextMeshProUGUI>().text = "�D�Ԧ��\";
                    floatingTextBool = true;
                }

            }


            if (TimerTextObj != null)
            {
                TimerTextObj.text = "�ɶ�:" + Mathf.RoundToInt(timer);
            }




            if (SpawnTimer <= 1f) //�i���d1���~��Ĳ�o
            {
                SpawnTimer += Time.deltaTime;
            }
            else
            {
                if (timer > 0) //�p�G�b����ɶ��������A�U���pboss�����v+50
                {
                    if (GameObject.FindWithTag("Enemy") == null && StageClear == false)
                    {
                        StageClear = true;
                        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().MiniBossPercent += 50;

                        GameObject FT = Instantiate(MiniBossfloatingText); //�ͦ����ܦr
                        int randomNum = UnityEngine.Random.Range(1, 5);
                        switch (randomNum)
                        {
                            case 1:
                                FT.GetComponentInChildren<TextMeshProUGUI>().text = "�@�����˪������b���v���n�ۧA...\n(�pboss�X�{���v+50%)";
                                break;
                            case 2:
                                FT.GetComponentInChildren<TextMeshProUGUI>().text = "���˪������O�b�Ů𤤾��E...\n(�pboss�X�{���v+50%)";
                                break;
                            case 3:
                                FT.GetComponentInChildren<TextMeshProUGUI>().text = "������M�I���F�西�b����...\n(�pboss�X�{���v+50%)";
                                break;
                            case 4:
                                FT.GetComponentInChildren<TextMeshProUGUI>().text = "�����B�ǨӤF�R�q�n...\n(�pboss�X�{���v+50%)";
                                break;
                        }
                    }
                }
                else
                {
                    if (GameObject.FindWithTag("Enemy") == null && StageClear == false)
                    {
                        StageClear = true;
                    }
                }

                if (GameObject.FindWithTag("Enemy") == null)
                {
                    SceneDoor.SetActive(true);
                }
            }
        }

        if (stageType == StageType.�pboss���d)
        {
            if (SpawnTimer <= 1f) //�i���d1���~��Ĳ�o
            {
                SpawnTimer += Time.deltaTime;
            }
            else
            {
                if (GameObject.FindWithTag("Enemy") == null)
                {
                    SceneDoor.SetActive(true);
                }
            }
        }
    }
}
