using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootKatanaBullet : MonoBehaviour
{
    public float TimeBeforeShoot = 3f;
    public GameObject bullet;
    public float bulletSpeed = 15f;
    void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(TimeBeforeShoot);
        GameObject BulletObj = Instantiate(bullet,transform.position,transform.rotation);
        BulletObj.GetComponent<Rigidbody2D>().AddForce(BulletObj.transform.right * bulletSpeed, ForceMode2D.Impulse);
        Destroy(this.gameObject);
    }
}
