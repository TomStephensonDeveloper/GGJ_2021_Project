
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateManager : Singleton<GameStateManager>
{
    public GameStates currentGameState = GameStates.Intro;

    public bool hasGeneratorRoomKey = false;

    public event EventHandler OnLightsOn;


    public void TriggerLightEvent()
    {
        // call light event=
        if (OnLightsOn != null)
        {
            OnLightsOn(null, EventArgs.Empty);
        }
    }

    public void SetNewState(GameStates newState)
    {
        currentGameState = newState;

    }

}

public enum GameStates
{
    Intro,
    MonsterEncounter_01,
    TurnOnGenerator,
    MonsterEncounter_02,
    TurnOnLights,
    ExitWard
}
