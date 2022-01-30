using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : Singleton<InteractionManager>
{

    [SerializeField] Interactable currentInteractable;

    [SerializeField] GameObject pressEIconToInteract;
    [SerializeField] GameObject pressEIconToOpen;
    [SerializeField] GameObject pressEIconToClose;
    [SerializeField] GameObject pressEIconToPickUp;
    [SerializeField] GameObject lockedText;

    public void SetCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;



        if (currentInteractable.GetType() == typeof(Door))
        {
            Debug.Log("it's a door");
            Door door = (Door)currentInteractable;
            if (door.isLocked)
            {
                lockedText.SetActive(true);
                pressEIconToInteract.SetActive(false);
                pressEIconToOpen.SetActive(false);
                pressEIconToClose.SetActive(false);
                pressEIconToPickUp.SetActive(false);
            }
            else
            {
                if (!door.isOpen)
                {
                    pressEIconToOpen.SetActive(true);
                    pressEIconToInteract.SetActive(false);
                    lockedText.SetActive(false);
                    pressEIconToClose.SetActive(false);
                    pressEIconToPickUp.SetActive(false);
                }
                else
                {
                    pressEIconToOpen.SetActive(false);
                    pressEIconToInteract.SetActive(false);
                    lockedText.SetActive(false);
                    pressEIconToClose.SetActive(true);
                    pressEIconToPickUp.SetActive(false);
                }

            }
        }
        else if (currentInteractable.GetType() == typeof(Key))
        {
            Debug.Log("it's a key");
            Key key = (Key)currentInteractable;
            if (key.pickedUp)
            {
                pressEIconToOpen.SetActive(false);
                pressEIconToInteract.SetActive(false);
                lockedText.SetActive(false);
                pressEIconToClose.SetActive(false);
                pressEIconToPickUp.SetActive(false);
            }
            else
            {
                pressEIconToOpen.SetActive(false);
                pressEIconToInteract.SetActive(false);
                lockedText.SetActive(false);
                pressEIconToClose.SetActive(false);
                pressEIconToPickUp.SetActive(true);
            }
        }

        else if (currentInteractable.GetType() == typeof(WardLightBoard))
        {
            WardLightBoard wardLightBoard = (WardLightBoard)currentInteractable;
            if(wardLightBoard.pulled)
            {
                pressEIconToOpen.SetActive(false);
                pressEIconToInteract.SetActive(false);
                lockedText.SetActive(false);
                pressEIconToClose.SetActive(false);
                pressEIconToPickUp.SetActive(false);
            }
            else
            {
                pressEIconToOpen.SetActive(false);
                pressEIconToInteract.SetActive(true);
                lockedText.SetActive(false);
                pressEIconToClose.SetActive(false);
                pressEIconToPickUp.SetActive(false);
            }
        }
        else if (currentInteractable.GetType() == typeof(GeneratorSwitch))
        {
            GeneratorSwitch generatorSwitch = (GeneratorSwitch)currentInteractable;
            if (generatorSwitch.pulled)
            {
                pressEIconToOpen.SetActive(false);
                pressEIconToInteract.SetActive(false);
                lockedText.SetActive(false);
                pressEIconToClose.SetActive(false);
                pressEIconToPickUp.SetActive(false);
            }
            else
            {
                pressEIconToOpen.SetActive(false);
                pressEIconToInteract.SetActive(true);
                lockedText.SetActive(false);
                pressEIconToClose.SetActive(false);
                pressEIconToPickUp.SetActive(false);
            }
        }
        else
        {
            pressEIconToInteract.SetActive(true);
            lockedText.SetActive(false);
            pressEIconToOpen.SetActive(false);
            pressEIconToClose.SetActive(false);
            pressEIconToPickUp.SetActive(false);

        }

    }

    public void RemoveCurrentInteractable()
    {
        currentInteractable = null;
        pressEIconToInteract.SetActive(false);
        lockedText.SetActive(false);
        pressEIconToOpen.SetActive(false);
        pressEIconToClose.SetActive(false);
        pressEIconToPickUp.SetActive(false);
    }

    public void PerformCurrentInteraction()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
        else
        {
            Debug.Log("no interactable in range");
        }
    }
}
