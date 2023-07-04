using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip NormalButtonSound;
    [SerializeField] private AudioClip EnterBattleSound;
    [SerializeField] private AudioClip BattleStartSound;
    [SerializeField] private AudioClip EndGameSound;
    [SerializeField] private AudioClip MapOpenSound;
    [SerializeField] private AudioClip LoseSound;

    private AudioSource audioSource;

    void Start()
    {

    }

    public void PlayNormalButtonSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(NormalButtonSound);
    }

    public void PlayEnterBattleSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(EnterBattleSound);
    }

    public void PlayBattleStartSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(BattleStartSound);
    }

    public void PlayEndGameSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(EndGameSound);
    }

    public void PlayMapOpenSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(MapOpenSound);
    }

    public void PlayLoseSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(LoseSound);
    }

}
