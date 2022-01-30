using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WardLightBoard : Interactable
{
    public bool hasPower = false;
    public bool pulled = false;

   

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip lightSwitchClip;

    public Transform handle_1;
    public Transform handle_2;
    public Transform handle_3;
    public Transform handle_4;


    public Animator exitDoorsAnim;


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
        audioSource.PlayOneShot(lightSwitchClip);

        GameStateManager.Instance.TriggerLightEvent();

        // stop monster
        BlinkMonsterManager.Instance.DeActivateBlinkMonster();
        GameStateManager.Instance.SetNewState(GameStates.TurnOnGenerator);


        // rotate switches
        handle_1.Rotate(new Vector3(0, 0, 128));
        handle_2.Rotate(new Vector3(0, 0, 128));
        handle_3.Rotate(new Vector3(0, 0, 128));
        handle_4.Rotate(new Vector3(0, 0, 128));
        // turn on all lights




        exitDoorsAnim.SetTrigger("DoorsOpen");
    }
}
