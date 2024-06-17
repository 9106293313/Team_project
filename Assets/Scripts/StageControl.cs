using TMPro;
using UnityEngine;

public class StageControl : MonoBehaviour
{
    public GameObject[] events; // 各個事件
    float SpawnTimer = 0;
    public GameObject SceneDoor;

    public GameObject floatingText;
    bool floatingTextBool=false;
    public GameObject MiniBossfloatingText;
    public enum StageType
    {
        小怪關卡,
        小boss關卡
    }
    public StageType stageType;
    
    [Header("限制時間(小怪關卡才需要)")]public float MaxTime = 30f;
    float timer;
    public TextMeshProUGUI TimerTextObj;
    bool StageClear=false;

    void Start()
    {
        if (events.Length > 0)
        {
            int randomIndex = Random.Range(0, events.Length); // 隨機選擇一個事件

            // 啟用選擇的事件
            events[randomIndex].SetActive(true);
        }
        if(stageType == StageType.小怪關卡)
        {
            GameObject FT = Instantiate(floatingText); //生成提示字
            FT.GetComponentInChildren<TextMeshProUGUI>().text = "在時間內擊敗敵人獲得額外獎勵";
        }

        timer = MaxTime;
    }

    void Update()
    {
        if(stageType == StageType.小怪關卡)
        {
            if (StageClear == false)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else if (floatingTextBool == false)
                {
                    GameObject FT = Instantiate(floatingText); //生成提示字
                    FT.GetComponentInChildren<TextMeshProUGUI>().text = "挑戰失敗";
                    floatingTextBool = true;
                }
            }
            else
            {
                TimerTextObj.enabled = false;
                if (timer > 0 && floatingTextBool == false)
                {
                    GameObject FT = Instantiate(floatingText); //生成提示字
                    FT.GetComponentInChildren<TextMeshProUGUI>().text = "挑戰成功";
                    floatingTextBool = true;
                }

            }


            if (TimerTextObj != null)
            {
                TimerTextObj.text = "時間:" + Mathf.RoundToInt(timer);
            }




            if (SpawnTimer <= 1f) //進關卡1秒後才能觸發
            {
                SpawnTimer += Time.deltaTime;
            }
            else
            {
                if (timer > 0) //如果在限制的時間內完成，下次小boss關機率+50
                {
                    if (GameObject.FindWithTag("Enemy") == null && StageClear == false)
                    {
                        StageClear = true;
                        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().MiniBossPercent += 50;

                        GameObject FT = Instantiate(MiniBossfloatingText); //生成提示字
                        int randomNum = UnityEngine.Random.Range(1, 5);
                        switch (randomNum)
                        {
                            case 1:
                                FT.GetComponentInChildren<TextMeshProUGUI>().text = "一雙陰森的眼睛在陰影中盯著你...\n(小boss出現機率+50%)";
                                break;
                            case 2:
                                FT.GetComponentInChildren<TextMeshProUGUI>().text = "異樣的元素力在空氣中凝聚...\n(小boss出現機率+50%)";
                                break;
                            case 3:
                                FT.GetComponentInChildren<TextMeshProUGUI>().text = "有什麼危險的東西正在接近...\n(小boss出現機率+50%)";
                                break;
                            case 4:
                                FT.GetComponentInChildren<TextMeshProUGUI>().text = "不遠處傳來了嘶吼聲...\n(小boss出現機率+50%)";
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

        if (stageType == StageType.小boss關卡)
        {
            if (SpawnTimer <= 1f) //進關卡1秒後才能觸發
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
