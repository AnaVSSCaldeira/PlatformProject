using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private Animator fireAnimator;
    [SerializeField] private bool canButtonOff;
    public Collider2D colliderChildren;

    void Start()
    {
        fireAnimator = GetComponent<Animator>();
        fireAnimator.Play("fireButtonOff");
        canButtonOff = true;
        colliderChildren = this.gameObject.transform.GetChild(0).GetComponent<Collider2D>();
        colliderChildren.enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canButtonOff)
        {
            fireAnimator.Play("fireButtonOn");
            StartCoroutine(ReturnToIdleAfterJumpAnimation());
        }
    }

    IEnumerator ReturnToIdleAfterJumpAnimation()
    {
        canButtonOff = false;

        yield return new WaitForSeconds(0.3f);

        fireAnimator.Play("fire");
        colliderChildren.enabled = true;

        yield return new WaitForSeconds(5f);
        colliderChildren.enabled = false;

        fireAnimator.Play("fireButtonOff");

        canButtonOff = true;
    }
}
