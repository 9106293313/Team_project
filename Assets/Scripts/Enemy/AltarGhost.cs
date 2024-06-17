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
        // 找到所有擁有 "Player" 標籤的物件
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // 選擇第一個找到的 "Player" 作為目標
        if (players.Length > 0)
        {
            GameObject targetPlayer = players[0];

            // 產生子彈
            GameObject bulletObj = Instantiate(bullet, transform.position, Quaternion.identity);

            // 計算子彈方向
            Vector2 direction = (targetPlayer.transform.position - transform.position).normalized;

            // 計算旋轉角度
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 將子彈套用旋轉
            bulletObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // 將子彈套用方向和速度
            bulletObj.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

            if (bulletObj.GetComponent<EnemyBullet>() != false)
            {
                bulletObj.GetComponent<EnemyBullet>().Damage = BulletDamage;
            }
        }
        else
        {
            Debug.LogWarning("沒有找到玩家可以攻擊");
        }
    }
}
