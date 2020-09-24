using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Sprite onSprite;
    public Sprite offSprite;
    public bool on = false;
    public GameObject[] targets;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Light" || other.name == "Dark")
        {
            foreach (GameObject target in targets)
            {
                //target.gameObject.SetActive(!target.activeSelf);

                if (target.tag == "Moving")
                    target.gameObject.GetComponent<MovingPlatform>().Action();
                else
                    target.gameObject.GetComponent<Area>().Action();
            }

            if (on)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = offSprite;
                on = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = onSprite;
                on = true;
            }
        }

    }
}
