using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss1Skill : MonoBehaviour
{
    public GameObject ShieldObj;
    bool ShieldOn =false;
    public BossInfo bossInfo;

    public Transform AtkPoint1;
    public Transform AtkPoint2;
    public GameObject BulletA;
    public float BulletASpeed = 10f;

    public GameObject BulletB;
    public float BulletBSpeed = 15f;

    public GameObject FirePillow;
    public GameObject BigFirePillow;
    public GameObject FirePillowWarning;

    public Boss1SoundEft Boss1SoundEft;

    GameObject Target;

    public treeBossSkill treeBossSkill;

    public EnemyAI enemyAI;

    public bool WaitForHeal = false;
    BossInfo TreeInfo;

    public GameObject TreeFruit;
    public GameObject FloatingText;

    void Start()
    {
        bossInfo = GetComponent<BossInfo>();
        ShieldObj.SetActive(false);
        Target = GameObject.FindWithTag("Player");

        TreeInfo = treeBossSkill.gameObject.GetComponent<BossInfo>();

    }

    // Update is called once per frame
    void Update()
    {
        if(bossInfo.curHealth<=0 && WaitForHeal==false)
        {
            
            
            WaitForHeal = true;
            GetComponentInChildren<Animator>().SetTrigger("SelfHeal");
            enemyAI.speed = 0;
            
            
        }
    }
    public void NormalAtk()
    {
        int i = UnityEngine.Random.Range(1, 4); //1~3
        if(i == 1)
        {StartCoroutine(NormalAtkA());}
        if (i == 2)
        { StartCoroutine(NormalAtkB()); }
        if (i == 3)
        { StartCoroutine(NormalAtkC()); }

    }
    IEnumerator NormalAtkA()
    {
        Boss1SoundEft.PlayNormalAtkSound();
        GameObject bullet01 = Instantiate(BulletA, AtkPoint1.position, AtkPoint1.rotation);
        bullet01.GetComponent<Rigidbody2D>().AddForce(bullet01.transform.right * BulletASpeed, ForceMode2D.Impulse);
        GameObject bullet02 = Instantiate(BulletA, AtkPoint1.position, AtkPoint1.rotation * Quaternion.Euler(0f, 0f, 25f));
        bullet02.GetComponent<Rigidbody2D>().AddForce(bullet02.transform.right * BulletASpeed, ForceMode2D.Impulse);
        GameObject bullet03 = Instantiate(BulletA, AtkPoint1.position, AtkPoint1.rotation * Quaternion.Euler(0f, 0f, -25f));
        bullet03.GetComponent<Rigidbody2D>().AddForce(bullet03.transform.right * BulletASpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        Boss1SoundEft.PlayNormalAtkSound();
        GameObject bullet04 = Instantiate(BulletA, AtkPoint2.position, AtkPoint2.rotation);
        bullet04.GetComponent<Rigidbody2D>().AddForce(bullet04.transform.right * BulletASpeed, ForceMode2D.Impulse);
        GameObject bullet05 = Instantiate(BulletA, AtkPoint2.position, AtkPoint2.rotation * Quaternion.Euler(0f, 0f, 25f));
        bullet05.GetComponent<Rigidbody2D>().AddForce(bullet05.transform.right * BulletASpeed, ForceMode2D.Impulse);
        GameObject bullet06 = Instantiate(BulletA, AtkPoint2.position, AtkPoint2.rotation * Quaternion.Euler(0f, 0f, -25f));
        bullet06.GetComponent<Rigidbody2D>().AddForce(bullet06.transform.right * BulletASpeed, ForceMode2D.Impulse);
    }
    IEnumerator NormalAtkB()
    {
        Boss1SoundEft.PlayNormalAtkSound();
        GameObject bullet01 = Instantiate(BulletA, AtkPoint1.position, AtkPoint1.rotation);
        bullet01.GetComponent<Rigidbody2D>().AddForce(bullet01.transform.right * BulletASpeed, ForceMode2D.Impulse);
        GameObject bullet02 = Instantiate(BulletA, AtkPoint1.position, AtkPoint1.rotation * Quaternion.Euler(0f, 0f, 5f));
        bullet02.GetComponent<Rigidbody2D>().AddForce(bullet02.transform.right * BulletASpeed, ForceMode2D.Impulse);
        GameObject bullet03 = Instantiate(BulletA, AtkPoint1.position, AtkPoint1.rotation * Quaternion.Euler(0f, 0f, -5f));
        bullet03.GetComponent<Rigidbody2D>().AddForce(bullet03.transform.right * BulletASpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        Boss1SoundEft.PlayNormalAtkSound();
        GameObject bullet04 = Instantiate(BulletA, AtkPoint2.position, AtkPoint2.rotation * Quaternion.Euler(0f, 0f, 15f));
        bullet04.GetComponent<Rigidbody2D>().AddForce(bullet04.transform.right * BulletASpeed, ForceMode2D.Impulse);
        GameObject bullet05 = Instantiate(BulletA, AtkPoint2.position, AtkPoint2.rotation * Quaternion.Euler(0f, 0f, -15f));
        bullet05.GetComponent<Rigidbody2D>().AddForce(bullet05.transform.right * BulletASpeed, ForceMode2D.Impulse);
        GameObject bullet06 = Instantiate(BulletA, AtkPoint2.position, AtkPoint2.rotation * Quaternion.Euler(0f, 0f, 30f));
        bullet06.GetComponent<Rigidbody2D>().AddForce(bullet06.transform.right * BulletASpeed, ForceMode2D.Impulse);
        GameObject bullet07 = Instantiate(BulletA, AtkPoint2.position, AtkPoint2.rotation * Quaternion.Euler(0f, 0f, -30f));
        bullet07.GetComponent<Rigidbody2D>().AddForce(bullet07.transform.right * BulletASpeed, ForceMode2D.Impulse);
    }
    IEnumerator NormalAtkC()
    {
        int i = UnityEngine.Random.Range(1, 3); //1~2
        if(i==1)
        {
            Boss1SoundEft.PlayNormalAtkSound();
            GameObject bullet01 = Instantiate(BulletA, AtkPoint1.position, AtkPoint1.rotation);
            bullet01.GetComponent<Rigidbody2D>().AddForce(bullet01.transform.right * BulletASpeed * 0.2f, ForceMode2D.Impulse);
            GameObject bullet02 = Instantiate(BulletA, AtkPoint1.position, AtkPoint1.rotation * Quaternion.Euler(0f, 0f, 60f));
            bullet02.GetComponent<Rigidbody2D>().AddForce(bullet02.transform.right * BulletASpeed * 0.2f, ForceMode2D.Impulse);
            GameObject bullet03 = Instantiate(BulletA, AtkPoint1.position, AtkPoint1.rotation * Quaternion.Euler(0f, 0f, 120f));
            bullet03.GetComponent<Rigidbody2D>().AddForce(bullet03.transform.right * BulletASpeed * 0.2f, ForceMode2D.Impulse);
            GameObject bullet04 = Instantiate(BulletA, AtkPoint1.position, AtkPoint1.rotation * Quaternion.Euler(0f, 0f, 180f));
            bullet04.GetComponent<Rigidbody2D>().AddForce(bullet04.transform.right * BulletASpeed * 0.2f, ForceMode2D.Impulse);
            GameObject bullet05 = Instantiate(BulletA, AtkPoint1.position, AtkPoint1.rotation * Quaternion.Euler(0f, 0f, 240f));
            bullet05.GetComponent<Rigidbody2D>().AddForce(bullet05.transform.right * BulletASpeed * 0.2f, ForceMode2D.Impulse);
            GameObject bullet06 = Instantiate(BulletA, AtkPoint1.position, AtkPoint1.rotation * Quaternion.Euler(0f, 0f, 300f));
            bullet06.GetComponent<Rigidbody2D>().AddForce(bullet06.transform.right * BulletASpeed * 0.2f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.3f);
            bullet01.GetComponent<Rigidbody2D>().AddForce(bullet01.transform.right * BulletASpeed, ForceMode2D.Impulse);
            bullet02.GetComponent<Rigidbody2D>().AddForce(bullet02.transform.right * BulletASpeed, ForceMode2D.Impulse);
            bullet03.GetComponent<Rigidbody2D>().AddForce(bullet03.transform.right * BulletASpeed, ForceMode2D.Impulse);
            bullet04.GetComponent<Rigidbody2D>().AddForce(bullet04.transform.right * BulletASpeed, ForceMode2D.Impulse);
            bullet05.GetComponent<Rigidbody2D>().AddForce(bullet05.transform.right * BulletASpeed, ForceMode2D.Impulse);
            bullet06.GetComponent<Rigidbody2D>().AddForce(bullet06.transform.right * BulletASpeed, ForceMode2D.Impulse);
        }
        else
        {
            Boss1SoundEft.PlayNormalAtkSound();
            GameObject bullet01 = Instantiate(BulletA, AtkPoint2.position, AtkPoint2.rotation);
            bullet01.GetComponent<Rigidbody2D>().AddForce(bullet01.transform.right * BulletASpeed * 0.2f, ForceMode2D.Impulse);
            GameObject bullet02 = Instantiate(BulletA, AtkPoint2.position, AtkPoint2.rotation * Quaternion.Euler(0f, 0f, 60f));
            bullet02.GetComponent<Rigidbody2D>().AddForce(bullet02.transform.right * BulletASpeed * 0.2f, ForceMode2D.Impulse);
            GameObject bullet03 = Instantiate(BulletA, AtkPoint2.position, AtkPoint2.rotation * Quaternion.Euler(0f, 0f, 120f));
            bullet03.GetComponent<Rigidbody2D>().AddForce(bullet03.transform.right * BulletASpeed * 0.2f, ForceMode2D.Impulse);
            GameObject bullet04 = Instantiate(BulletA, AtkPoint2.position, AtkPoint2.rotation * Quaternion.Euler(0f, 0f, 180f));
            bullet04.GetComponent<Rigidbody2D>().AddForce(bullet04.transform.right * BulletASpeed * 0.2f, ForceMode2D.Impulse);
            GameObject bullet05 = Instantiate(BulletA, AtkPoint2.position, AtkPoint2.rotation * Quaternion.Euler(0f, 0f, 240f));
            bullet05.GetComponent<Rigidbody2D>().AddForce(bullet05.transform.right * BulletASpeed * 0.2f, ForceMode2D.Impulse);
            GameObject bullet06 = Instantiate(BulletA, AtkPoint2.position, AtkPoint2.rotation * Quaternion.Euler(0f, 0f, 300f));
            bullet06.GetComponent<Rigidbody2D>().AddForce(bullet06.transform.right * BulletASpeed * 0.2f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.3f);
            bullet01.GetComponent<Rigidbody2D>().AddForce(bullet01.transform.right * BulletASpeed, ForceMode2D.Impulse);
            bullet02.GetComponent<Rigidbody2D>().AddForce(bullet02.transform.right * BulletASpeed, ForceMode2D.Impulse);
            bullet03.GetComponent<Rigidbody2D>().AddForce(bullet03.transform.right * BulletASpeed, ForceMode2D.Impulse);
            bullet04.GetComponent<Rigidbody2D>().AddForce(bullet04.transform.right * BulletASpeed, ForceMode2D.Impulse);
            bullet05.GetComponent<Rigidbody2D>().AddForce(bullet05.transform.right * BulletASpeed, ForceMode2D.Impulse);
            bullet06.GetComponent<Rigidbody2D>().AddForce(bullet06.transform.right * BulletASpeed, ForceMode2D.Impulse);
        }
        
    }

    public void OpenShield()
    {
        if(ShieldOn==false)
        {
            ShieldObj.SetActive(true);
            bossInfo.CanTakeDamage = false;
            ShieldOn = true;
        }
        
    }
    public void CloseShield()
    {
        if(ShieldOn)
        {
            StartCoroutine(CloseShieldDelay());
        }
        
    }
    IEnumerator CloseShieldDelay()
    {
        Boss1SoundEft.PlayShieldCloseSound();
        ShieldObj.GetComponent<Animator>().SetTrigger("ShieldBreak");
        bossInfo.CanTakeDamage = true;
        yield return new WaitForSeconds(0.6f);
        ShieldObj.SetActive(false);
        ShieldOn = false;
    }

    public void RandomAtk()
    {
        int A;
        if (TreeInfo.curHealth >= Convert.ToInt16(TreeInfo.maxHealth * 0.8f) && TreeInfo.curHealth > 0) //如果樹的血量小於80%且大於0
        {
            A = UnityEngine.Random.Range(1, 3); //1~2
        }
        else
        {
            A = UnityEngine.Random.Range(1, 4); //1~3
        }
            
        if(A == 1)
        {
            GetComponentInChildren<Animator>().SetTrigger("Atk01");
        }
        if (A == 2)
        {
            GetComponentInChildren<Animator>().SetTrigger("Atk02");
        }
        if (A == 3)
        {
            GetComponentInChildren<Animator>().SetBool("HealTree", true);
        }
    }
    public void Skill01()
    {
        StartCoroutine(StartSkill01());
    }
    IEnumerator StartSkill01()
    {
        Boss1SoundEft.PlayZoomOutSound();
        yield return new WaitForSeconds(0.8f);
        GameObject bullet01 = Instantiate(BulletB, AtkPoint2.position, AtkPoint2.rotation);
        bullet01.GetComponent<Rigidbody2D>().AddForce(bullet01.transform.right * BulletBSpeed, ForceMode2D.Impulse);
        GameObject bullet02 = Instantiate(BulletB, AtkPoint2.position, AtkPoint2.rotation * Quaternion.Euler(0f, 0f, 30f));
        bullet02.GetComponent<Rigidbody2D>().AddForce(bullet02.transform.right * BulletBSpeed, ForceMode2D.Impulse);
        GameObject bullet03 = Instantiate(BulletB, AtkPoint2.position, AtkPoint2.rotation * Quaternion.Euler(0f, 0f, -30f));
        bullet03.GetComponent<Rigidbody2D>().AddForce(bullet03.transform.right * BulletBSpeed, ForceMode2D.Impulse);
        Boss1SoundEft.PlayPoisonArrowSound();
    }
    public void Skill02()
    {
        int A = UnityEngine.Random.Range(2, 3); //1~2

        if(A==1)
        {
            Boss1SoundEft.PlayFirePillowSound();
            Vector3 AtkPoint = new Vector3(Target.transform.position.x + UnityEngine.Random.Range(-3.0f, 3.0f), -3f, Target.transform.position.z);
            Instantiate(FirePillow, AtkPoint, Quaternion.identity);
            AtkPoint = new Vector3(Target.transform.position.x + 15 + UnityEngine.Random.Range(-3.0f, 3.0f), -3f, Target.transform.position.z);
            Instantiate(FirePillow, AtkPoint, Quaternion.identity);
            AtkPoint = new Vector3(Target.transform.position.x + -15 + UnityEngine.Random.Range(-3.0f, 3.0f), -3f, Target.transform.position.z);
            Instantiate(FirePillow, AtkPoint, Quaternion.identity);
        }
        else
        {
            Skill02Big();
        }
        
    }
    public void Skill02Big()
    {
        Vector3 AtkPoint = new Vector3(Target.transform.position.x + UnityEngine.Random.Range(-3.0f, 3.0f), -3f, Target.transform.position.z);
        StartCoroutine(SpawnBigFirePillow(AtkPoint));
    }
    IEnumerator SpawnBigFirePillow(Vector3 AtkPoint)
    {
        Instantiate(FirePillowWarning, AtkPoint, Quaternion.identity);
        yield return new WaitForSeconds(1.3f);
        Boss1SoundEft.PlayFirePillowSound();
        Instantiate(BigFirePillow, AtkPoint, Quaternion.identity);
    }

    public void HealTree()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject FT = Instantiate(FloatingText);
            FT.GetComponentInChildren<TextMeshProUGUI>().text = "撿起生命之果，阻止敵人回復血量!";
            Instantiate(TreeFruit, new Vector3(treeBossSkill.transform.position.x + UnityEngine.Random.Range(-13.0f, -10.0f), treeBossSkill.transform.position.y + UnityEngine.Random.Range(-5.0f, 5.0f),0), Quaternion.identity);
            Instantiate(TreeFruit, new Vector3(treeBossSkill.transform.position.x + UnityEngine.Random.Range(13.0f, 10.0f), treeBossSkill.transform.position.y + UnityEngine.Random.Range(-5.0f, 5.0f), 0), Quaternion.identity);
            GetComponentInChildren<Animator>().SetBool("HealTree", false);
        }

    }
}
