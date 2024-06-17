using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : MonoBehaviour
{
    public int curHealth;
    public int maxHealth = 100;
    public HealthBarScript healthBar;
    public GameObject HealthBarImage;
    public GameObject HealthBarBorder;
    float ToggleImageTime = 2f;
    float ToggleImageTimer;

    public bool IsShield = false;

    ///讓敵人受傷時變紅
    float hurtEftDuration = 0.2f;
    public SpriteRenderer spriteRenderer;
    private Color hurtColor;
    private Coroutine hurtCoroutine;
    public bool HasMainObj = false;
    public GameObject MainObj;


    void Start()
    {

        curHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        ToggleImageTimer = ToggleImageTime;

        //////////
        hurtColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        ToggleImageTimer += Time.deltaTime;
        if (curHealth <= 0 && HasMainObj==false) //正常情況，沒有主物件，不屬於boss中的一部分
        {
            if(IsShield==false)
            {
                Destroy(gameObject);
            }
            else
            {
                ShieldBreak();
            }
        }
        if (curHealth <= 0 && HasMainObj) //非通常情況，有主物件，屬於boss中的一部分
        {
            return;
        }


        if (ToggleImageTimer>=ToggleImageTime)
        {
            if(HealthBarImage.activeInHierarchy)
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
    public void TakeDamage(int damage)
    {
        if (curHealth == 0)
        {
            return;
        }
        if(HasMainObj)//如果有主物件(共用血條的boss)，刷新boss血條一次
        {
            MainObj.GetComponent<BossInfo>().healthBar.SetHealth(MainObj.GetComponent<BossInfo>().curHealth);
        }

        ToggleImageTimer = 0f;

        curHealth -= damage;

        healthBar.SetHealth(curHealth);

        //////////
        if (hurtCoroutine != null)
        {
            StopCoroutine(hurtCoroutine);
        }
        hurtCoroutine = StartCoroutine(HurtEffect());
    }

    public void ShieldBreak()
    {
        StartCoroutine(StartShieldBreak());
    }
    IEnumerator StartShieldBreak()
    {
        GetComponent<Animator>().SetTrigger("ShieldBreak");
        yield return new WaitForSeconds(0.5f);
        curHealth = maxHealth;
        gameObject.SetActive(false);
    }

    private IEnumerator HurtEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(hurtEftDuration);
        spriteRenderer.color = hurtColor;
    }
}
