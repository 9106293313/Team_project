using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss2Skill : MonoBehaviour
{
    public GameObject SceneDoor;
    public GameObject FloatingText;
    public GhostEft ghostEft;
    public EnemyAI EnemyAI;
    float DefaultMoveSpeed;
    ///////////////
    float dashDuration = 0.1f;
    float dashSpeed = 10f;
    float maxDashDistance = 3f;
    bool IsDashing = false; //是否在進行衝刺

    public AudioSource DashSound;
    public AudioSource WarningSound;
    public AudioSource SummonKatanaSound, ReleaseKatanaSound, Atk5Sound,BambooSound,ThunderWaveSound,WarpSound;

    private Transform playerTransform;

    public GameObject DashPoint;
    public GameObject DashEffect;
    public GameObject DashRangeWarningEft;
    /////////////////
    bool Atkking = false;
    bool CanDash = true;//是否能衝刺攻擊
    bool CanAtk2 = true; 
    bool CanAtk3 = true; 
    bool CanAtk4 = true;
    bool CanAtk5 = true;
    bool CanAtk6 = true;
    bool CanUlt = false;
    //////////////
    public GameObject shootKatanaObj;

    public GameObject KatanaBullet2;
    float KatanaBullet2Speed = 10f;

    public GameObject BigSlashEft;

    public GameObject Bamboo1, Bamboo2 , Bamboo3 , Bamboo4 ;
    public GameObject ThunderWave;  //刀波
    float ThunderWaveSpeed = 15f;
    public GameObject EB3_Thunder;
    float EB3_ThunderSpeed = 10f;
    ///////////////////////////
    bool IsState2 = false;
    bool ChangeingState=false;

    public GameObject CameraEft;
    public AudioSource timeSlowDownSoundSource; // 不受慢放影響的音效
    public AudioClip timeSlowDownSoundClip;
    public GameObject UltEft1,Boss2UltPlayerShield, UltEft2;
    public GameObject BulletCleanObj, ExplodeEft;

    bool IsDead = false;

    bool UltAttractPlayer = false;
    void Start()
    {
        StartCoroutine(OpenEnemyAI());
        playerTransform = GameObject.FindWithTag("Player").transform;
        DefaultMoveSpeed = EnemyAI.speed;
        UltEft1.SetActive(false);
    }

    void Update()
    {

        if (gameObject.GetComponent<BossInfo>().curHealth <= 0 && IsDead == false)
        {
            IsDead = true;
            StartCoroutine(BossDead());
        }

        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("State1"))
        {
            if(Atkking==false)
            {
                if (gameObject.GetComponent<BossInfo>().curHealth < (gameObject.GetComponent<BossInfo>().maxHealth * 0.4f))
                {
                    if(IsState2==false)
                    {
                        ChangeState();
                    }
                }
            }
            if (Atkking == false)
            {
                if(ChangeingState==false)
                {
                    RandomAtk();
                }
            }


        }
    }
    private void FixedUpdate()
    {
        float attractionForce = 20.0f; // 吸引力的強度
        if (UltAttractPlayer == true)
        {
            // 計算吸引力的方向
            Vector2 direction = (transform.position - playerTransform.position).normalized;

            // 應用吸引力到玩家的Rigidbody2D
            playerTransform.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * attractionForce);
        }
    }
    void ChangeState()
    {
        StartCoroutine(PlayChangeStateAM());
    }
    public void RandomAtk()
    {
        Atkking = true;

        List<int> options = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8}; // 可選的數值
        //List<int> options = new List<int> { 7 , 8 }; // 可選的數值

        if (!CanDash)
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
        if (!CanAtk4)
        {
            options.Remove(4);
        }
        if (!CanAtk5)
        {
            options.Remove(5);
        }
        if (!CanAtk6)
        {
            options.Remove(6);
        }
        if (!CanUlt)
        {
            options.Remove(7);
        }

        int A = options[UnityEngine.Random.Range(0, options.Count)]; // 從剩下的數值中隨機選擇一個
        if (A == 1)
        {
            if(IsState2==false)
            {
                Dash();
            }
            else
            {
                DashState2();
            }
            
        }
        else if (A == 2)
        {
            if (IsState2 == false)
            {
                Atk2();
            }
            else
            {
                Atk2State2();
            }
            
        }
        else if (A == 3)
        {
            Atk3();         
        }
        else if (A == 4)
        {
            Atk4();
        }
        else if (A == 5)
        {
            Atk5();
        }
        else if (A == 6)
        {
            Atk6();
        }
        else if (A == 7)
        {
            Ult();
        }
        else
        {
            Atkking = false;
        }
    }
    IEnumerator PlayChangeStateAM()
    {
        ChangeingState = true;

        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        GameObject FT = Instantiate(FloatingText);
        FT.GetComponentInChildren<TextMeshProUGUI>().text = "小心!敵人的攻擊將變得更加猛烈!";

        gameObject.GetComponentInChildren<Animator>().SetTrigger("ChangeState");
        IsState2 = true;
        yield return new WaitForSeconds(4f);

        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;

        ChangeingState = false;

        CanUlt = true;
    }
    IEnumerator OpenEnemyAI()
    {
        yield return new WaitForSeconds(1.5f);
        EnemyAI.enabled = true;
    }
    void DashState2()
    {
        StartCoroutine(DashSkill02());
    }
    void Dash()
    {
        StartCoroutine(DashSkill01());
    }
    IEnumerator DashSkillCoolDown()
    {
        CanDash = false;
        yield return new WaitForSeconds(15f);
        CanDash = true;
    }
    IEnumerator DashSkill01()
    {
        StartCoroutine(DashSkillCoolDown());//進冷卻

        StartCoroutine(StartDash(1f));
        yield return new WaitForSeconds(1.1f);
        StartCoroutine(StartDash(0.7f));
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(StartDash(0.7f));
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(StartDash(0.7f));
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(StartDash(0.7f));
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(StartDash(0.7f));
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(StartDash(0.7f));
        yield return new WaitForSeconds(0.8f);
        gameObject.GetComponentInChildren<Animator>().SetTrigger("End");
        ghostEft.ghostDelay = 0.4f;
        Atkking = false;
    }
    IEnumerator DashSkill02()
    {
        StartCoroutine(DashSkillCoolDown());//進冷卻

        StartCoroutine(StartDash(1f));
        yield return new WaitForSeconds(1.1f);
        StartCoroutine(StartDash(0.7f));
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(StartDash(0.6f));
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(StartDash(0.5f));
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(StartDash(0.4f));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(StartDash(0.3f));
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(StartDash(0.3f));
        yield return new WaitForSeconds(0.4f);
        gameObject.GetComponentInChildren<Animator>().SetTrigger("End");
        ghostEft.ghostDelay = 0.4f;
        Atkking = false;
    }
    IEnumerator StartDash(float DashTime)
    {
        ghostEft.ghostDelay = 0.005f;

        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        gameObject.GetComponentInChildren<Animator>().SetTrigger("StartDash");

        GameObject dashPoint = Instantiate(DashPoint, playerTransform.position, Quaternion.identity); //生成標記點在玩家身上
        Animator animator = dashPoint.GetComponent<Animator>();
        float animationSpeedMultiplier = 1f / DashTime;// 計算動畫撥放速度倍率
        animator.speed = animationSpeedMultiplier;// 設置動畫撥放速度
        Destroy(dashPoint, DashTime);
        WarningSound.Play(); //播放警示音效

        // 計算衝刺距離
        float dashDistance = Mathf.Lerp(0f, maxDashDistance * 10f, Time.time / dashDuration);

        // 計算衝刺方向
        Vector2 dashDirection = (dashPoint.transform.position - transform.position).normalized;

        // 計算衝刺速度
        Vector2 dashVelocity = dashDirection * dashSpeed * dashDistance;

        // 生成攻擊範圍特效在衝刺起點和終點的中心位置
        Vector3 dashCenter = transform.position + (dashPoint.transform.position - transform.position) * 0.5f;
        GameObject dashRangeEft = Instantiate(DashRangeWarningEft, dashCenter, Quaternion.identity);

        // 設定攻擊範圍特效的方向
        float angle = Mathf.Atan2(dashDirection.y, dashDirection.x) * Mathf.Rad2Deg;
        dashRangeEft.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Destroy(dashRangeEft, DashTime); // 摧毀特效

        yield return new WaitForSeconds(DashTime);

        DashSound.Play();

        gameObject.GetComponentInChildren<Animator>().SetTrigger("Dash");

        StartCoroutine(DashTimeCal());


        // 添加衝刺速度
        GetComponent<Rigidbody2D>().AddForce(dashVelocity, ForceMode2D.Impulse);

        // 設置衝刺
        StartCoroutine(EndDash(dashDuration));

        // 生成衝刺特效在衝刺起點和終點的中心位置
        GameObject dashEffect = Instantiate(DashEffect, dashCenter, Quaternion.identity);

        // 設定衝刺特效的方向
        dashEffect.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Destroy(dashEffect, 0.5f); // 在衝刺結束後摧毀特效

        
    }
    IEnumerator DashTimeCal()
    {
        IsDashing = true;
        yield return new WaitForSeconds(dashDuration);
        IsDashing = false;
    }
    IEnumerator EndDash(float duration) 
    {
        yield return new WaitForSeconds(duration);

        // 停止衝刺速度
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;

    }

    void Atk2()
    {
        StartCoroutine(StartAtk2());
    }
    void Atk2State2()
    {
        StartCoroutine(StartAtk2State2());
    }
    IEnumerator Atk2CoolDown()
    {
        CanAtk2 = false;
        yield return new WaitForSeconds(20f);
        CanAtk2 = true;
    }
    IEnumerator PlaySummonKatanaSound()
    {
        yield return new WaitForSeconds(2.8f);
        SummonKatanaSound.Play();
    }
    IEnumerator StartAtk2()
    {
        float spawnInterval = 0.15f;
        int totalAngles = 360;
        float angleStep = 30f;

        StartCoroutine(Atk2CoolDown());

        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        gameObject.GetComponentInChildren<Animator>().SetTrigger("Atk2");
        yield return new WaitForSeconds(1f);

        //丟刀
        for (int angle = 0; angle < totalAngles; angle += Mathf.RoundToInt(angleStep))
        {
            float angleInRadians = angle * Mathf.Deg2Rad;

            Vector3 spawnPosition = transform.position + new Vector3(0f,5f,0f) + new Vector3(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians), 0f);

            GameObject newShootKatanaObj = Instantiate(shootKatanaObj, spawnPosition, Quaternion.identity);

            newShootKatanaObj.transform.eulerAngles = new Vector3(0f, 0f, angle);

            StartCoroutine(PlaySummonKatanaSound());

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponentInChildren<Animator>().SetTrigger("Unleash2");
        yield return new WaitForSeconds(1f);

        ReleaseKatanaSound.Play();

        StartCoroutine(StartDash(0.7f));
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(StartDash(0.3f));
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(StartDash(0.3f));
        yield return new WaitForSeconds(0.4f);

        yield return new WaitForSeconds(0.4f);
        gameObject.GetComponentInChildren<Animator>().SetTrigger("End");

        ghostEft.ghostDelay = 0.4f;
        Atkking = false;

        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;
    }
    IEnumerator StartAtk2State2()
    {
        float spawnInterval = 0.15f;
        int totalAngles = 1080;
        float angleStep = 30f;
        int totalAngles2 = 360;
        float angleStep2 = 75f;

        StartCoroutine(Atk2CoolDown());

        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        gameObject.GetComponentInChildren<Animator>().SetTrigger("Atk2");
        yield return new WaitForSeconds(1f);

        //丟刀
        for (int angle = 0; angle < totalAngles; angle += Mathf.RoundToInt(angleStep))
        {
            float angleInRadians = angle * Mathf.Deg2Rad;

            Vector3 spawnPosition = transform.position + new Vector3(0f, 5f, 0f) + new Vector3(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians), 0f);

            GameObject newShootKatanaObj = Instantiate(shootKatanaObj, spawnPosition, Quaternion.identity);

            newShootKatanaObj.transform.eulerAngles = new Vector3(0f, 0f, angle);

            StartCoroutine(PlaySummonKatanaSound());

            yield return new WaitForSeconds(spawnInterval);
        }
        
        gameObject.GetComponentInChildren<Animator>().SetTrigger("Unleash2");

        yield return new WaitForSeconds(0.2f);
        //二次丟刀
        for (int angle = 0; angle < totalAngles2; angle += Mathf.RoundToInt(angleStep2))
        {
            float angleInRadians = angle * Mathf.Deg2Rad;

            Vector3 spawnPosition = transform.position + new Vector3(0f, 5f, 0f) + new Vector3(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians), 0f);

            GameObject newShootKatanaObj = Instantiate(shootKatanaObj, spawnPosition, Quaternion.identity);

            newShootKatanaObj.transform.eulerAngles = new Vector3(0f, 0f, angle);

        }

        yield return new WaitForSeconds(2f);

        ReleaseKatanaSound.Play();
        yield return new WaitForSeconds(1f);


        StartCoroutine(StartDash(0.7f));
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(StartDash(0.3f));
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(StartDash(0.3f));
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(StartDash(0.7f));
        yield return new WaitForSeconds(0.8f);

        gameObject.GetComponentInChildren<Animator>().SetTrigger("End");

        ghostEft.ghostDelay = 0.4f;
        Atkking = false;

        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;
    }

    void Atk3()
    {
        StartCoroutine(StartAtk3());
    }
    IEnumerator Atk3CoolDown()
    {
        CanAtk3 = false;
        yield return new WaitForSeconds(30f);
        CanAtk3 = true;
    }
    IEnumerator StartAtk3()
    {
        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        gameObject.GetComponentInChildren<Animator>().SetTrigger("Atk3");

        StartCoroutine(Atk3CoolDown());

        for (float i = 1.1f; i > 0.5f; i-=0.1f)
        {
            if (IsState2 == false) //第一階段
            {
                StartCoroutine(Atk3Once(i));
            }
            else //第二階段
            {
                StartCoroutine(Atk3Once2(i));
            }
                
            yield return new WaitForSeconds(i);
        }
        StartCoroutine(Atk3Once(0.45f));
        yield return new WaitForSeconds(0.45f);
        StartCoroutine(Atk3Once(0.45f));
        yield return new WaitForSeconds(0.45f);
        StartCoroutine(Atk3Once(0.45f));

        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponentInChildren<Animator>().SetTrigger("End");

        Atkking = false;

        EnemyAI.speed = DefaultMoveSpeed;

    }
    IEnumerator Atk3Once(float delaytime) //生成一條斬擊
    {
        // 隨機生成一個在0到360度之間的角度
        float randomAngle = UnityEngine.Random.Range(0f, 360f);

        // 將角度轉換為Quaternion格式
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomAngle);


        GameObject dashPoint = Instantiate(DashPoint, playerTransform.position, Quaternion.identity); //生成標記點在玩家身上
        Animator animator = dashPoint.GetComponent<Animator>();
        float animationSpeedMultiplier = 1f / delaytime;// 計算動畫撥放速度倍率
        animator.speed = animationSpeedMultiplier;// 設置動畫撥放速度
        Destroy(dashPoint, delaytime);

        WarningSound.Play();
        GameObject Warning = Instantiate(DashRangeWarningEft, playerTransform.position, randomRotation);
        Warning.transform.localScale = new Vector3(5f, 2f, 1f);
        yield return new WaitForSeconds(delaytime);
        GameObject AtkObj = Instantiate(DashEffect, Warning.transform.position, Warning.transform.rotation);
        AtkObj.transform.localScale = new Vector3(5f, 2f, 1f);
        Destroy(Warning.gameObject);
        DashSound.Play();
    }
    IEnumerator Atk3Once2(float delaytime) //生成兩條垂直的斬擊
    {
        // 隨機生成一個在0到360度之間的角度
        float randomAngle = UnityEngine.Random.Range(0f, 360f);
        float Angle2 = randomAngle + 90;

        // 將角度轉換為Quaternion格式
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomAngle);
        Quaternion Rotation2 = Quaternion.Euler(0f, 0f, Angle2);


        GameObject dashPoint = Instantiate(DashPoint, playerTransform.position, Quaternion.identity); //生成標記點在玩家身上
        Animator animator = dashPoint.GetComponent<Animator>();
        float animationSpeedMultiplier = 1f / delaytime;// 計算動畫撥放速度倍率
        animator.speed = animationSpeedMultiplier;// 設置動畫撥放速度
        Destroy(dashPoint, delaytime);

        WarningSound.Play();
        GameObject Warning = Instantiate(DashRangeWarningEft, playerTransform.position, randomRotation);
        GameObject Warning2 = Instantiate(DashRangeWarningEft, playerTransform.position, Rotation2);
        Warning.transform.localScale = new Vector3(5f, 1.5f, 1f);
        Warning2.transform.localScale = new Vector3(5f, 1.5f, 1f);
        yield return new WaitForSeconds(delaytime);
        GameObject AtkObj = Instantiate(DashEffect, Warning.transform.position, Warning.transform.rotation);
        GameObject AtkObj2 = Instantiate(DashEffect, Warning2.transform.position, Warning2.transform.rotation);
        AtkObj.transform.localScale = new Vector3(5f, 1.5f, 1f);
        AtkObj2.transform.localScale = new Vector3(5f, 1.5f, 1f);
        Destroy(Warning.gameObject);
        Destroy(Warning2.gameObject);
        DashSound.Play();
    }

    void Atk4()
    {
        if (IsState2 == false)
        {
            int A = UnityEngine.Random.Range(1, 4);
            StartCoroutine(Atk4CoolDown());
            if (A == 1)
            {
                StartCoroutine(StartAtk4A());
            }
            else if (A == 2)
            {
                StartCoroutine(StartAtk4B());
            }
            else if (A == 3)
            {
                StartCoroutine(StartAtk4C());
            }
        }
        else
        {
            int A = UnityEngine.Random.Range(1, 3);
            StartCoroutine(Atk4CoolDown());
            if (A == 1)
            {
                StartCoroutine(StartAtk4A());
            }
            else
            {
                StartCoroutine(StartAtk4B());
                StartCoroutine(StartAtk4C());
            }
        }
        
    }
    IEnumerator Atk4CoolDown()
    {
        CanAtk4 = false;
        yield return new WaitForSeconds(10f);
        CanAtk4 = true;
    }
    IEnumerator StartAtk4A()
    {
        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        gameObject.GetComponentInChildren<Animator>().SetTrigger("Atk4");
        yield return new WaitForSeconds(1f);

        if(IsState2==false)
        {
            for (int i = 0; i < 360; i += 30)
            {
                GameObject bullet = Instantiate(KatanaBullet2, transform.position, Quaternion.Euler(0, 0, i));
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * KatanaBullet2Speed, ForceMode2D.Impulse);
                ThunderWaveSound.Play();
            }
        }
        else
        {
            for (int i = 0; i < 360; i += 30)
            {
                GameObject bullet = Instantiate(KatanaBullet2, transform.position, Quaternion.Euler(0, 0, i));
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * KatanaBullet2Speed, ForceMode2D.Impulse);
                ThunderWaveSound.Play();
            }
            yield return new WaitForSeconds(0.8f);
            for (int i = 10; i < 360; i += 30)
            {
                GameObject bullet = Instantiate(KatanaBullet2, transform.position, Quaternion.Euler(0, 0, i));
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * KatanaBullet2Speed, ForceMode2D.Impulse);
                ThunderWaveSound.Play();
            }
            yield return new WaitForSeconds(0.4f);
            for (int i = 20; i < 360; i += 30)
            {
                GameObject bullet = Instantiate(KatanaBullet2, transform.position, Quaternion.Euler(0, 0, i));
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * KatanaBullet2Speed, ForceMode2D.Impulse);
                ThunderWaveSound.Play();
            }
            yield return new WaitForSeconds(0.4f);
            for (int i = 0; i < 360; i += 25)
            {
                GameObject bullet = Instantiate(KatanaBullet2, transform.position, Quaternion.Euler(0, 0, i));
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * KatanaBullet2Speed, ForceMode2D.Impulse);
                ThunderWaveSound.Play();
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.2f);
            for (int i = 10; i < 360; i += 25)
            {
                GameObject bullet = Instantiate(KatanaBullet2, transform.position, Quaternion.Euler(0, 0, i));
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * KatanaBullet2Speed, ForceMode2D.Impulse);
                ThunderWaveSound.Play();
            }
            yield return new WaitForSeconds(0.2f);
            for (int i = 20; i < 360; i += 25)
            {
                GameObject bullet = Instantiate(KatanaBullet2, transform.position, Quaternion.Euler(0, 0, i));
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * KatanaBullet2Speed, ForceMode2D.Impulse);
                ThunderWaveSound.Play();
            }
        }
        

        yield return new WaitForSeconds(2f);
        Atkking = false;

        EnemyAI.speed = DefaultMoveSpeed;
    }
    IEnumerator StartAtk4B()
    {
        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        gameObject.GetComponentInChildren<Animator>().SetTrigger("Atk4");
        yield return new WaitForSeconds(1f);

        for (int i = -32; i <= 32; i += 8)
        {
            float delaytime = 0.5f;

            GameObject dashPoint = Instantiate(DashPoint, new Vector3(0,i,0), Quaternion.identity); //生成標記點
            Animator animator = dashPoint.GetComponent<Animator>();
            float animationSpeedMultiplier = 1f / delaytime;// 計算動畫撥放速度倍率
            animator.speed = animationSpeedMultiplier;// 設置動畫撥放速度
            Destroy(dashPoint, delaytime);

            WarningSound.Play();
            GameObject Warning = Instantiate(DashRangeWarningEft, dashPoint.transform.position, Quaternion.identity);
            Warning.transform.localScale = new Vector3(5f, 2f, 1f);
            yield return new WaitForSeconds(delaytime);
            GameObject AtkObj = Instantiate(DashEffect, Warning.transform.position, Warning.transform.rotation);
            AtkObj.transform.localScale = new Vector3(5f, 2f, 1f);
            Destroy(Warning.gameObject);
            DashSound.Play();
        }

        yield return new WaitForSeconds(2f);
        Atkking = false;

        EnemyAI.speed = DefaultMoveSpeed;
    }
    IEnumerator StartAtk4C()
    {
        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        gameObject.GetComponentInChildren<Animator>().SetTrigger("Atk4");
        yield return new WaitForSeconds(1f);

        for (int i = -32; i <= 32; i += 8)
        {
            float delaytime = 0.5f;

            GameObject dashPoint = Instantiate(DashPoint, new Vector3(i, 0, 0), Quaternion.identity); //生成標記點
            Animator animator = dashPoint.GetComponent<Animator>();
            float animationSpeedMultiplier = 1f / delaytime;// 計算動畫撥放速度倍率
            animator.speed = animationSpeedMultiplier;// 設置動畫撥放速度
            Destroy(dashPoint, delaytime);

            WarningSound.Play();
            GameObject Warning = Instantiate(DashRangeWarningEft, dashPoint.transform.position, Quaternion.Euler(0,0,90f));
            Warning.transform.localScale = new Vector3(5f, 2f, 1f);
            yield return new WaitForSeconds(delaytime);
            GameObject AtkObj = Instantiate(DashEffect, Warning.transform.position, Warning.transform.rotation);
            AtkObj.transform.localScale = new Vector3(5f, 2f, 1f);
            Destroy(Warning.gameObject);
            DashSound.Play();
        }

        yield return new WaitForSeconds(2f);
        Atkking = false;

        EnemyAI.speed = DefaultMoveSpeed;
    }

    void Atk5()
    {
        StartCoroutine(Atk5CoolDown());
        StartCoroutine(StartAtk5());
    }
    IEnumerator Atk5CoolDown()
    {
        CanAtk5 = false;
        yield return new WaitForSeconds(30f);
        CanAtk5 = true;
    }
    IEnumerator StartAtk5()
    {
        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        gameObject.GetComponentInChildren<Animator>().SetTrigger("Atk2");

        for (int i = 1; i <=10; i++)
        {
            Vector3 PlayerPos = playerTransform.position;
            StartCoroutine(Atk5Slash(2f - i * 0.1f, PlayerPos));
            yield return new WaitForSeconds(1.2f-i*0.1f);
        }

        if (IsState2)
        {
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(Atk5SlashState2());

            gameObject.GetComponentInChildren<Animator>().SetTrigger("Unleash");

            yield return new WaitForSeconds(11f);
            Atkking = false;

            EnemyAI.speed = DefaultMoveSpeed;
        }
        else
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("Unleash");

            yield return new WaitForSeconds(1f);
            Atkking = false;

            EnemyAI.speed = DefaultMoveSpeed;
        }
        

    }
    IEnumerator Atk5Slash(float delay, Vector3 pos)
    {
        Atk5Sound.Play();

        float delaytime = delay;

        GameObject dashPoint = Instantiate(DashPoint, pos, Quaternion.identity); //生成標記點在玩家身上
        Animator animator = dashPoint.GetComponent<Animator>();
        float animationSpeedMultiplier = 1f / delaytime;// 計算動畫撥放速度倍率
        animator.speed = animationSpeedMultiplier;// 設置動畫撥放速度


        dashPoint.gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 1f);
        Color newColorRed = Color.red;
        dashPoint.GetComponent<SpriteRenderer>().color = newColorRed; //將標記點設為紅色

        yield return new WaitForSeconds(delaytime);

        dashPoint.gameObject.transform.localScale = new Vector3(0f, 0f, 1f); //將標記點縮小到不可見

        GameObject bigSlashEft = Instantiate(BigSlashEft, dashPoint.transform.position, Quaternion.identity);
        Destroy(bigSlashEft, 1f);

        for (int i = 0; i < 10; i++)
        {
            // 隨機生成一個在0到360度之間的角度
            float randomAngle = UnityEngine.Random.Range(0f, 360f);

            // 將角度轉換為Quaternion格式
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomAngle);

            WarningSound.Play();
            GameObject Warning = Instantiate(DashRangeWarningEft, dashPoint.transform.position, randomRotation);
            Warning.transform.localScale = new Vector3(0.8f, 0.4f, 1f);
            yield return new WaitForSeconds(0.1f);
            GameObject AtkObj = Instantiate(DashEffect, Warning.transform.position, Warning.transform.rotation);
            AtkObj.transform.localScale = new Vector3(0.8f, 0.4f, 1f);
            Destroy(Warning.gameObject);
            DashSound.Play();
        }

        Destroy(dashPoint);
    }
    IEnumerator Atk5SlashState2()
    {
        float attackRadius = 6f;
        float angleStep = 120f; //每次的旋轉角度
        for (float currentAngle = 0f; currentAngle < 360f; currentAngle += angleStep)
        {
            // 計算當前角度對應的位置
            Vector3 offset = Quaternion.Euler(0, 0, currentAngle) * Vector3.right * attackRadius;
            Vector3 targetPosition = transform.position + offset;

            // 執行攻擊
            StartCoroutine(Atk5Slash(1.5f, targetPosition)); 
        }
        yield return new WaitForSeconds(1.5f);
        for (int j = 0; j < 360; j += 30)
        {
            GameObject bullet = Instantiate(ThunderWave, transform.position, Quaternion.Euler(0, 0, j));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * ThunderWaveSpeed, ForceMode2D.Impulse);
            ThunderWaveSound.Play();
            GameObject bullet2 = Instantiate(EB3_Thunder, transform.position, Quaternion.Euler(0, 0, j + 10f));
            bullet2.GetComponent<Rigidbody2D>().AddForce(bullet2.transform.right * EB3_ThunderSpeed, ForceMode2D.Impulse);
            GameObject bullet3 = Instantiate(EB3_Thunder, transform.position, Quaternion.Euler(0, 0, j + 20f));
            bullet3.GetComponent<Rigidbody2D>().AddForce(bullet3.transform.right * EB3_ThunderSpeed, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(1.5f);

        attackRadius = 10f;
        angleStep = 120f; //每次的旋轉角度
        for (float currentAngle = 30f; currentAngle < 390f; currentAngle += angleStep)
        {
            // 計算當前角度對應的位置
            Vector3 offset = Quaternion.Euler(0, 0, currentAngle) * Vector3.right * attackRadius;
            Vector3 targetPosition = transform.position + offset;

            // 執行攻擊
            StartCoroutine(Atk5Slash(1.5f, targetPosition));
        }
        yield return new WaitForSeconds(1.5f);
        for (int j = 0; j < 360; j += 30)
        {
            GameObject bullet = Instantiate(ThunderWave, transform.position, Quaternion.Euler(0, 0, j));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * ThunderWaveSpeed, ForceMode2D.Impulse);
            ThunderWaveSound.Play();
            GameObject bullet2 = Instantiate(EB3_Thunder, transform.position, Quaternion.Euler(0, 0, j + 10f));
            bullet2.GetComponent<Rigidbody2D>().AddForce(bullet2.transform.right * EB3_ThunderSpeed, ForceMode2D.Impulse);
            GameObject bullet3 = Instantiate(EB3_Thunder, transform.position, Quaternion.Euler(0, 0, j + 20f));
            bullet3.GetComponent<Rigidbody2D>().AddForce(bullet3.transform.right * EB3_ThunderSpeed, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(1.5f);

        attackRadius = 16f;
        angleStep = 80f; //每次的旋轉角度
        for (float currentAngle = 60f; currentAngle < 420f; currentAngle += angleStep)
        {
            // 計算當前角度對應的位置
            Vector3 offset = Quaternion.Euler(0, 0, currentAngle) * Vector3.right * attackRadius;
            Vector3 targetPosition = transform.position + offset;

            // 執行攻擊
            StartCoroutine(Atk5Slash(1.5f, targetPosition));
        }
        yield return new WaitForSeconds(1.5f);
        for (int j = 0; j < 360; j += 30)
        {
            GameObject bullet = Instantiate(ThunderWave, transform.position, Quaternion.Euler(0, 0, j));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * ThunderWaveSpeed, ForceMode2D.Impulse);
            ThunderWaveSound.Play();
            GameObject bullet2 = Instantiate(EB3_Thunder, transform.position, Quaternion.Euler(0, 0, j + 10f));
            bullet2.GetComponent<Rigidbody2D>().AddForce(bullet2.transform.right * EB3_ThunderSpeed, ForceMode2D.Impulse);
            GameObject bullet3 = Instantiate(EB3_Thunder, transform.position, Quaternion.Euler(0, 0, j + 20f));
            bullet3.GetComponent<Rigidbody2D>().AddForce(bullet3.transform.right * EB3_ThunderSpeed, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(1.5f);
    }

    void Atk6()
    {
        StartCoroutine(Atk6CoolDown());
        StartCoroutine(StartAtk6());
    }
    IEnumerator Atk6CoolDown()
    {
        CanAtk6 = false;
        yield return new WaitForSeconds(30f);
        CanAtk6 = true;
    }
    IEnumerator StartAtk6()
    {
        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        gameObject.GetComponentInChildren<Animator>().SetTrigger("Atk6");
        yield return new WaitForSeconds(1f);
        WarpSound.Play();
        yield return new WaitForSeconds(0.5f);

        gameObject.transform.position = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(1f);


        if(IsState2 == false)
        {
            StartCoroutine(Atk6Throw());

            for (int i = 1; i <= 30; i++)
            {
                Vector3 RandomPos = new Vector3(UnityEngine.Random.Range(-28, 28), -1.6f, 0f);
                int A = UnityEngine.Random.Range(1, 5);
                if (A == 1)
                {
                    GameObject BambooObj = Instantiate(Bamboo1, RandomPos, Quaternion.identity);
                }
                else if (A == 2)
                {
                    GameObject BambooObj = Instantiate(Bamboo2, RandomPos, Quaternion.identity);
                }
                else if (A == 3)
                {
                    GameObject BambooObj = Instantiate(Bamboo3, RandomPos, Quaternion.identity);
                }
                else if (A == 4)
                {
                    GameObject BambooObj = Instantiate(Bamboo4, RandomPos, Quaternion.identity);
                }
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.4f));
                BambooSound.Play();
            }
        }
        else
        {
            StartCoroutine(Atk6Throw());

            for (int i = 1; i <= 60; i++)
            {
                Vector3 RandomPos = new Vector3(UnityEngine.Random.Range(-28, 28), -1.6f, 0f);
                int A = UnityEngine.Random.Range(1, 5);
                if (A == 1)
                {
                    GameObject BambooObj = Instantiate(Bamboo1, RandomPos, Quaternion.identity);
                }
                else if (A == 2)
                {
                    GameObject BambooObj = Instantiate(Bamboo2, RandomPos, Quaternion.identity);
                }
                else if (A == 3)
                {
                    GameObject BambooObj = Instantiate(Bamboo3, RandomPos, Quaternion.identity);
                }
                else if (A == 4)
                {
                    GameObject BambooObj = Instantiate(Bamboo4, RandomPos, Quaternion.identity);
                }
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.4f));
                BambooSound.Play();
            }
        }

        yield return new WaitForSeconds(2f);

        float delaytime = 1.5f;

        // 隨機生成一個在0到360度之間的角度
        float randomAngle = UnityEngine.Random.Range(0f, 360f);
        float randomAngle2 = UnityEngine.Random.Range(0f, 360f);
        float randomAngle3 = UnityEngine.Random.Range(0f, 360f);

        // 將角度轉換為Quaternion格式
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomAngle);
        Quaternion randomRotation2 = Quaternion.Euler(0f, 0f, randomAngle2);
        Quaternion randomRotation3 = Quaternion.Euler(0f, 0f, randomAngle3);

        GameObject dashPoint = Instantiate(DashPoint, transform.position, Quaternion.identity); //生成標記點
        GameObject dashPoint2 = Instantiate(DashPoint, transform.position, Quaternion.identity); //生成標記點
        GameObject dashPoint3 = Instantiate(DashPoint, transform.position, Quaternion.identity); //生成標記點
        Animator animator = dashPoint.GetComponent<Animator>();
        Animator animator2 = dashPoint2.GetComponent<Animator>();
        Animator animator3 = dashPoint3.GetComponent<Animator>();
        float animationSpeedMultiplier = 1f / delaytime;// 計算動畫撥放速度倍率
        animator.speed = animationSpeedMultiplier;// 設置動畫撥放速度
        animator2.speed = animationSpeedMultiplier;// 設置動畫撥放速度
        animator3.speed = animationSpeedMultiplier;// 設置動畫撥放速度
        Destroy(dashPoint, delaytime);
        Destroy(dashPoint2, delaytime);
        Destroy(dashPoint3, delaytime);

        WarningSound.Play();
        GameObject Warning = Instantiate(DashRangeWarningEft, dashPoint.transform.position, randomRotation);
        GameObject Warning2 = Instantiate(DashRangeWarningEft, dashPoint2.transform.position, randomRotation2);
        GameObject Warning3 = Instantiate(DashRangeWarningEft, dashPoint3.transform.position, randomRotation3);
        Warning.transform.localScale = new Vector3(3f, 3f, 1f);
        Warning2.transform.localScale = new Vector3(3f, 3f, 1f);
        Warning3.transform.localScale = new Vector3(3f, 3f, 1f);
        yield return new WaitForSeconds(delaytime);
        GameObject AtkObj = Instantiate(DashEffect, Warning.transform.position, Warning.transform.rotation);
        GameObject AtkObj2 = Instantiate(DashEffect, Warning2.transform.position, Warning2.transform.rotation);
        GameObject AtkObj3 = Instantiate(DashEffect, Warning3.transform.position, Warning3.transform.rotation);
        AtkObj.transform.localScale = new Vector3(3f, 3f, 1f);
        AtkObj2.transform.localScale = new Vector3(3f, 3f, 1f);
        AtkObj3.transform.localScale = new Vector3(3f, 3f, 1f);
        Destroy(Warning.gameObject);
        Destroy(Warning2.gameObject);
        Destroy(Warning3.gameObject);
        DashSound.Play();

        yield return new WaitForSeconds(1f);

        gameObject.GetComponentInChildren<Animator>().SetTrigger("End");

        yield return new WaitForSeconds(1f);

        //每幾秒發射刀波攻擊幾次
        Atkking = false;

        EnemyAI.speed = DefaultMoveSpeed;
    }
    IEnumerator Atk6Throw()
    {
        if(IsState2==false)
        {
            for (int i = 1; i <= 4; i++)
            {
                gameObject.GetComponentInChildren<Animator>().SetTrigger("Throw");
                yield return new WaitForSeconds(0.2f);
                for (int j = 0; j < 360; j += 30)
                {
                    GameObject bullet = Instantiate(ThunderWave, transform.position, Quaternion.Euler(0, 0, j));
                    bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * ThunderWaveSpeed, ForceMode2D.Impulse);
                    ThunderWaveSound.Play();
                    GameObject bullet2 = Instantiate(EB3_Thunder, transform.position, Quaternion.Euler(0, 0, j + 10f));
                    bullet2.GetComponent<Rigidbody2D>().AddForce(bullet2.transform.right * EB3_ThunderSpeed, ForceMode2D.Impulse);
                    GameObject bullet3 = Instantiate(EB3_Thunder, transform.position, Quaternion.Euler(0, 0, j + 20f));
                    bullet3.GetComponent<Rigidbody2D>().AddForce(bullet3.transform.right * EB3_ThunderSpeed, ForceMode2D.Impulse);
                }
                yield return new WaitForSeconds(2f);
            }
        }
        else
        {
            for (int i = 1; i <= 14; i++)
            {
                gameObject.GetComponentInChildren<Animator>().SetTrigger("Throw");
                yield return new WaitForSeconds(0.2f);
                for (int j = 0; j < 360; j += 20)
                {
                    GameObject bullet = Instantiate(ThunderWave, transform.position, Quaternion.Euler(0, 0, j));
                    bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * ThunderWaveSpeed, ForceMode2D.Impulse);
                    ThunderWaveSound.Play();
                    GameObject bullet2 = Instantiate(EB3_Thunder, transform.position, Quaternion.Euler(0, 0, j + 5f));
                    bullet2.GetComponent<Rigidbody2D>().AddForce(bullet2.transform.right * EB3_ThunderSpeed, ForceMode2D.Impulse);
                    GameObject bullet3 = Instantiate(EB3_Thunder, transform.position, Quaternion.Euler(0, 0, j + 10f));
                    bullet3.GetComponent<Rigidbody2D>().AddForce(bullet3.transform.right * EB3_ThunderSpeed, ForceMode2D.Impulse);
                }
                yield return new WaitForSeconds(1f);
            }
        }
        
    }
    void Ult()
    {
        StartCoroutine(StartUlt());
    }
    IEnumerator UltCoolDown(float time)
    {
        CanUlt = false;
        yield return new WaitForSeconds(time);
        CanUlt = true;
    }
    IEnumerator StartUlt()
    {
        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        GameObject CloudEft = Instantiate(UltEft2, new Vector3(0, 0, 0), Quaternion.identity);
        Destroy(CloudEft, 6f);

        GameObject FT = Instantiate(FloatingText);
        FT.GetComponentInChildren<TextMeshProUGUI>().text = "必須待在角落躲避敵人的攻擊!";

        gameObject.GetComponentInChildren<Animator>().SetTrigger("Ult");
        yield return new WaitForSeconds(1f);
        WarpSound.Play();
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().Shake(3.5f, 0.2f));//搖晃畫面

        gameObject.transform.position = new Vector3(0, 0, 0);

        UltEft1.SetActive(true);

        GameObject ShieldObj = Instantiate(Boss2UltPlayerShield, new Vector3(0,0,0), Quaternion.identity); //生成護盾

        UltAttractPlayer = true; //開始把玩家朝boss方向吸

        yield return new WaitForSeconds(3.5f);

        GameObject bigSlashEft = Instantiate(BigSlashEft, new Vector3(0, 9f, 0), Quaternion.identity);
        bigSlashEft.gameObject.transform.localScale = new Vector3(9f, 9f, 1f);
        Destroy(bigSlashEft, 1f);

        yield return new WaitForSeconds(0.5f);

        UltAttractPlayer = false; //停止把玩家朝boss方向吸

        UltEft1.SetActive(false);

        if (ShieldObj.GetComponent<Boss2PlayerShield>().PlayerIsSafe==false) //如果玩家沒有在護盾內
        {
            StartCoroutine(UltCoolDown(100f)); //技能冷卻100秒

            GameObject FT2 = Instantiate(FloatingText);
            FT2.GetComponentInChildren<TextMeshProUGUI>().text = "被敵人標記了!";
            FT2.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;

            WarpSound.Play();
            StartCoroutine(UltAtk());
            Destroy(ShieldObj);
        }
        else
        {
            StartCoroutine(UltCoolDown(30f)); //技能冷卻30秒

            GameObject FT2 = Instantiate(FloatingText);
            FT2.GetComponentInChildren<TextMeshProUGUI>().text = "躲避了敵人的攻擊!";

            gameObject.GetComponentInChildren<Animator>().SetTrigger("End");

            Destroy(ShieldObj);

            yield return new WaitForSeconds(1f);

            Atkking = false;

            EnemyAI.speed = DefaultMoveSpeed;
        }

    }
    IEnumerator UltAtk()
    {
        float pushForce = 20f; // 推開玩家的力量
        float timeSlowDuration = 2f; // 時間慢放持續時間


        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;


        gameObject.GetComponentInChildren<Animator>().SetTrigger("Dash");

        yield return new WaitForSeconds(1f);

        StartCoroutine(GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().Shake(3.5f, 0.2f));//搖晃畫面

        // 順移到玩家旁
        Vector3 ultDestination = playerTransform.position;
        transform.position = ultDestination;
        WarpSound.Play();

        GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        CameraEft.GetComponent<Animator>().SetTrigger("Eft");

        //播放不受時間影響的音效
        timeSlowDownSoundSource.PlayOneShot(timeSlowDownSoundClip);

        yield return new WaitForSeconds(0.3f);

        // 時間慢放
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSeconds(0.2f);

        // 推開玩家
        Vector2 pushDirection = (playerTransform.position - transform.position).normalized;
        playerTransform.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);

        StartCoroutine(UltSlash(0.5f));

        yield return new WaitForSeconds(timeSlowDuration); // 等待時間慢放持續時間

        // 恢復時間
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        CameraEft.GetComponent<Animator>().SetTrigger("End");

        yield return new WaitForSeconds(3f);

        Atkking = false;

        EnemyAI.speed = DefaultMoveSpeed;
    }

    IEnumerator UltSlash(float delay)
    {
        Transform parentObject = GameObject.FindWithTag("Player").gameObject.transform;


        Atk5Sound.Play();

        float delaytime = delay;

        GameObject dashPoint = Instantiate(DashPoint, playerTransform.position, Quaternion.identity); //生成標記點
        dashPoint.transform.SetParent(parentObject,true); //將dashPoint變為玩家的子物件

        Animator animator = dashPoint.GetComponent<Animator>();
        float animationSpeedMultiplier = 1f / delaytime;// 計算動畫撥放速度倍率
        animator.speed = animationSpeedMultiplier;// 設置動畫撥放速度


        dashPoint.gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 1f);
        Color newColorRed = Color.red;
        dashPoint.GetComponent<SpriteRenderer>().color = newColorRed; //將標記點設為紅色

        yield return new WaitForSeconds(delaytime);

        dashPoint.gameObject.transform.localScale = new Vector3(0f, 0f, 1f); //將標記點縮小到不可見

        GameObject bigSlashEft = Instantiate(BigSlashEft, dashPoint.transform.position, Quaternion.identity);
        bigSlashEft.transform.SetParent(parentObject, true); //將bigSlashEft變為玩家的子物件
        Destroy(bigSlashEft, 1f);

        for (int i = 0; i < 10; i++)
        {
            // 隨機生成一個在0到360度之間的角度
            float randomAngle = UnityEngine.Random.Range(0f, 360f);

            // 將角度轉換為Quaternion格式
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomAngle);

            WarningSound.Play();
            GameObject Warning = Instantiate(DashRangeWarningEft, dashPoint.transform.position, randomRotation);
            Warning.transform.localScale = new Vector3(2.4f, 1.2f, 1f);
            Warning.transform.SetParent(parentObject, true); //將Warning變為玩家的子物件
            yield return new WaitForSeconds(0.1f);
            GameObject AtkObj = Instantiate(DashEffect, Warning.transform.position, Warning.transform.rotation);
            AtkObj.transform.localScale = new Vector3(2.4f, 1.2f, 1f);
            AtkObj.transform.SetParent(parentObject, true); //將AtkObj變為玩家的子物件
            Destroy(Warning.gameObject);
            DashSound.Play();

            playerTransform.gameObject.GetComponent<PlayerInfo>().TakeSpecialDamage(5);
        }

        Destroy(dashPoint);

        playerTransform.gameObject.GetComponent<PlayerInfo>().TakeDamage(10);
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

        Instantiate(SceneDoor, new Vector3(0,-3,0), Quaternion.identity);

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
}
