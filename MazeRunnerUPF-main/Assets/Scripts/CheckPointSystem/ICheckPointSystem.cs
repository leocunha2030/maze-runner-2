using UnityEngine;

public interface ICheckPointSystem
{
    public Vector3 LastCheckPoint();
    public void SetCheckPoint(Vector3 checkPoint);
}