using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 offset = new Vector3(0f, 0f, -10f);
    float smoothTime = 0.25f;
    Vector3 velocity = Vector3.zero;

    Transform target;

    public Vector2 minLimit = new Vector2(-10, -10);
    public Vector2 maxLimit = new Vector2(10, 10);

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;

        
    }
    private void Update()
    {
        minLimit = GameObject.FindWithTag("CameraLimit").GetComponent<CameraLockLimit>().minLimit;
        maxLimit = GameObject.FindWithTag("CameraLimit").GetComponent<CameraLockLimit>().maxLimit;
    }
    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minLimit.x, maxLimit.x),
            Mathf.Clamp(transform.position.y, minLimit.y, maxLimit.y),
            transform.position.z);
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position += new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        //transform.position = originalPos;
    }
}
