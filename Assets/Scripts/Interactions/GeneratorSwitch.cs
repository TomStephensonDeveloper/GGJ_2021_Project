using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorSwitch : Interactable
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip generatorSwitchClip;


    [SerializeField] AudioSource audioSourceGeneratorHum;

    public Animator switchAnim;

    public bool pulled = false;

    [SerializeField] WardLightBoard wardLightBoard;

    public override void Interact()
    {
        if (playerInTrigger && !pulled)
        {
            pulled = true;
            PullSwitch();
        }
    }

    void PullSwitch()
    {
        switchAnim.SetTrigger("pullSwitch");
        audioSource.PlayOneShot(generatorSwitchClip);
        wardLightBoard.hasPower = true;
        audioSourceGeneratorHum.Play();
    }
}
