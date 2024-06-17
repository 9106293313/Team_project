using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class RotatetoMouse : MonoBehaviour
{
    public float speed= 100f ;
    public GameObject playerSprite;
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

        
        Vector3 theScale = playerSprite.transform.localScale;
        if (direction.x>=0)
        {
            theScale.x = 1;
        }
        else
        {
            theScale.x = -1;
        }
        playerSprite.transform.localScale = theScale;

    }

}
