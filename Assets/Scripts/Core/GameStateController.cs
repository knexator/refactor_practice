using System;
using UnityEngine;

namespace Core
{
    public class GameStateController : Singleton<GameStateController>
    {
        public static bool dontDestroyOnLoad = true;

        public enum GameState
        {
            Gameplay,
            Pause,
            None
        }

        public GameState gameState;
        

        public void ChangeGameStateTo(GameState state)
        {
            gameState = state;
        }
    }
}
