using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] ambientClips;


    public IEnumerator PlayRandomAmbientCoRoutine;

    int lastRandom;

    void Start()
    {
        StartRandomAmbientSounds();
    }


    void StartRandomAmbientSounds()
    {
        if (PlayRandomAmbientCoRoutine != null)
        {
            StopCoroutine(PlayRandomAmbientCoRoutine);
        }
        PlayRandomAmbientCoRoutine = PlayRandomAmbientSounds();
        StartCoroutine(PlayRandomAmbientCoRoutine);
    }
    void StopRandomAmbientSounds()
    {
        if (PlayRandomAmbientCoRoutine != null)
        {
            StopCoroutine(PlayRandomAmbientCoRoutine);
        }
    }



    IEnumerator PlayRandomAmbientSounds()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range( 10 ,60));
            PlayRandomSound();
        }
    }

    void PlayRandomSound()
    {
        
        int random = Random.Range(0, ambientClips.Length );
        
        if(random == lastRandom)
        {
            for (int i = 0; i < 6; i++)
            {
                random = Random.Range(0, ambientClips.Length );
                if (random == lastRandom)
                {
                    // grrrr
                }
                else
                {
                    lastRandom = random;
                    break;
                }
            }
        }


        audioSource.PlayOneShot(ambientClips[random]);
    }
}
