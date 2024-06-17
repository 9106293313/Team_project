using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class AddEnergyWhenBulletHit : MonoBehaviour
{
    float Timer = 0;
    float TimeA = 0.1f; //擦彈冷卻時間
    int Number = 0; 
    public PlayerInfo PlayerInfo;
    public GameObject NumberText; //顯示當前擦彈的分數
    public AudioSource AddNumberSound;

    private void Update()
    {
        NumberText.GetComponent<TextMeshProUGUI>().text = Number.ToString();
        if(Number >= 5) //擦彈的分數達到5就可以加1個能量
        {
            PlayerInfo.AddEnergyWhenBulletHit();
            Number = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyBullet") && PlayerInfo.CanTakeDamage) //玩家在沒有受傷的狀態下才能擦彈
        {
            if(Timer == 0)
            {
                Number++;
                AddNumberSound.Play();
            }
            if(Timer <= TimeA)
            {
                Timer+=Time.deltaTime;
            }
            else
            {
                Timer = 0;
            }
            
        }
    }
}
