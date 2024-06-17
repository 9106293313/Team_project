using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSummon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public void Atk() //當weaponShoot中的Shoot()被觸發時會跟著被觸發
    {
        // 獲取滑鼠當前位置
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        float AtkAngle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;

        GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, AtkAngle));

        bulletObj.GetComponent<Rigidbody2D>().AddForce(bulletObj.transform.right * bulletSpeed, ForceMode2D.Impulse);

    }
}
