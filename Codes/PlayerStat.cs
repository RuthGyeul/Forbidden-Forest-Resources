using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public GameObject GameManager; //get gamemanager
    public GameObject FloorControl; //get floor contorl

    public float PointC = 0; //point gain count
    public float HealC = 0; //heal gain count
    public float DamageC = 0; //damage gain count

    GameManager GM;

    void Start()
    {
        GM = GameManager.GetComponent<GameManager>(); //get gamemanager component
        PointC = 0; //reset to 0
        HealC = 0;
        DamageC = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision) //if trigger with an object
    {
        if (collision.CompareTag("Damage")) //if object tag is damage
        {
            GM.DamageB = true; //set gamemanager's DamageB boolen to true
            DamageC++; //count the triggered object
            Destroy(collision.gameObject); //destory the triggered object
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
        if (collision.CompareTag("Lava"))
        {
            GM.FloorExcutionB = true;
            FloorControl.GetComponent<FloorControl>().ImOnLava = true;
        }
    }
}
