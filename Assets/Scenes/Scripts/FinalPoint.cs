using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPoint : MonoBehaviour
{
    public int currentStrawberry;
    public static FinalPoint finalPointInstance;
    private PlayerMove playerMove;
    void Start()
    {
        finalPointInstance = this;
        playerMove = PlayerMove.playerInstance;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().buildIndex < 5)
            {
                currentStrawberry = 0;
                playerMove.stopCoroutine = playerMove.stopCoroutine == false ? playerMove.stopCoroutine = true : playerMove.stopCoroutine;
                playerMove.level += 1;
                playerMove.levelNumberText.text = (playerMove.level).ToString();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                
            }
        }    
    }

    void Update()
    {
        //if (Input.GetKeyDown("e"))
        //{
        //    if (SceneManager.GetActiveScene().buildIndex < 5)
        //    {
        //        playerMove.level += 1;
        //        playerMove.levelNumberText.text = (playerMove.level).ToString();
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //    }
        //}
        //if (Input.GetKeyDown("q"))
        //{
        //    if (SceneManager.GetActiveScene().buildIndex > 1)
        //    {
        //        playerMove.level -= 1;
        //        playerMove.levelNumberText.text = (playerMove.level).ToString();
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + -1);
        //    }
        //}
    }
}