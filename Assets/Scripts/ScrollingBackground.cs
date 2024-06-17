using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 1.0f; // �I��??�t��
    private float backgroundWidth;
    public float PlacePos;

    void Start()
    {
        // ?���I����?��
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        backgroundWidth = spriteRenderer.bounds.size.x;
    }

    void Update()
    {
        // ��?�I��
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        // �p�G�I�����X�̹���?�A���s��m�b�k?
        if (transform.position.x < -backgroundWidth)
        {
            Vector3 newPos = new Vector3(PlacePos, transform.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}
