using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpact : MonoBehaviour
{
    public int Damage;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag==("Player"))
        {
            collision.GetComponent<PlayerInfo>().TakeDamage(Damage);
        }
        
    }
}
