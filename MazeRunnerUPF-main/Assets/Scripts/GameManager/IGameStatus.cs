using System;

public interface IGameStatus
{
    event Action OnGameOver;
    event Action OnWinGame;
    event Action OnPlayerDead;

    void InvokePlayerDeadEvent();
    void InvokeGameOverEvent();
    void InvokeWinGameEvent();
}