using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnThunderball : MonoBehaviour
{
    public GameObject thunderball;
    public GameObject BigThunderball;

    public int Damage;
    public float critRate;
    public float critDamage;
    public GameObject FloatingTextPrefeb;
    void Start()
    {
        if(!CardSystem.HasCard("¬Ó«Ò"))
        {
            StartCoroutine(SpawnThunderBall(transform, 0.2f));
        }
        else
        {
            StartCoroutine(SpawnThunderBall2(transform, 0.13f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnThunderBall(Transform pos, float time)
    {
        GameObject RangeAtk1 = Instantiate(thunderball, pos.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
        RangeAtk1.GetComponent<thunderballObj>().Damage = System.Convert.ToInt16(Damage * 0.2f);
        RangeAtk1.GetComponent<thunderballObj>().critRate = critRate;
        RangeAtk1.GetComponent<thunderballObj>().critDamage = critDamage;
        RangeAtk1.GetComponent<thunderballObj>().FloatingTextPrefeb = FloatingTextPrefeb;
        yield return new WaitForSeconds(time);
        GameObject RangeAtk2 = Instantiate(thunderball, pos.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
        RangeAtk2.GetComponent<thunderballObj>().Damage = System.Convert.ToInt16(Damage * 0.2f);
        RangeAtk2.GetComponent<thunderballObj>().critRate = critRate;
        RangeAtk2.GetComponent<thunderballObj>().critDamage = critDamage;
        RangeAtk2.GetComponent<thunderballObj>().FloatingTextPrefeb = FloatingTextPrefeb;
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    IEnumerator SpawnThunderBall2(Transform pos, float time)
    {
        GameObject RangeAtk1 = Instantiate(BigThunderball, pos.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
        RangeAtk1.GetComponent<thunderballObj>().Damage = System.Convert.ToInt16(Damage * 0.3f);
        RangeAtk1.GetComponent<thunderballObj>().critRate = critRate;
        RangeAtk1.GetComponent<thunderballObj>().critDamage = critDamage;
        RangeAtk1.GetComponent<thunderballObj>().FloatingTextPrefeb = FloatingTextPrefeb;
        yield return new WaitForSeconds(time);
        GameObject RangeAtk2 = Instantiate(BigThunderball, pos.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
        RangeAtk2.GetComponent<thunderballObj>().Damage = System.Convert.ToInt16(Damage * 0.3f);
        RangeAtk2.GetComponent<thunderballObj>().critRate = critRate;
        RangeAtk2.GetComponent<thunderballObj>().critDamage = critDamage;
        RangeAtk2.GetComponent<thunderballObj>().FloatingTextPrefeb = FloatingTextPrefeb;
        yield return new WaitForSeconds(time);
        GameObject RangeAtk3 = Instantiate(BigThunderball, pos.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
        RangeAtk3.GetComponent<thunderballObj>().Damage = System.Convert.ToInt16(Damage * 0.3f);
        RangeAtk3.GetComponent<thunderballObj>().critRate = critRate;
        RangeAtk3.GetComponent<thunderballObj>().critDamage = critDamage;
        RangeAtk3.GetComponent<thunderballObj>().FloatingTextPrefeb = FloatingTextPrefeb;
        yield return new WaitForSeconds(time);
        GameObject RangeAtk4 = Instantiate(BigThunderball, pos.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
        RangeAtk4.GetComponent<thunderballObj>().Damage = System.Convert.ToInt16(Damage * 0.3f);
        RangeAtk4.GetComponent<thunderballObj>().critRate = critRate;
        RangeAtk4.GetComponent<thunderballObj>().critDamage = critDamage;
        RangeAtk4.GetComponent<thunderballObj>().FloatingTextPrefeb = FloatingTextPrefeb;
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
