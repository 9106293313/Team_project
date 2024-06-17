using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorObject : MonoBehaviour
{

    [SerializeField] private CursorManagement.CursorType cursorType;
    private void OnMouseEnter()
    {
        CursorManagement.Instance.SetActiveCursorType(cursorType);
    }
    private void OnMouseExit()
    {
        CursorManagement.Instance.SetActiveCursorType(CursorManagement.CursorType.Arrow);
    }
}
