using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet3 : MonoBehaviour
{
    public float existTime = 3f;
    float Timer = 0;
    public float PoisonTime = 3f;
    public GameObject FloatingTextPrefeb;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerInfo>().TakePoisonDamage(PoisonTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > existTime)
        {
            GetComponent<Animator>().SetTrigger("Destroy");
        }
    }
    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
