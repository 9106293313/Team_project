using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingAtk : MonoBehaviour
{
    public float speed = 10f;
    float rotateSpeed = 200f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        /////////////尋找離的最近的且有ChaseableEntity這個Script的目標
        Vector3 pos = this.transform.position;
        float dist = float.PositiveInfinity;
        ChaseableEntity targ = null;
        foreach (var obj in ChaseableEntity.Entities)
        {
            var d = (pos - obj.transform.position).sqrMagnitude;
            if (d < dist)
            {
                targ = obj;
                dist = d;
            }
        }

        /////////////

        rotateSpeed = speed * 40f;

        if (GameObject.FindWithTag("Enemy") != null)
        {
            if(targ != null)
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
        else
        {
            rb.velocity = transform.right * speed;
            rb.angularVelocity = 0;
        }
        
    }


}
