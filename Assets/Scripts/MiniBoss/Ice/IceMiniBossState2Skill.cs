using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMiniBossState2Skill : MonoBehaviour
{
    public EnemyAI EnemyAI;
    float DefaultMoveSpeed;

    bool Atkking = false;
    bool CanAtk1 = true;

    private Transform playerTransform;
    public Animator BodyAnimator, WingAnimator;
    public GameObject IceMiniBossBulletObj, IceWaveObj;
    public AudioSource AtkSound;

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
    IEnumerator StartRandomAtk()//攻擊前停2秒讓他移動
    {
        Atkking = true;
        yield return new WaitForSeconds(2f);
        RandomAtk();
    }
    public void RandomAtk()
    {

        List<int> options = new List<int> { 1, 2}; // 可選的數值

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
        yield return new WaitForSeconds(3f);
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
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 4; i++)
        {
            float angleStep = 60f; //每60度發射一顆子彈
            float addAngle = 10f; //每射完一圈子彈後下一波子彈偏移10度

            for (float angle = i * addAngle; angle < 360f + i * addAngle; angle += angleStep)
            {
                StartCoroutine(Atk1FireBullet(angle));
                AtkSound.Play();
            }
            yield return new WaitForSeconds(0.1f);
        }


        //恢復移動速度
        EnemyAI.speed = DefaultMoveSpeed;
        Atkking = false;
    }
    private IEnumerator Atk1FireBullet(float angle)
    {
        GameObject bullet = Instantiate(IceWaveObj, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // 計算子彈的初始速度方向
        Vector2 bulletDirection = Quaternion.Euler(0, 0, angle) * Vector2.right;

        // 設置子彈的旋轉，使其方向匹配飛行方向
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        // 初始速度，0.5秒後改變為最終速度
        bulletRb.velocity = 2f * bulletDirection;

        // 等待0.5秒
        yield return new WaitForSeconds(0.5f);

        // 改變速度為最終速度
        bulletRb.velocity = 8f * bulletDirection;
    }
}
