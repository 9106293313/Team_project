using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSkull : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private float AtkDistance;
    float AtkTimer = 0f;
    float AtkCoolDownTime = 5f;
    [SerializeField] GameObject bullet;
    [SerializeField] float rotationStep = 30f;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] private int BulletDamage = 10;
    public GameObject SpriteObj;
    void Update()
    {
        if ((enemyAI.target.position - enemyAI.transform.position).magnitude <= AtkDistance)
        {
            if (AtkTimer < AtkCoolDownTime)
            {
                AtkTimer += Time.deltaTime;
            }
            else
            {
                AtkTimer = 0;
                StartCoroutine(StartAtk());
            }
        }
    }
    IEnumerator StartAtk()
    {
        SpriteObj.GetComponent<Animator>().SetTrigger("Atk");
        yield return new WaitForSeconds(1f);
        Atk();
    }
    void Atk()
    {
        for (float angle = 0; angle < 360f; angle += rotationStep)
        {
            Vector3 direction = Quaternion.Euler(0f, 0f, angle) * transform.right;
            GameObject bulletObj = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletObj.transform.right = direction;

            bulletObj.GetComponent<Rigidbody2D>().AddForce(bulletObj.transform.right * bulletSpeed, ForceMode2D.Impulse);

            if (bulletObj.GetComponent<EnemyBullet>() != false)
            {
                bulletObj.GetComponent<EnemyBullet>().Damage = BulletDamage;
            }
        }
    }
}
