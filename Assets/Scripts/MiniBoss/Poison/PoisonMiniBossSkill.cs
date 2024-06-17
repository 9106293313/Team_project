using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonMiniBossSkill : MonoBehaviour
{
    public EnemyAI EnemyAI;
    float DefaultMoveSpeed;
    float DefaultMinDistance;

    bool Atkking = false;
    bool CanAtk1 = true;
    bool CanAtk2 = true;
    bool CanAtk3 = true;

    bool ChangeColor = false;

    public GameObject PoisonSplitBullet,Eb4_Poison,Eb3_Poison, WormSummoner, PoisonArrowSplit;
    public AudioSource AtkSound, PoisonArrowSound, DashSound;

    private Transform playerTransform;
    bool IsDead = false;
    public GameObject BulletCleanObj, ExplodeEft;
    Animator animator;
    bool enemyImpact;
    public GameObject PoisonSummonPrefab; //�l�ꪫ���y(����boss����o)
    void Start()
    {
        StartCoroutine(OpenEnemyAI());
        playerTransform = GameObject.FindWithTag("Player").transform;
        DefaultMoveSpeed = EnemyAI.speed;
        DefaultMinDistance = EnemyAI.MinDistance;

        animator = gameObject.GetComponentInChildren<Animator>();
        enemyImpact = false;
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
        if (ChangeColor)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0xE2 / 255f, 0x5A / 255f, 0xFF / 255f, 1f);
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
        List<int> options = new List<int> {1, 2, 3, 4}; // �i�諸�ƭ�

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


        int A = options[UnityEngine.Random.Range(0, options.Count)]; // �q�ѤU���ƭȤ��H����ܤ@��
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
        yield return new WaitForSeconds(10f);
        CanAtk1 = true;
    }
    IEnumerator StartAtk1()
    {
        StartCoroutine(Atk1CoolDown());

        // �����
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        animator.SetTrigger("Atk");
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 360; i+=60)
        {
            StartCoroutine(Atk1FireBullet(Eb3_Poison,i,6f,1.5f));
            AtkSound.Play();
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 360; i += 45)
        {
            StartCoroutine(Atk1FireBullet(Eb4_Poison,i,8f,1.2f));
            AtkSound.Play();
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 360; i += 30)
        {
            StartCoroutine(Atk1FireBullet(Eb4_Poison, i,12f,0.8f));
            AtkSound.Play();
        }
        
        yield return new WaitForSeconds(3f);

        //��_���ʳt��
        EnemyAI.speed = DefaultMoveSpeed;
        Atkking = false;
    }
    private IEnumerator Atk1FireBullet(GameObject bulletObj,float angle,float bulletSpeed,float bulletSize)//�����X���l�u�����B�C�h�֨���1�o�l�u�B�t�סB�j�p
    {
        GameObject bullet = Instantiate(PoisonSplitBullet, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        bullet.GetComponent<PoisonSplitBullet>().bulletSpeed = bulletSpeed;
        bullet.GetComponent<PoisonSplitBullet>().bulletSize = bulletSize;
        bullet.GetComponent<PoisonSplitBullet>().bullet = bulletObj;

        // �p��l�u����l�t�פ�V
        Vector2 bulletDirection = Quaternion.Euler(0, 0, angle) * Vector2.right;

        // �]�m�l�u������A�Ϩ��V�ǰt�����V
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        // ��l�t�סA0.8�����ܬ��̲׳t��
        bulletRb.velocity = 5f * bulletDirection;

        // ����0.8��
        yield return new WaitForSeconds(0.8f);

        // ���ܳt�׬��̲׳t��
        bulletRb.velocity = 0f * bulletDirection;
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

        animator.SetTrigger("Rotate");
        DashSound.Play();
        yield return new WaitForSeconds(1.5f);

        enemyImpact = true;
        ChangeColor = true;

        EnemyAI.speed = 4500;
        EnemyAI.MinDistance = 0;

        for (int i = 0; i < 35; i++)
        {
            PoisonArrowSound.Play();
            Instantiate(WormSummoner, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1f);

        ChangeColor = false;
        enemyImpact = false;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;

        yield return new WaitForSeconds(2f);

        //��_���ʳt��
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
        yield return new WaitForSeconds(20f);
        CanAtk3 = true;
    }
    IEnumerator StartAtk3()
    {
        StartCoroutine(Atk3CoolDown());

        // �����
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EnemyAI.speed = 0f;

        animator.SetTrigger("Atk");
        yield return new WaitForSeconds(1f);

        for (float angle = 0; angle < 360f; angle += 30)
        {
            Vector3 direction = Quaternion.Euler(0f, 0f, angle) * transform.right;
            GameObject bulletObj = Instantiate(PoisonArrowSplit, transform.position, Quaternion.identity);
            bulletObj.transform.right = direction;

            bulletObj.GetComponent<Rigidbody2D>().AddForce(bulletObj.transform.right * 4f, ForceMode2D.Impulse);
            AtkSound.Play();
        }

        yield return new WaitForSeconds(8f);

        //��_���ʳt��
        EnemyAI.speed = DefaultMoveSpeed;
        Atkking = false;
    }
    IEnumerator OpenEnemyAI()
    {
        yield return new WaitForSeconds(1.5f);
        EnemyAI.enabled = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemyImpact==true)
        {
            collision.GetComponent<PlayerInfo>().TakeDamage(15);
        }

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
        if(player.GetComponent<PlayerInfo>().HasPoisonSummon == false)
        {
            player.GetComponent<PlayerInfo>().HasPoisonSummon = true;
            GameObject Summon = Instantiate(PoisonSummonPrefab, player.transform.position, Quaternion.identity);
            Summon.transform.SetParent(player.transform);
        }
    }
}
