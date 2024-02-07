using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPoint : MonoBehaviour
{
    public int currentStrawberry;
    public static FinalPoint finalPointInstance;
    void Start()
    {
        finalPointInstance = this;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().buildIndex < 10)
            {
                currentStrawberry = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }    
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}