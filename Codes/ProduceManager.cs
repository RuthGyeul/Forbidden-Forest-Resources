using System.Collections;
using UnityEngine;

public class ProduceManager : MonoBehaviour
{
    public GameObject[] Product = new GameObject[7]; //set products
    public GameObject ProductGroup; //get product group
    public GameObject GameManager; //get gamemanager

    public float ProduceTime = 3f; //set produce time
    public float dropSpeedMin = 10.0f; //set minimum drop speed of object
    public float dropSpeedMax = 25.0f; //set maximum drop speed of object
    bool produceB = true; //determine rather produce item or not

    void Update()
    {
        if (produceB && GameManager.GetComponent<GameManager>().GameOnGoing && GameManager.GetComponent<GameManager>().NotAlive == false) //if game is on & player is alive & allow to produce
        {
            produceB = false; //prevent coroutine to repeat every single frame
            StartCoroutine("Produce"); //start produce coroutine
        }
    }

    IEnumerator Produce()
    {
        for (int i = 0; i <= 10; i++) //repeat 9 times
        {
            int ran = Random.Range(0, 7); //get random product to create
            ProduceP(ran); //produce product
        }
        yield return new WaitForSeconds(ProduceTime);  //wait for produce time
        produceB = true; //set true to restart the coroutine
    }

    void ProduceP(int ran)
    {
        GameObject ProduceP = Instantiate(Product[ran]) as GameObject; //create product
        ProduceP.transform.parent = ProductGroup.transform; //get product location
        ProduceP.transform.position = new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(5.0f, 10.0f), 0); //change product x-axis & y-axis location randomly
        ProduceP.GetComponent<DropBehavior>().objectSpeed = Random.Range(dropSpeedMin, dropSpeedMax); //set ramdom drop speed on dropbehavior script
        ProduceP.GetComponent<DropBehavior>().DropT = true; //let them drop
    }
}
