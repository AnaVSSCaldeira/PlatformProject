using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHeadChild : MonoBehaviour
{
    public bool fallRock;
    public static RockHeadChild rockHeadChild;
    void Start()
    {
        fallRock = true;

        rockHeadChild = this;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && fallRock)
        {
            print("esta aqui");
            fallRock = false;
            print(fallRock);
            RockHead.instance.RockHit();

        }
    }
}
