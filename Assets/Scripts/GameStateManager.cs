using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateManager
{
    public enum GameState
    {
        Playing,
        Menu,
        Dead,
        Pause
    }
    private static GameState currentState;
    public static GameState CurrentState { get { return currentState; } set { currentState = value; } }
}
