using System;
using System.Net.Security;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private ICheckPointSystem _checkPointSystem;

    private void Start()
    {
        _checkPointSystem = ServiceLocator.GetService<ICheckPointSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _checkPointSystem.SetCheckPoint(transform.position);
        }
    }
}
