using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTimer : MonoBehaviour
{
    public float timer;
    public GameObject[] platforms;
    private float clock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        clock = clock + Time.deltaTime * Time.timeScale;
        if (clock >= timer)
        {
            clock = 0;
            foreach (GameObject p in platforms)
            {
                p.GetComponent<MovingPlatform>().Action();
            }
        }
    }
}
