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

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = new Vector2(bg.bounds.min.x, bg.bounds.max.x);
        objectWidth = sr.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        bool moveLeft = transform.position.x > screenBounds.x + objectWidth ? true : false;
        bool moveRight = transform.position.x < screenBounds.y - objectWidth ? true : false;

        if(Input.GetKey(KeyCode.A) & moveLeft)
        {
            rb.velocity = new Vector2(-4, 0);
            sr.flipX = true;

        } else if (Input.GetKey(KeyCode.D) & moveRight)
        {
            rb.velocity = new Vector2(4, 0);
            sr.flipX = false;
        } else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
