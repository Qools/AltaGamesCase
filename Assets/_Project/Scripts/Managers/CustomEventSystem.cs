using System;
using UnityEngine;

public static class CustomEventSystem
{
    public static Action OnStartGame;
    public static void CallStartGame() => OnStartGame?.Invoke();

    public static Action<GameResult> OnGameOver;
    public static void CallGameOver(GameResult gameResult) => OnGameOver?.Invoke(gameResult);

    public static Action OnNewLevelLoad;
    public static void CallNewLevelLoad() => OnNewLevelLoad?.Invoke();

    public static Action OnBallKicked;
    public static void CallBallKicked() => OnBallKicked?.Invoke();

    public static Action OnBallExploded;
    public static void CallBallExploded() => OnBallExploded?.Invoke();
    
    
    public static Action OnObstaclesCleared;
    public static void CallObstaclesCleared() => OnObstaclesCleared?.Invoke();
}
