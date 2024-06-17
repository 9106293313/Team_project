using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 1.0f; // 背景??速度
    private float backgroundWidth;
    public float PlacePos;

    void Start()
    {
        // ?取背景的?度
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        backgroundWidth = spriteRenderer.bounds.size.x;
    }

    void Update()
    {
        // 移?背景
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        // 如果背景移出屏幕左?，重新放置在右?
        if (transform.position.x < -backgroundWidth)
        {
            Vector3 newPos = new Vector3(PlacePos, transform.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}
