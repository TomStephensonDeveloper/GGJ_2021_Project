using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : Singleton<RespawnManager>
{
    public IEnumerator FadeInOutTimerCoRoutine;

    public Animator screenBlack;

    public ActivateBlinkMonsterTrigger monsterEnounter_01_Trigger;
    public ActivateBlinkMonsterTrigger monsterEnounter_02_Trigger;

    public Transform playerRespawnPoint_MonsterEncounter_01;
    public Transform playerRespawnPoint_MonsterEncounter_02;


    public Transform player;

    IEnumerator FadeInOutTimer()
    {
        Debug.Log("restart timer coroutine started");

        //yield return new WaitForSeconds(3);
        screenBlack.SetTrigger("fadeIn");
        yield return new WaitForSeconds(0.5f);
        Respawn();
        yield return new WaitForSeconds(2f);
         screenBlack.SetTrigger("fadeOut");

        // Show hud
        PlayerHUDManager.Instance.ShowHUD();

    }

    public void StartRespawnTimer()
    {
        if (FadeInOutTimerCoRoutine != null)
        {
            StopCoroutine(FadeInOutTimerCoRoutine);
        }
        FadeInOutTimerCoRoutine = FadeInOutTimer();
        StartCoroutine(FadeInOutTimerCoRoutine);
    }

    void Respawn()
    {
        // put player at last checkpoint
        Debug.Log("Player Respawned");
        // reset scene

        // disable monster
        BlinkMonsterManager.Instance.DeActivateBlinkMonster();

        if (GameStateManager.Instance.currentGameState == GameStates.MonsterEncounter_01)
        {
            // Set player pos
            player.position = playerRespawnPoint_MonsterEncounter_01.position;
            player.rotation = playerRespawnPoint_MonsterEncounter_01.rotation;
          

            PlayerDeathManager.Instance.SetPlayerAlive();



            // reset monster trigger
            monsterEnounter_01_Trigger.ResetBlinkMonsterTrigger();
            monsterEnounter_02_Trigger.ResetBlinkMonsterTrigger();
        }
       else if(GameStateManager.Instance.currentGameState == GameStates.MonsterEncounter_02)
        {
            // Set player pos
            player.position = playerRespawnPoint_MonsterEncounter_02.position;
            player.rotation = playerRespawnPoint_MonsterEncounter_02.rotation;
          

            PlayerDeathManager.Instance.SetPlayerAlive();


            // reset monster trigger
            monsterEnounter_02_Trigger.ResetBlinkMonsterTrigger();
        }
        else
        {
            Debug.Log("WTF, player shouldn't be dead");


            
        }
    }
}
