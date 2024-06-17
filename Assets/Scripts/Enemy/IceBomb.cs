using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBomb : MonoBehaviour
{
    public GameObject IceBullet;
    bool IsExplode = false;
    public AudioSource ExplodeSound;
    public float BulletSpeed = 5f;
    public int Angle = 15;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && IsExplode == false)
        {
            IsExplode = true;
            StartCoroutine(Explode());
        }
    }
    IEnumerator Explode()
    {
        GetComponent<Animator>().SetTrigger("Explode");
        yield return new WaitForSeconds(0.5f);
        ExplodeSound.Play();
        for (int i = 0; i < 360; i += Angle)
        {
            GameObject bullet = Instantiate(IceBullet, transform.position, Quaternion.Euler(0, 0, i));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * BulletSpeed, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
