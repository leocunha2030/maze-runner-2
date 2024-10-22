using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IGameStatus _gameStatus;
    private ThirdPersonController _thirdPersonController;
    private CharacterController _characterController;
    private ICheckPointSystem _checkPointSystem;
    private int _qtdLife;
    private IUILifeManager _uiLifeManager;
    
    void Start()
    {
        _gameStatus = ServiceLocator.GetService<IGameStatus>();
        _gameStatus.OnPlayerDead += HandlerPlayerDead;
        
        _checkPointSystem = ServiceLocator.GetService<ICheckPointSystem>();
        _checkPointSystem.SetCheckPoint(transform.position);
        
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _characterController = GetComponent<CharacterController>();
        
        _uiLifeManager = ServiceLocator.GetService<IUILifeManager>();
        _qtdLife = 3;
    }

    private void HandlerPlayerDead()
    {
        _thirdPersonController.enabled = false;
        _characterController.enabled = false;

        transform.position = _checkPointSystem.LastCheckPoint();
        
        _thirdPersonController.enabled = true;
        _characterController.enabled = true;

        _qtdLife--;
        _uiLifeManager.SetQtdLife(_qtdLife);
    }
}
