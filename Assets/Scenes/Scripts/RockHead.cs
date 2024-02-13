using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHead : MonoBehaviour
{
    public static RockHead instance;
    [SerializeField] private Animator rockHeadAnimator;
    [SerializeField] private GameObject player;
    [SerializeField] private bool hitGround;
    [SerializeField] private bool fall;
    [SerializeField] private float impulseForce;

    [SerializeField] private bool canShake = false;
    public float speed = 1.0f;
    public float amount = 1.0f;
    private Quaternion initialRotation;

    [SerializeField] private float height;
    [SerializeField] private Transform currentPosition;

    private void Start()
    {
        hitGround = false;
        fall = false;

        rockHeadAnimator = GetComponent<Animator>();
        rockHeadAnimator.Play("rockHeadIdle");

        instance = this;

        player = GameObject.FindGameObjectWithTag("Player");

        currentPosition = GetComponent<Transform>();
        currentPosition.position = new Vector3(this.transform.position.x, height, this.transform.position.z);
    }

    void Update()
    {
        if (canShake)
        {
            Shake();
        }

    }

    private void FixedUpdate()
    {
        if (fall)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector3.down * impulseForce, ForceMode2D.Impulse);
        }
        if (hitGround)
        {
            if(currentPosition.position.y >= height)
            {
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                GetComponent<Rigidbody2D>().angularVelocity = 0f;
                hitGround = false;
                RockHeadChild.rockHeadChild.fallRock = true;
            }
            else
            {
                GetComponent<Rigidbody2D>().AddForce(Vector3.up * impulseForce, ForceMode2D.Impulse);
            }
        }
    }

    public void Shake()
    {
        float shakeAmount = Mathf.Sin(Time.time * speed) * amount;
        Quaternion newRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z + shakeAmount);
        transform.rotation = newRotation;
    }
    public void RockHit()
    {
        StartCoroutine(Fall());
    }

    IEnumerator Fall()
    {
        rockHeadAnimator.Play("rockHeadBlink");
        canShake = true;
        yield return new WaitForSeconds(0.4f);
        canShake = false;
        transform.rotation = initialRotation;
        yield return new WaitForSeconds(0.2f);
        rockHeadAnimator.enabled = false;
        fall = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMove.playerInstance.Hit(true);
        }

        if(collision.gameObject.layer == 6)
        {
            fall = false;
            StartCoroutine(Wake());
        }
    }

    IEnumerator Wake()
    {
        yield return new WaitForSeconds(1f);
        rockHeadAnimator.enabled = true;
        rockHeadAnimator.Play("rockHeadIdle");
        hitGround = true;
    }
}
