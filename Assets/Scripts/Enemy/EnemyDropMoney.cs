using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropMoney : MonoBehaviour
{
    public int MoneyNum;

    private void OnDestroy()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerInfo>().money += MoneyNum;
    }
}
