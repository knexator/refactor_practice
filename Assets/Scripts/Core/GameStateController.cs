using System;
using UnityEngine;

namespace Core
{
    public class GameStateController : PersistentSingleton<GameStateController>
    {
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
