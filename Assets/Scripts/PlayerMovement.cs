using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    public bool blockLeft = false;
    public bool blockRight = false;

    string playerName;

    private void Start()
    {
        playerName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw(playerName + "Horizontal") * runSpeed;

        // Block moving to the side we touch obstacle with
        if ((horizontalMove > 0 && blockRight) || (horizontalMove < 0 && blockLeft))
            horizontalMove = 0;

        if (Input.GetButtonDown(playerName + "Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown(playerName + "Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp(playerName + "Crouch"))
        {
            crouch = false;
        }

    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 vecToCollision = new Vector2(other.GetContact(0).point.x, other.GetContact(0).point.y) - playerPos;

        // Block the side we collide with
        if (other.gameObject.name == "Platforms" || other.gameObject.tag == "Moving")
        {
            if (vecToCollision.x < -0.3)
            {
                blockLeft = true;
            }
            else if (vecToCollision.x > 0.3)
            {
                blockRight = true;
            }
        }

        
        if (other.gameObject.tag == "Moving")
        {
            transform.parent = other.transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // Unblock turning
        blockRight = false;
        blockLeft = false;

        if (other.gameObject.tag == "Moving")
        {
            transform.parent = null;
        }
    }
}