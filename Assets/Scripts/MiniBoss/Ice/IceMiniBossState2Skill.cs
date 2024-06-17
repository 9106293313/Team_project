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
    IEnumerator StartRandomAtk()//�����e��2�����L����
    {
        Atkking = true;
        yield return new WaitForSeconds(2f);
        RandomAtk();
    }
    public void RandomAtk()
    {

        List<int> options = new List<int> { 1, 2}; // �i�諸�ƭ�

        if (!CanAtk1)
        {
            options.Remove(1);
        }

        int A = options[UnityEngine.Random.Range(0, options.Count)]; // �q�ѤU���ƭȤ��H����ܤ@��
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

        // �����
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        BodyAnimator.SetTrigger("Atk");
        WingAnimator.SetTrigger("Atk");
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 4; i++)
        {
            float angleStep = 60f; //�C60�׵o�g�@���l�u
            float addAngle = 10f; //�C�g���@��l�u��U�@�i�l�u����10��

            for (float angle = i * addAngle; angle < 360f + i * addAngle; angle += angleStep)
            {
                StartCoroutine(Atk1FireBullet(angle));
                AtkSound.Play();
            }
            yield return new WaitForSeconds(0.1f);
        }


        //��_���ʳt��
        EnemyAI.speed = DefaultMoveSpeed;
        Atkking = false;
    }
    private IEnumerator Atk1FireBullet(float angle)
    {
        GameObject bullet = Instantiate(IceWaveObj, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // �p��l�u����l�t�פ�V
        Vector2 bulletDirection = Quaternion.Euler(0, 0, angle) * Vector2.right;

        // �]�m�l�u������A�Ϩ��V�ǰt�����V
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        // ��l�t�סA0.5�����ܬ��̲׳t��
        bulletRb.velocity = 2f * bulletDirection;

        // ����0.5��
        yield return new WaitForSeconds(0.5f);

        // ���ܳt�׬��̲׳t��
        bulletRb.velocity = 8f * bulletDirection;
    }
}
