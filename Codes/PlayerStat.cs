using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    Rigidbody2D rigid; //get rigid
    public GameObject Player; //get player
    public GameObject GameManager; //get gamemanager

    float PointC = 0;
    float HealC = 0;
    float DamageC = 0;

    GameManager GM;

    void Start()
    {
        rigid = Player.GetComponent<Rigidbody2D>(); //get rigidbody2d
        GM = GameManager.GetComponent<GameManager>(); //get gamemanager component
    }

    private void OnTriggerEnter2D(Collider2D collision) //if trigger
    {
        if (collision.CompareTag("Damage")) //if object tag is damage
        {
            GM.DamageB = true; //set gamemanager's DamageB boolen to true
            DamageC++; //count how many damage got triggered
            Destroy(collision.gameObject); //Destory the triggered object
        }
        if (collision.CompareTag("Heal"))
        {
            GM.HealB = true;
            HealC++;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Point"))
        {
            GM.PointB = true;
            PointC++;
            Destroy(collision.gameObject);
        }
    }
}
