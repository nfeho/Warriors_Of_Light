using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFell : MonoBehaviour
{
    public bool gameOver = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameOver = true;
    }
}
