using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrawLine : MonoBehaviour
{
    public Transform player; // ���a��m
    public float lineLength = 5f; // �u������

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // ������a�M�ƹ���Ъ���m
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // �p��u����V�M����
        Vector3 direction = mousePosition - player.position;
        direction = Vector3.ClampMagnitude(direction, lineLength);

        // �]�m�u���_�I�M���I��m
        lineRenderer.SetPosition(0, player.position);
        lineRenderer.SetPosition(1, player.position + direction);
    }
}
