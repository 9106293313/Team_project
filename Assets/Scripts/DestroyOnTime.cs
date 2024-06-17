using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    public float TimeLimit = 5f;
    void Start()
    {
        Destroy(gameObject, TimeLimit);
    }
}
