using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumberEft:MonoBehaviour
{
    public static void ShowFloatingTextEft(GameObject FloatingTextPrefeb,Collider2D collision,Transform transform,int Damage)
    {
        if (collision.gameObject.GetComponent<BossInfo>() == null)
        {
            GameObject go = Instantiate(FloatingTextPrefeb, transform.position, Quaternion.identity);
            go.GetComponent<TextMeshPro>().text = Damage.ToString();
        }
        else
        {
            if (collision.gameObject.GetComponent<BossInfo>().CanTakeDamage == true)
            {
                GameObject go = Instantiate(FloatingTextPrefeb, transform.position, Quaternion.identity);
                go.GetComponent<TextMeshPro>().text = Damage.ToString();
            }
        }
    }
}
