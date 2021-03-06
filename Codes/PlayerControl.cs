using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rigid; //get rigid
    SpriteRenderer rend; //get renderer
    Vector2 moveLimit = new Vector2(7.0f, 0); //player position limit
    Vector3 playerPosition; //player position

    public GameObject GameManager; //get gamemanager
    public float moveSpeed = 8.0f; //move speed

    GameManager GM;

    void Update()
    {
        GM = GameManager.GetComponent<GameManager>(); //get gamemanager component
        ActiveKey(); //allow to use keys
    }

    public void ActiveKey()
    {
        rigid = GetComponent<Rigidbody2D>(); //get rigid
        rend = GetComponent<SpriteRenderer>(); //get renderer
        playerPosition = ClampPosition(this.transform.position); //get player position & position limit

        if (GM.GameOnGoing) //if game is not paused let the key move
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                rend.flipX = true; //flip character by x-axis
                playerPosition += Vector3.left * moveSpeed * Time.deltaTime; //set new position
                rigid.MovePosition(playerPosition); //move
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rend.flipX = false;
                playerPosition += Vector3.right * moveSpeed * Time.deltaTime;
                rigid.MovePosition(playerPosition);
            }
        }
    }

    public Vector3 ClampPosition(Vector3 position) //set position limit
    {
        return new Vector3(Mathf.Clamp(position.x, -moveLimit.x, moveLimit.x), -3f, 0); //player position always set on x = moveLimit, y = -3, z = 0
    }
}
