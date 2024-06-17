using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjMove : MonoBehaviour
{
    private Rigidbody2D rb2D;
    float Timer=0.1f;
    void Start()
    {
        rb2D = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Timer -= Time.deltaTime;
    }
    void FixedUpdate()
    {
        if(Timer>=0)
        {
            rb2D.AddForce(transform.up * 80f * Time.deltaTime, ForceMode2D.Impulse);
            float A = Random.Range(-15.0f, 15.0f);  // -15.0 ~ 15.0
            rb2D.AddForce(transform.right * A * Time.deltaTime, ForceMode2D.Impulse);
        }
        
    }
}
