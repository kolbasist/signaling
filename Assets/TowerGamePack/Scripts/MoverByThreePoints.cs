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

    private Vector3[] _pathPoints = new Vector3[4];
    private int _endsOfLineCount = 2;
    private int[] _order = new int[] { 1, 2, 1, 3 };
    private int _currentTarget;
    private WaitForSeconds _grabbingTime;

    private void Awake()
    {
        int begin = 0;
        int end = 3;
        float middlepoint = 1 / 2;
        _grabbingTime = new WaitForSeconds(_timeToGrab);
        //better generate _grabtime before start coroutine

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
        _pathPoints[1] = Vector3.Lerp(_pathPoints[end], _pathPoints[begin], middlepoint);
        _pathPoints[2] = _target.position;
        _currentTarget = 0;
    }
    
    private void Update()
    {           
        transform.position = Vector3.MoveTowards(transform.position, _pathPoints[_order[_currentTarget]], _speed*Time.deltaTime);
        if (base.transform.position == _pathPoints[_order[_currentTarget]])
        {
            _currentTarget++;

            if (_currentTarget >= _order.Length) {
                _currentTarget = 0;
                transform.position = _pathPoints[0];                
            }
                
            if (transform.position == _pathPoints[2])
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
        yield return _grabbingTime;        
        _speed = speed;
    }
}
