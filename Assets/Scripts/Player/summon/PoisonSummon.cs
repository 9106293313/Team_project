using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSummon : MonoBehaviour
{
    bool CanAtk = true;
    public GameObject WormSummoner;
    Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        GameObject closestEnemy = FindClosestEnemyObject();

        if (closestEnemy != null)
        {
            Atk();
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
    void Atk()
    {
        if(CanAtk)
        {
            StartCoroutine(StartAtk());
        }
    }
    IEnumerator StartAtk()
    {
        StartCoroutine(AtkCoolDown());
        animator.SetTrigger("Rotate");
        for (int i = 0; i < 14; i++)
        {
            Instantiate(WormSummoner, transform.position , Quaternion.identity);
            yield return new WaitForSeconds(0.6f);
        }
        yield return new WaitForSeconds(1f);
    }
    IEnumerator AtkCoolDown()
    {
        CanAtk = false;
        yield return new WaitForSeconds(20f);
        CanAtk = true;
    }
}
