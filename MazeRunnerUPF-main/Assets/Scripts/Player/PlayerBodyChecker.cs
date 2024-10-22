using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBodyChecker : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    private IGameStatus _gameStatus;
    private ActionAreaScript _actionAreaScript;


    private void ActionKey(bool pressedKey)
    {
        if (!_actionAreaScript || !pressedKey) return;
        
        Debug.Log($"ActionKey = {pressedKey}");
        _actionAreaScript.TriggerArea();
    }

    private void Start()
    {
        _playerInput.actions["ActionKey"].performed += ctx => ActionKey(true);
        _playerInput.actions["ActionKey"].canceled += ctx => ActionKey(false);

        _gameStatus = ServiceLocator.GetService<IGameStatus>();
        _actionAreaScript = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dead"))
        {
            Debug.Log("Dead");
            _gameStatus.InvokePlayerDeadEvent();
        }
        else if (other.CompareTag("ActionArea"))
        {
            _actionAreaScript = other.GetComponent<ActionAreaScript>();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ActionArea"))
        {
            _actionAreaScript = null;
        }
    }
}