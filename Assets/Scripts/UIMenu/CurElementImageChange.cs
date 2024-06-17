using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CurElementImageChange : MonoBehaviour
{
    Image image;
    public Sprite Wind, Fire, Ice, Thunder, Poison;
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindWithTag("PlayerWeapon").GetComponent<WeaponShoot>().elements == ElementsType.Elements.wind)
        {
            image.sprite = Wind;
        }
        else if(GameObject.FindWithTag("PlayerWeapon").GetComponent<WeaponShoot>().elements == ElementsType.Elements.Fire)
        {
            image.sprite = Fire;
        }
        else if (GameObject.FindWithTag("PlayerWeapon").GetComponent<WeaponShoot>().elements == ElementsType.Elements.Ice)
        {
            image.sprite = Ice;
        }
        else if (GameObject.FindWithTag("PlayerWeapon").GetComponent<WeaponShoot>().elements == ElementsType.Elements.Thunder)
        {
            image.sprite = Thunder;
        }
        else if (GameObject.FindWithTag("PlayerWeapon").GetComponent<WeaponShoot>().elements == ElementsType.Elements.Poison)
        {
            image.sprite = Poison;
        }
    }
}
