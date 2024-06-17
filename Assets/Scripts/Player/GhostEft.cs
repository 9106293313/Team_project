using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEft : MonoBehaviour
{
    public float ghostDelay;
    float ghostDelaySeconds;
    public GameObject ghost;
    public bool makeGhost = false;
    
    void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    
    void Update()
    {
        if(makeGhost)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                //¥Í¦¨´Ý¼v
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                currentGhost.transform.localScale = this.transform.localScale;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost,1f);
            }
        }
        
    }
}
