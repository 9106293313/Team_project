using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Animator animator;
    public AudioSource audioSource;
    public bool IsPlayer = false;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    
    public void SetHealth(int health)//sethealth同時播動畫
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);

        if (IsPlayer)
        {
            animator.SetTrigger("Damaged");
        }

        audioSource.Play();
    }
    public void SetHealth2(int health)//sethealth不播動畫
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);

    }
    public void AddMaxHealth(int num)
    {
        slider.maxValue += num;

        slider.value += num;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
