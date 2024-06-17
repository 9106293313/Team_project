using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 3f;
    public Vector3 Offset = new Vector3(0, 1, 0);
    public float UpSpeed = 2f;
    public bool randomOffset = true;
    void Start()
    {
        if(randomOffset)
        {
            Offset = Offset + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.25f, 0.25f), 0);
        }
        Destroy(gameObject,destroyTime);

        transform.localPosition += Offset;
    }
    private void Update()
    {
        transform.position += new Vector3( 0,UpSpeed * Time.deltaTime,0);
    }

}
