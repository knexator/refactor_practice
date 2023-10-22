using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Gameplay,
    Cutscene,
}

public static class StaticData 
{
    [Header("[Values]")]
    public static int gamePhase;

    public static GameState gameState;
}
