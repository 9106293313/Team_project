using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3BigFireBall : MonoBehaviour
{
    public float existTime = 15f;
    public GameObject childObj;
    public AudioSource ExplodeSound,ShootSound;
    public GameObject Eft;
    void Start()
    {
        Destroy(gameObject, existTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Grid"))
        {
            StartCoroutine(Explode());
        }
    }
    void NormalAtk(GameObject BulletType, float BulletSpeed, int Radius, int MinRadius, int MaxRadius) //朝周圍扇形攻擊，可設定角度和子彈
    {

        for (int i = 0; (i+MinRadius) < MaxRadius; i += Radius)
        {
            ShootSound.Play();
            GameObject bullet = Instantiate(BulletType, gameObject.transform.position, Quaternion.Euler(0, 0, transform.rotation.z + MinRadius + i));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * BulletSpeed, ForceMode2D.Impulse);
        }
    }
    IEnumerator Explode()
    {
        Eft.SetActive(false);
        ExplodeSound.Play();
        GetComponent<CircleCollider2D>().enabled = false;
        Destroy(gameObject, 2f);

        NormalAtk(childObj, 25f, 120, 0, 360);
        yield return new WaitForSeconds(0.1f);
        NormalAtk(childObj, 22f, 90, 0, 360);
        yield return new WaitForSeconds(0.1f);
        NormalAtk(childObj, 18f, 60, 0, 360);
        
    }
}
