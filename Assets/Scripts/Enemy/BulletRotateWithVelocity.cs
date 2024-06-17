using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRotateWithVelocity : MonoBehaviour
{
    private Vector3 previousPosition;
    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        // ?��l?����?��V
        Vector3 direction = (transform.position - previousPosition).normalized;

        // �p�G�l?����?
        if (direction != Vector3.zero)
        {
            // ?���?����
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // ?�m�l?����?����
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        // ��s�e�@?����m
        previousPosition = transform.position;
    }
}
