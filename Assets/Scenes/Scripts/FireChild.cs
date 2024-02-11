using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireChild : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMove>().FireHit();
        }
    }
}
