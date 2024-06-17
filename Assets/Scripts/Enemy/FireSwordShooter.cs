using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSwordShooter : MonoBehaviour
{
    public GameObject PrefabObj;
    public float BulletSpeed = 15f;

    private void Start()
    {
        Destroy(gameObject,1.2f);
    }
    public void ShootSword()
    {
        GameObject bullet = Instantiate(PrefabObj, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * BulletSpeed, ForceMode2D.Impulse);
    }
}
