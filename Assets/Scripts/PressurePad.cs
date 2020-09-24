using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    public Sprite onSprite;
    public Sprite offSprite;
    public GameObject player;
    public GameObject[] targets;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject == player)
        //{
            //target.gameObject.SetActive(false);
            foreach (GameObject t in targets)
            {
                if (t.tag == "Moving")
                    t.gameObject.GetComponent<MovingPlatform>().Action();
                else
                    t.gameObject.GetComponent<Area>().Action();
            }

            gameObject.GetComponent<SpriteRenderer>().sprite = onSprite;
        //}
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //if (other.gameObject == player)
        //{
            //target.gameObject.SetActive(true);
            foreach (GameObject t in targets)
            {
                if (t.tag == "Moving")
                    t.gameObject.GetComponent<MovingPlatform>().Action();
                else
                    t.gameObject.GetComponent<Area>().Action();
            }

            gameObject.GetComponent<SpriteRenderer>().sprite = offSprite;
        //}
    }
}
