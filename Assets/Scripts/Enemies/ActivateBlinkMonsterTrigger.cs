using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBlinkMonsterTrigger : MonoBehaviour
{
    [SerializeField] bool activated = false;
    [SerializeField] Transform blinkMonsterSpawnLocation;

    [SerializeField] GameStates stateChange;
    public void ResetBlinkMonsterTrigger()
    {
        activated = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !activated)
        {
            ActivateMonster();
        }
    }

    void ActivateMonster()
    {
        if(!activated)
        {
            activated = true;
            BlinkMonsterManager.Instance.ActivateBlinkMonster(blinkMonsterSpawnLocation);
            GameStateManager.Instance.SetNewState(stateChange);
        }
     
    }
}
