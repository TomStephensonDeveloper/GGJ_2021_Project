using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseStates : Singleton<PauseStates>
{

    public bool mainMenu;

    public bool paused;

    public MenuManager menuManager;


    void Start()
    {
        // !!! This should ensure the game runs when you load in - And that the Menu is Closed !!!
        Resume();
    }

    public void TogglePause()
    {
        paused = !paused;
        if (paused)
        {
            Pause();
        }
        else
        {
            Resume();
        }

    }

    public void Pause()
    {
        paused = true;

        Time.timeScale = 0;



        if (!mainMenu)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Input.Instance.SwitchToUIInput();
        }

        menuManager.ShowMenuPage(0);


    }
    public void Resume()
    {
        paused = false;
        Time.timeScale = 1;




        if (!mainMenu)
        {
            Input.Instance.SwitchToPlayerInput();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            menuManager.HideAllMenuPages();
        }



    }


}
