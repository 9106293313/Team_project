using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSplitBullet : MonoBehaviour
{
    public float existTime = 3f;
    float Timer = 0;
    public int Damage = 10;
    public float PoisonTime = 3f;
    public GameObject bullet;
    public int SplitAngle = 45;
    public float bulletSpeed = 12f;
    public float bulletSize = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerInfo>().TakeDamage(Damage);
            collision.GetComponent<PlayerInfo>().TakePoisonDamage(PoisonTime);
        }
    }
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > existTime)
        {
            for (float i = 0; i < 360f; i += SplitAngle)
            {
                Vector3 direction = Quaternion.Euler(0f, 0f, i) * transform.right;
                GameObject bulletObj = Instantiate(bullet, transform.position, Quaternion.identity);
                bulletObj.transform.right = direction;

                bulletObj.transform.localScale = new Vector3(1*bulletSize,1*bulletSize, 1);

                bulletObj.GetComponent<Rigidbody2D>().AddForce(bulletObj.transform.right * bulletSpeed, ForceMode2D.Impulse);
            }
            Destroy(gameObject);
        }
    }
}
