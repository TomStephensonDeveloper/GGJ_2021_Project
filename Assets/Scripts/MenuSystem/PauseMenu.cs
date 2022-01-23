using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public MenuNavigation menuNavigation;

    public void Resume()
    {
        // Close Menu 
        PauseStates.Instance.Resume();
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }



}
