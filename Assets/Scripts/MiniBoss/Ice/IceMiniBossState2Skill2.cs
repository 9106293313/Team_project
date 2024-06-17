using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMiniBossState2Skill2 : MonoBehaviour
{
    public EnemyAI EnemyAI;
    float DefaultMoveSpeed;

    bool Atkking = false;
    bool CanAtk1 = true;
    bool CanAtk2 = true;

    private Transform playerTransform;
    public Animator BodyAnimator, WingAnimator;
    public GameObject IceWall,IceMiniBossBullet,Eb4_Ice,IceWaveObj;
    public AudioSource AtkSound,ChargeSound;
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        DefaultMoveSpeed = EnemyAI.speed;
    }

    void Update()
    {
        if (BodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (Atkking == false)
            {
                StartCoroutine(StartRandomAtk());
            }
        }
    }
    IEnumerator StartRandomAtk()//攻擊前停3秒讓他移動
    {
        Atkking = true;
        yield return new WaitForSeconds(3f);
        RandomAtk();
    }
    public void RandomAtk()
    {

        List<int> options = new List<int> { 1,2 }; // 可選的數值

        if (!CanAtk1)
        {
            options.Remove(1);
        }

        int A = options[UnityEngine.Random.Range(0, options.Count)]; // 從剩下的數值中隨機選擇一個
        if (A == 1)
        {
            Atk1();
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
        yield return new WaitForSeconds(15f);
        CanAtk1 = true;
    }
    IEnumerator StartAtk1()
    {
        StartCoroutine(Atk1CoolDown());

        // 停止移動
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        BodyAnimator.SetTrigger("Atk");
        WingAnimator.SetTrigger("Atk");
        ChargeSound.Play();
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(Atk2Shoot(45f));
        StartCoroutine(Atk2Shoot(-45f));
        StartCoroutine(Atk2Shoot(135f));
        StartCoroutine(Atk2Shoot(-135f));
        StartCoroutine(Atk2Shoot(0f));
        StartCoroutine(Atk2Shoot(90f));
        StartCoroutine(Atk2Shoot(180f));
        StartCoroutine(Atk2Shoot(270f));
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(Atk2Shoot(45f));
        StartCoroutine(Atk2Shoot(-45f));
        StartCoroutine(Atk2Shoot(135f));
        StartCoroutine(Atk2Shoot(-135f));
        StartCoroutine(Atk2Shoot(0f));
        StartCoroutine(Atk2Shoot(90f));
        StartCoroutine(Atk2Shoot(180f));
        StartCoroutine(Atk2Shoot(270f));
        yield return new WaitForSeconds(0.5f);
        ShootIceWave();
        yield return new WaitForSeconds(0.8f);

        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;
        Atkking = false;
    }
    void ShootIceWave()
    {
        for (float i = 0; i < 360f; i += 30)
        {
            Vector3 direction = Quaternion.Euler(0f, 0f, i) * transform.right;
            GameObject bulletObj = Instantiate(IceWaveObj, transform.position, Quaternion.identity);
            bulletObj.transform.right = direction;

            bulletObj.GetComponent<Rigidbody2D>().AddForce(bulletObj.transform.right * 6f, ForceMode2D.Impulse);
        }
    }
    IEnumerator Atk2Shoot(float angle)
    {
        StartCoroutine(Atk2ShootBullet(angle));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Atk2ShootBullet(angle + 10f));
        StartCoroutine(Atk2ShootBullet(angle - 10f));
        AtkSound.Play();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Atk2ShootBullet(angle + 20f));
        StartCoroutine(Atk2ShootBullet(angle - 20f));
        AtkSound.Play();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Atk2ShootBullet(angle + 30f));
        StartCoroutine(Atk2ShootBullet(angle - 30f));
        AtkSound.Play();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Atk2ShootBullet(angle + 30f));
        StartCoroutine(Atk2ShootBullet(angle - 30f));
        AtkSound.Play();


    }
    IEnumerator Atk2ShootBullet(float angle)
    {
        GameObject bullet = Instantiate(Eb4_Ice, transform.position, Quaternion.Euler(0, 0, angle));
        Vector2 bulletDirection = Quaternion.Euler(0, 0, angle) * Vector2.right;
        bullet.GetComponent<Rigidbody2D>().velocity = 2f * bulletDirection;
        yield return new WaitForSeconds(1f);
        bullet.GetComponent<Rigidbody2D>().velocity = 10f * bulletDirection;
    }
}
