using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleFollow : MonoBehaviour
{
    public Transform target;
    public float delay = 0.1f;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        // 計算目標位置
        Vector3 targetPosition = target.position;

        // 延遲
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, delay);
    }
}
