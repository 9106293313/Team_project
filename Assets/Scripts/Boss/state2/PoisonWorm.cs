using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoisonWorm : MonoBehaviour
{
    public int Damage = 10;
    public float existTime = 3f;
    public float PoisonTime = 3f;
    public GameObject FloatingTextPrefeb;
    public GameObject PoisonCloud;

    public GameObject targ;
    Rigidbody2D rb;
    public float speed1 = 8f;
    public float speed2 = 16f;
    float speed;
    float rotateSpeed = 200f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, existTime);
        targ = GameObject.FindWithTag("Player");

        speed = Random.Range(speed1, speed2);
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
            collision.GetComponent<PlayerInfo>().TakeDamage(Damage);
            collision.GetComponent<PlayerInfo>().TakePoisonDamage(PoisonTime);

            Destroy(gameObject);
        }
        if (collision.tag == "Grid")
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        Instantiate(PoisonCloud, transform.position, Quaternion.identity);
    }
}
