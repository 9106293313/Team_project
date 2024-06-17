using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSummonObj : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed = 25;
    void Start()
    {
        Atk();
        Destroy(gameObject, 1.2f);
    }
    void Atk()
    {
        for (float angle = 0; angle < 360f; angle += 60)
        {
            Vector3 direction = Quaternion.Euler(0f, 0f, angle) * transform.right;
            GameObject bulletObj = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletObj.transform.right = direction;

            bulletObj.GetComponent<Rigidbody2D>().AddForce(bulletObj.transform.right * 3f, ForceMode2D.Impulse);

            // �ϥ�Invoke����0.5������
            Invoke("AccelerateBullet", 0.8f);

        }
    }
    void AccelerateBullet()
    {
        // ���Ҧ��ĤH
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // �p�G�S���ĤH�h�R���l�u
        if (enemies.Length == 0)
        {
            foreach (GameObject bulletObj in GameObject.FindGameObjectsWithTag("FireSummonBullet"))
            {
                Destroy(bulletObj);
            }
        }

        // ��̪񪺼ĤH
        GameObject nearestEnemy = GetNearestEnemy(enemies);

        // �p�G���̪񪺼ĤH�A�N�l�u�[�t�¦V�L
        if (nearestEnemy != null)
        {
            Transform EnemyPos = nearestEnemy.transform;
            foreach (GameObject bulletObj in GameObject.FindGameObjectsWithTag("FireSummonBullet"))
            {
                bulletObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;//���N�l�u�t�׳W0

                float AtkAngle = Mathf.Atan2(EnemyPos.position.y - bulletObj.transform.position.y, EnemyPos.position.x - bulletObj.transform.position.x) * Mathf.Rad2Deg;

                bulletObj.transform.rotation = Quaternion.Euler(0, 0, AtkAngle);

                bulletObj.GetComponent<Rigidbody2D>().AddForce(bulletObj.transform.right * bulletSpeed, ForceMode2D.Impulse);
            }
        }
    }

    GameObject GetNearestEnemy(GameObject[] enemies)
    {
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}
