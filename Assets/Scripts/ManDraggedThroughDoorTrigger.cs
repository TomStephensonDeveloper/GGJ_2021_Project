using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManDraggedThroughDoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject draggedThroughDoorContainer;


    [SerializeField] bool isTriggered;

    void StartDraggingEvent()
    {
        draggedThroughDoorContainer.SetActive(true);
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            StartDraggingEvent();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            StartDraggingEvent();
        }
    }
}
