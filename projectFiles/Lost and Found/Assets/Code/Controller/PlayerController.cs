using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Rigidbody2D rb;
    public SpriteRenderer sr;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-4, 0);
            sr.flipX = true;

        } else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(4, 0);
            sr.flipX = false;
        } else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
