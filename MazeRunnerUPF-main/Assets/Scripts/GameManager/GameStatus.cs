using System;

public class GameStatus : IGameStatus
{
    public event Action OnGameOver;
    public event Action OnWinGame;
    public event Action OnPlayerDead;
    
    public void InvokePlayerDeadEvent()
    {
        OnPlayerDead?.Invoke();
    }

    public void InvokeGameOverEvent()
    {
        OnGameOver?.Invoke();
    }

    public void InvokeWinGameEvent()
    {
        // if (OnWinGame != null)
        // {
        //     OnWinGame.Invoke();
        // }
        
        OnWinGame?.Invoke();
    }
}