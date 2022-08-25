using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class SpriteSetter : MonoBehaviour
{    
    [SerializeField] private Sprite _initialSprite;
    [SerializeField] private Sprite _finalSprite;
    [SerializeField] private float _scaleMultiplier;
    [SerializeField] private Transform _initialPoint;

    private SpriteRenderer _spriteRenderer;
    private Vector2 _initialScale;
    private Vector2 _finalScale;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();       
        SetScales();
        SetInitialSprite();
    }

    private void Update()
    {
        if (base.transform.position == _initialPoint.position)
            SetInitialSprite();
    }

    private void SetScales()
    {
        Vector2 initialSize = _initialSprite.texture.Size();
        Vector2 finalSize = _finalSprite.texture.Size();
        _initialScale = finalSize / initialSize;
        _finalScale = initialSize / finalSize;

        if (_initialScale.magnitude > _finalScale.magnitude)
        {
            _finalScale = Vector2.one;
        }
        else
        {
            _initialScale = Vector2.one;
        }
    }

    public void SetInitialSprite()
    {
        _spriteRenderer.sprite = _initialSprite;
        transform.localScale = _initialScale * _scaleMultiplier;
    }

    public void SetFinalSprite()
    {
        _spriteRenderer.sprite = _finalSprite;
        transform.localScale = _finalScale * _scaleMultiplier;
    }

    public void SetEmpty()
    {
        _spriteRenderer.sprite = null;
    }
}
