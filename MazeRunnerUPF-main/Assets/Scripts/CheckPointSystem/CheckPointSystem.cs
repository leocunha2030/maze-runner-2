using UnityEngine;

public class CheckPointSystem : ICheckPointSystem
{
    private Vector3 _lastCheckPoint;
    
    public Vector3 LastCheckPoint()
    {
        return _lastCheckPoint;
    }

    public void SetCheckPoint(Vector3 checkPoint)
    {
        _lastCheckPoint = checkPoint;
    }
}