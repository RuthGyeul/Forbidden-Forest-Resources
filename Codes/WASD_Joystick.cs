using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD_Joystick : MonoBehaviour
{
    Rigidbody2D rigid; //get rigid
    SpriteRenderer rend; //get renderer
    Vector2 moveLimit = new Vector2(7.0f, 0); //player position limit
    Vector3 playerPosition; //player position
    public float moveSpeed = 8.0f; //move speed
   
    void Update()
    {
        Movement(); //move
    }

    public void Movement()
    {
        rigid = GetComponent<Rigidbody2D>(); //get rigid
        rend = GetComponent<SpriteRenderer>(); //get renerer
        playerPosition = ClampPosition(this.transform.position); //get player position & position limit

        if (Input.GetKey(KeyCode.A)) //if input is A key
        {
            rend.flipX = true; //change character's view(?) point left or right
            playerPosition += Vector3.left * moveSpeed * Time.deltaTime; //set player position change
            rigid.MovePosition(playerPosition); //move player position
        }
        if (Input.GetKey(KeyCode.D))
        {
            rend.flipX = false;
            playerPosition += Vector3.right * moveSpeed * Time.deltaTime;
            rigid.MovePosition(playerPosition);
        }

    }
    public Vector3 ClampPosition(Vector3 position)
    {
        return new Vector3(Mathf.Clamp(position.x, -moveLimit.x, moveLimit.x), -3f, 0); //player position always set on x = moveLimit, y = -3, z = 0
    }
}