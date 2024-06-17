using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePoisonCloud : MonoBehaviour
{
    public bulletScript bulletScript;
    public GameObject poisonCloud;
    public GameObject BigpoisonCloud;
    public float CoolDown = 1f;
    float Timer = 0f;
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer >= CoolDown)
        {
            Timer = 0f;
            CreatePoisonCloundObj();
        }
    }

    public void CreatePoisonCloundObj()
    {
        if(!CardSystem.HasCard("¬Ó«Ò")) //¦pªG¨S¦³¬Ó«Ò¥d¡A¤@¯ëªº¬r¶³
        {
            GameObject cloudObj = Instantiate(poisonCloud, transform.position, Quaternion.identity);
            cloudObj.GetComponentInChildren<PoisonCloud>().Damage = System.Convert.ToInt16(bulletScript.Damage * 0.2f); //¬r¶³¶Ë®`¬°0.2­¿ªº½b¥Ú¶Ë®`
            cloudObj.GetComponentInChildren<PoisonCloud>().critRate = bulletScript.critRate;
            cloudObj.GetComponentInChildren<PoisonCloud>().critDamage = bulletScript.critDamage;
        }
        else //¦³¬Ó«Ò¥d¡A¤j¬r¶³
        {
            GameObject cloudObj = Instantiate(BigpoisonCloud, transform.position, Quaternion.identity);
            cloudObj.GetComponentInChildren<PoisonCloud>().Damage = System.Convert.ToInt16(bulletScript.Damage * 0.3f); //¤j¬r¶³¶Ë®`¬°0.4­¿ªº½b¥Ú¶Ë®`
            cloudObj.GetComponentInChildren<PoisonCloud>().critRate = bulletScript.critRate;
            cloudObj.GetComponentInChildren<PoisonCloud>().critDamage = bulletScript.critDamage;
        }
        
    }
}
