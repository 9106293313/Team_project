using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderMiniBossSkill : MonoBehaviour
{
    public EnemyAI EnemyAI;
    float DefaultMoveSpeed;
    float DefaultMinDistance;

    bool Atkking = false;
    bool CanAtk1 = true;
    bool CanAtk2 = true;
    bool CanAtk3 = true;

    bool ChangeColor = false;

    public GameObject bulletPrefab1, bulletPrefab2;
    public AudioSource AtkSound,DashSound;

    private Transform playerTransform;
    bool IsDead = false;
    public GameObject BulletCleanObj, ExplodeEft;
    Animator animator;
    public GameObject ThunderSummonPrefab; //召喚物獎勵(擊敗boss後獲得)
    void Start()
    {
        StartCoroutine(OpenEnemyAI());
        playerTransform = GameObject.FindWithTag("Player").transform;
        DefaultMoveSpeed = EnemyAI.speed;
        DefaultMinDistance = EnemyAI.MinDistance;

        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<BossInfo>().curHealth <= 0 && IsDead == false)
        {
            IsDead = true;
            StartCoroutine(BossDead());
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (Atkking == false)
            {
                StartCoroutine(StartRandomAtk());
            }
        }
        if(ChangeColor)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0xD8 / 255f, 0xF1 / 255f, 0x33 / 255f, 1f);
        }
    }
    IEnumerator StartRandomAtk()//攻擊前停2秒讓他移動
    {
        Atkking = true;
        yield return new WaitForSeconds(2f);
        RandomAtk();
    }
    public void RandomAtk()
    {
        //Atkking = true;

        List<int> options = new List<int> { 1, 2, 3 ,4}; // 可選的數值

        if (!CanAtk1)
        {
            options.Remove(1);
        }
        if (!CanAtk2)
        {
            options.Remove(2);
        }
        if (!CanAtk3)
        {
            options.Remove(3);
        }


        int A = options[UnityEngine.Random.Range(0, options.Count)]; // 從剩下的數值中隨機選擇一個
        if (A == 1)
        {
            Atk1();
        }
        else if (A == 2)
        {
            Atk2();
        }
        else if (A == 3)
        {
            Atk3();
        }
        else
        {
            Atkking = false;
        }
    }
    void Atk1()
    {
        StartCoroutine(StartAtk1());
    }
    IEnumerator Atk1CoolDown()
    {
        CanAtk1 = false;
        yield return new WaitForSeconds(3f);
        CanAtk1 = true;
    }
    IEnumerator StartAtk1()
    {
        StartCoroutine(Atk1CoolDown());

        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        animator.SetTrigger("Atk");
        yield return new WaitForSeconds(0.5f);

        ShootBulletWithOffset(bulletPrefab1, 5f, 12f);
        ShootBulletWithOffset(bulletPrefab1, -5f, 12f);
        AtkSound.Play();
        yield return new WaitForSeconds(0.2f);
        ShootBulletWithOffset(bulletPrefab1, 20f, 8f);
        ShootBulletWithOffset(bulletPrefab1, -20f, 8f);
        ShootBulletWithOffset(bulletPrefab2, 40f, 10f);
        ShootBulletWithOffset(bulletPrefab2, -40f, 10f);
        AtkSound.Play();
        yield return new WaitForSeconds(0.2f);
        ShootBulletWithOffset(bulletPrefab2, 30f, 6f);
        ShootBulletWithOffset(bulletPrefab2, -30f, 6f);
        ShootBulletWithOffset(bulletPrefab1, 0f, 8f);
        ShootBulletWithOffset(bulletPrefab2, 60f, 6f);
        ShootBulletWithOffset(bulletPrefab2, -60f, 6f);
        AtkSound.Play();
        yield return new WaitForSeconds(0.1f);
        ShootBulletWithOffset(bulletPrefab1, 0f, 10f);
        AtkSound.Play();
        yield return new WaitForSeconds(0.1f);
        ShootBulletWithOffset(bulletPrefab1, 0f, 12f);
        AtkSound.Play();
        yield return new WaitForSeconds(0.1f);
        ShootBulletWithOffset(bulletPrefab1, 0f, 14f);
        AtkSound.Play();
        yield return new WaitForSeconds(0.1f);
        ShootBulletWithOffset(bulletPrefab1, 0f, 16f);
        AtkSound.Play();

        yield return new WaitForSeconds(1f);

        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;
        Atkking = false;
    }
    private void ShootBulletWithOffset(GameObject BulletType ,float offsetAngle , float Bulletspeed)
    {
        float playerAngle = Mathf.Atan2(playerTransform.position.y - transform.position.y, playerTransform.position.x - transform.position.x) * Mathf.Rad2Deg;

        float angle = playerAngle + offsetAngle;
        Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;

        GameObject bullet = Instantiate(BulletType, transform.position, Quaternion.Euler(0, 0, angle));
        bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * Bulletspeed;
    }
    void Atk2()
    {
        StartCoroutine(StartAtk2());
    }
    IEnumerator Atk2CoolDown()
    {
        CanAtk2 = false;
        yield return new WaitForSeconds(20f);
        CanAtk2 = true;
    }
    IEnumerator StartAtk2()
    {
        StartCoroutine(Atk2CoolDown());

        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        animator.speed = 0.33f;
        animator.SetTrigger("Atk");
        DashSound.Play();
        yield return new WaitForSeconds(1.5f);

        ChangeColor = true;

        EnemyAI.speed = 4500;
        EnemyAI.MinDistance = 0;

        animator.speed = 1.5f;

        for (int i = 0; i < 8; i++)//在接下來8秒內每秒朝周圍每60度射一次子彈
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("Atk");
            yield return new WaitForSeconds(0.3f);
            for (float angle = 0; angle < 360f; angle += 60)
            {
                Vector3 direction = Quaternion.Euler(0f, 0f, angle) * transform.right;
                GameObject bulletObj = Instantiate(bulletPrefab2, transform.position, Quaternion.identity);
                bulletObj.transform.right = direction;

                bulletObj.GetComponent<Rigidbody2D>().AddForce(bulletObj.transform.right * 10f, ForceMode2D.Impulse);
                AtkSound.Play();
            }
            yield return new WaitForSeconds(0.7f);
        }

        yield return new WaitForSeconds(1f);

        animator.speed = 1f;

        ChangeColor = false;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;

        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;
        EnemyAI.MinDistance = DefaultMinDistance;

        Atkking = false;
    }
    void Atk3()
    {
        StartCoroutine(StartAtk3());
    }
    IEnumerator Atk3CoolDown()
    {
        CanAtk3 = false;
        yield return new WaitForSeconds(5f);
        CanAtk3 = true;
    }
    IEnumerator StartAtk3()
    {
        StartCoroutine(Atk3CoolDown());

        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        animator.SetTrigger("Atk");
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 8; i++)
        {
            for (float angle = i*15; angle < 360f + i*15; angle += 45)
            {
                Vector3 direction = Quaternion.Euler(0f, 0f, angle) * transform.right;
                GameObject bulletObj = Instantiate(bulletPrefab1, transform.position, Quaternion.identity);
                bulletObj.transform.right = direction;

                bulletObj.GetComponent<Rigidbody2D>().AddForce(bulletObj.transform.right * 10f, ForceMode2D.Impulse);
                AtkSound.Play();
            }
            yield return new WaitForSeconds(0.15f);
        }

        yield return new WaitForSeconds(1f);

        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;

        Atkking = false;
    }
    IEnumerator OpenEnemyAI()
    {
        yield return new WaitForSeconds(1.5f);
        EnemyAI.enabled = true;
    }
    IEnumerator BossDead()
    {
        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        StartCoroutine(PlayBossDeadEft());
        //GetComponentInChildren<Animator>().SetTrigger("Death");
        Instantiate(BulletCleanObj, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.2f);
        //Instantiate(BossDeadObj, transform.position, Quaternion.identity);
        Instantiate(BulletCleanObj, transform.position, Quaternion.identity);

        giveSummon();

        Destroy(gameObject);
    }
    IEnumerator PlayBossDeadEft()
    {
        for (int i = 0; i < 8; i++)
        {
            Vector3 Pos = new Vector3(transform.position.x + UnityEngine.Random.Range(-1f, 1f), transform.position.y + UnityEngine.Random.Range(-1f, 1f), 0f);
            Instantiate(ExplodeEft, Pos, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }

    }
    public void giveSummon()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if(player.GetComponent<PlayerInfo>().HasThunderSummon == false)
        {
            player.GetComponent<PlayerInfo>().HasThunderSummon = true;
            Instantiate(ThunderSummonPrefab, playerTransform.position, Quaternion.identity);
        }
    }
}
