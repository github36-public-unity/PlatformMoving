using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    [SerializeField] private Transform pointToMove1;
    [SerializeField] private Transform pointToMove2;
    [SerializeField] private float timeToMove;

    private Vector2 Position1;
    private Vector2 Position2;
    private Rigidbody2D rb;
    private Coroutine moveCoroutine;
    private bool moveDirectionflag = true;
    GameObject player;


    void OnEnable()
    {

        rb = GetComponent<Rigidbody2D>();
        if (pointToMove1 == null || pointToMove2 == null)
        {
            Debug.LogError("��������� �� ������� EndlessMoveFromPointToPoint � ������� " + gameObject.name + ". ������. �� ������� ����� ��� �����������.");
        }

        Position1 = pointToMove1.position;
        Position2 = pointToMove2.position;

        // �������� ����������� �����������
        StartMoving();
    }

    void StartMoving()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = StartCoroutine(MoveBetweenPoints(Position1, Position2, timeToMove));
    }



    IEnumerator MoveBetweenPoints(Vector2 Position1, Vector2 Position2, float timeToMove)
    {
        float elapsedTime = 0f;

        while (elapsedTime < timeToMove)
        {

            if (moveDirectionflag == true)
            {

                rb.MovePosition(Vector2.Lerp(Position1, Position2, elapsedTime / timeToMove));


            }
            if (moveDirectionflag == false) rb.MovePosition(Vector2.Lerp(Position2, Position1, elapsedTime / timeToMove));



            elapsedTime += Time.deltaTime;
            yield return null;
        }

        moveDirectionflag = !moveDirectionflag;

        // ���� ������� ������� ����� ��������� ������
        yield return new WaitForSeconds(0.5f);

        // �������� ��������� ���� ������������ �����������
        StartMoving();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("OnCollisionEnter2D");
            player = collision.gameObject;
            player.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.parent = null;
            player = null;
        }
    }







}
