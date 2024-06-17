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
        // ?算子?的移?方向
        Vector3 direction = (transform.position - previousPosition).normalized;

        // 如果子?有移?
        if (direction != Vector3.zero)
        {
            // ?算旋?角度
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // ?置子?的旋?角度
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        // 更新前一?的位置
        previousPosition = transform.position;
    }
}
