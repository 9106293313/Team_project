using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1SoundEft : MonoBehaviour
{
    public AudioSource ShieldOpenSound;
    public AudioSource ShieldCloseSound;
    public AudioSource ShieldSound;
    public AudioSource NormalAtkSound;
    public AudioSource PoisonArrowSound;
    public AudioSource ZoomOutSound;
    public AudioSource FirePillowSound;
    public AudioSource FirePillowChargeSound;

    public AudioSource RootDirtSound;
    public AudioSource RootAtkSound;
    public AudioSource TreePoisonAtkSound;
    public AudioSource TentacleSound;
    public AudioSource TreeDamageSound;

    public void PlayShieldOpenSound()
    {
        ShieldOpenSound.Play();
    }
    public void PlayShieldCloseSound()
    {
        ShieldCloseSound.Play();
    }
    public void PlayShieldSound()
    {
        ShieldSound.Play();
    }
    public void PlayNormalAtkSound()
    {
        NormalAtkSound.Play();
    }
    public void PlayPoisonArrowSound()
    {
        PoisonArrowSound.Play();
    }
    public void PlayZoomOutSound()
    {
        ZoomOutSound.Play();
    }
    public void PlayFirePillowSound()
    {
        FirePillowSound.Play();
    }
    public void PlayFirePillowChargeSound()
    {
        FirePillowChargeSound.Play();
    }

    public void PlayRootDirtSound()
    {
        RootDirtSound.Play();
    }
    public void PlayRootAtkSound()
    {
        RootAtkSound.Play();
    }
    public void PlayTreePoisonAtkSound()
    {
        TreePoisonAtkSound.Play();
    }
    public void PlayTreeTentacleSound()
    {
        TentacleSound.Play();
    }
    public void PlayTreeDamageSound()
    {
        TreeDamageSound.Play();
    }
}
