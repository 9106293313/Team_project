using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonWormPlayer : MonoBehaviour
{
    public SummonBulletScript summonBulletScript;
    public GameObject FloatingTextPrefeb;
    public GameObject PoisonCloud, BigPoisonCloud;

    public GameObject targ;
    Rigidbody2D rb;
    public float speed1 = 8f;
    public float speed2 = 16f;
    float speed;
    float rotateSpeed = 200f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        speed = Random.Range(speed1, speed2);
    }
    void Update()
    {
        GameObject closestEnemy = FindClosestEnemyObject();

        if (closestEnemy != null)
        {
            targ = closestEnemy;
        }
    }
    GameObject FindClosestEnemyObject()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            // 計算到敵人的距離
            float distanceToEnemy = Vector3.Distance(currentPosition, enemy.transform.position);

            // 如果距離比目前最近的還要近，更新最近的敵人和距離
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    private void FixedUpdate()
    {
        Vector3 pos = this.transform.position;
        float dist = float.PositiveInfinity;


        var d = (pos - targ.transform.position).sqrMagnitude;
        if (d < dist)
        {
            dist = d;
        }


        /////////////

        rotateSpeed = speed * 40f;


        if (targ != null)
        {
            Vector2 direction = (Vector2)targ.transform.position - rb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.right).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;

            rb.velocity = transform.right * speed;
        }
        else
        {
            rb.velocity = transform.right * speed;
            rb.angularVelocity = 0;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {

            CreatePoisonCloundObj();

            Destroy(gameObject);
        }
    }
    public void CreatePoisonCloundObj()
    {
        if (!CardSystem.HasCard("皇帝")) //如果沒有皇帝卡，一般的毒雲
        {
            GameObject cloudObj = Instantiate(PoisonCloud, transform.position, Quaternion.identity);
            cloudObj.GetComponentInChildren<PoisonCloud>().Damage = System.Convert.ToInt16(summonBulletScript.Damage * 0.2f); //毒雲傷害為0.2倍的箭矢傷害
            cloudObj.GetComponentInChildren<PoisonCloud>().critRate = summonBulletScript.critRate;
            cloudObj.GetComponentInChildren<PoisonCloud>().critDamage = summonBulletScript.critDamage;
        }
        else //有皇帝卡，大毒雲
        {
            GameObject cloudObj = Instantiate(BigPoisonCloud, transform.position, Quaternion.identity);
            cloudObj.GetComponentInChildren<PoisonCloud>().Damage = System.Convert.ToInt16(summonBulletScript.Damage * 0.3f); //大毒雲傷害為0.4倍的箭矢傷害
            cloudObj.GetComponentInChildren<PoisonCloud>().critRate = summonBulletScript.critRate;
            cloudObj.GetComponentInChildren<PoisonCloud>().critDamage = summonBulletScript.critDamage;
        }

    }
}
