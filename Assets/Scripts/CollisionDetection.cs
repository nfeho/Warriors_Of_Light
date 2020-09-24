using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetection : MonoBehaviour
{
    public GameObject teammate;

    BoxCollider2D myCollider;
    string teammateName;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = gameObject.GetComponent<BoxCollider2D>();
        teammateName = teammate.name;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == teammateName)
        {
            Debug.Log(gameObject.name + " player entered " + teammateName + " area");
            gameObject.SetActive(false);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log(gameObject.name + " player collided with " + other.gameObject.name + " player");
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == teammateName)
        {
            Debug.Log(gameObject.name + " player entered " + teammateName + " area");
            gameObject.SetActive(false);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log(gameObject.name + " player collided with " + other.gameObject.name + " player");
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
