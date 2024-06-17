using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArrowSplit : MonoBehaviour
{
    public float existTime = 5f;
    float Timer = 0;
    public int Damage = 10;
    public float PoisonTime = 3f;
    public GameObject bullet;
    public float bulletSpeed = 12f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerInfo>().TakeDamage(Damage);
            collision.GetComponent<PlayerInfo>().TakePoisonDamage(PoisonTime);
        }
    }
    private void Start()
    {
        Destroy(gameObject, existTime);
    }
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer > 0.5f)
        {
            Timer = 0;
            GameObject bulletObj = Instantiate(bullet, transform.position, transform.rotation);
            GameObject bulletObj2 = Instantiate(bullet, transform.position, transform.rotation);
            bulletObj.transform.Rotate(0, 0, 90);
            bulletObj2.transform.Rotate(0, 0, -90);

            bulletObj.GetComponent<Rigidbody2D>().AddForce(bulletObj.transform.right * bulletSpeed, ForceMode2D.Impulse);
            bulletObj2.GetComponent<Rigidbody2D>().AddForce(bulletObj2.transform.right * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
