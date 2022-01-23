using UnityEngine.Audio;
using System.Collections;
using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("References")]
    // AudioMixers
    [SerializeField] AudioMixer mainAudioMixer;
    // AudioSources
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource ambienceAudioSource;
    [SerializeField] AudioSource UIAudioSource;



    // ????
    // Make an ambient sound manager??? -- can contain ambient sound 'groups' that can be switched between, for layered ambience. Handy for changing biome etc.
    // Make a music manager? Shuffle music, make music groups for use in different areas/situations. 
    // Make UI audio manager -- tell audio in it to play when game is paused. 

    // Generic audio group scriptable object.   can be used for stuff like multiple footstep sounds etc

    // ????



    void SetUpMainAudioSources()
    {
        // Set to 2D
        SetAudioSourceTo2D(musicAudioSource);
        SetAudioSourceTo2D(ambienceAudioSource);
        SetAudioSourceTo2D(UIAudioSource);


    }

    public void SetAudioSourceTo2D(AudioSource audioSource)
    {
        audioSource.spatialBlend = 0;
    }
    public void SetAudioSourceTo3D(AudioSource audioSource)
    {
        audioSource.spatialBlend = 1;
    }

    public void SetRandomPitch(AudioSource audioSource, float minPitch, float maxPitch)
    {
        audioSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
    }

    public void PlayOneShot(AudioSource audioSource, AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }

    // ? playoneshot random pitch --- set the pitch for the sound like in website example




   
}
