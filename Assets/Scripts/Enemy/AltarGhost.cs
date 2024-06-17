using System;
using System.Collections;
using UnityEngine;

public class AltarGhost : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private float AtkDistance;
    [SerializeField] private float AtkDelayTime = 0.2f;
    [SerializeField] private int AtkType = 1;
    float AtkTimer = 0f;
    float AtkCoolDownTime = 5f;
    bool IsAtk = false;
    [SerializeField] GameObject bullet;
    [SerializeField] float rotationStep = 30f;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] private int BulletDamage = 10;

    void Update()
    {
        
        if ((enemyAI.target.position - enemyAI.transform.position).magnitude <= AtkDistance)
        {
            if (AtkTimer < AtkCoolDownTime)
            {
                AtkTimer += Time.deltaTime;
            }
            else if (IsAtk==false)
            {
                StartCoroutine(StartAtk());
            }
        }
    }

    IEnumerator StartAtk()
    {
        IsAtk = true;
        this.GetComponentInChildren<Animator>().SetTrigger("Atk");
        yield return new WaitForSeconds(AtkDelayTime);
        switch (AtkType)
        {
            case 1:
                Atk();
                break;
            case 2:
                Atk2();
                break;
        }

        AtkTimer = 0;
        IsAtk = false;
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
    void Atk2()
    {
        // ���Ҧ��֦� "Player" ���Ҫ�����
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // ��ܲĤ@�ӧ�쪺 "Player" �@���ؼ�
        if (players.Length > 0)
        {
            GameObject targetPlayer = players[0];

            // ���ͤl�u
            GameObject bulletObj = Instantiate(bullet, transform.position, Quaternion.identity);

            // �p��l�u��V
            Vector2 direction = (targetPlayer.transform.position - transform.position).normalized;

            // �p����ਤ��
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // �N�l�u�M�α���
            bulletObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // �N�l�u�M�Τ�V�M�t��
            bulletObj.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

            if (bulletObj.GetComponent<EnemyBullet>() != false)
            {
                bulletObj.GetComponent<EnemyBullet>().Damage = BulletDamage;
            }
        }
        else
        {
            Debug.LogWarning("�S����쪱�a�i�H����");
        }
    }
}
