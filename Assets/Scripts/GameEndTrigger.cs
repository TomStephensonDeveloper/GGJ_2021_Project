using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{

    public bool playerInTrigger;
    public bool triggeredEnd;

    public GameObject endScreenUI;
    public MenuManager menuManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            TriggerEnd();
        }
    }

    void TriggerEnd()
    {
        if(!triggeredEnd)
        {
            GameStateManager.Instance.SetNewState(GameStates.ExitWard);
            PlayerHUDManager.Instance.HideHUD();
            endScreenUI.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Input.Instance.SwitchToUIInput();

            menuManager.ShowMenuPage(3);
        }
    }
}
