using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarScript : MonoBehaviour
{
    public int StaminaNumber = 1;
    [SerializeField] private Image myImage;
    [SerializeField] private Color myColor;
    [SerializeField] private Color myColor2;
    [SerializeField] private Color myColor3;
    private void Start()
    {
        myColor.a = 1f;
        myColor2.a = 0.4f;
        myColor3.a = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Player").GetComponent<PlayerInfo>().maxStamina >= StaminaNumber)
        {
            if (GameObject.FindWithTag("Player").GetComponent<PlayerInfo>().curStamina >= StaminaNumber)
            {
                myImage.color = myColor;
            }
            else
            {
                myImage.color = myColor2;
            }
        }
        else
        {
            myImage.color = myColor3;
        }
    }
}
