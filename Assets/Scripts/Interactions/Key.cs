using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip grabKeyClip;

    [SerializeField] GameObject key;
 public bool pickedUp = false;

    [SerializeField] Door doorToUnlock;
    [SerializeField] Door doubleDoorToUnlock;

    [SerializeField] GameObject noKeyImage;
    [SerializeField] GameObject hasKeyImage;

    public override void Interact()
    {
        if (playerInTrigger && !pickedUp)
        {
            pickedUp = true;
            PickUpKey();
        }
    }

    void PickUpKey()
    {
        key.SetActive(false);
        audioSource.PlayOneShot(grabKeyClip);
        doorToUnlock.isLocked = false;
        doubleDoorToUnlock.isLocked = false;

        noKeyImage.SetActive(false);
        hasKeyImage.SetActive(true);
    }
}
