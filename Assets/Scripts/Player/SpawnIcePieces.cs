using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIcePieces : MonoBehaviour
{
    public GameObject[] IcePieces;
    public float speed = 25f;
    float AngleX = 45f; //每個碎塊差距的角度

    private void OnDestroy()
    {
        if(!CardSystem.HasCard("皇帝"))
        {
            for (int i = 0; i < 6; i++)
            {
                AngleX = 60f;
                GameObject IceObj = Instantiate(IcePieces[UnityEngine.Random.Range(0, 9)], transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 0f + (AngleX * i)));
                IceObj.GetComponent<Rigidbody2D>().AddForce(IceObj.transform.right * speed, ForceMode2D.Impulse);
                IceObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(this.GetComponent<bulletScript>().Damage * 0.15f);
                IceObj.GetComponent<bulletScript>().critDamage = this.GetComponent<bulletScript>().critDamage;
                IceObj.GetComponent<bulletScript>().critRate = this.GetComponent<bulletScript>().critRate;
                IceObj.GetComponent<Transform>().localScale = this.transform.localScale;
            }
        }
        else
        {
            for (int i = 0; i < 12; i++)
            {
                AngleX = 30f;
                GameObject IceObj = Instantiate(IcePieces[UnityEngine.Random.Range(0, 9)], transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 0f + (AngleX * i)));
                IceObj.GetComponent<Rigidbody2D>().AddForce(IceObj.transform.right * speed *1.5f , ForceMode2D.Impulse);
                IceObj.GetComponent<bulletScript>().Damage = Convert.ToInt16(this.GetComponent<bulletScript>().Damage * 0.15f);
                IceObj.GetComponent<bulletScript>().critDamage = this.GetComponent<bulletScript>().critDamage;
                IceObj.GetComponent<bulletScript>().critRate = this.GetComponent<bulletScript>().critRate;
                IceObj.GetComponent<Transform>().localScale = this.transform.localScale;
            }
        }
        

        
    }
}
