using System;
using UnityEngine;

public class WinCheckPoint : MonoBehaviour
{
    private IGameStatus _gameStatus;

    private void Start()
    {
        _gameStatus = ServiceLocator.GetService<IGameStatus>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gameStatus.InvokeWinGameEvent();
        }
    }
}