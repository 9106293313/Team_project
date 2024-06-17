using System.Collections;
using UnityEngine;

public class SummonController : MonoBehaviour
{
    public float minDistance = 1f;                // �H�����檺�̤p�Z��
    public float maxDistance = 4f;                // �H�����檺�̤j�Z��
    public float randomMoveInterval = 3f;// �C�j�h�[�@������
    float randomMoveTimer = 0f;
    Vector2 randomTargetPosition;
    public float followDistance = 2f; // �]�w�l�ꪫ�P���a�����H�Z��
    public float moveSpeed = 3f; // �]�w�l�ꪫ�����ʳt��
    public float detectionRadius = 5f; //�Z�����a�d��h��|�Q������
    public float attackCooldown = 2f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    private Transform player;
    private bool isAttacking = false;
    Transform target; //��e���ʪ��ؼ�

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        randomMoveTimer += Time.deltaTime;

        CheckForEnemies();

        // �ˬd�l�ꪫ�P���ʥؼФ������Z��
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        // �p�G�Z���j����H�Z���A�h���ʦV�ؼ�
        if (distanceToTarget > followDistance)
        {
            MoveTowardsTarget();
        }
        else
        {
            RandomMove();
        }
    }
    void MoveTowardsTarget()
    {
        // �p��¦V�ؼЪ���V
        Vector2 direction = (target.position - transform.position).normalized;

        // ���ʥl�ꪫ
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
    void RandomMove()
    {
        if (randomMoveTimer > randomMoveInterval)
        {
            // �����V�q�A����b�̤p�M�̤j�Z����
            float randomDistance = Random.Range(minDistance, maxDistance);
            Vector2 randomOffset = Random.insideUnitCircle.normalized * randomDistance;

            // �]�m�s���ؼЦ�m
            randomTargetPosition = (Vector2)target.position + randomOffset;

            // ��sTimer
            randomMoveTimer = 0f;
        }
        // ����
        transform.position = Vector2.MoveTowards(transform.position, randomTargetPosition, moveSpeed * Time.deltaTime);
    }

    void CheckForEnemies()
    {
        // �˴����a�P��ĤH
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(player.transform.position, detectionRadius);

        if(hitEnemies.Length > 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    target = enemy.transform;

                    if (!isAttacking)
                    {
                        StartCoroutine(AttackEnemy(enemy.transform));
                    }
                    break;  // ����1�ӼĤH��N�h�X�`��
                }
                else
                {
                    target = player.transform;
                }
            }
        }
    }

    IEnumerator AttackEnemy(Transform enemyTransform)
    {
        isAttacking = true;

        float AtkAngle = Mathf.Atan2(enemyTransform.position.y - transform.position.y, enemyTransform.position.x - transform.position.x) * Mathf.Rad2Deg;
        GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, AtkAngle));

        bulletObj.GetComponent<Rigidbody2D>().AddForce(bulletObj.transform.right * bulletSpeed, ForceMode2D.Impulse);

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }
}
