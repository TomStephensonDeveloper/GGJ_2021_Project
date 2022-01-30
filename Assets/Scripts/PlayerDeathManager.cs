using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathManager : Singleton<PlayerDeathManager>
{
    public bool isDead = false;

    public event EventHandler OnPlayerDied;
    public event EventHandler OnPlayerRespawned;

    public Animator playerDeathAnimator;


    public GameObject playerDeathAnimModel_1;
    public GameObject playerDeathAnimModel_2;


    public void SetPlayerDead(PlayerDeathType playerDeathType)
    {
        if(!isDead)
        {
            isDead = true;

            // call death event
            if (OnPlayerDied != null)
            {
                OnPlayerDied(null, EventArgs.Empty);
            }

            PlayerHUDManager.Instance.HideHUD();

            switch (playerDeathType)
            {
                case PlayerDeathType.LookedAwayFromBlinkMonster:
                    {
                        playerDeathAnimator.SetTrigger("LookedAwayFromBlinkMonsterDeath");
                        break;
                    }
                case PlayerDeathType.BlinkedWhenBlinkMonsterIsTooClose:
                    {
                        playerDeathAnimator.SetTrigger("BlinkedTooCloseBlinkMonsterDeath");
                        break;
                    }
            }
        }
       
    }
  
    public void SetPlayerAlive()
    {
        isDead = false;

        // call respawned event
        if (OnPlayerRespawned != null)
        {
            OnPlayerRespawned(null, EventArgs.Empty);
        }

        playerDeathAnimModel_1.SetActive(false);
        playerDeathAnimModel_2.SetActive(false);
        playerDeathAnimator.SetTrigger("PlayerRespawned");

       
    }
    
}

public enum PlayerDeathType
    {
        LookedAwayFromBlinkMonster,
        BlinkedWhenBlinkMonsterIsTooClose,

    }