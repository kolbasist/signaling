using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");
        if (collision.TryGetComponent(out SpriteSetter spriteSetter))
        {
            spriteSetter.SetEmpty();
            Debug.Log("door enter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit");
        if (collision.TryGetComponent(out SpriteSetter spriteSetter))
        {
            spriteSetter.SetFinalSprite();
            Debug.Log("Door exit");
        }
    }
}
