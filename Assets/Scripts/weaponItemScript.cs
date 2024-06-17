using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponItemScript : MonoBehaviour
{
    bool IsTrigger = true;
    public GameObject MainObj;
    public GameObject FButton;
    public GameObject WeaponItemObj; //用來生成新的weaponItemObj
    ///////
    public int WeaponItemNum = 1;//武器種類
    public int WeaponItemBaseDamage;
    public float WeaponItemBulletSpeed;
    public float WeaponItemCritRate;
    public float WeaponItemCritDamage;
    public float WeaponItemAtkCoolDown;
    public ElementsType.Elements WeaponItemElements;
    //////下面用來儲存玩家原本武器的值
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
        StartCoroutine(CanPickUpTimer(1f)); //用一個timer讓他剛生成時無法馬上被撿起來
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

                GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayerScript>().PlayPickUpWeaponSound(); //播放音效

                GameObject colObj = collision.gameObject.GetComponent<PlayerMovement>().ArrowPivot.GetComponent<ChooseWeapon>().currentWeapon;
                //讀取玩家的ChooseWeapon的CurrentWeapon的數值和武器種類
                PlayerWeaponItemNum = collision.gameObject.GetComponent<PlayerMovement>().ArrowPivot.GetComponent<ChooseWeapon>().weaponNum;
                PlayerWeaponItemBaseDamage = colObj.GetComponent<WeaponShoot>().weaponBaseDamage;
                PlayerWeaponItemBulletSpeed = colObj.GetComponent<WeaponShoot>().bulletSpeed;
                PlayerWeaponItemCritRate = colObj.GetComponent<WeaponShoot>().critRate;
                PlayerWeaponItemCritDamage = colObj.GetComponent<WeaponShoot>().critDamage;
                PlayerWeaponItemAtkCoolDown = colObj.GetComponent<WeaponShoot>().AtkCooldown;
                PlayerWeaponItemElements = colObj.GetComponent<WeaponShoot>().elements;

                //如果武器種類不同，換武器
                if(PlayerWeaponItemNum != WeaponItemNum)
                {
                    collision.gameObject.GetComponent<PlayerMovement>().ArrowPivot.GetComponent<ChooseWeapon>().weaponNum = WeaponItemNum;
                }

                //把自己的數值給玩家的武器
                if(WeaponItemNum==1)//因為換了武器所以再讀取一次
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

                //把之前讀取玩家的舊武器數值拿來生成一個新的weaponItem
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
