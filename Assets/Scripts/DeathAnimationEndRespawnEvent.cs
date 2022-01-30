using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimationEndRespawnEvent : MonoBehaviour
{
   public void EndOfDeathAnimation()
    {
        RespawnManager.Instance.StartRespawnTimer();
    }
}
