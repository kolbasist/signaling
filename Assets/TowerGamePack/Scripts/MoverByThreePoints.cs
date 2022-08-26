using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoverByThreePoints : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Transform _path;
    [SerializeField] private Transform _target;
    [SerializeField] private float _timeToGrab;

    private Vector3[] _pathPoints;
    private int[] _order = new int[] { 1, 2, 1, 3 };
    private int _currentTarget;   

    private void Awake()
    {
        PathParser pathParser = new PathParser();
        _pathPoints = pathParser.Parse(_path, _target);
        _currentTarget = 0;       
    }
    
    private void Update()
    {           
        transform.position = Vector3.MoveTowards(transform.position, _pathPoints[_order[_currentTarget]], _speed*Time.deltaTime);
        if (transform.position == _pathPoints[_order[_currentTarget]])
        {
            _currentTarget++;

            if (_currentTarget >= _order.Length) {
                _currentTarget = 0;
                transform.position = _pathPoints[0];                
            }
                
            if (transform.position == _target.position)
            {
                StartCoroutine(GrabTower());
            }
        }
    }  

    private IEnumerator GrabTower()
    {
        float speed = _speed;
        float grabbingSpeed = 0f;
        _speed = grabbingSpeed;       
        yield return new WaitForSeconds(_timeToGrab);        
        _speed = speed;
    }
}
