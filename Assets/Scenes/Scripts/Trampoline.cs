using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceForce;
    public Animator trampolineAnimator;
    public bool isPlayerOnTrampoline = false;

    void Start()
    {
        trampolineAnimator = GetComponent<Animator>();

        trampolineAnimator.Play("trampolineIdle");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayerOnTrampoline)
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                PlayerMove.playerInstance.canJump = false;
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * bounceForce);
                trampolineAnimator.Play("trampoline");
                isPlayerOnTrampoline = true;

                StartCoroutine(ReturnToIdleAfterJumpAnimation());
            }
        }
    }

    IEnumerator ReturnToIdleAfterJumpAnimation()
    {
        yield return new WaitForSeconds(0.5f);

        trampolineAnimator.Play("trampolineIdle");
        isPlayerOnTrampoline = false;
    }
}