using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseableEntity : MonoBehaviour
{
    public static readonly HashSet<ChaseableEntity> Entities = new HashSet<ChaseableEntity>();

    void Awake()
    {
        Entities.Add(this);
    }

    void OnDestroy()
    {
        Entities.Remove(this);
    }
}
