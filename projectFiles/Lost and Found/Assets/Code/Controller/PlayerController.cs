using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public BoxCollider2D bg;
    private Vector2 screenBounds;
    private float objectWidth;

    //--------------------------------------
    // Animation properties
    //--------------------------------------
    public Animator animator;
    public AudioSource m_MyAudioSource;
    public float walkSpeed = 1; // player left right walk speed
        
    //animation states - the values in the animator conditions
    private enum AnimationState {
        Idle = 0,
        Walk = 1
    }
    // const int STATE_IDLE = 0;
    // const int STATE_WALK = 1;
 
    string _currentDirection = "right";
    AnimationState _currentAnimationState = AnimationState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = new Vector2(bg.bounds.min.x, bg.bounds.max.x);
        objectWidth = sr.bounds.size.x;
        
        //define the animator attached to the player
        animator = this.GetComponent<Animator>();

        //Fetch the AudioSource from the GameObject
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    // FixedUpdate is used insead of Update to better handle the physics based jump
    void FixedUpdate()
    {
        bool moveLeft = transform.position.x > screenBounds.x + objectWidth ? true : false;
        bool moveRight = transform.position.x < screenBounds.y - objectWidth ? true : false;

        //Check for keyboard input
        if (Input.GetKey (KeyCode.D) & moveRight)
        {
            changeDirection ("right");
            changeState(AnimationState.Walk);

            rb.velocity = new Vector2(4, 0);
        }
        else if (Input.GetKey (KeyCode.A) & moveLeft)
        {
            changeDirection ("left");
            changeState(AnimationState.Walk);

            rb.velocity = new Vector2(-4, 0);
        }
        else
        {
            changeState(AnimationState.Idle);

            //Stop the audio
            m_MyAudioSource.Stop();

            rb.velocity = new Vector2(0, 0);
        }
    }

        //--------------------------------------
    // Change the players animation state
    //--------------------------------------
    void changeState(AnimationState state){
 
        if (_currentAnimationState == state)
        return;
 
        switch (state) {
            case AnimationState.Walk:

                //Play the audio you attach to the AudioSource component
                m_MyAudioSource.Play();
                animator.SetInteger ("state", (int) AnimationState.Walk);
                break;

            case AnimationState.Idle:
                animator.SetInteger ("state", (int) AnimationState.Idle);
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
                sr.flipX = true;
                _currentDirection = "left";
             }
             else if (direction == "right")
             {
                sr.flipX = false;
                _currentDirection = "right";
             }
         }
     }
}
