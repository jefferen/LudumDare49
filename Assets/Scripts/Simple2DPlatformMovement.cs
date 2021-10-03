using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple2DPlatformMovement : MonoBehaviour
{
    float h, v;
    [Range(1, 100)]
    public float moveSpeed;
    public float jumpForce, jumpHeight;
    public float minG, maxG;
    bool onGround, dashMove;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if ((Input.GetKeyDown(KeyCode.Space) || v == 1 || Input.GetKeyDown(KeyCode.Joystick1Button0)) && onGround) 
        {
            rb.velocity = new Vector2(0, Mathf.Sqrt(-2.0f * Physics2D.gravity.y * minG * jumpHeight)); // for some reason depending on which button you press, v input gives stronger jump than space and joystick, how the hell?!?
        }

        if(!onGround && rb.velocity.y < -0.05f)
        {
            rb.gravityScale = maxG;
        }
    }

    void FixedUpdate()
    {
        if (!dashMove)
        {
            rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);
            rb.velocity += new Vector2(rb.velocity.x, 0);
        }
        else
            rb.AddRelativeForce(new Vector2(h * moveSpeed * 5, 0));
    }

    public void Dashing()
    {
        rb.gravityScale = minG; 
        dashMove = true;
        onGround = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            dashMove = false;
            rb.gravityScale = minG;
        }
    }
    void OnCollisionExit2D(Collision2D other) // if falling from a platform
    {
        if (other.gameObject.CompareTag("Ground")) 
        {
            onGround = false;
        }
    }
}
