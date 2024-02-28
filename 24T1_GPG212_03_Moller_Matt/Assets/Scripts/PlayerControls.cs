using System.Collections;
using System.Collections.Generic;
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

    public AudioSource jumpAudioSource;
    public AudioSource wallAudioSource;
    public AudioSource orbAudioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        forward = true;

        // Assuming you have two separate AudioSources attached to the same GameObject
        jumpAudioSource = GetComponents<AudioSource>()[0];
        wallAudioSource = GetComponents<AudioSource>()[1];
        orbAudioSource = GetComponents<AudioSource>()[2];
    }

    void Update()
    {
        float moveSpeed = forward ? 6f : -6f;
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (Input.GetMouseButtonDown(0) && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, 5);
            jumpAudioSource.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            forward = !forward;
            rb.velocity = new Vector2(forward ? 6f : -6f, rb.velocity.y);
            wallAudioSource.Play();
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
            orbAudioSource.Play();
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