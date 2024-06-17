using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMiniBossSkill : MonoBehaviour
{
    public EnemyAI EnemyAI;
    float DefaultMoveSpeed;

    public GameObject IceMiniBossState2;
    public GameObject IceMiniBossBulletObj, Eb4_Ice, Eb3_Ice, IceWaveObj;

    bool Atkking = false;
    bool CanAtk1 = true;
    bool CanAtk2 = true;

    private Transform playerTransform;
    bool IsDead = false;
    public GameObject BulletCleanObj, ExplodeEft;
    public Animator BodyAnimator,WingAnimator;
    public AudioSource AtkSound, AtkSound2;
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        DefaultMoveSpeed = EnemyAI.speed;
    }
    void Update()
    {
        if (gameObject.GetComponent<BossInfo>().curHealth <= gameObject.GetComponent<BossInfo>().maxHealth *0.5f)
        {
            gameObject.GetComponent<BossInfo>().curHealth = Convert.ToInt32(gameObject.GetComponent<BossInfo>().maxHealth * 0.5f);//�O���@�b����q
            if (IsDead == false)
            {
                IsDead = true;
                StartCoroutine(BossDead()); //�o�̤����`�令����
            }
        }
        if (BodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (Atkking == false)
            {
                StartCoroutine(StartRandomAtk());
            }
        }
    }
    IEnumerator StartRandomAtk()//�����e��2�����L����
    {
        Atkking = true;
        yield return new WaitForSeconds(2f);
        RandomAtk();
    }
    public void RandomAtk()
    {

        List<int> options = new List<int> { 1, 2, 3 }; // �i�諸�ƭ�

        if (!CanAtk1)
        {
            options.Remove(1);
        }
        if (!CanAtk2)
        {
            options.Remove(2);
        }

        int A = options[UnityEngine.Random.Range(0, options.Count)]; // �q�ѤU���ƭȤ��H����ܤ@��
        if (A == 1)
        {
            Atk1();
        }
        else if (A == 2)
        {
            Atk2();
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
        yield return new WaitForSeconds(6f);
        CanAtk1 = true;
    }
    IEnumerator StartAtk1()
    {
        StartCoroutine(Atk1CoolDown());

        // �����
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        BodyAnimator.SetTrigger("Atk");
        WingAnimator.SetTrigger("Atk");
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 8; i++)
        {
            float angleStep = 30f; //�C30�׵o�g�@���l�u
            float addAngle = 8f; //�C�g���@��l�u��U�@�i�l�u����8��

            for (float angle = i * addAngle; angle < 360f + i * addAngle; angle += angleStep)
            {
                StartCoroutine(Atk1FireBullet(angle));
                AtkSound.Play();
            }
            yield return new WaitForSeconds(0.3f);
        }


        //��_���ʳt��
        EnemyAI.speed = DefaultMoveSpeed;
        Atkking = false;
    }
    private IEnumerator Atk1FireBullet(float angle)
    {
        GameObject bullet = Instantiate(IceMiniBossBulletObj, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // �p��l�u����l�t�פ�V
        Vector2 bulletDirection = Quaternion.Euler(0, 0, angle) * Vector2.right;

        // �]�m�l�u������A�Ϩ��V�ǰt�����V
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        // ��l�t�סA0.8�����ܬ��̲׳t��
        bulletRb.velocity = 2f * bulletDirection;

        // ����0.8��
        yield return new WaitForSeconds(0.8f);

        // ���ܳt�׬��̲׳t��
        bulletRb.velocity = 12f * bulletDirection;
    }
    void Atk2()
    {
        StartCoroutine(StartAtk2());
    }
    IEnumerator Atk2CoolDown()
    {
        CanAtk2 = false;
        yield return new WaitForSeconds(15f);
        CanAtk2 = true;
    }
    IEnumerator StartAtk2()
    {
        StartCoroutine(Atk2CoolDown());

        // �����
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        BodyAnimator.SetTrigger("Atk");
        WingAnimator.SetTrigger("Atk");
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(Atk2Shoot(45f));
        StartCoroutine(Atk2Shoot(-45f));
        StartCoroutine(Atk2Shoot(135f));
        StartCoroutine(Atk2Shoot(-135f));
        ShootIceWave();
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(Atk2Shoot(0f));
        StartCoroutine(Atk2Shoot(90f));
        StartCoroutine(Atk2Shoot(180f));
        StartCoroutine(Atk2Shoot(270f));
        ShootIceWave();
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(Atk2Shoot(45f));
        StartCoroutine(Atk2Shoot(-45f));
        StartCoroutine(Atk2Shoot(135f));
        StartCoroutine(Atk2Shoot(-135f));
        ShootIceWave();
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(Atk2Shoot(0f));
        StartCoroutine(Atk2Shoot(90f));
        StartCoroutine(Atk2Shoot(180f));
        StartCoroutine(Atk2Shoot(270f));
        ShootIceWave();
        yield return new WaitForSeconds(0.8f);

        //��_���ʳt��
        EnemyAI.speed = DefaultMoveSpeed;
        Atkking = false;
    }
    void ShootIceWave()
    {
        for (float i = 0; i < 360f; i += 45)
        {
            Vector3 direction = Quaternion.Euler(0f, 0f, i) * transform.right;
            GameObject bulletObj = Instantiate(IceWaveObj, transform.position, Quaternion.identity);
            bulletObj.transform.right = direction;

            bulletObj.GetComponent<Rigidbody2D>().AddForce(bulletObj.transform.right * 12f, ForceMode2D.Impulse);
        }
    }
    IEnumerator Atk2Shoot(float angle)
    {
        StartCoroutine(Atk2ShootBullet(angle));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Atk2ShootBullet(angle + 10f));
        StartCoroutine(Atk2ShootBullet(angle - 10f));
        AtkSound2.Play();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Atk2ShootBullet(angle + 20f));
        StartCoroutine(Atk2ShootBullet(angle - 20f));
        AtkSound2.Play();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Atk2ShootBullet(angle + 30f));
        StartCoroutine(Atk2ShootBullet(angle - 30f));
        AtkSound2.Play();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Atk2ShootBullet(angle + 30f));
        StartCoroutine(Atk2ShootBullet(angle - 30f));
        AtkSound2.Play();


    }
    IEnumerator Atk2ShootBullet(float angle)
    {
        GameObject bullet = Instantiate(Eb4_Ice, transform.position, Quaternion.Euler(0, 0, angle));
        Vector2 bulletDirection = Quaternion.Euler(0, 0, angle) * Vector2.right;
        bullet.GetComponent<Rigidbody2D>().velocity = 2f * bulletDirection;
        yield return new WaitForSeconds(1f);
        bullet.GetComponent<Rigidbody2D>().velocity = 10f * bulletDirection;
    }
    IEnumerator BossDead()
    {
        // �����
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        StartCoroutine(PlayBossDeadEft());
        Instantiate(BulletCleanObj, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.2f);
        Instantiate(BulletCleanObj, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.2f);
        Instantiate(IceMiniBossState2, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
    IEnumerator PlayBossDeadEft()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 Pos = new Vector3(transform.position.x + UnityEngine.Random.Range(-1f, 1f), transform.position.y + UnityEngine.Random.Range(-1f, 1f), 0f);
            Instantiate(ExplodeEft, Pos, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }

    }
}
