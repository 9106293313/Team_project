using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class boss1State2Skill : MonoBehaviour
{
    public GameObject MagicCircleFollowObj;
    Vector3 StartPos;
    [HideInInspector] public EnemyAI enemyAI;
    float DefaultSpeed;
    public Transform AtkPoint1;
    public Transform AtkPoint2;
    public GameObject BulletA; //���K�b��
    public float BulletASpeed = 10f;
    public GameObject FireExplode; //���K�z���S��
    public GameObject WormSummoner;

    public Boss1SoundEft Boss1SoundEft;

    public GameObject MagicCircleEft; //�]�k�}�S��
    public GameObject MagicCircleEft2; //�]�k�}2�S��
    public GameObject MagicCircleEft3; //�]�k�}3�S��
    public GameObject ULTMagicCircle;
    public GameObject ShieldObj;
    public Transform AtkPointRotate1;
    public Transform AtkPointRotate2;
    public Transform MagicCircleAtkPoint1, MagicCircleAtkPoint2, MagicCircleAtkPoint3, MagicCircleAtkPoint4, MagicCircleAtkPoint5;
    public GameObject FireWave;
    public float FireWaveSpeed = 12f;
    public GameObject PoisonBall;
    public float PoisonBallSpeed = 6f;
    public GameObject EB4_Fire;
    public float EB4_FireSpeed = 10f;
    public GameObject EB4_Poison;
    public float EB4_PoisonSpeed = 6f;
    public GameObject BigFirePillow;
    public GameObject FirePillowWarning;
    public GameObject FirePillowWarningNoSound;

    bool CanMagicCircleAtk = true;//�O�_����]�k�}����
    bool CanFireRush = true; //�O�_����K�Ĩ�
    bool CanULT = true; //�O�_��j��
    bool IsULT = false; //�O�_���b�j�۶���(20��)

    public GameObject floatingText; //���ܦr
    public GameObject ExplodeEft; //�ۨ��z���ĪG

    bool IsDead=false;
    public GameObject BossDeadObj;
    public GameObject BulletCleanObj;
    public AudioSource BossDeadSound;
    public GameObject GameWinObj;

    void Start()
    {
        StartPos = transform.position;
        enemyAI=GetComponent<EnemyAI>();
        DefaultSpeed = enemyAI.speed;

        GameObject MC_Follow = Instantiate(MagicCircleFollowObj, transform.position, Quaternion.identity);
        MC_Follow.GetComponent<MagicCircleFollow>().target = this.transform;

        AtkPointRotate1.position = this.transform.position + new Vector3(-0.75f,-0.25f,0f); //�]�m�o�g�_�I����m�A�קK��X��X��
        AtkPointRotate2.position = this.transform.position + new Vector3(0.75f, -0.25f, 0f);

        StartCoroutine(ULTCoolDownSmall()); //�@�}�l�����j�۶i�N�o20��

    }
    private void Update()
    {
        if(IsULT)
        {
            if(ShieldObj.activeInHierarchy==false) //�b�j�۶�������@�ޯ}��
            {
                StartCoroutine(ULTFail());
            }
        }
        if(gameObject.GetComponent<BossInfo>().curHealth<=0 && IsDead==false)
        {
            IsDead = true;
            StartCoroutine(BossDead());
        }
        if(IsDead)
        {
            if (GameObject.Find("PoisonWorm") != null)
            {
                Destroy(GameObject.Find("PoisonWorm"));
            }
        }
    }

    public void BacktoNormalMoveSpeed(float delay)
    {
        StartCoroutine(StartBacktoNormalMoveSpeed(delay));
    }
    IEnumerator StartBacktoNormalMoveSpeed(float delay)
    {
        yield return new WaitForSeconds(delay);
        enemyAI.speed = DefaultSpeed;
    }
    public void NormalAtk()
    {
        int i = UnityEngine.Random.Range(1, 4); //1~3
        if (i == 1)
        { StartCoroutine(NormalAtkA()); }
        if (i == 2)
        { StartCoroutine(NormalAtkB()); }
        if (i == 3)
        { StartCoroutine(NormalAtkC()); }

    }
    IEnumerator NormalAtkA()
    {
        Instantiate(FireExplode, AtkPoint1.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(FireExplode, AtkPoint2.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
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

        Instantiate(FireExplode, AtkPoint1.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(FireExplode, AtkPoint2.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
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
        if (i == 1)
        {
            Instantiate(FireExplode, AtkPoint1.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.8f);
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
            Instantiate(FireExplode, AtkPoint2.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.8f);
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

    public void RandomAtk()
    {
        List<int> options = new List<int> { 1, 2, 3, 4 }; // �i�諸�ƭ�

        if (!CanMagicCircleAtk)
        {
            options.Remove(2);
        }
        if (!CanFireRush)
        {
            options.Remove(3);
        }
        if (!CanULT)
        {
            options.Remove(4);
        }

        int A = options[Random.Range(0, options.Count)]; // �q�ѤU���ƭȤ��H����ܤ@��
        if (A == 1)
        {
            GetComponentInChildren<Animator>().SetTrigger("Summon");
        }
        if (A == 2)
        {
            GetComponentInChildren<Animator>().SetTrigger("MagicCircle");
        }
        if (A == 3)
        {
            GetComponentInChildren<Animator>().SetTrigger("FireRush");
        }
        if (A == 4)
        {
            GetComponentInChildren<Animator>().SetTrigger("ULT");
        }
    }

    public void Summon()
    {
        int A;
        A = UnityEngine.Random.Range(1, 3); //1~2
        if(A == 1)
        {
            StartCoroutine(StartSummon()); //�l����
        }
        if(A == 2)
        {
            StartCoroutine(StartSummonFireBullet()); //�l���
        }  
    }
    IEnumerator StartSummon()
    {
        for(int i = 0; i < 10; i++)
        {
            Boss1SoundEft.PlayPoisonArrowSound();
            Instantiate(WormSummoner, AtkPoint1.position + new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f),0f), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1f);
        GetComponentInChildren<Animator>().SetTrigger("HandDown");
    }
    IEnumerator StartSummonFireBullet()
    {
        AtkPointRotate1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        AtkPointRotate1.GetComponent<KeepRotate>().SetRotateSpeed(120f); //�]�w����t��
        for (int i = 0; i < 30; i++)
        {
            Boss1SoundEft.PlayNormalAtkSound();
            GameObject bullet01 = Instantiate(FireWave, AtkPointRotate1.position , AtkPointRotate1.rotation);
            bullet01.GetComponent<Rigidbody2D>().AddForce(bullet01.transform.right * FireWaveSpeed, ForceMode2D.Impulse);
            GameObject bullet02 = Instantiate(FireWave, AtkPointRotate1.position, AtkPointRotate1.rotation * Quaternion.Euler(0f, 0f, 120f));
            bullet02.GetComponent<Rigidbody2D>().AddForce(bullet02.transform.right * FireWaveSpeed, ForceMode2D.Impulse);
            GameObject bullet03 = Instantiate(FireWave, AtkPointRotate1.position, AtkPointRotate1.rotation * Quaternion.Euler(0f, 0f, 240f));
            bullet03.GetComponent<Rigidbody2D>().AddForce(bullet03.transform.right * FireWaveSpeed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(1f);
        GetComponentInChildren<Animator>().SetTrigger("HandDown");
    }

    public void MagicCircleAtk()
    {
        int A;
        A = UnityEngine.Random.Range(1, 3); //1~2
        if(A==1)
        {
            StartCoroutine(StartMagicCircleAtk());
            StartCoroutine(MagicCircleAtkCoolDown());
        }
        else if(A==2)
        {
            StartCoroutine(StartMagicCircleAtk2());
            StartCoroutine(MagicCircleAtkCoolDown());
        }
    }
    IEnumerator MagicCircleAtkCoolDown()
    {
        CanMagicCircleAtk = false;
        yield return new WaitForSeconds(40f);
        CanMagicCircleAtk = true;
    }
    IEnumerator StartMagicCircleAtk()
    {
        
        AtkPointRotate1.transform.rotation = Quaternion.Euler(new Vector3(0,0,0)); //���m���ਤ��
        AtkPointRotate1.GetComponent<KeepRotate>().SetRotateSpeed(160f); //�]�w����t��
        AtkPointRotate2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        AtkPointRotate2.GetComponent<KeepRotate>().SetRotateSpeed(160f); //�]�w����t��
        MagicCircleEft.SetActive(true);

        StartCoroutine(MC_Poison_1());

        float initialFireRate = 1.5f;  // ��l�g�t
        float maxFireRate = 0.08f;  // �̤j�g�t
        float currentFireRate = initialFireRate;
        float timer = 0f;
        float duration = 20f; // ��������ɶ�
        float elapsedTime = 0f;
        float timeToMaxFireRate = 8f; // ��F�̤j�g�t���ɶ�

        while (elapsedTime < duration)
        {
            if (timer >= currentFireRate)
            {
                Boss1SoundEft.PlayNormalAtkSound();

                // �ͦ��l�u
                GameObject bullet01 = Instantiate(BulletA, AtkPointRotate1.position, AtkPointRotate1.rotation);
                bullet01.GetComponent<Rigidbody2D>().AddForce(bullet01.transform.right * BulletASpeed, ForceMode2D.Impulse);
                GameObject bullet03 = Instantiate(BulletA, AtkPointRotate1.position, AtkPointRotate1.rotation * Quaternion.Euler(0, 0, 180f));
                bullet03.GetComponent<Rigidbody2D>().AddForce(bullet03.transform.right * BulletASpeed, ForceMode2D.Impulse);

                GameObject bullet04 = Instantiate(BulletA, AtkPointRotate2.position, AtkPointRotate2.rotation * Quaternion.Euler(0, 0, 180f));
                bullet04.GetComponent<Rigidbody2D>().AddForce(bullet04.transform.right * BulletASpeed, ForceMode2D.Impulse);
                GameObject bullet02 = Instantiate(BulletA, AtkPointRotate2.position, AtkPointRotate2.rotation);
                bullet02.GetComponent<Rigidbody2D>().AddForce(bullet02.transform.right * BulletASpeed, ForceMode2D.Impulse);

                // ���]�p�ɾ��ܶq
                timer = 0f;

                // �[�t�l�u�o�g�t��
                if (elapsedTime < timeToMaxFireRate)
                {
                    currentFireRate = Mathf.Lerp(initialFireRate, maxFireRate, elapsedTime / timeToMaxFireRate);
                }
                else
                {
                    currentFireRate = maxFireRate;
                }
            }

            // ��s�p�ɾ��M�g�L�ɶ�
            timer += Time.deltaTime;
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(1f);
        GetComponentInChildren<Animator>().SetTrigger("HandDown");
        MagicCircleEft.SetActive(false);
    }
    IEnumerator MC_FireWave_1()
    {
        yield return new WaitForSeconds(6f);
        for (int i = 0; i < 10; i++)
        {
            Boss1SoundEft.PlayNormalAtkSound();

            for (int j = 0; j < 12; j++)
            {
                GameObject bullet01 = Instantiate(EB4_Fire, AtkPointRotate1.position, AtkPointRotate1.rotation * Quaternion.Euler(0f, 0f, j* 30f));
                bullet01.GetComponent<Rigidbody2D>().AddForce(bullet01.transform.right * EB4_FireSpeed, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(1.4f);
        }
    }

    IEnumerator MC_Poison_1()
    {
        yield return new WaitForSeconds(6f);
        for (int i = 0; i < 10; i++)
        {
            Boss1SoundEft.PlayPoisonArrowSound();

            for (int j = 0; j < 12; j++)
            {
                GameObject bullet01 = Instantiate(PoisonBall, AtkPointRotate1.position, AtkPointRotate1.rotation * Quaternion.Euler(0f, 0f, j * 30f));
                bullet01.GetComponent<Rigidbody2D>().AddForce(bullet01.transform.right * PoisonBallSpeed, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(1.4f);
        }
    }
    IEnumerator StartMagicCircleAtk2()
    {
        AtkPointRotate1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        AtkPointRotate1.GetComponent<KeepRotate>().SetRotateSpeed(160f); //�]�w����t��
        AtkPointRotate2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        AtkPointRotate2.GetComponent<KeepRotate>().SetRotateSpeed(160f); //�]�w����t��
        MagicCircleEft.SetActive(true);

        StartCoroutine(MC_FireWave_1());

        float initialFireRate = 1.5f;  // ��l�g�t
        float maxFireRate = 0.08f;  // �̤j�g�t
        float currentFireRate = initialFireRate;
        float timer = 0f;
        float duration = 20f; // ��������ɶ�
        float elapsedTime = 0f;
        float timeToMaxFireRate = 8f; // ��F�̤j�g�t���ɶ�

        while (elapsedTime < duration)
        {
            if (timer >= currentFireRate)
            {
                Boss1SoundEft.PlayPoisonArrowSound();

                // �ͦ��l�u
                GameObject bullet01 = Instantiate(EB4_Poison, AtkPointRotate1.position, AtkPointRotate1.rotation);
                bullet01.GetComponent<Rigidbody2D>().AddForce(bullet01.transform.right * EB4_PoisonSpeed, ForceMode2D.Impulse);
                GameObject bullet02 = Instantiate(EB4_Poison, AtkPointRotate2.position, AtkPointRotate2.rotation);
                bullet02.GetComponent<Rigidbody2D>().AddForce(bullet02.transform.right * EB4_PoisonSpeed, ForceMode2D.Impulse);
                GameObject bullet03 = Instantiate(EB4_Poison, AtkPointRotate1.position, AtkPointRotate1.rotation * Quaternion.Euler(0, 0, 180f));
                bullet03.GetComponent<Rigidbody2D>().AddForce(bullet03.transform.right * EB4_PoisonSpeed, ForceMode2D.Impulse);
                GameObject bullet04 = Instantiate(EB4_Poison, AtkPointRotate2.position, AtkPointRotate2.rotation * Quaternion.Euler(0, 0, 180f));
                bullet04.GetComponent<Rigidbody2D>().AddForce(bullet04.transform.right * EB4_PoisonSpeed, ForceMode2D.Impulse);

                // ���]�p�ɾ��ܶq
                timer = 0f;

                // �[�t�l�u�o�g�t��
                if (elapsedTime < timeToMaxFireRate)
                {
                    currentFireRate = Mathf.Lerp(initialFireRate, maxFireRate, elapsedTime / timeToMaxFireRate);
                }
                else
                {
                    currentFireRate = maxFireRate;
                }
            }

            // ��s�p�ɾ��M�g�L�ɶ�
            timer += Time.deltaTime;
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(1f);
        GetComponentInChildren<Animator>().SetTrigger("HandDown");
        MagicCircleEft.SetActive(false);
    }

    public void FireRush()
    {
        StartCoroutine(EnemyRushAndShoot());
        StartCoroutine(FireRushCoolDown());
    }
    IEnumerator FireRushCoolDown()
    {
        CanFireRush = false;
        yield return new WaitForSeconds(80f);
        CanFireRush = true;
    }
    IEnumerator EnemyRushAndShoot() //���K�Ĩ�
    {
        MagicCircleEft2.SetActive(true);

        StartCoroutine(GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().Shake(1f, 0.15f));//�n�̵e��

        StartCoroutine(StartFirePillow());

        Boss1SoundEft.PlayFirePillowChargeSound();
        yield return new WaitForSeconds(2f);

        float rushSpeed = 30f;  // �Ĩ�t��
        float rushDuration = 1f;  // �Ĩ����ɶ�
        float rushInterval = 2f;  // �Ĩ붡�j�ɶ�
        float shootInterval = 0.15f;  // �g�����j�ɶ�
        float shootAngle = 30f;  // ���ήg������
        float elapsedTime = 0f;
        float totalTime = 9f;  // �`����ɶ�
        GameObject player = GameObject.FindWithTag("Player");

        while (elapsedTime < totalTime)
        {
            Boss1SoundEft.PlayPoisonArrowSound();

            // �V���a��V�Ĩ�
            Vector2 direction = (player.transform.position - transform.position).normalized;
            float timer = 0f;
            while (timer < rushDuration)
            {
                transform.Translate(direction * rushSpeed * Time.deltaTime);
                timer += Time.deltaTime;

                // �b�Ĩ�L�{���g��
                if (timer % shootInterval < Time.deltaTime)
                {
                    ShootInDirection(-direction, shootAngle);
                }

                yield return null;
            }

            // ���ݤ@�q�ɶ��A�i��U�@���Ĩ�
            elapsedTime += rushDuration;
            if (elapsedTime < totalTime)
            {
                if (elapsedTime + rushInterval > totalTime)
                {
                    yield return new WaitForSeconds(totalTime - elapsedTime);
                }
                else
                {
                    yield return new WaitForSeconds(rushInterval);
                }
            }
        }
        yield return new WaitForSeconds(1f);
        MagicCircleEft2.SetActive(false);
        GetComponentInChildren<Animator>().SetTrigger("HandDown");
        StartCoroutine(StartBackToStartPoint());
        
    }
    void ShootInDirection(Vector2 direction, float angle)
    {
        Boss1SoundEft.PlayNormalAtkSound();

        // �p��g����V�M���Ψ���
        float startAngle = Vector2.SignedAngle(Vector2.right, direction) - angle / 2f;
        float endAngle = startAngle + angle;

        // �b���νd�򤺥ͦ��l�u
        for (float i = startAngle; i <= endAngle; i += 6f)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, i);
            GameObject bullet01 = Instantiate(FireWave, transform.position, rotation);
            bullet01.GetComponent<Rigidbody2D>().AddForce(bullet01.transform.right * FireWaveSpeed, ForceMode2D.Impulse);
        }
    }
    IEnumerator StartFirePillow()
    {
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < 5; i++)
        {
            FirePillow();
            yield return new WaitForSeconds(5f);
        }
    }
    public void FirePillow()
    {
        GameObject player = GameObject.FindWithTag("Player");

        Vector3 AtkPoint = new Vector3(player.transform.position.x + UnityEngine.Random.Range(-3.0f, 3.0f), -3f, player.transform.position.z);
        StartCoroutine(SpawnBigFirePillow(AtkPoint));
    }
    IEnumerator SpawnBigFirePillow(Vector3 AtkPoint)
    {
        Instantiate(FirePillowWarning, AtkPoint, Quaternion.identity);
        yield return new WaitForSeconds(1.3f);
        Boss1SoundEft.PlayFirePillowSound();
        Instantiate(BigFirePillow, AtkPoint, Quaternion.identity);
    }
    IEnumerator StartBackToStartPoint()
    {
        Destroy(GameObject.FindWithTag("MagicCircleFollow"));
        GameObject A =  Instantiate(MagicCircleEft3, StartPos, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        transform.position = StartPos;
        yield return new WaitForSeconds(1f);
        GameObject MC_Follow = Instantiate(MagicCircleFollowObj, transform.position, Quaternion.identity);
        MC_Follow.GetComponent<MagicCircleFollow>().target = this.transform;
        Destroy(A);
    }

    public void ULT()
    {
        StartCoroutine(StartULT());
    }
    IEnumerator ULTCoolDown()
    {
        CanULT = false;
        yield return new WaitForSeconds(60f);
        CanULT = true;
    }
    IEnumerator ULTCoolDownSmall()
    {
        CanULT = false;
        yield return new WaitForSeconds(20f);
        CanULT = true;
    }
    IEnumerator StartULT()
    {
        Boss1SoundEft.PlayShieldOpenSound();
        ULTMagicCircle.SetActive(true);
        ShieldObj.SetActive(true);
        IsULT = true;
        GetComponent<BossInfo>().CanTakeDamage = false;
        GameObject FT = Instantiate(floatingText); //�ͦ����ܦr
        FT.GetComponentInChildren<TextMeshProUGUI>().text = "���}�@�ޡA����ĤH�I�񵴩�!";
        yield return new WaitForSeconds(15f);
        if(IsULT)
        {
            StartCoroutine(GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().Shake(0.8f, 0.15f));//�n�̵e��

            StartCoroutine(ULTCoolDown()); //���\�I��j�ۡA�N�o60��
            IsULT =false;
            Debug.Log("ULT");
            Boss1SoundEft.PlayShieldCloseSound();
            ShieldObj.GetComponent<EnemyInfo>().ShieldBreak();
            GetComponent<BossInfo>().CanTakeDamage = true;
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(ULTAtk());
            yield return new WaitForSeconds(25f);
            GetComponentInChildren<Animator>().SetTrigger("HandDown");
            ULTMagicCircle.SetActive(false);
        }

        yield break;
    }
    void ShieldBreak()
    {
        Boss1SoundEft.PlayShieldCloseSound();
        Debug.Log("ShieldBreak");
        GetComponent<BossInfo>().CanTakeDamage = true;
    }
    IEnumerator ULTFail()
    {
        StartCoroutine(ULTCoolDownSmall()); //�j�ۥ��ѧN�o20��
        IsULT = false;
        ShieldBreak();
        ULTMagicCircle.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 5; i++)
        {
            Vector3 Pos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), 0f);
            Instantiate(ExplodeEft, Pos, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(0.5f);
        GetComponentInChildren<Animator>().SetTrigger("HandDown");
    }
    IEnumerator ULTAtk()
    {
        Vector3 Pos = new Vector3(StartPos.x, -3f, 0f);
        Vector3 PosA = new Vector3(StartPos.x + 20f, -3f, 0f);
        Vector3 PosB = new Vector3(StartPos.x +-20f, -3f, 0f);
        Instantiate(FirePillowWarning, Pos, Quaternion.identity);
        Instantiate(FirePillowWarningNoSound, PosA, Quaternion.identity);
        Instantiate(FirePillowWarningNoSound, PosB, Quaternion.identity);
        yield return new WaitForSeconds(1.3f);
        Boss1SoundEft.PlayFirePillowSound();
        Instantiate(BigFirePillow, Pos, Quaternion.identity);
        Instantiate(BigFirePillow, PosA, Quaternion.identity);
        Instantiate(BigFirePillow, PosB, Quaternion.identity);

        AtkPointRotate1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        AtkPointRotate1.GetComponent<KeepRotate>().SetRotateSpeed(200f); //�]�w����t��
        AtkPointRotate2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        AtkPointRotate2.GetComponent<KeepRotate>().SetRotateSpeed(-200f); //�]�w����t��

        // �Ĥ@�i�ϥ� EB4_Fire �l�u�A�`�� 5 ��
        StartCoroutine(ULTFireBullet(AtkPointRotate1, EB4_Fire, EB4_FireSpeed, 10, 360f, 0.5f, 0f, 5f));
        // �ĤG�i�ϥ� EB4_Poison �l�u�A�`�� 5 ��
        StartCoroutine(ULTFireBullet(AtkPointRotate2, EB4_Poison, EB4_PoisonSpeed, 10, 360f, 0.5f, 0f, 5f));
        yield return new WaitForSeconds(5f);
        StartCoroutine(ULTFireBullet(AtkPointRotate1, EB4_Poison, EB4_PoisonSpeed, 10, 360f, 0.5f, 0f, 5f));
        StartCoroutine(ULTFireBullet(AtkPointRotate2, EB4_Fire, EB4_FireSpeed, 10, 360f, 0.5f, 0f, 5f));
        yield return new WaitForSeconds(5f);

        Vector3 PosC = new Vector3(StartPos.x + 10, -3f, 0f);
        Vector3 PosD = new Vector3(StartPos.x + 30f, -3f, 0f);
        Vector3 PosE = new Vector3(StartPos.x + -10f, -3f, 0f);
        Vector3 PosF = new Vector3(StartPos.x + -30f, -3f, 0f);
        Instantiate(FirePillowWarning, PosC, Quaternion.identity);
        Instantiate(FirePillowWarning, PosD, Quaternion.identity);
        Instantiate(FirePillowWarning, PosE, Quaternion.identity);
        Instantiate(FirePillowWarning, PosF, Quaternion.identity);
        yield return new WaitForSeconds(1.3f);
        Boss1SoundEft.PlayFirePillowSound();
        Instantiate(BigFirePillow, PosC, Quaternion.identity);
        Instantiate(BigFirePillow, PosD, Quaternion.identity);
        Instantiate(BigFirePillow, PosE, Quaternion.identity);
        Instantiate(BigFirePillow, PosF, Quaternion.identity);

        StartCoroutine(ULTFireBullet(AtkPointRotate1, EB4_Fire, EB4_FireSpeed, 10, 360f, 0.5f, 0f, 5f));
        StartCoroutine(ULTFireBullet(AtkPointRotate2, EB4_Poison, EB4_PoisonSpeed, 10, 360f, 0.5f, 0f, 5f));
        yield return new WaitForSeconds(5f);
        StartCoroutine(ULTFireBullet(AtkPointRotate1, EB4_Poison, EB4_FireSpeed, 10, 360f, 0.5f, 0f, 5f));
        StartCoroutine(ULTFireBullet(AtkPointRotate2, EB4_Fire, EB4_PoisonSpeed, 10, 360f, 0.5f, 0f, 5f));
        yield return new WaitForSeconds(5f);

    }

    //bulletSpeed �O�ݭn�ۤv�w�q���l�u�t�סA
    //atkPoint �O�ݭn�ǤJ�������I��m�A
    //bulletPrefab �O�ݭn�ǤJ���l�u�w����A
    //bulletCount �O�l�u�ƶq�A
    //bulletAngle �O�l�u���o�g�d�򮰧Ψ��סA
    //waveDelay �O�C�i�l�u�����j�ɶ��A
    //bulletDelay �O�C�o�g�@�i�l�u�᪺���ݮɶ��A
    //duration �O�ӧ������`����ɶ��C
    IEnumerator ULTFireBullet(Transform atkPoint, GameObject bulletPrefab,float bulletSpeed ,int bulletCount, float bulletAngle, float waveDelay, float bulletDelay, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                Boss1SoundEft.PlayNormalAtkSound();

                Vector2 bulletDirection = Quaternion.Euler(0f, 0f, -bulletAngle / 2f + i * bulletAngle / (bulletCount - 1f)) * atkPoint.right;
                GameObject bullet = Instantiate(bulletPrefab, atkPoint.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
            }
            yield return new WaitForSeconds(waveDelay);

            timer += waveDelay;

            if (timer < duration)
            {
                yield return new WaitForSeconds(bulletDelay);
                timer += bulletDelay;
            }
        }
    }

    IEnumerator BossDead()
    {
        StartCoroutine(GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().Shake(1.2f, 0.15f));//�n�̵e��
        enemyAI.speed = 0;
        StartCoroutine(PlayBossDeadEft());
        GetComponentInChildren<Animator>().SetTrigger("Death");
        Instantiate(BulletCleanObj, transform.position, Quaternion.identity);
        Destroy(GameObject.FindWithTag("MagicCircleFollow"));
        yield return new WaitForSeconds(1.2f);
        Instantiate(BossDeadObj,transform.position,Quaternion.identity);
        Instantiate(BulletCleanObj, transform.position, Quaternion.identity);
        Instantiate(GameWinObj);
        Destroy(gameObject);
    }
    IEnumerator PlayBossDeadEft()
    {
        for (int i = 0; i < 8; i++)
        {
            Vector3 Pos = new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f), 0f);
            Instantiate(ExplodeEft, Pos, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
        
    }
}
