using System.Collections;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private Transform positionA, positionB;
    private Vector3 targetPosition;
    [SerializeField] private float platformSpeed;
    private Vector2 platformMoveDirection;


    private Rigidbody2D playerRigidbody2D, platformRigidbody2D;


    private GameObject player;
    private PlayerController playerController;





    private void Awake()
    {
        platformRigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();
    }


    private void Start()
    {
        targetPosition = positionB.position;
        DirectionCalculate();
    }

    private void FixedUpdate()
    {
        platformRigidbody2D.velocity = platformMoveDirection * platformSpeed;

        if (Vector2.Distance(transform.position, positionA.position) < 0.05f)
        {
            targetPosition = positionB.position;
            DirectionCalculate();
        }

       else if (Vector2.Distance(transform.position, positionB.position) < 0.05f)
        {
            targetPosition = positionA.position;
            DirectionCalculate();
        }
    }

    private void DirectionCalculate()
    {
        platformMoveDirection = (targetPosition - transform.position).normalized;
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // player.transform.parent = transform; 
         //  player.transform.SetParent(transform);
             playerController.PlayerIsOnHorizontalPlatform = true;
            playerController.PlatformRigidbody2D = platformRigidbody2D;
           // playerRigidbody2D.gravityScale *= 50;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // player.transform.parent = null;
          // player.transform.SetParent(null);
            playerController.PlayerIsOnHorizontalPlatform = false;
           // playerRigidbody2D.gravityScale /= 50;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(positionA.position, positionB.position);
    }


}
