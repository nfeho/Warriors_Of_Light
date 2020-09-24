using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public Vector2[] waypoints;

    private float speedX;
    private float speedY;
    private Vector2 offsets;
    private Vector2 next;
    private int currentWaypoint;
    private float timer;

    private bool wasTriggered = false;
    public bool triggerOnlyOnce;
    public bool isOn;
    public bool deactivateOnWaypointReach;

    // Start is called before the first frame update
    void Start() { 
    
        currentWaypoint = -1;
        SwitchWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn && Time.timeScale > 0) {

            timer = timer - Time.deltaTime;
            if (timer < 0)
            {
                if (deactivateOnWaypointReach)
                    isOn = false;
                SwitchWaypoint();
            }

            var offsetX = speedX * Time.deltaTime;
            var offsetY = Time.deltaTime * speedY;

            transform.SetPositionAndRotation(new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z), this.transform.rotation);
        }

    }
    
    void SwitchWaypoint()
    {
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        next = waypoints[currentWaypoint];
        offsets = new Vector2(next.x - transform.position.x, next.y - transform.position.y);
        timer = Mathf.Max(Mathf.Abs(offsets.x), Mathf.Abs(offsets.y)) / speed;
        speedX = speed * (offsets.x / Mathf.Max(Mathf.Abs(offsets.x), Mathf.Abs(offsets.y)));
        speedY = speed * (offsets.y / Mathf.Max(Mathf.Abs(offsets.x), Mathf.Abs(offsets.y)));
    }

    public void Action()
    {
        if (!wasTriggered)
            isOn = !isOn;
        if (triggerOnlyOnce)
            wasTriggered = true;
    }
}
