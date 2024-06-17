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
            // �p���ĤH���Z��
            float distanceToEnemy = Vector3.Distance(currentPosition, enemy.transform.position);

            // �p�G�Z����ثe�̪��٭n��A��s�̪񪺼ĤH�M�Z��
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
        if (!CardSystem.HasCard("�ӫ�")) //�p�G�S���ӫҥd�A�@�몺�r��
        {
            GameObject cloudObj = Instantiate(PoisonCloud, transform.position, Quaternion.identity);
            cloudObj.GetComponentInChildren<PoisonCloud>().Damage = System.Convert.ToInt16(summonBulletScript.Damage * 0.2f); //�r���ˮ`��0.2�����b�ڶˮ`
            cloudObj.GetComponentInChildren<PoisonCloud>().critRate = summonBulletScript.critRate;
            cloudObj.GetComponentInChildren<PoisonCloud>().critDamage = summonBulletScript.critDamage;
        }
        else //���ӫҥd�A�j�r��
        {
            GameObject cloudObj = Instantiate(BigPoisonCloud, transform.position, Quaternion.identity);
            cloudObj.GetComponentInChildren<PoisonCloud>().Damage = System.Convert.ToInt16(summonBulletScript.Damage * 0.3f); //�j�r���ˮ`��0.4�����b�ڶˮ`
            cloudObj.GetComponentInChildren<PoisonCloud>().critRate = summonBulletScript.critRate;
            cloudObj.GetComponentInChildren<PoisonCloud>().critDamage = summonBulletScript.critDamage;
        }

    }
}
