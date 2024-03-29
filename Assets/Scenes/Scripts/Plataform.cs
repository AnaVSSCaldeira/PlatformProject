using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public TargetJoint2D joint;
    public float fallingTime;

    void Falling()
    {
        boxCollider.enabled = false;
        joint.enabled = false;
        Destroy(gameObject,2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.transform.CompareTag("Player"))
        {
            Invoke("Falling",fallingTime);
        }    
    }
}