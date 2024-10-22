using System;
using UnityEngine;
using UnityEngine.UI;

public class UILIfe : MonoBehaviour
{
    [SerializeField] private Image _image;

    private void Start()
    {
        //_image = GetComponent<Image>();
    }

    public void SetImage(Sprite sprite)
    {
        _image.sprite = sprite;
    }
}