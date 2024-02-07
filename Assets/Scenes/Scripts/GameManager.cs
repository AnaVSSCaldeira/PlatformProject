using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform point;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerMove>().transform.position = point.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
