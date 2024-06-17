using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRotate : MonoBehaviour
{
    public float rotateSpeed = 50f;

    void Update()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    public void SetRotateSpeed(float speed)
    {
        rotateSpeed = speed;
    }
}
