using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rigid; //get rigid
    SpriteRenderer rend; //get renderer
    Vector2 moveLimit = new Vector2(7.0f, 0); //player position limit
    Vector3 playerPosition; //player position
    public GameObject GameManager; //get gamemanager
    public GameObject Option;
    public float moveSpeed = 8.0f; //move speed
    GameManager GM;

    void Update()
    {
        GM = GameManager.GetComponent<GameManager>(); //get gamemanager component

        if (GM.GameOnGoing) //if game is not paused let the key move
        {
            ActiveKey(); //move
        }

        if (Input.GetKeyDown(KeyCode.P)) //pause game
        {
            GM.GameOnGoing = false;
            Option.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.T)) //resume game
        {
            Option.SetActive(false);
            GameStartCount();
        }
    }

    public void ActiveKey()
    {
        rigid = GetComponent<Rigidbody2D>(); //get rigid
        rend = GetComponent<SpriteRenderer>(); //get renerer
        playerPosition = ClampPosition(this.transform.position); //get player position & position limit

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rend.flipX = true;
            playerPosition += Vector3.left * moveSpeed * Time.deltaTime;
            rigid.MovePosition(playerPosition);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
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

    public void GameStartCount()
    {
        //add count down 3
        GM.GameOnGoing = true;
    }
}
