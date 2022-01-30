using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] BoxCollider doorCollider;
public bool isLocked = false;
    public bool isOpen = false;
    [SerializeField] Animator doorAnim;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip openDoorClip;
    [SerializeField] AudioClip closeDoorClip;


    void Start()
    {
        if(isOpen)
        {
            doorAnim.SetBool("isOpen", true);
        }
    }

    public override void Interact()
    {
        if(playerInTrigger)
        {
            InteractWithDoor();
        }
    }


    public void EnableDoorCollider()
    {
        doorCollider.enabled = true;
    }
    public void DisableDoorCollider()
    {
        doorCollider.enabled = false;
    }

    void InteractWithDoor()
    {
        Debug.Log("Interacted with door");

        if (!isLocked)
        {
            if(isOpen)
            {
                ShutDoor();
            }
            else
            {
                OpenDoor();
            }
        }
        else
        {
            Debug.Log("DoorLocked");
        }
    }


    public void OpenDoor()
    {
        if(!isOpen)
        {
            // open
            isOpen = true;

            // play open animation
            doorAnim.SetBool("isOpen", true);
            // play audio
            audioSource.PlayOneShot(openDoorClip);
        }
    }

   public void ShutDoor()
    {
        if(isOpen)
        {
            // shut
            isOpen = false;

            // play shut animation

            doorAnim.SetBool("isOpen", false);
            // play audio
            audioSource.PlayOneShot(closeDoorClip);
        }
    }
}
