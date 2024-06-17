using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrawLine : MonoBehaviour
{
    public Transform player; // 玩家位置
    public float lineLength = 5f; // 線的長度

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // 獲取玩家和滑鼠游標的位置
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // 計算線的方向和長度
        Vector3 direction = mousePosition - player.position;
        direction = Vector3.ClampMagnitude(direction, lineLength);

        // 設置線的起點和終點位置
        lineRenderer.SetPosition(0, player.position);
        lineRenderer.SetPosition(1, player.position + direction);
    }
}
