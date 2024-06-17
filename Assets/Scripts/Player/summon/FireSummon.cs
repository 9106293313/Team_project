using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSummon : MonoBehaviour
{
    public GameObject FireSummonObj;
    bool CanAtk = true;
    public void Atk()
    {
        if(CanAtk)
        {
            Instantiate(FireSummonObj, transform.position, Quaternion.identity);
            StartCoroutine(CoolDown());
        }
        
    }
    IEnumerator CoolDown()
    {
        CanAtk = false;
        yield return new WaitForSeconds(3f);
        CanAtk = true;
    }
}
