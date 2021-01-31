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
    // Collision properties
    //--------------------------------------
    
    // determines whether the player's path is blocked
    // TODO: make this a list of BlockingItem, to keep track of what items are currently in-scope and blocking player
    public bool isBlocked = false;

    // the interactable item currently in scope for the player
    public InteractableItem interactableItem;

    //--------------------------------------
    // Animation properties
    //--------------------------------------
    public Animator animator;
    public AudioSource m_MyAudioSource;
        
    //animation states - the values in the animator conditions
    private enum AnimationState {
        Idle = 0,
        Walk = 1
    }
 
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
        //Check for keyboard input
        if ((Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) & canWalkRight())
        {
            changeDirection ("right");
            changeState(AnimationState.Walk);
            walk(new Vector2(4, 0));
        }
        else if ((Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) && canWalkLeft())
        {
            // changeDirection ("left");
            // changeState(AnimationState.Walk);

            // // walk(new Vector2(-4, 0));
            // rb.velocity = new Vector2(-4, 0);
            walkLeft(4);
        } else if (Input.GetKey(KeyCode.Space)) 
        {
            // if the player is in scope of an interactable item and presses [SPACE], interact with item
            if(Input.GetKey(KeyCode.Space)) {
                Debug.Log("FORCING DOG TO WALK LEFT");
                walkLeft(10);

                if(interactableItem != null) {
                    Debug.Log($"[Player Interaction] - Trigger interaction with {interactableItem.name}");
                    interactableItem.interact();    
                } else {
                    Debug.Log($"[Player Interaction] - No interactable items in-scope!"); 
                }
                
            }
        }
        else
        {
            changeState(AnimationState.Idle);

            //Stop the audio
            m_MyAudioSource.Stop();

            rb.velocity = new Vector2(0, 0);
        }
    }

    private bool canWalkLeft() {
        return transform.position.x > screenBounds.x + objectWidth ? true : false;
    }

    private bool canWalkRight() {
        return transform.position.x < screenBounds.y - objectWidth ? true : false;
    }

    private void walkLeft(int distance) {
        if(canWalkLeft()) {
            changeDirection ("left");
            changeState(AnimationState.Walk);

            // walk(new Vector2(-4, 0));
            rb.velocity = new Vector2(-distance, 0);
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

    void walk(Vector2 vector) {
        if(!isBlocked) {
            rb.velocity = vector;
        } else {
            Debug.Log("[Player Walk] - Cannot move, blocked by item!");
            rb.velocity = new Vector2(0, 0);
        }
    }     

    void OnTriggerEnter2D(Collider2D other) {
        switch(other.name) {
            case ItemNames.Portrait:
                Debug.Log("[Player to Portrait Item] - Collided with Portrait!");
                PortraitItem portrait = (other.GetComponent<PortraitItem>());
                if(!portrait.hasInteracted) {
                    interactableItem = portrait;    
                }
                break;
            default:
                GenericItem collideItem = GameObject.Find(other.name).GetComponent<GenericItem> ();
                if(collideItem.isBlocking) {
                    Debug.Log("[Player to Blocking Item] - Blocked by item!");
                    isBlocked = true;
                } else {
                    Debug.Log("[Player to Blocking Item] - Item state no longer blocking!");
                    isBlocked = false;
                }
                break;
        }
     }

    void OnTriggerExit2D(Collider2D other) {
        // if the current in-scope interactable item has become out-of-scope, remove reference
        GenericItem collideItem = GameObject.Find(other.name).GetComponent<GenericItem> ();
        if(collideItem.isInteractable && (interactableItem != null && interactableItem.name == other.name)) {
            Debug.Log($"[OnTriggerExit2D] - {interactableItem.name} has been removed as in-scope interactable");
            interactableItem = null;
        }

        isBlocked = false;
    }
}
