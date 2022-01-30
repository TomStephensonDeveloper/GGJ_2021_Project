using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDManager : Singleton<PlayerHUDManager>
{
    [SerializeField] GameObject playerHudCanvas;

    public void ShowHUD()
    {
        playerHudCanvas.SetActive(true);
    }
    public void HideHUD()
    {
        playerHudCanvas.SetActive(false);
    }
}
