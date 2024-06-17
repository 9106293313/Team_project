using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Boss3Skill : MonoBehaviour
{
    public GameObject SceneDoor;
    public GameObject FloatingText;
    public EnemyAI EnemyAI;
    float DefaultMoveSpeed;
    ///////////////
    private Transform playerTransform;
    public Transform Boss3AtkPosition1;
    public GameObject Eb4_Ice;
    public GameObject Boss3IceBomb;
    public GameObject LaserPrefab;
    public GameObject LaserEndRotateObj,LaserEndPos1, LaserEndPos2, LaserEndPos3, LaserEndPos4, LaserEndPos5, LaserEndPos6, LaserEndPos7, LaserEndPos8;
    public GameObject IceWall;
    public GameObject Eb4_Fire, Boss3FireBomb,FireSwordShooter,Boss3BigFireBall;
    public GameObject SmallFireBall;
    ///////////////////////
    public AudioSource Atk1Sound, ShieldOpenSound, ShieldCloseSound,LaserStartSound,LaserLoopSound,PlaceBombSound,ShootFireSwordSound;
    /////////////////
    bool Atkking = false;
    bool CanAtk1 = true;
    bool CanAtk2 = true;
    bool CanAtk3 = true;
    bool CanAtk4 = true;
    bool CanAtk5 = true;
    bool CanAtk6 = true;
    bool CanAtk7 = true;
    //////////////

    public GameObject ShieldObj;
    bool IsLaserCharging = false;
    bool IsState2 = false;
    bool ChangeingState = false;

    public GameObject BulletCleanObj, ExplodeEft;
    bool IsDead = false;


    void Start()
    {
        StartCoroutine(OpenEnemyAI());
        playerTransform = GameObject.FindWithTag("Player").transform;
        DefaultMoveSpeed = EnemyAI.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<BossInfo>().curHealth <= 0 && IsDead == false)
        {
            IsDead = true;
            StartCoroutine(BossDead());
        }

        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Boss3Idle"))
        {
            if (Atkking == false)
            {
                if (gameObject.GetComponent<BossInfo>().curHealth < (gameObject.GetComponent<BossInfo>().maxHealth * 0.5f))
                {
                    if (IsState2 == false)
                    {
                        ChangeState();
                    }
                }
            }
            if (Atkking == false)
            {
                if (ChangeingState == false)
                {
                    RandomAtk();
                }
            }
        }
        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Boss3State2Idle"))
        {
            if (Atkking == false)
            {
                RandomAtk();
            }
        }

        if (IsLaserCharging)
        {
            if (ShieldObj.activeInHierarchy == false) //在大招集氣期間護盾破裂
            {
                StartCoroutine(LaserFail());
            }
        }
    }
    void ChangeState()
    {
        StartCoroutine(PlayChangeStateAM());
    }
    IEnumerator PlayChangeStateAM()
    {
        ChangeingState = true;

        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        GameObject FT = Instantiate(FloatingText);
        FT.GetComponentInChildren<TextMeshProUGUI>().text = "小心!敵人的攻擊將變得更加猛烈!";

        //gameObject.GetComponentInChildren<Animator>().SetTrigger("ChangeState");
        IsState2 = true;
        yield return new WaitForSeconds(4f);

        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;

        ChangeingState = false;

    }
    IEnumerator OpenEnemyAI()
    {
        yield return new WaitForSeconds(1f);
        EnemyAI.enabled = true;
    }
    public void RandomAtk()
    {
        Atkking = true;

        List<int> options;
        if(!IsState2)
        {
            options = new List<int> { 1, 2, 3, 4, 8 }; // 第一階段可選的技能
        }
        else
        {
            options = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 }; // 第二階段可選的技能
        }
        
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
        if (!CanAtk7)
        {
            options.Remove(7);
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
            Atk7();
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
        yield return new WaitForSeconds(1f);
        CanAtk1 = true;
    }
    IEnumerator StartAtk1()
    {
        
        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Boss3State2Idle"))
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("ToggleState");
            yield return new WaitForSeconds(2f);
        }

        StartCoroutine(Atk1CoolDown());

        gameObject.GetComponentInChildren<Animator>().SetTrigger("Rise");
        yield return new WaitForSeconds(1f);
        if(!IsState2)
        {
            NormalAtk(Eb4_Ice, 10f, 20, 360);
        }
        else
        {
            NormalAtk(Eb4_Ice, 5f, 15, 360);
            yield return new WaitForSeconds(0.3f);
            NormalAtk(Eb4_Ice, 8f, 20, 360);
            yield return new WaitForSeconds(0.3f);
            NormalAtk(Eb4_Ice, 12f, 25, 360);
            yield return new WaitForSeconds(1f);
            NormalAtk(Eb4_Fire, 15f, 10, 360);
        }
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponentInChildren<Animator>().SetTrigger("End");
        yield return new WaitForSeconds(1f);

        Atkking = false;

        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;
    }
    void NormalAtk(GameObject BulletType,float BulletSpeed ,int Radius, int MaxRadius) //朝周圍扇形攻擊，可設定角度和子彈
    {
        Atk1Sound.Play();

        for (int i = 0; i < MaxRadius; i+=Radius)
        {
            GameObject bullet = Instantiate(BulletType, Boss3AtkPosition1.position, Quaternion.Euler(0, 0, i));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * BulletSpeed, ForceMode2D.Impulse);
        }
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
        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Boss3State2Idle"))
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("ToggleState");
            yield return new WaitForSeconds(2f);
        }

        StartCoroutine(Atk2CoolDown());

        gameObject.GetComponentInChildren<Animator>().SetTrigger("Cast");

        yield return new WaitForSeconds(0.5f);

        if(!IsState2)
        {
            StartCoroutine(SpawnIceBomb());
            for (int i = 0; i < 5; i++)
            {
                PlaceBombSound.Play();
                yield return new WaitForSeconds(0.2f);
            }
        }
        else
        {
            StartCoroutine(SpawnIceBomb2());
            StartCoroutine(SpawnFireBomb());
            for (int i = 0; i < 8; i++)
            {
                PlaceBombSound.Play();
                yield return new WaitForSeconds(0.2f);
            }
        }

        yield return new WaitForSeconds(1f);

        Atkking = false;

        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;

    }
    IEnumerator SpawnIceBomb()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 RandomPos = new Vector3(UnityEngine.Random.Range(-8f, 8f), UnityEngine.Random.Range(-8f, 8f), 0f);
            Instantiate(Boss3IceBomb, transform.position + RandomPos, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator SpawnIceBomb2()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 RandomPos = new Vector3(UnityEngine.Random.Range(-8f, 8f), UnityEngine.Random.Range(-8f, 8f), 0f);
            Instantiate(Boss3IceBomb, transform.position + RandomPos, Quaternion.identity);
            Vector3 RandomPos2 = new Vector3(UnityEngine.Random.Range(-12f, 12f), UnityEngine.Random.Range(-12f, 12f), 0f);
            Instantiate(Boss3IceBomb, transform.position + RandomPos2, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator SpawnFireBomb()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3 RandomPos = new Vector3(UnityEngine.Random.Range(-20f, 20f), UnityEngine.Random.Range(-20f, 20f), 0f);
            Instantiate(Boss3FireBomb, transform.position + RandomPos, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }
    void Atk3()
    {
        StartCoroutine(StartAtk3());
    }
    IEnumerator Atk3FailCoolDown()
    {
        CanAtk3 = false;
        yield return new WaitForSeconds(20f);
        CanAtk3 = true;
    }
    IEnumerator Atk3SucceseCoolDown()
    {
        CanAtk3 = false;
        yield return new WaitForSeconds(45f);
        CanAtk3 = true;
    }
    IEnumerator StartAtk3()
    {
        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Boss3State2Idle"))
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("ToggleState");
            yield return new WaitForSeconds(2f);
        }

        gameObject.GetComponentInChildren<Animator>().SetTrigger("Rise");

        yield return new WaitForSeconds(1.5f);


        ShieldOpenSound.Play();
        ShieldObj.SetActive(true);
        IsLaserCharging = true;
        GetComponent<BossInfo>().CanTakeDamage = false;
        GameObject FT = Instantiate(FloatingText); //生成提示字
        FT.GetComponentInChildren<TextMeshProUGUI>().text = "擊破護盾，阻止敵人攻擊!";
        yield return new WaitForSeconds(5f);
        if (IsLaserCharging)
        {
            StartCoroutine(GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().Shake(0.4f, 0.2f));//搖晃畫面

            StartCoroutine(Atk3SucceseCoolDown()); //成功施放，冷卻長
            IsLaserCharging = false;
            ShieldCloseSound.Play();
            ShieldObj.GetComponent<EnemyInfo>().ShieldBreak();
            GetComponent<BossInfo>().CanTakeDamage = true;
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(LaserAtk());
        }

        yield break;
    }
    IEnumerator LaserAtk()
    {
        
        StartCoroutine(LaserControl());

        yield return new WaitForSeconds(20f);
        
        gameObject.GetComponentInChildren<Animator>().SetTrigger("End");

        LaserEndRotateObj.GetComponent<KeepRotate>().rotateSpeed = 0;
        LaserEndRotateObj.GetComponent<Transform>().rotation = Quaternion.identity;

        yield return new WaitForSeconds(1f);

        Atkking = false;

        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;
    }
    IEnumerator LaserAnimatorControl()
    {
        if(!IsState2)
        {
            LaserEndRotateObj.GetComponent<Animator>().SetTrigger("Move");
        }
        else
        {
            LaserEndRotateObj.GetComponent<Animator>().SetTrigger("Move2");
        }
        yield return new WaitForSeconds(15f);
        LaserEndRotateObj.GetComponent<Animator>().SetTrigger("End");
    }
    IEnumerator LaserSoundControl()
    {
        LaserStartSound.Play();
        yield return new WaitForSeconds(1f);
        LaserLoopSound.Play();
        yield return new WaitForSeconds(17f);
        LaserLoopSound.Stop();
        LaserStartSound.Play();
    }
    IEnumerator LaserControl()
    {
        StartCoroutine(LaserAnimatorControl());

        StartCoroutine(LaserSoundControl());

        if (!IsState2)
        {
            StartCoroutine(SpawnLaser(LaserEndPos1));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(SpawnLaser(LaserEndPos2));
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            StartCoroutine(SpawnLaser(LaserEndPos1));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(SpawnLaser(LaserEndPos2));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(SpawnLaser(LaserEndPos3));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(SpawnLaser(LaserEndPos4));
            yield return new WaitForSeconds(0.1f);
        }

        

        for (int i = 0; i < 100; i+=10)
        {
            LaserEndRotateObj.GetComponent<KeepRotate>().rotateSpeed = i;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(13f);
        for (int i = 100; i > 0; i -= 10)
        {
            LaserEndRotateObj.GetComponent<KeepRotate>().rotateSpeed = i;
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator SpawnLaser(GameObject LaserEndObj)
    {
        GameObject laserAtk = Instantiate(LaserPrefab, transform.position, Quaternion.identity);
        laserAtk.transform.SetParent(gameObject.transform);
        laserAtk.GetComponent<LaserScript>().startTransform = Boss3AtkPosition1.transform;
        laserAtk.GetComponent<LaserScript>().targetTransform = LaserEndObj.transform;

        yield return new WaitForSeconds(19.5f);

        Destroy(laserAtk);
    }
    void ShieldBreak()
    {
        ShieldCloseSound.Play();
        Debug.Log("ShieldBreak");
        GetComponent<BossInfo>().CanTakeDamage = true;
    }
    IEnumerator LaserFail()
    {
        StartCoroutine(Atk3FailCoolDown()); //攻擊失敗進冷卻短
        IsLaserCharging = false;
        ShieldBreak();
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 1; i++) //爆炸的次數
        {
            Vector3 Pos = new Vector3(transform.position.x + UnityEngine.Random.Range(-0.5f, 0.5f), transform.position.y + UnityEngine.Random.Range(-0.5f, 0.5f), 0f);
            Instantiate(ExplodeEft, Pos, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(0.5f);
        GetComponentInChildren<Animator>().SetTrigger("End");

        yield return new WaitForSeconds(1f);

        Atkking = false;

        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;
    }

    void Atk4()
    {
        StartCoroutine(StartAtk4());
    }
    IEnumerator Atk4CoolDown()
    {
        CanAtk4 = false;
        yield return new WaitForSeconds(20f);
        CanAtk4 = true;
    }
    IEnumerator StartAtk4()
    {
        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Boss3State2Idle"))
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("ToggleState");
            yield return new WaitForSeconds(2f);
        }

        StartCoroutine(Atk4CoolDown());

        gameObject.GetComponentInChildren<Animator>().SetTrigger("Rise");
        yield return new WaitForSeconds(2f);

        if(!IsState2)
        {
            Atk1Sound.Play();
            GameObject bullet = Instantiate(IceWall, Boss3AtkPosition1.position, Quaternion.Euler(0, 0, 60f));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * 20f, ForceMode2D.Impulse);
            GameObject bullet2 = Instantiate(IceWall, Boss3AtkPosition1.position, Quaternion.Euler(0, 0, 120f));
            bullet2.GetComponent<Rigidbody2D>().AddForce(bullet2.transform.right * 20f, ForceMode2D.Impulse);

            yield return new WaitForSeconds(8f);
        }
        else
        {
            Atk1Sound.Play();
            GameObject bullet = Instantiate(IceWall, Boss3AtkPosition1.position, Quaternion.Euler(0, 0, 45f));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * 20f, ForceMode2D.Impulse);
            GameObject bullet2 = Instantiate(IceWall, Boss3AtkPosition1.position, Quaternion.Euler(0, 0, 135f));
            bullet2.GetComponent<Rigidbody2D>().AddForce(bullet2.transform.right * 20f, ForceMode2D.Impulse);
            GameObject bullet3 = Instantiate(IceWall, Boss3AtkPosition1.position, Quaternion.Euler(0, 0, 225f));
            bullet3.GetComponent<Rigidbody2D>().AddForce(bullet3.transform.right * 20f, ForceMode2D.Impulse);
            GameObject bullet4 = Instantiate(IceWall, Boss3AtkPosition1.position, Quaternion.Euler(0, 0, 325f));
            bullet4.GetComponent<Rigidbody2D>().AddForce(bullet4.transform.right * 20f, ForceMode2D.Impulse);

            yield return new WaitForSeconds(8f);
        }
        
        gameObject.GetComponentInChildren<Animator>().SetTrigger("End");
        yield return new WaitForSeconds(1f);

        Atkking = false;

        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;
    }
    void Atk5()
    {
        StartCoroutine(StartAtk5());
    }
    IEnumerator Atk5CoolDown()
    {
        CanAtk5 = false;
        yield return new WaitForSeconds(20f);
        CanAtk5 = true;
    }
    IEnumerator StartAtk5()
    {
        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Boss3Idle"))
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("ToggleState");
            yield return new WaitForSeconds(2f);
        }

        StartCoroutine(Atk5CoolDown());

        gameObject.GetComponentInChildren<Animator>().SetTrigger("FireSword");
        yield return new WaitForSeconds(2f);
        StartCoroutine(SpawnFireSwordShooter(3,0.5f,5f));
        yield return new WaitForSeconds(2f);
        StartCoroutine(SpawnFireSwordShooter(2, 0.3f,8f));
        yield return new WaitForSeconds(0.9f);
        StartCoroutine(SpawnFireSwordShooter(10, 0.1f,16f));
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnFireSwordShooter(25, 0.1f, 4f));

        yield return new WaitForSeconds(2f);

        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;

        yield return new WaitForSeconds(3f);
        Atkking = false;
    }
    IEnumerator SpawnFireSwordShooter(int times,float delayTime,float Range)
    {
        for (int i = 0; i < times; i++)
        {
            ShootFireSwordSound.Play();
            Vector3 RandomPos = new Vector3(UnityEngine.Random.Range(-Range, Range), UnityEngine.Random.Range(-Range, Range), 0f);
            Instantiate(FireSwordShooter, transform.position + RandomPos, Quaternion.identity);
            yield return new WaitForSeconds(delayTime);
        }
    }

    void Atk6()
    {
        StartCoroutine(StartAtk6());
    }
    IEnumerator Atk6CoolDown()
    {
        CanAtk6 = false;
        yield return new WaitForSeconds(20f);
        CanAtk6 = true;
    }
    IEnumerator StartAtk6()
    {
        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Boss3Idle"))
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("ToggleState");
            yield return new WaitForSeconds(2f);
        }

        StartCoroutine(Atk6CoolDown());

        gameObject.GetComponentInChildren<Animator>().SetTrigger("FireCast");
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 8; i++)
        {
            ShootFireSwordSound.Play();
            Vector3 RandomPos = new Vector3(UnityEngine.Random.Range(-30f, 30f), UnityEngine.Random.Range(30f, 40f), 0f);
            Instantiate(Boss3BigFireBall, RandomPos, Quaternion.identity);
            yield return new WaitForSeconds(1.2f);
        }

        yield return new WaitForSeconds(1.2f);

        gameObject.GetComponentInChildren<Animator>().SetTrigger("End");


        Atkking = false;

        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;
    }

    void Atk7()
    {
        StartCoroutine(StartAtk7());
    }
    IEnumerator Atk7CoolDown()
    {
        CanAtk7 = false;
        yield return new WaitForSeconds(35f);
        CanAtk7 = true;
    }
    IEnumerator StartAtk7()
    {
        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Boss3Idle"))
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("ToggleState");
            yield return new WaitForSeconds(2f);
        }

        transform.position = new Vector3(0,8,0);

        StartCoroutine(Atk7CoolDown());

        gameObject.GetComponentInChildren<Animator>().SetTrigger("GreatFireSword");
        yield return new WaitForSeconds(1.5f);

        StartCoroutine(Atk7Shoot());
        yield return new WaitForSeconds(15f);
        StartCoroutine(SpawnFireSwordShooter(4, 0f, 5f));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(SpawnFireSwordShooter(6, 0f, 5f));
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnFireSwordShooter(10, 0.1f, 5f));

        yield return new WaitForSeconds(4f);

        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;

        yield return new WaitForSeconds(2f);

        Atkking = false;
    }
    IEnumerator Atk7Shoot()
    {
        for (int i = 0; i < 16; i++)
        {
            Atk1Sound.Play();
            int RandomNum = UnityEngine.Random.Range(5, 8);
            for (int j = 0; j < RandomNum; j++)
            {
                int RandomAngle = UnityEngine.Random.Range(-20, 20);
                float RandomSpeed = UnityEngine.Random.Range(20f, 30f);
                GameObject bullet = Instantiate(SmallFireBall, gameObject.transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90 + RandomAngle));
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * RandomSpeed, ForceMode2D.Impulse);
            }  
            yield return new WaitForSeconds(0.8f);
        }
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

        Instantiate(SceneDoor, new Vector3(0, -5, 0), Quaternion.identity);

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
