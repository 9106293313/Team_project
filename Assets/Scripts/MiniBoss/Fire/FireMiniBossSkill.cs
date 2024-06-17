using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMiniBossSkill : MonoBehaviour
{
    [HideInInspector]public Transform target;  // 玩家的Transform
    public float jumpForce = 5f;  // 跳的力度
    public float upwardsForce = 2f;  // 向上的力度
    private Animator animator;  // ??控制器
    private Rigidbody2D rb;

    private bool isGrounded = false;
    private int gridLayerMask;

    [SerializeField] GameObject FireWave,Eb4_Fire, FireArrow;
    [SerializeField] float rotationStep = 30f;
    [SerializeField] float bulletSpeed = 5f;

    bool JumpAMDelayBool = false;

    float Timer = 0;
    int State = 1;
    int ChangeCount = 0;

    bool IsJumpAtk=false;
    bool IsJumpAtk2 = false;
    bool IsJumpAtk3 = false;

    int Atk3Count = 0;
    public GameObject BigFirePillow;
    public GameObject FirePillowWarning, FirePillowWarningNoSound;
    public AudioSource FirePillowSound,normalAtkSound;
    bool IsDead = false;
    public GameObject BulletCleanObj, ExplodeEft;
    public GameObject FireSummonPrefab; //召喚物獎勵(擊敗boss後獲得)

    void Start()
    {
        gridLayerMask = LayerMask.GetMask("Grid");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<BossInfo>().curHealth <= 0)
        {
            if (IsDead == false)
            {
                IsDead = true;
                StartCoroutine(BossDead()); 
            }
        }

        Timer +=Time.deltaTime;
        if(State == 1 && Timer > 10)
        {
            if(ChangeCount<1)
            {
                State = 2;
                Timer = 0;
                ChangeCount += 1;
            }
            else
            {
                State = 3;
                Timer = 0;
                ChangeCount = 0;
            }

        }
        if(State == 2 && Timer > 10)
        {
            if (ChangeCount < 1)
            {
                State = 1;
                Timer = 0;
                ChangeCount += 1;
            }
            else
            {
                State = 3;
                Timer = 0;
                ChangeCount = 0;
            }
        }
        if (State == 4 && Timer > 8)
        {
            State = 1;
            Timer = 0;
        }
        Debug.Log("State=" + State);
        if(State == 1)
        {
            jumpForce = 15;
            upwardsForce = 20;
            JumpAtk();
        }
        if (State == 2)
        {
            jumpForce = 15;
            upwardsForce = 30;
            StartCoroutine(JumpAtk2());
        }
        if (State == 3)
        {
            jumpForce = 10;
            upwardsForce = 15;
            JumpAtk3();
        }



        isGrounded = IsOnGround();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") && JumpAMDelayBool==false)
        {
            if(isGrounded)
            {
                animator.SetBool("Land", true);
            }
        }
        else
        {
            animator.SetBool("Land", false);
        }
    }
    void JumpAtk()
    {
        if(IsJumpAtk)
        { return; }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && isGrounded)
        {
            StartCoroutine(JumpAtkCoolDown());

            // 算朝向玩家的方向向量
            Vector3 direction = (target.position - transform.position).normalized;

            // 跳力度
            rb.velocity = Vector2.zero;
            rb.AddForce(direction * jumpForce, ForceMode2D.Impulse);
            rb.AddForce(Vector2.up * upwardsForce, ForceMode2D.Impulse);

            StartCoroutine(JumpAMDelay());
            animator.SetTrigger("Jump");
        }
    }
    IEnumerator JumpAtkCoolDown()
    {
        IsJumpAtk = true;
        yield return new WaitForSeconds(3f);
        IsJumpAtk = false;
    }
    IEnumerator JumpAtk2()
    {
        if (!IsJumpAtk2)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && isGrounded)
            {
                StartCoroutine(JumpAtkCoolDown2());

                yield return new WaitForSeconds(0.2f);

                // 算朝向玩家的方向向量
                Vector3 direction = (target.position - transform.position).normalized;

                // 跳力度
                rb.velocity = Vector2.zero;
                rb.AddForce(direction * jumpForce, ForceMode2D.Impulse);
                rb.AddForce(Vector2.up * upwardsForce, ForceMode2D.Impulse);

                animator.SetTrigger("Jump");
                StartCoroutine(JumpAMDelay());

            }
        }
        
    }
    IEnumerator JumpAtkCoolDown2()
    {
        IsJumpAtk2 = true;
        yield return new WaitForSeconds(4f);
        IsJumpAtk2 = false;
    }
    void JumpAtk3()
    {
        if (IsJumpAtk3)
        { return; }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && isGrounded)
        {
            StartCoroutine(JumpAtk3CoolDown());

            // 算朝向玩家的方向向量
            Vector3 direction = (target.position - transform.position).normalized;

            // 跳力度
            rb.velocity = Vector2.zero;
            rb.AddForce(direction * jumpForce, ForceMode2D.Impulse);
            rb.AddForce(Vector2.up * upwardsForce, ForceMode2D.Impulse);

            StartCoroutine(JumpAMDelay());
            animator.SetTrigger("Jump");
        }
    }
    IEnumerator JumpAtk3CoolDown()
    {
        IsJumpAtk3 = true;
        yield return new WaitForSeconds(1f);
        IsJumpAtk3 = false;
    }
    private bool IsOnGround()
    {
        // 使用射線檢測是否在 Grid 地面上
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position + new Vector3(-1.5f, 0f, 0f), Vector2.down, 2.5f, gridLayerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(1.5f, 0f, 0f), Vector2.down, 2.5f, gridLayerMask);

        Debug.DrawRay(transform.position + new Vector3(-1.5f, 0f, 0f), Vector2.down * 2.5f, Color.red);
        Debug.DrawRay(transform.position + new Vector3(1.5f, 0f, 0f), Vector2.down * 2.5f, Color.red);

        if ((hit1.collider != null || hit2.collider != null) && !isGrounded)
        {
            if(State==1)
            {
                StartCoroutine(Shoot());
            }
            if (State == 2)
            {
                StartCoroutine(Shoot2());
            }
            if (State == 3)
            {
                Shoot3();
            }
        }

        return hit1.collider != null || hit2.collider != null;
    }
    
    IEnumerator JumpAMDelay()
    {
        JumpAMDelayBool = true;
        yield return new WaitForSeconds(1f);
        JumpAMDelayBool = false;
    }
    IEnumerator Shoot()
    {
        float startAngle = 0;  // 扇形起始角度
        float endAngle = 180f;  // 扇形?束角度
        float intervalAngle = 45f;  // 每次?射子?的角度?隔

        for (int i = 0; i < 2; i++)
        {
            normalAtkSound.Play();
            for (float angle = startAngle +i*15; angle <= endAngle+i*15; angle += intervalAngle)
            {
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                Vector3 direction = rotation * transform.right;

                GameObject bulletObj = Instantiate(FireWave, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
                bulletObj.transform.right = direction;

                bulletObj.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
            }
            for (float angle = startAngle - i * 15; angle <= endAngle - i * 15; angle += intervalAngle)
            {
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                Vector3 direction = rotation * transform.right;

                GameObject bulletObj = Instantiate(FireWave, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
                bulletObj.transform.right = direction;

                bulletObj.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    IEnumerator Shoot2()
    {
        float startAngle = 30;  // 扇形起始角度
        float endAngle = 150f;  // 扇形?束角度
        float intervalAngle = 20f;  // 每次?射子?的角度?隔

        for (int i = 0; i < 3; i++)
        {
            normalAtkSound.Play();
            for (float angle = startAngle+i*10; angle <= endAngle+i*10; angle += intervalAngle)
            {
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                Vector3 direction = rotation * transform.right;

                GameObject bulletObj = Instantiate(Eb4_Fire, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
                bulletObj.transform.right = direction;

                bulletObj.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
            }
            for (float angle = startAngle - i * 10; angle <= endAngle - i * 10; angle += intervalAngle)
            {
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                Vector3 direction = rotation * transform.right;

                GameObject bulletObj = Instantiate(Eb4_Fire, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
                bulletObj.transform.right = direction;

                bulletObj.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    void Shoot3()
    {
        if(Atk3Count >= 0 && Atk3Count<5)
        {
            StartCoroutine(Shoot3_1());
        }
        else
        {
            StartCoroutine(Shoot3_2());
            State = 4;
            Timer = 0;
            Atk3Count = -1;
        }
    }
    IEnumerator Shoot3_1()
    {
        float startAngle = -10;  // 扇形起始角度
        float endAngle = 190f;  // 扇形?束角度
        float intervalAngle = 30f;  // 每次?射子?的角度?隔

        for (int i = 0; i < 3; i++)
        {
            normalAtkSound.Play();
            for (float angle = startAngle + i * 20; angle <= endAngle + i * 20; angle += intervalAngle)
            {
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                Vector3 direction = rotation * transform.right;

                GameObject bulletObj = Instantiate(FireArrow, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
                bulletObj.transform.right = direction;

                bulletObj.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
            }
            for (float angle = startAngle - i * 20; angle <= endAngle - i * 20; angle += intervalAngle)
            {
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                Vector3 direction = rotation * transform.right;

                GameObject bulletObj = Instantiate(FireArrow, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
                bulletObj.transform.right = direction;

                bulletObj.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(0.1f);
        }
        Atk3Count++;
    }
    IEnumerator Shoot3_2()
    {
        StartCoroutine(GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().Shake(1f, 0.15f));//搖晃畫面
        FirePillow(0);
        FirePillowNoSound(25f);
        FirePillowNoSound(-25f);
        yield return new WaitForSeconds(1f);
        Atk3Count = 0;
    }
    public void FirePillow(float num)
    {
        Vector3 AtkPoint = new Vector3(transform.position.x + num, -3f, transform.position.z);
        StartCoroutine(SpawnBigFirePillow(AtkPoint));
    }
    IEnumerator SpawnBigFirePillow(Vector3 AtkPoint)
    {
        Instantiate(FirePillowWarning, AtkPoint, Quaternion.identity);
        yield return new WaitForSeconds(1.3f);
        FirePillowSound.Play();
        Instantiate(BigFirePillow, AtkPoint, Quaternion.identity);
    }
    public void FirePillowNoSound(float num)
    {
        Vector3 AtkPoint = new Vector3(transform.position.x + num, -3f, transform.position.z);
        StartCoroutine(SpawnBigFirePillowNoSound(AtkPoint));
    }
    IEnumerator SpawnBigFirePillowNoSound(Vector3 AtkPoint)
    {
        Instantiate(FirePillowWarningNoSound, AtkPoint, Quaternion.identity);
        yield return new WaitForSeconds(1.3f);
        FirePillowSound.Play();
        Instantiate(BigFirePillow, AtkPoint, Quaternion.identity);
    }

    IEnumerator BossDead()
    {
        StartCoroutine(PlayBossDeadEft());
        GetComponentInChildren<Animator>().SetTrigger("Death");
        Instantiate(BulletCleanObj, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.2f);
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
    void giveSummon()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if(player.GetComponent<PlayerInfo>().HasFireSummon == false)
        {
            player.GetComponent<PlayerInfo>().HasFireSummon = true;
            GameObject Summon = Instantiate(FireSummonPrefab, player.transform.position, Quaternion.identity);
            Summon.transform.SetParent(player.transform);
        }
        
    }
}
