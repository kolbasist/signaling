using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(SpriteSetter))]

public class MoverByThreePoints : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Transform _path;
    [SerializeField] private Transform _target;
    [SerializeField] private float _fadingTime;

    private Transform _transform;
    private Vector3[] _pathPoints = new Vector3[4];
    private int _endsOfLineCount = 2;
    private bool _isSpriteChanged;
    private float _moveCycleDuration = 5f;
    private SpriteSetter _spriteSetter;
    private int[] _order = new int[] { 1, 2, 1, 3 };
    private int _currentTarget;
    private WaitForSeconds _grabbingTime;

    private void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();       
        _spriteSetter = gameObject.GetComponent<SpriteSetter>();
        _spriteSetter.SetInitialSprite();
        _grabbingTime = new WaitForSeconds(_fadingTime);

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
        _pathPoints[1] = Vector3.Lerp(_pathPoints[3], _pathPoints[0], 0.5f);
        _pathPoints[2] = _target.position;
        _currentTarget = 0;
    }
    
    private void Update()
    {
        float linearizedPosition = (Time.time % _moveCycleDuration)/_moveCycleDuration;        

        Vector3 currentPath  =  _pathPoints[3] - _pathPoints[0];
        //Debug.Log(linearizedPosition + " " + _isSpriteChanged);
        _transform.position = Vector3.MoveTowards(_transform.position, _pathPoints[_order[_currentTarget]], _speed*Time.deltaTime);
        if (transform.position == _pathPoints[_order[_currentTarget]])
        {
            _currentTarget++;

            if (_currentTarget >= _order.Length) { 
                _currentTarget = 0; 
                _transform.position = _pathPoints[0];
                _spriteSetter.SetInitialSprite();
            }
                
            if (_transform.position == _pathPoints[2])
            {
                var wait = StartCoroutine(GrabTower());
            }
        }
    }  

    private IEnumerator GrabTower()
    {
        float speed = _speed;
        _speed = 0f;
        Debug.Log("grab " + Time.time);
        yield return _grabbingTime;
        Debug.Log("Exit " + Time.time);
        _speed = speed;
    }
}
