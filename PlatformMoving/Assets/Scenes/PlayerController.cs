using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 0.5f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Transform groundCheck;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        //rb.velocity = movement;



        //// Прыжок
        //if (Input.GetKey(KeyCode.Space))

        //{
        //    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //}

        rb.AddForce(new Vector2(horizontalInput * moveSpeed * Time.fixedDeltaTime, 0), ForceMode2D.Impulse);

        if (Input.GetKey(KeyCode.Space))
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.AddForce(new Vector2(0, jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
        }

    }
}
