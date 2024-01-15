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
            Debug.LogError("Сообщение из скрипта EndlessMoveFromPointToPoint у объекта " + gameObject.name + ". Ошибка. Не указана точка для перемещения.");
        }

        Position1 = pointToMove1.position;
        Position2 = pointToMove2.position;

        // Начинаем бесконечное перемещение
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

        // Даем немного времени перед следующим циклом
        yield return new WaitForSeconds(0.5f);

        // Начинаем следующий цикл бесконечного перемещения
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
