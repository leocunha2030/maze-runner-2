using UnityEngine;

public class SmoothPlatformMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;

    private Vector3[] _targetPosition;
    private int _idTarget;

    private void Start()
    {
        _targetPosition = new Vector3[2];
        _targetPosition[0] = _target.position;
        _targetPosition[1] = transform.position;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            _targetPosition[_idTarget], _speed * Time.deltaTime);

        if (transform.position == _targetPosition[_idTarget])
        {
            _idTarget =_idTarget < 1 ? _idTarget + 1 : 0;
        }
    }
}