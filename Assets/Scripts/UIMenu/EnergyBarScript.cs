using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarScript : MonoBehaviour
{
    public int EnergyNumber = 1;
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


    void Update()
    {
        if (GameObject.FindWithTag("Player").GetComponent<PlayerInfo>().maxEnergy >= EnergyNumber)
        {
            if (GameObject.FindWithTag("Player").GetComponent<PlayerInfo>().curEnergy >= EnergyNumber)
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
