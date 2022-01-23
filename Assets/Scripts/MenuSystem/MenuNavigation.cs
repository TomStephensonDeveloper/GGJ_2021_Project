using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MenuNavigation : MonoBehaviour
{
    public MenuManager menuManager;


    public bool mainMenu;
    public float menuInputY;


    // Delay input so you don't scroll through menu options each frame
    public float lastInputTime;
    public float maxMenuInputDelay = 0.25f;



    public Vector2 moveDirictionInput;

    public void OnMovement(InputAction.CallbackContext context)
    {
        moveDirictionInput = context.ReadValue<Vector2>();
    }


    
    


    void Update()
    {
        menuInputY = moveDirictionInput.y;

        if (PauseStates.Instance.paused || mainMenu)// || mainMenu)
        {
            //// If no navigation input reset timer and return
            if (menuInputY == 0)
            {
                lastInputTime = maxMenuInputDelay;
            }



            if (Time.realtimeSinceStartup - lastInputTime >= maxMenuInputDelay)
            {
                // Cycle through UI
                if (menuInputY < -0.5f)
                {
                    // Down
                    menuManager.NextElement();
                    lastInputTime = Time.realtimeSinceStartup;
                }
                else if (menuInputY > 0.5f)
                {
                    // Up
                    menuManager.PreviousElement();
                    lastInputTime = Time.realtimeSinceStartup;
                }
            }
        }
    }
}
