using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Player"))
        {
            // other.GetComponent<PlayerMove>().strawberry += 1;
            other.GetComponent<PlayerMove>().IncreaseScore();
            Destroy(gameObject);
        }    
    }
}