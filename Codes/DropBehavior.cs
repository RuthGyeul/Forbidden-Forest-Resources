using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBehavior : MonoBehaviour
{
    public GameObject GameManager; //get gamemanager
    public float objectSpeed = 1.0f; //object drop speed
    public bool DropT = false; //determine rather drop obj or not

    void Update()
    {
        if (DropT && GameManager.GetComponent<GameManager>().GameOnGoing) //if obj is allow to drop
        {
            Drop(); //drop it
        }
    }

    void Drop()
    {
        if (GameManager.GetComponent<GameManager>().SCStat) //if game is ongoing
        {
            Vector3 moveVelocity = Vector3.zero;
            moveVelocity = new Vector3(0, -0.25f, 0); //get object position
            transform.position += moveVelocity * objectSpeed * Time.deltaTime; //change object position

            if (transform.position.y <= -10f) //if object is out of screen                               
            {
                Destroy(gameObject); //destroy it                    
            }
        } else {
            Destroy(gameObject); //or destory all
        }
    }
}
