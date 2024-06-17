using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossInfo : MonoBehaviour
{
    public int curHealth;
    public int maxHealth = 1000;
    public HealthBarScript healthBar;
    public bool CanTakeDamage = true;

    public bool DisplayHealthBar=true;
    [SerializeField] GameObject HealthBarImage;
    [SerializeField] GameObject HealthBarBorder;
    float ToggleImageTime = 2f;
    float ToggleImageTimer;
    ///////////////////////////
    ///讓敵人受傷時變紅
    public float hurtEftDuration = 0.2f;
    public SpriteRenderer spriteRenderer;
    private Color hurtColor;
    private Coroutine hurtCoroutine;


    void Start()
    {
        curHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        ToggleImageTimer = ToggleImageTime;
        //////////
        if(spriteRenderer!=null)
        {
            hurtColor = spriteRenderer.color;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DisplayHealthBar==false)
        {
            ToggleImageTimer += Time.deltaTime;

            if (ToggleImageTimer >= ToggleImageTime)
            {
                if (HealthBarImage.activeInHierarchy)
                {
                    HealthBarImage.SetActive(false);
                    HealthBarBorder.SetActive(false);
                }
            }
            else
            {
                if (!HealthBarImage.activeInHierarchy)
                {
                    HealthBarImage.SetActive(true);
                    HealthBarBorder.SetActive(true);
                }
            }
        }
        

        if (curHealth < 0)
        {
            curHealth = 0;
        }
        if(curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        if (CanTakeDamage)
        {
            curHealth -= damage;
            healthBar.SetHealth(curHealth);

            if(DisplayHealthBar == false)
            {
                ToggleImageTimer = 0f;
            }
            //////////
            if (hurtCoroutine != null)
            {
                StopCoroutine(hurtCoroutine);
            }
            hurtCoroutine = StartCoroutine(HurtEffect());
        }
    }
    private IEnumerator HurtEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(hurtEftDuration);
        spriteRenderer.color = hurtColor;
    }
    public void Heal(int damage)
    {
        curHealth += damage;
        healthBar.SetHealth(curHealth);

        if (DisplayHealthBar == false)
        {
            ToggleImageTimer = 0f;
        }
    }
    public void FullHeal()
    {
        StartCoroutine(StartFullHeal());
    }
    IEnumerator StartFullHeal()
    {
        for (int i = 0; i < 10; i++)
        {
            Heal(Convert.ToUInt16(maxHealth * 0.1f));
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
