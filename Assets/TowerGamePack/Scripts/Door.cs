using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private UnityEvent _lineCrossedIn;
    [SerializeField] private UnityEvent _lineCrossedOut;
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.TryGetComponent(out SpriteSetter spriteSetter))
        {
            _lineCrossedIn.Invoke();
            spriteSetter.SetEmpty();             
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {       
        if (collision.TryGetComponent(out SpriteSetter spriteSetter))
        {
            spriteSetter.SetFinalSprite();            
        }
        _lineCrossedOut.Invoke();
    }
}
