using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponItemScript : MonoBehaviour
{
    bool IsTrigger = true;
    public GameObject MainObj;
    public GameObject FButton;
    public GameObject WeaponItemObj; //�Ψӥͦ��s��weaponItemObj
    ///////
    public int WeaponItemNum = 1;//�Z������
    public int WeaponItemBaseDamage;
    public float WeaponItemBulletSpeed;
    public float WeaponItemCritRate;
    public float WeaponItemCritDamage;
    public float WeaponItemAtkCoolDown;
    public ElementsType.Elements WeaponItemElements;
    //////�U���Ψ��x�s���a�쥻�Z������
    int PlayerWeaponItemNum;
    int PlayerWeaponItemBaseDamage;
    float PlayerWeaponItemBulletSpeed;
    float PlayerWeaponItemCritRate;
    float PlayerWeaponItemCritDamage;
    float PlayerWeaponItemAtkCoolDown;
    ElementsType.Elements PlayerWeaponItemElements;

    public Sprite LongBowSprite, ShortBowSprite, CrossBowSprite;

    public GameObject WeaponSprite;


    private void Start()
    {
        FButton.SetActive(false);
        StartCoroutine(CanPickUpTimer(1f)); //�Τ@��timer���L��ͦ��ɵL�k���W�Q�߰_��
        if(WeaponItemNum==1)
        {
            WeaponSprite.GetComponent<SpriteRenderer>().sprite = LongBowSprite;
        }
        if (WeaponItemNum == 2)
        {
            WeaponSprite.GetComponent<SpriteRenderer>().sprite = ShortBowSprite;
        }
        if (WeaponItemNum == 3)
        {
            WeaponSprite.GetComponent<SpriteRenderer>().sprite = CrossBowSprite;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FButton.SetActive(true);

            if (Input.GetKey(KeyCode.F) && IsTrigger==false)
            {
                IsTrigger = true;

                GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayerScript>().PlayPickUpWeaponSound(); //���񭵮�

                GameObject colObj = collision.gameObject.GetComponent<PlayerMovement>().ArrowPivot.GetComponent<ChooseWeapon>().currentWeapon;
                //Ū�����a��ChooseWeapon��CurrentWeapon���ƭȩM�Z������
                PlayerWeaponItemNum = collision.gameObject.GetComponent<PlayerMovement>().ArrowPivot.GetComponent<ChooseWeapon>().weaponNum;
                PlayerWeaponItemBaseDamage = colObj.GetComponent<WeaponShoot>().weaponBaseDamage;
                PlayerWeaponItemBulletSpeed = colObj.GetComponent<WeaponShoot>().bulletSpeed;
                PlayerWeaponItemCritRate = colObj.GetComponent<WeaponShoot>().critRate;
                PlayerWeaponItemCritDamage = colObj.GetComponent<WeaponShoot>().critDamage;
                PlayerWeaponItemAtkCoolDown = colObj.GetComponent<WeaponShoot>().AtkCooldown;
                PlayerWeaponItemElements = colObj.GetComponent<WeaponShoot>().elements;

                //�p�G�Z���������P�A���Z��
                if(PlayerWeaponItemNum != WeaponItemNum)
                {
                    collision.gameObject.GetComponent<PlayerMovement>().ArrowPivot.GetComponent<ChooseWeapon>().weaponNum = WeaponItemNum;
                }

                //��ۤv���ƭȵ����a���Z��
                if(WeaponItemNum==1)//�]�����F�Z���ҥH�AŪ���@��
                { colObj = collision.gameObject.GetComponent<PlayerMovement>().ArrowPivot.GetComponent<ChooseWeapon>().longBow; }
                if (WeaponItemNum == 2)
                { colObj = collision.gameObject.GetComponent<PlayerMovement>().ArrowPivot.GetComponent<ChooseWeapon>().shortBow; }
                if (WeaponItemNum == 3)
                { colObj = collision.gameObject.GetComponent<PlayerMovement>().ArrowPivot.GetComponent<ChooseWeapon>().crossBow; }

                colObj.GetComponent<WeaponShoot>().weaponBaseDamage = WeaponItemBaseDamage;
                colObj.GetComponent<WeaponShoot>().bulletSpeed = WeaponItemBulletSpeed;
                colObj.GetComponent<WeaponShoot>().critRate = WeaponItemCritRate;
                colObj.GetComponent<WeaponShoot>().critDamage = WeaponItemCritDamage;
                colObj.GetComponent<WeaponShoot>().defaultAtkCooldown = WeaponItemAtkCoolDown;
                colObj.GetComponent<WeaponShoot>().elements = WeaponItemElements;

                //�⤧�eŪ�����a���ªZ���ƭȮ��ӥͦ��@�ӷs��weaponItem
                GameObject OldPlayerWeaponObj = Instantiate(WeaponItemObj, collision.transform.position, Quaternion.identity);
                OldPlayerWeaponObj.GetComponentInChildren<weaponItemScript>().WeaponItemNum = PlayerWeaponItemNum;
                OldPlayerWeaponObj.GetComponentInChildren<weaponItemScript>().WeaponItemBaseDamage = PlayerWeaponItemBaseDamage;
                OldPlayerWeaponObj.GetComponentInChildren<weaponItemScript>().WeaponItemBulletSpeed = PlayerWeaponItemBulletSpeed;
                OldPlayerWeaponObj.GetComponentInChildren<weaponItemScript>().WeaponItemCritRate = PlayerWeaponItemCritRate;
                OldPlayerWeaponObj.GetComponentInChildren<weaponItemScript>().WeaponItemCritDamage = PlayerWeaponItemCritDamage;
                OldPlayerWeaponObj.GetComponentInChildren<weaponItemScript>().WeaponItemAtkCoolDown = PlayerWeaponItemAtkCoolDown;
                OldPlayerWeaponObj.GetComponentInChildren<weaponItemScript>().WeaponItemElements = PlayerWeaponItemElements;
                Destroy(MainObj);
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

    IEnumerator CanPickUpTimer(float time)
    {
        yield return new WaitForSeconds(time);
        IsTrigger = false;
    }

}
