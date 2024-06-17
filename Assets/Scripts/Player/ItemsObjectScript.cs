using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsObjectScript : MonoBehaviour
{
    public GameObject MainObj;
    public int ItemID = 0;
    public GameObject FButton;
    private void Start()
    {
        FButton.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            FButton.SetActive(true);

            if (Input.GetKey(KeyCode.F))
            {
                GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayerScript>().PlayPickUpItemSound(); //¼½©ñ­µ®Ä
                //collision.GetComponent<PlayerItemsScript>().GetItem(ItemID);
                Destroy(MainObj);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FButton.SetActive(false);
        }
    }
}
