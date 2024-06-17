using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundInAnimation : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlaySound()
    {
        audioSource.Play();
    }
}
