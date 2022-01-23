using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Input : Singleton<Input>
{
    [Header("Refrences")]
    public PlayerInput playerInput;

    [Header("Inputs")]
    public Vector2 mousePosition;
    public Vector2 moveDirictionInput;
    public bool holdingSprint = false;

    public void SwitchToUIInput()
    {
        playerInput.SwitchCurrentActionMap("UI");
    }
    public void SwitchToPlayerInput()
    {
        playerInput.SwitchCurrentActionMap("Player");
    }

    public void OnTogglePause(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        if(PauseStates.Instance.paused)
        {
            PauseStates.Instance.Resume();
            SwitchToPlayerInput();
        }
        else
        {
            PauseStates.Instance.Pause();
            SwitchToUIInput();
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        moveDirictionInput = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }



    public void OnSprintDown(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        holdingSprint = true;
    }
    public void OnSprintUp(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        holdingSprint = false;
    }



    public void OnInteractionDown(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
    }
    public void OnInteractionUp(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
    }
}
