using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAOPlayerController : MonoBehaviour
{
public float walkSpeed = 1; // player left right walk speed
 
    Animator animator;


    //animation states - the values in the animator conditions
    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
 
    string _currentDirection = "right";
    int _currentAnimationState = STATE_IDLE;
 
    // Use this for initialization
    void Start()
    {
        //define the animator attached to the player
        animator = this.GetComponent<Animator>();
    }
 
    // FixedUpdate is used insead of Update to better handle the physics based jump
    void FixedUpdate()
    {
        //Check for keyboard input

if (Input.GetKey (KeyCode.D))
        {
            changeDirection ("right");
            transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
            changeState(STATE_WALK);
 
        }
        else if (Input.GetKey (KeyCode.A))
        {
            changeDirection ("left");
            transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
            changeState(STATE_WALK);
 
        }
        else
        {
            changeState(STATE_IDLE);
        }
 
    }
 
    //--------------------------------------
    // Change the players animation state
    //--------------------------------------
    void changeState(int state){
 
        if (_currentAnimationState == state)
        return;
 
        switch (state) {
 
        case STATE_WALK:
            animator.SetInteger ("state", STATE_WALK);
            break;

        case STATE_IDLE:
            animator.SetInteger ("state", STATE_IDLE);
            break;
 
        }
 
        _currentAnimationState = state;
    }
 
     //--------------------------------------
     // Flip player sprite for left/right walking
     //--------------------------------------
     void changeDirection(string direction)
     {
 
         if (_currentDirection != direction)
         {
             if (direction == "left")
             {
             transform.Rotate (0, 180, 0);
             _currentDirection = "left";
             }
             else if (direction == "right")
             {
             transform.Rotate (0, -180, 0);
             _currentDirection = "right";
             }
         }
 
     }
}
