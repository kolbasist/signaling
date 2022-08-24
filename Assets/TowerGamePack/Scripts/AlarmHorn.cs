using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmHorn : MonoBehaviour
{
    [SerializeField] UnityEvent _crossed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thiev thiev))            
        {
            _crossed?.Invoke();
            Debug.Log("Crossed");
        }
    }
}
