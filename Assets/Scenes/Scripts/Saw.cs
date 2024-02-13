using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public bool isVertical;

    [SerializeField] private float offsetLeft;
    [SerializeField] private float offsetRight;
    [SerializeField] private float speed;

    [SerializeField] private bool hasReachedDestiny = false;
    [SerializeField] private bool hasReachedOrigin = false;

    [SerializeField] private Vector3 startPosition = Vector3.zero;

    [SerializeField] private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Awake()
    {
        startPosition = transform.position;
    }
    void FixedUpdate()
    {
        float transformPositionDirection = isVertical ? transform.position.y : transform.position.x;
        float startPositionDirection = isVertical ? startPosition.y : startPosition.x;

        if (!hasReachedDestiny)
        {

            if (transformPositionDirection < startPositionDirection + offsetRight)
            {
                Move(offsetRight);
                anim.SetFloat("Speed", -1f);
            }
            else if (transformPositionDirection >= startPositionDirection + offsetRight)
            {
                hasReachedDestiny = true;
                hasReachedOrigin = false;
            }
        }
        else if (!hasReachedOrigin)
        {
            if (transformPositionDirection > startPositionDirection + offsetLeft)
            {
                Move(offsetLeft);
                anim.SetFloat("Speed", 1f);
            }
            else if (transformPositionDirection <= startPositionDirection + offsetLeft)
            {
                hasReachedDestiny = false;
                hasReachedOrigin = true;
            }
        }
    }

    void Move(float offset)
    {
        float x = isVertical ? transform.position.x : startPosition.x + offset;
        float y = isVertical ? startPosition.y + offset : transform.position.y;

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(x, y), speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMove>().Hit(false);
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;

        float width = GetComponent<SpriteRenderer>().size.x;
        float height = GetComponent<SpriteRenderer>().size.y;

        float offsetNegX = startPosition.x - (width / 2) + offsetLeft;
        float offsetPosX = startPosition.x + (width / 2) + offsetRight;
        float offsetBottomPoint = startPosition.y + (height / 2);
        float offsetTopPoint = startPosition.y - (height / 2);
        float offsetTransformNegX = transform.position.x - (width / 2) + offsetLeft;
        float offsetTransformPosX = transform.position.x + (width / 2) + offsetRight;
        float offsetTransformTopPoint = transform.position.y + (height / 2);
        float offsetTransformBottomPoint = transform.position.y - (height / 2);

        Gizmos.DrawLine(new Vector3(offsetNegX, offsetTopPoint, 0),
                        new Vector3(offsetNegX, offsetBottomPoint, 0));

        Gizmos.color = Color.green;

        Gizmos.DrawLine(new Vector3(offsetPosX, offsetTopPoint, 0),
                        new Vector3(offsetPosX, offsetBottomPoint, 0));

        Gizmos.color = Color.blue;

        Gizmos.DrawLine(new Vector3(offsetTransformNegX, offsetTransformBottomPoint, 0),
                        new Vector3(offsetTransformNegX, offsetTransformTopPoint, 0));
        Gizmos.DrawLine(new Vector3(offsetTransformPosX, offsetTransformBottomPoint, 0),
                        new Vector3(offsetTransformPosX, offsetTransformTopPoint, 0));
    }
}
