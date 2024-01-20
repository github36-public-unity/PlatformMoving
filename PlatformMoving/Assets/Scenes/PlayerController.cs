using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerRunSpeed = 5f;
    public float jumpForce = 0.5f;

    private Rigidbody2D playerRigidbody2D;
	
    private Rigidbody2D platformRigidbody2D;
    public Rigidbody2D PlatformRigidbody2D { get { return platformRigidbody2D; } set { platformRigidbody2D = value; } }

    private bool playerIsOnHorizontalPlatform = false;
    public bool PlayerIsOnHorizontalPlatform { get { return playerIsOnHorizontalPlatform; } set { playerIsOnHorizontalPlatform = value; } }


    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }



    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
   
              if (playerIsOnHorizontalPlatform == true)
                    {
                        playerRigidbody2D.velocity = new Vector2((horizontalInput * playerRunSpeed) + platformRigidbody2D.velocity.x, playerRigidbody2D.velocity.y);
                    }
                    else
                    {
                        playerRigidbody2D.velocity = new Vector2(horizontalInput * playerRunSpeed, playerRigidbody2D.velocity.y);
                    }

        if (Input.GetKey(KeyCode.Space))
        {
           // playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, jumpForce);
            playerRigidbody2D.AddForce(new Vector2(0, jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
        }

    }
}
