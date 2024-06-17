using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2PlayerShield : MonoBehaviour
{
    public bool PlayerIsSafe = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerIsSafe = true;
            Debug.Log("safe");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerIsSafe = false;
            Debug.Log("Notsafe");
        }
    }
}
