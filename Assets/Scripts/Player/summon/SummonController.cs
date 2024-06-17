using System.Collections;
using UnityEngine;

public class SummonController : MonoBehaviour
{
    public float minDistance = 1f;                // 隨機飛行的最小距離
    public float maxDistance = 4f;                // 隨機飛行的最大距離
    public float randomMoveInterval = 3f;// 每隔多久一次移動
    float randomMoveTimer = 0f;
    Vector2 randomTargetPosition;
    public float followDistance = 2f; // 設定召喚物與玩家的跟隨距離
    public float moveSpeed = 3f; // 設定召喚物的移動速度
    public float detectionRadius = 5f; //距離玩家範圍多近會被偵測到
    public float attackCooldown = 2f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    private Transform player;
    private bool isAttacking = false;
    Transform target; //當前移動的目標

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        randomMoveTimer += Time.deltaTime;

        CheckForEnemies();

        // 檢查召喚物與移動目標之間的距離
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        // 如果距離大於跟隨距離，則移動向目標
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
        // 計算朝向目標的方向
        Vector2 direction = (target.position - transform.position).normalized;

        // 移動召喚物
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
    void RandomMove()
    {
        if (randomMoveTimer > randomMoveInterval)
        {
            // 偏移向量，限制在最小和最大距離間
            float randomDistance = Random.Range(minDistance, maxDistance);
            Vector2 randomOffset = Random.insideUnitCircle.normalized * randomDistance;

            // 設置新的目標位置
            randomTargetPosition = (Vector2)target.position + randomOffset;

            // 更新Timer
            randomMoveTimer = 0f;
        }
        // 移動
        transform.position = Vector2.MoveTowards(transform.position, randomTargetPosition, moveSpeed * Time.deltaTime);
    }

    void CheckForEnemies()
    {
        // 檢測玩家周圍敵人
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
                    break;  // 找到第1個敵人後就退出循環
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
