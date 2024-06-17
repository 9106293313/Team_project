using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjToggle : MonoBehaviour
{
    public GameObject[] objectsToToggle;
    void Start()
    {
        if (objectsToToggle.Length > 0)
        {
            int randomIndex = Random.Range(0, objectsToToggle.Length);

            for (int i = 0; i < objectsToToggle.Length; i++)
            {
                if (i == randomIndex)
                {
                    objectsToToggle[i].SetActive(true);
                }
                else
                {
                    objectsToToggle[i].SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogError("需要添加物件到 objectsToToggle 中！");
        }
    }

}
