using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDragAnimMethods : MonoBehaviour
{
    public Door door;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip screamClip;
    public void PlayScream()
    {
        audioSource.PlayOneShot(screamClip);
    }


    public void OpenDoor()
    {
        door.OpenDoor();
    }

    public void CloseDoor()
    {
        door.ShutDoor();
    }
}
