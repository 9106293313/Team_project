using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanBullet : MonoBehaviour
{
    void Update()
    {
        Destroy(gameObject,1f);
        if(GameObject.FindWithTag("EnemyBullet")!=null)
        {
            Destroy(GameObject.FindWithTag("EnemyBullet"));
        }
        if (GameObject.Find("TreeFruit") != null)
        {
            Destroy(GameObject.Find("TreeFruit"));
        }
    }
}
