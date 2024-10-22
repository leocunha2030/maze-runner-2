using UnityEngine;

public class LavaOffsetAnimation : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Renderer _renderer;
    private Vector2 _offset;
    
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _offset = _renderer.material.GetTextureOffset("_BaseMap");
    }
    
    void Update()
    {
        _offset.y += _speed * Time.deltaTime;
        _renderer.material.SetTextureOffset("_BaseMap", _offset);
    }
}
