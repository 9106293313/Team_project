using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeBossSkill : MonoBehaviour
{
    public Animator BodyAnimator;
    public Animator EyeAnimator;
    Collider2D colliderA;
    public Boss1SoundEft soundEft;
    public GameObject RootAtkObj;
    public GameObject PoisonAtkObj;
    public GameObject PoisonLeaf;
    public BossInfo bossInfo;
    public BossInfo MainBossInfo;
    public bool IsHealing = false;
    public GameObject HealingEft;
    public GameObject SelfDamageEft;
    public GameObject TreeBossDeadSprite;
    public GameObject BossDeathSprite;
    public GameObject BossState2;
    public GameObject TreeDeadSound;
    bool IsDead=false;
    public GameObject CleanBulletObj;
    void Start()
    {
        colliderA = this.GetComponent<Collider2D>();
        bossInfo = GetComponent<BossInfo>();
        TreeBossDeadSprite.SetActive(false);
    }

    void Update()
    {
        if(EyeAnimator.GetCurrentAnimatorStateInfo(0).IsName("treeEyeCloseState") || EyeAnimator.GetCurrentAnimatorStateInfo(0).IsName("TreeDead"))
        {
            colliderA.enabled = false;
        }
        else
        {
            colliderA.enabled = true;
        }
        if(bossInfo.curHealth<=0)
        {
            BodyAnimator.SetBool("Dead",true);
            EyeAnimator.SetBool("Dead", true);
        }
    }
    public void RandomAtk()
    {
        int i = UnityEngine.Random.Range(1, 4); //1~3
        if (i == 1)
        { StartCoroutine(AtkA()); }
        if (i == 2)
        { StartCoroutine(AtkB()); }
        if (i == 3)
        { StartCoroutine(AtkC()); }
    }
    IEnumerator AtkA()
    {
        EyeAnimator.SetTrigger("OpenEye");
        yield return new WaitForSeconds(1f);
        BodyAnimator.SetTrigger("OpenTentacle");
        soundEft.PlayTreeTentacleSound();
        StartCoroutine(GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().Shake(0.6f, 0.2f));//搖晃畫面
        yield return new WaitForSeconds(1f);

        int A = UnityEngine.Random.Range(1, 3); //1~2
        if(A == 1)
        {
            for (int i = 2; i <= 26; i += 4)
            {
                soundEft.PlayRootAtkSound();
                Instantiate(RootAtkObj, new Vector3(transform.position.x + i, transform.position.y - 16.6f, 0), Quaternion.identity);
                Instantiate(RootAtkObj, new Vector3(transform.position.x - i, transform.position.y - 16.6f, 0), Quaternion.identity);
                yield return new WaitForSeconds(0.6f);
                soundEft.PlayRootDirtSound();
                yield return new WaitForSeconds(0.2f);
            }
        }
        else
        {
            for (int i = 26; i >= 2; i -= 4)
            {
                soundEft.PlayRootAtkSound();
                Instantiate(RootAtkObj, new Vector3(transform.position.x + i, transform.position.y - 16.6f, 0), Quaternion.identity);
                Instantiate(RootAtkObj, new Vector3(transform.position.x - i, transform.position.y - 16.6f, 0), Quaternion.identity);
                yield return new WaitForSeconds(0.6f);
                soundEft.PlayRootDirtSound();
                yield return new WaitForSeconds(0.2f);
            }
        }
        
        yield return new WaitForSeconds(4f);
        BodyAnimator.SetTrigger("CloseTentacle");
        yield return new WaitForSeconds(1f);
        EyeAnimator.SetTrigger("CloseEye");
    }
    IEnumerator AtkB()
    {
        EyeAnimator.SetTrigger("OpenEye");
        yield return new WaitForSeconds(1f);
        BodyAnimator.SetTrigger("PoisonLeaf");
        StartCoroutine(GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().Shake(1.4f, 0.1f));//搖晃畫面
        yield return new WaitForSeconds(1.5f);
        soundEft.PlayTreePoisonAtkSound();
        for (int i = 1; i <= 10; i++)
        {
            if(IsDead)
            { 
                yield break; 
            }
            Instantiate(PoisonAtkObj, new Vector3(transform.position.x + UnityEngine.Random.Range(-20.0f, 20.0f), transform.position.y + 36, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            Instantiate(PoisonAtkObj, new Vector3(transform.position.x + UnityEngine.Random.Range(-20.0f, 20.0f), transform.position.y + 36, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            Instantiate(PoisonAtkObj, new Vector3(transform.position.x + UnityEngine.Random.Range(-20.0f, 20.0f), transform.position.y + 36, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1f);
        BodyAnimator.SetTrigger("CloseLeaf");
        EyeAnimator.SetTrigger("CloseEye");
    }
    IEnumerator AtkC()
    {
        EyeAnimator.SetTrigger("OpenEye");
        yield return new WaitForSeconds(1f);
        BodyAnimator.SetTrigger("OpenSmallEyes");
        StartCoroutine(GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().Shake(1.4f, 0.1f));//搖晃畫面
        yield return new WaitForSeconds(1f);
        soundEft.PlayTreePoisonAtkSound();
        for (int i = 1; i <= 25; i++)
        {
            if (IsDead)
            {
                yield break;
            }
            GameObject bullet01 = Instantiate(PoisonLeaf, new Vector3(transform.position.x -7.93f, transform.position.y + 0.8f, 0), Quaternion.identity * Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0,360)));
            bullet01.GetComponent<Rigidbody2D>().AddForce(bullet01.transform.right * 7f, ForceMode2D.Impulse);
            GameObject bullet02 = Instantiate(PoisonLeaf, new Vector3(transform.position.x - 10.56f, transform.position.y + 5.9f, 0), Quaternion.identity * Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 360)));
            bullet02.GetComponent<Rigidbody2D>().AddForce(bullet02.transform.right * 7f, ForceMode2D.Impulse);
            GameObject bullet03 = Instantiate(PoisonLeaf, new Vector3(transform.position.x - 3.45f, transform.position.y + 13.3f, 0), Quaternion.identity * Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 360)));
            bullet03.GetComponent<Rigidbody2D>().AddForce(bullet03.transform.right * 7f, ForceMode2D.Impulse);
            GameObject bullet04 = Instantiate(PoisonLeaf, new Vector3(transform.position.x +3.3f, transform.position.y + 3.7f, 0), Quaternion.identity * Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 360)));
            bullet04.GetComponent<Rigidbody2D>().AddForce(bullet04.transform.right * 7f, ForceMode2D.Impulse);
            GameObject bullet05 = Instantiate(PoisonLeaf, new Vector3(transform.position.x + 2.14f, transform.position.y + 10f, 0), Quaternion.identity * Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 360)));
            bullet05.GetComponent<Rigidbody2D>().AddForce(bullet05.transform.right * 7f, ForceMode2D.Impulse);
            GameObject bullet06 = Instantiate(PoisonLeaf, new Vector3(transform.position.x + 13.68f, transform.position.y + 10.28f, 0), Quaternion.identity * Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 360)));
            bullet06.GetComponent<Rigidbody2D>().AddForce(bullet06.transform.right * 7f, ForceMode2D.Impulse);
            GameObject bullet07 = Instantiate(PoisonLeaf, new Vector3(transform.position.x + 11.67f, transform.position.y + 5.38f, 0), Quaternion.identity * Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 360)));
            bullet07.GetComponent<Rigidbody2D>().AddForce(bullet07.transform.right * 7f, ForceMode2D.Impulse);
            GameObject bullet08 = Instantiate(PoisonLeaf, new Vector3(transform.position.x + 8.67f, transform.position.y -0.52f, 0), Quaternion.identity * Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 360)));
            bullet08.GetComponent<Rigidbody2D>().AddForce(bullet08.transform.right * 7f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.2f);

        }
        yield return new WaitForSeconds(1f);
        BodyAnimator.SetTrigger("CloseSmallEyes");
        EyeAnimator.SetTrigger("CloseEye");
    }

    public void TreeEndHeal()
    {
        BodyAnimator.SetBool("TreeHeal", false);
    }
    public void TreeHeal()
    {
        if(bossInfo.curHealth >= Convert.ToInt16(bossInfo.maxHealth * 0.33f))
        {
            BodyAnimator.SetBool("TreeHeal", true);
        }
        
    }
    public void StartHealing()
    {
        StartCoroutine(TreeHealing());
    }
    IEnumerator TreeHealing()
    {
        if(IsHealing==false)
        {
            IsHealing = true;
            bossInfo.TakeDamage(Convert.ToInt16(bossInfo.maxHealth * 0.33f));
            MainBossInfo.FullHeal();
            GameObject HealEft = Instantiate(HealingEft, MainBossInfo.gameObject.transform.position, Quaternion.identity);
            Destroy(HealEft, 2f);
            GameObject selfDamage = Instantiate(SelfDamageEft, EyeAnimator.transform.position, Quaternion.identity);
            Destroy(selfDamage, 2.5f);
            soundEft.PlayTreeDamageSound();
            yield return new WaitForSeconds(2f);
            IsHealing = false;
            TreeEndHeal();
        }
    }

    public void TreeDead()
    {
        if(IsDead==false)
        {
            StartCoroutine(StartTreeDead());
            IsDead = true;
        }
        
    }
    IEnumerator StartTreeDead()
    {
        Instantiate(CleanBulletObj, transform.position, Quaternion.identity);
        StartCoroutine(ChangeMusic());
        Instantiate(TreeDeadSound);

        StartCoroutine(GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().Shake(1.2f, 0.15f));//搖晃畫面

        BodyAnimator.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        EyeAnimator.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        TreeBossDeadSprite.SetActive(true);
        GameObject DeathSprite = Instantiate(BossDeathSprite, MainBossInfo.gameObject.transform.position, Quaternion.identity);
        Destroy(MainBossInfo.gameObject);
        yield return new WaitForSeconds(0.2f);
        DeathSprite.GetComponent<BossInfo>().TakeDamage(3000);
        yield return new WaitForSeconds(1f);
        DeathSprite.transform.position = EyeAnimator.gameObject.transform.position;
        yield return new WaitForSeconds(5.5f);
        DeathSprite.GetComponent<Animator>().SetTrigger("warp");
        yield return new WaitForSeconds(8f);
        DeathSprite.GetComponent<BossInfo>().FullHeal();
        yield return new WaitForSeconds(0.2f);
        DeathSprite.GetComponent<Animator>().SetTrigger("Change");
        StartCoroutine(GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().Shake(1.2f, 0.15f));//搖晃畫面
        yield return new WaitForSeconds(1.5f);
        Instantiate(BossState2, DeathSprite.transform.position, Quaternion.identity);
        Destroy(DeathSprite);
    }
    IEnumerator ChangeMusic()
    {
        FindObjectOfType<MusicPlayer>().StopPlaying("boss1Music1");
        FindObjectOfType<MusicPlayer>().Play("boss1MusicChange");
        yield return new WaitForSeconds(15f);
        FindObjectOfType<MusicPlayer>().Play("boss1Music2");
    }
}
