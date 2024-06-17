using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLifeFruit : MonoBehaviour
{
    public float speed = 10f;
    float rotateSpeed = 200f;
    Rigidbody2D rb;
    public GameObject targ;

    public float HealNumber = 0.1f; //回復的比例，0.1等於10%

    public float ExistTime = 20f;
    public GameObject SoundEft;
    public GameObject SoundEftEnemy;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject,ExistTime);
        targ = GameObject.Find("HealHitbox");
    }

    private void FixedUpdate()
    {
        Vector3 pos = this.transform.position;
        float dist = float.PositiveInfinity;


            var d = (pos - targ.transform.position).sqrMagnitude;
            if (d < dist)
            {
                dist = d;
            }


        /////////////

        rotateSpeed = speed * 40f;


            if (targ != null)
            {
                Vector2 direction = (Vector2)targ.transform.position - rb.position;

                direction.Normalize();

                float rotateAmount = Vector3.Cross(direction, transform.right).z;

                rb.angularVelocity = -rotateAmount * rotateSpeed;

                rb.velocity = transform.right * speed;
            }
            else
            {
                rb.velocity = transform.right * speed;
                rb.angularVelocity = 0;
            }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerInfo A = collision.GetComponent<PlayerInfo>();

            if(A.curHealth > 0)
            {
                A.Heal(Convert.ToInt16(A.maxHealth * HealNumber));
            }

            Instantiate(SoundEft);
            Destroy(gameObject);

        }
        if (collision.gameObject.GetComponent<TreeHealHitBox>())
        {
            BossInfo A = collision.GetComponent<TreeHealHitBox>().bossInfo;

            if(A.curHealth > 0)
            {
                A.Heal(Convert.ToInt16(A.maxHealth * HealNumber));
            }

            Instantiate(SoundEftEnemy);
            Destroy(gameObject);

        }
    }
}
