using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool onGround;
    private bool forward;
    private bool antiGrav;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        forward = true;
        antiGrav = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Adjust velocity based on the forward variable
        float moveSpeed = forward ? 6f : -6f;
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (Input.GetMouseButtonDown(0) && onGround)
        {
           
                rb.velocity = new Vector2(rb.velocity.x, 5);
            
        }
        
        

    } 

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the tag "Wall"
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Reverse the forward variable and adjust velocity accordingly
            forward = !forward;
            rb.velocity = new Vector2(forward ? 6f : -6f, rb.velocity.y);
        }
        if (collision.gameObject.CompareTag("Spike"))
        {
            SceneManager.LoadScene("Game");
        }
        if (collision.gameObject.CompareTag("FakeWall"))
        {
            SceneManager.LoadScene("Game");
        }
        else if (collision.gameObject.CompareTag("Portal"))
        {
            SceneManager.LoadScene("Win");
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Orb"))
        {
            // Apply upward force to defy gravity
            AntiGrav();
        }
    }

    private void AntiGrav()
    {
        antiGrav = true;
        rb.gravityScale = -1f;

        if (Input.GetMouseButtonDown(0) && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, 5);
        }
    }

}