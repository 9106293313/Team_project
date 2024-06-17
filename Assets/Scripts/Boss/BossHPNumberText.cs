using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossHPNumberText : MonoBehaviour
{
    public BossInfo BossInfo;

    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = (BossInfo.curHealth.ToString() + "/" + BossInfo.maxHealth.ToString());
    }
}
