using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float existTime = 3f;
    public int Damage = 10;
    public GameObject FloatingTextPrefeb;
    public bool ShootThrough = false;
    public bool CreateChildObjWhenDestroy = false;
    public GameObject childObj;
    public bool ShootThroughGrid = false;
    void Start()
    {
        StartCoroutine(StartDestroy(existTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerInfo>().TakeDamage(Damage);
            if(ShootThrough==false)
            {
                if(CreateChildObjWhenDestroy)
                {
                    Instantiate(childObj,transform.position,transform.rotation);
                }
                Destroy(gameObject);
            }
        }
        if(collision.tag == "Grid")
        {
            if(ShootThroughGrid==false)
            {
                if (CreateChildObjWhenDestroy)
                {
                    Instantiate(childObj, transform.position, transform.rotation);
                }
                Destroy(gameObject);
            }
        }
    }

    IEnumerator StartDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        if (CreateChildObjWhenDestroy)
        {
            Instantiate(childObj, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
