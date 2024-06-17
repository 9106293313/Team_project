using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpirit : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private float AtkDistance;
    float AtkTimer = 0f;
    float AtkCoolDownTime = 5f;
    [SerializeField] GameObject bullet;
    [SerializeField] float rotationStep = 30f;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] private int BulletDamage = 10;
    public GameObject SpriteObj, WingObj;
    public bool SummonMonsterWhenDestroy = false;
    public GameObject SummonedMonster;
    public int MinRandomNum,MaxRandomNum;
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
        WingObj.GetComponent<Animator>().SetTrigger("Atk");
        yield return new WaitForSeconds(0.25f);
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

    private void OnDestroy()
    {
        if(SummonMonsterWhenDestroy)
        {
            int RandomNum = UnityEngine.Random.Range(MinRandomNum, MaxRandomNum+1);
            for (int i = 0; i < RandomNum; i++)
            {
                Instantiate(SummonedMonster, transform.position+ new Vector3(Random.Range(-1.5f,1.5f), Random.Range(-1.5f, 1.5f),0), Quaternion.identity);
                Debug.Log(RandomNum);
            }
        }
    }
}
