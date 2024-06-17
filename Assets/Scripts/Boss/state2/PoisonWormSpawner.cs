using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonWormSpawner : MonoBehaviour
{
    public GameObject PoisonWorm;
    public void SpawnWorm()
    {
        Instantiate(PoisonWorm,transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }
}
