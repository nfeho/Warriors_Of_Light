using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDoor : MonoBehaviour
{
    public GameObject player;

    private int id;
    private Finish parentFinish;

    // Start is called before the first frame update
    void Start()
    {
        parentFinish = gameObject.GetComponentInParent<Finish>();
        id = parentFinish.playersReady.Count;
        parentFinish.playersReady.Add(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            parentFinish.playersReady[id] = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            parentFinish.playersReady[id] = false;
        }
    }
}
