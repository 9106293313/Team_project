using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipImage : MonoBehaviour
{
    public Image image;
    public Sprite frontSprite; // �����Ϲ�
    public Sprite backSprite; // �I���Ϲ�

    Vector3 DefaultScale;


    private void Start()
    {
        DefaultScale = transform.localScale;
    }
    void Update()
    {
        if (transform.rotation.eulerAngles.y >= 90f && transform.rotation.eulerAngles.y <= 270f)
        {
            image.sprite = backSprite;
            transform.localScale = new Vector3(DefaultScale.x * -1f, DefaultScale.y, DefaultScale.z);
        }
        else
        {
            image.sprite = frontSprite;
            transform.localScale = new Vector3(DefaultScale.x, DefaultScale.y, DefaultScale.z);
        }
        

    }

}
