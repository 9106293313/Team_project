using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildFollowParentObj : MonoBehaviour
{
    void Update()
    {
        transform.SetPositionAndRotation(transform.parent.position, transform.parent.rotation);
    }
}
