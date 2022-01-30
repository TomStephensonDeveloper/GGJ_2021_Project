using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatAudioManager : Singleton<HeartbeatAudioManager>
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip heartbeatSlowClip;
    [SerializeField] AudioClip heartbeatFastClip;


    public void PlayHeartBeatAudio()
    {
        audioSource.clip = heartbeatFastClip;
        audioSource.Play();
    }

    public void StopHeartBeatAudio()
    {
        audioSource.Stop();
    }
}
