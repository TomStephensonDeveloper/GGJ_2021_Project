using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMonsterTrigger : MonoBehaviour
{
    [SerializeField] bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            StopMonster();
        }
    }

    void StopMonster()
    {
        if (!triggered && GameStateManager.Instance.currentGameState == GameStates.MonsterEncounter_01)
        {
            triggered = true;
            BlinkMonsterManager.Instance.DeActivateBlinkMonster();
            GameStateManager.Instance.SetNewState(GameStates.TurnOnGenerator);
        }

    }
}
