using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAltar : MonoBehaviour
{
    public int WeaponItemNum = 1;
    public GameObject FButton;
    public AudioSource TriggerAltarSound;
    public GameObject TriggerEffect;
    ElementsType.Elements Elements;
    void Start()
    {
        FButton.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(collision.gameObject.GetComponent<PlayerMovement>().charging) //如果玩家在集氣，不能換武器(防止bug)
            {
                FButton.SetActive(false);
                return;
            }

            FButton.SetActive(true);

            if (Input.GetKey(KeyCode.F) && collision.gameObject.GetComponent<PlayerMovement>().ArrowPivot.GetComponent<ChooseWeapon>().weaponNum != WeaponItemNum)
            {
                Elements = GameObject.FindWithTag("PlayerWeapon").GetComponent<WeaponShoot>().elements;//讀取本來的元素屬性
                collision.gameObject.GetComponent<PlayerMovement>().ArrowPivot.GetComponent<ChooseWeapon>().weaponNum = WeaponItemNum;
                TriggerAltarSound.Play();
                StartCoroutine(StartTriggetEffect());
                StartCoroutine(ChangeElements());
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

    IEnumerator ChangeElements()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject.FindWithTag("PlayerWeapon").GetComponent<WeaponShoot>().elements = Elements; //將屬性替換成原本武器的元素屬性
    }
}
