using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private bool Alive = true;
    private Rigidbody2D rb2D;
    private SpriteRenderer _renderer;
    private float moveHorizontal;
    private float moveVertical;
    private float moveSpeed;
    private float jumpForce;
    private float jumperForce;
    private bool isGrounded;
    public ProjectileBehavior ProjectilePrefab;
    public Transform LaunchOffset;

    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = 2f;
        jumpForce = 25f;
        jumperForce = 60f;
        isGrounded = false;
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        // Flip
        if (moveHorizontal > 0.1f)
        {
            _renderer.flipX = false;
        }
        if (moveHorizontal < -0.1f)
        {
            _renderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_renderer.flipX == true) {
                ProjectilePrefab.Direction = -1;
                Instantiate(ProjectilePrefab, new Vector3(LaunchOffset.position.x - 3, LaunchOffset.position.y, LaunchOffset.position.z), transform.rotation);
            } else {
                ProjectilePrefab.Direction = 1;
                Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
            }
            
        }

        if (!Alive) {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        // Run
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }

        // Jump
        if (isGrounded && moveVertical > 0.1f)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Jumper")
        {
            isGrounded = false;
            rb2D.AddForce(new Vector2(0f, jumperForce), ForceMode2D.Impulse);
        }
        else if (collision.gameObject.tag == "Platform")
        {
            isGrounded = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isGrounded = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Substring(0, 5) == "Lazer")
        {
            if (collision.gameObject.tag != "Lazer_Blue") {
                Alive = false;
            }
        }
    }
}
