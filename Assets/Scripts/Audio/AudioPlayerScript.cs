using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerScript : MonoBehaviour
{
    public AudioSource PickUpItemSound;
    public AudioSource PickUpWeaponSound;
    public AudioSource PlayerHurtSound;
    public AudioSource PlayerDeadSound;
    public AudioSource WheelClock;
    public AudioSource WheelDing;

    public void PlayPickUpItemSound()
    {
        PickUpItemSound.Play();
    }
    public void PlayPickUpWeaponSound()
    {
        PickUpWeaponSound.Play();
    }
    public void PlayPlayerHurtSound()
    {
        PlayerHurtSound.Play();
    }
    public void PlayPlayerDeadSound()
    {
        PlayerDeadSound.Play();
    }
    public void PlayWheelClockSound()
    {
        WheelClock.Play();
    }
    public void PlayWheelDingSound()
    {
        WheelDing.Play();
    }
}
