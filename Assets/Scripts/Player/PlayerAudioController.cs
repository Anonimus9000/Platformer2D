using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField]
    private AudioClip _takeDamageAudio;
    [SerializeField]
    private AudioClip _attackAudio;

    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAttackAudio()
    {
        _audioSource.clip = _attackAudio;
        _audioSource.Play();
    }
    
    public void PlayTakeDamageAudio()
    {
        _audioSource.clip = _takeDamageAudio;
        _audioSource.Play();
    }
}
