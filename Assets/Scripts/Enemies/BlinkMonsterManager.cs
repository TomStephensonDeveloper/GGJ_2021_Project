using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkMonsterManager : Singleton<BlinkMonsterManager>
{
    public bool blinkMonsterIsActive = false;
    public GameObject blinkMonster;
    public BlinkEnemyMovementAI blinkEnemyMovementAI;

    public Transform lastSpawnLocation;




    void Start()
    {
        // Subscribe to death event
        PlayerDeathManager.Instance.OnPlayerDied += delegate (object sender, EventArgs e)
        {
            DeActivateBlinkMonster();
        };


   
    }


    // Activate the blink monster -- It is present in the game
    public void ActivateBlinkMonster(Transform spawnLocation)
    {
        // only activate if not currently active
        if(!blinkMonsterIsActive)
        {
            // used for respawning
            lastSpawnLocation = spawnLocation;

     

            // turn on monster
            blinkMonster.SetActive(true);

            // move to location
            blinkEnemyMovementAI.MoveToSpawnPoint(spawnLocation);

            // play audio cue
            HeartbeatAudioManager.Instance.PlayHeartBeatAudio();


            blinkMonsterIsActive = true;
        }
        else
        {
            return;
        }
    }

    // DeActivate the blink monster -- Player will no longer encounter
    public void DeActivateBlinkMonster()
    {
        // only deactivate if active
        if (blinkMonsterIsActive)
        {
            blinkEnemyMovementAI.MoveToSpawnPoint(lastSpawnLocation);

            blinkMonsterIsActive = false;

            // turn off monster
            blinkMonster.SetActive(false);

            // stop audio cue
            HeartbeatAudioManager.Instance.StopHeartBeatAudio();
        }
        else
        {
            return;
        }
    }


}
