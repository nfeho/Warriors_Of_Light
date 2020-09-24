using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public List<bool> playersReady;
    public bool everyoneReady = false;

    private void Update()
    {
        everyoneReady = true;

        for (int i = 0; i < playersReady.Count; i++)
        {
            if (!playersReady[i])
            {
                everyoneReady = false;
            }
        }
    }
}
