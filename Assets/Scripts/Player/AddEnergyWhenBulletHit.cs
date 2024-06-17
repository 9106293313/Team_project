using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class AddEnergyWhenBulletHit : MonoBehaviour
{
    float Timer = 0;
    float TimeA = 0.1f; //���u�N�o�ɶ�
    int Number = 0; 
    public PlayerInfo PlayerInfo;
    public GameObject NumberText; //��ܷ�e���u������
    public AudioSource AddNumberSound;

    private void Update()
    {
        NumberText.GetComponent<TextMeshProUGUI>().text = Number.ToString();
        if(Number >= 5) //���u�����ƹF��5�N�i�H�[1�ӯ�q
        {
            PlayerInfo.AddEnergyWhenBulletHit();
            Number = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyBullet") && PlayerInfo.CanTakeDamage) //���a�b�S�����˪����A�U�~�����u
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
