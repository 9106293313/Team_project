using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementAltar : MonoBehaviour
{

    public ElementsType.Elements AltarElements; //²½¾Â¤¸¯ÀÃþ«¬
    public GameObject FButton;
    public AudioSource TriggerAltarSound;
    public GameObject TriggerEffect;

    private void Start()
    {
        FButton.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FButton.SetActive(true);

            if (Input.GetKey(KeyCode.F) && GameObject.FindWithTag("PlayerWeapon").GetComponent<WeaponShoot>().elements != AltarElements)
            {
                GameObject.FindWithTag("PlayerWeapon").GetComponent<WeaponShoot>().elements = AltarElements;
                TriggerAltarSound.Play();
                StartCoroutine(StartTriggetEffect());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FButton.SetActive(false);
        }
    }

    IEnumerator StartTriggetEffect()
    {
        TriggerEffect.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        TriggerEffect.SetActive(false);
    }
}
