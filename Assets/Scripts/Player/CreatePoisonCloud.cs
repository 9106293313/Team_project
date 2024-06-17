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
        if(!CardSystem.HasCard("�ӫ�")) //�p�G�S���ӫҥd�A�@�몺�r��
        {
            GameObject cloudObj = Instantiate(poisonCloud, transform.position, Quaternion.identity);
            cloudObj.GetComponentInChildren<PoisonCloud>().Damage = System.Convert.ToInt16(bulletScript.Damage * 0.2f); //�r���ˮ`��0.2�����b�ڶˮ`
            cloudObj.GetComponentInChildren<PoisonCloud>().critRate = bulletScript.critRate;
            cloudObj.GetComponentInChildren<PoisonCloud>().critDamage = bulletScript.critDamage;
        }
        else //���ӫҥd�A�j�r��
        {
            GameObject cloudObj = Instantiate(BigpoisonCloud, transform.position, Quaternion.identity);
            cloudObj.GetComponentInChildren<PoisonCloud>().Damage = System.Convert.ToInt16(bulletScript.Damage * 0.3f); //�j�r���ˮ`��0.4�����b�ڶˮ`
            cloudObj.GetComponentInChildren<PoisonCloud>().critRate = bulletScript.critRate;
            cloudObj.GetComponentInChildren<PoisonCloud>().critDamage = bulletScript.critDamage;
        }
        
    }
}
