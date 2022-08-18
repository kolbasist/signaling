using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(SpriteRenderer))]

public class MoverByThreePoints : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Transform _path;
    [SerializeField] private Transform _target;
    [SerializeField] private Sprite _initialSprite;
    [SerializeField] private Sprite _finalSprite;
    [SerializeField] private float _scaleMultiplier;
    
    private Transform _transform;
    private Vector3[] _pathPoints = new Vector3[4];
    private int _endsOfLineCount = 2;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _initialScale;
    private Vector3 _finalScale;

    private void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        Vector2 initialSize = _initialSprite.texture.Size();
        Vector2 finalSize = _finalSprite.texture.Size();
        _initialScale = finalSize / initialSize;
        _finalScale = initialSize / finalSize;

        SetInitialSprite();     

        if (_path.childCount > _endsOfLineCount) 
        { 
            Debug.Log("Too much elements in path gameobject.");
            return;
        }

        for(int i = 0; i < _path.childCount; i++)
        {            
            _pathPoints[i] = _path.GetChild(i).transform.position;            
        }

        _pathPoints[3] = _pathPoints[1];
        _pathPoints[1] = new Vector3((_pathPoints[0].y + _pathPoints[3].y) / 2, _target.position.x);
        _pathPoints[2] = _target.position;
    }
    
    private void Update()
    {
        _transform.position = Vector3.MoveTowards(_pathPoints[0], _pathPoints[3], _speed);
    }

    private void SetInitialSprite()
    {
        _spriteRenderer.sprite = _initialSprite;
        _transform.localScale = _initialScale*_scaleMultiplier;
    }

    private void SetFinalSprite()
    {
        _spriteRenderer.sprite = _finalSprite;
        _transform.localScale = _finalScale*_scaleMultiplier;
    }
}
