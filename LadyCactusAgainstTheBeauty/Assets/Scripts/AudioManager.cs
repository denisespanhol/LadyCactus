using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip backgroundMenu;
    public AudioClip cactusAttack;
    public AudioClip cactusDeath;
    public AudioClip cactusDamage;
    public AudioClip littleFlowerDeath;
    public AudioClip fashiowPlantDeath;
    public AudioClip carnivoreKisserDeath;

    private void Start()
    {
        musicSource.clip = backgroundMenu;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
