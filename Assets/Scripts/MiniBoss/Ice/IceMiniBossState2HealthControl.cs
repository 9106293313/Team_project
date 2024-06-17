using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMiniBossState2HealthControl : MonoBehaviour
{
    public BossInfo bossinfo;
    public GameObject SplitObj1, SplitObj2;
    bool IsDead=false;

    public GameObject BulletCleanObj, ExplodeEft;
    public GameObject IceSummonPrefab; //召喚物獎勵(擊敗boss後獲得)
    void Start()
    {
        
        SplitObj1.GetComponent<EnemyInfo>().maxHealth = bossinfo.maxHealth / 4;
        SplitObj2.GetComponent<EnemyInfo>().maxHealth = bossinfo.maxHealth / 4;
        StartCoroutine(StartHealth());
    }

    void Update()
    {

        if (SplitObj1!=null && SplitObj2!=null)
        {
            bossinfo.curHealth = SplitObj1.GetComponent<EnemyInfo>().curHealth + SplitObj2.GetComponent<EnemyInfo>().curHealth;
        }
        if(bossinfo.curHealth < 0)
        {
            bossinfo.curHealth = 0;
        }
        if(bossinfo.curHealth == 0 && IsDead ==false)
        {
            IsDead = true;
            bossinfo.healthBar.SetHealth(bossinfo.curHealth);
            StartCoroutine(BossDead());
        }

    }
    IEnumerator StartHealth()
    {
        yield return new WaitForSeconds(0.1f);
        bossinfo.healthBar.SetHealth2(bossinfo.maxHealth / 2);
    }
    IEnumerator BossDead()
    {
        // 停止移動
        SplitObj1.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        SplitObj2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        SplitObj1.GetComponent<EnemyAI>().speed = 0f;
        SplitObj2.GetComponent<EnemyAI>().speed = 0f;

        StartCoroutine(PlayBossDeadEft());
        Instantiate(BulletCleanObj, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.2f);
        Instantiate(BulletCleanObj, transform.position, Quaternion.identity);

        giveSummon();

        Destroy(gameObject);
    }
    IEnumerator PlayBossDeadEft()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3 Pos = new Vector3(SplitObj1.transform.position.x + UnityEngine.Random.Range(-1f, 1f), SplitObj1.transform.position.y + UnityEngine.Random.Range(-1f, 1f), 0f);
            Vector3 Pos2 = new Vector3(SplitObj2.transform.position.x + UnityEngine.Random.Range(-1f, 1f), SplitObj2.transform.position.y + UnityEngine.Random.Range(-1f, 1f), 0f);
            Instantiate(ExplodeEft, Pos, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            Instantiate(ExplodeEft, Pos2, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }

    }
    void giveSummon()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if(player.GetComponent<PlayerInfo>().HasIceSummon == false)
        {
            player.GetComponent<PlayerInfo>().HasIceSummon = true;
            GameObject Summon = Instantiate(IceSummonPrefab, player.transform.position, Quaternion.identity);
            Summon.transform.SetParent(player.transform);
        }
        
    }
}
