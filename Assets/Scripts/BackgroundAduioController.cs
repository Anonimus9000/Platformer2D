using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BackgroundAduioController : MonoBehaviour
{
    public AudioClip BackgroundMusic;
    public AudioClip FightBackgroundMusic;
    public float MusicTransitionTime = 3;
    private PlayerController _player;
    private AudioSource _audioSource;
    private bool _isPlayingBaclgroundMusic = true;
    private float _timer;
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = BackgroundMusic;
    }

    void FixedUpdate()
    {
        if (_player.IsFight() && _isPlayingBaclgroundMusic)
        {
            if (IsTimeChagneExpire())
            {
                SmoothChangeMusic(BackgroundMusic, FightBackgroundMusic);
                _isPlayingBaclgroundMusic = false;
            }
        }
        else if (!_player.IsFight() && !_isPlayingBaclgroundMusic)
        {
            if (IsTimeChagneExpire())
            {
                SmoothChangeMusic(FightBackgroundMusic, BackgroundMusic);
                _isPlayingBaclgroundMusic = true;
            }
        }

    }

    private void SmoothChangeMusic(AudioClip StopAudio, AudioClip StartAudio)
    {
        float StartVolume = _audioSource.volume;
        float NowVolume = _audioSource.volume;

        while (NowVolume > 0)
        {
            NowVolume -= 0.001f * Time.fixedDeltaTime;
            _audioSource.volume = NowVolume;
        }
        _audioSource.Stop();
        _audioSource.clip = StartAudio;
        _audioSource.Play();

        while (NowVolume <= StartVolume)
        {
            NowVolume += 0.001f * Time.fixedDeltaTime;
            _audioSource.volume = NowVolume;
        }
    }

    private bool IsTimeChagneExpire()
    {
        
        print("MusicTransTime: " + MusicTransitionTime);
        while (_timer < MusicTransitionTime)
        {
            print("Timer: " + _timer);
            _timer += 1 * Time.fixedDeltaTime;
        }

        if (_timer >= MusicTransitionTime)
        {
            _timer = 0;
            return true;
        }
        else
        {
            _timer = 0;
            return false;
        }
    }
}
