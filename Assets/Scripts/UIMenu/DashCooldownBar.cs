using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCooldownBar : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Color Basecolors;
    public Color HighLightColor;
    public Image border;
    void Update()
    {
        

        if (playerMovement.charging)
        {
            Slider slider = GetComponent<Slider>();
            ColorBlock cb = slider.colors;

            if (playerMovement.Dashcooldown >= playerMovement.DashcooldownLimit)
            {
                cb.normalColor = new Color(HighLightColor.r, HighLightColor.g, HighLightColor.b, 0f); // 變更顏色，設定透明度為0
                slider.colors = cb;
                slider.value = 1;

                border.enabled = false;
            }
            else
            {
                
                cb.normalColor = new Color(Basecolors.r, Basecolors.g, Basecolors.b, 1f); // 設定透明度為1
                slider.colors = cb;
                slider.value = playerMovement.Dashcooldown / playerMovement.DashcooldownLimit;

                border.enabled = true;
            }

        }
        else
        {
            Slider slider = GetComponent<Slider>();
            ColorBlock cb = slider.colors;
            cb.normalColor = new Color(Basecolors.r, Basecolors.g, Basecolors.b, 0f); // 設定透明度為0
            slider.colors = cb;

            border.enabled = false;
        }

        //this.transform.position = Input.mousePosition + new Vector3(10f, -40f, 0f);
    }
}
