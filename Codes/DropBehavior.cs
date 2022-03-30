using UnityEngine;

public class DropBehavior : MonoBehaviour
{
    public GameObject GameManager; //get gamemanager

    public float objectSpeed = 1.0f; //object drop speed
    public bool DropT = false; //determine rather drop object or not

    void Update()
    {
        if (DropT && GameManager.GetComponent<GameManager>().GameOnGoing) //if obj is allow to drop & game is on
        {
            Drop(); //drop it
        }
    }

    void Drop()
    {
        if (GameManager.GetComponent<GameManager>().NotAlive == false) //if player is alive
        {
            Vector3 move = Vector3.zero; //set movement
            move = new Vector3(0, -0.25f, 0); //get object new position
            transform.position += move * objectSpeed * Time.deltaTime; //change object position

            if (transform.position.y <= -10f) //if object is out of screen                               
            {
                Destroy(gameObject); //destroy it                    
            }
        }
        else
        {
            Destroy(gameObject); //or destory object itself
        }
    }
}
