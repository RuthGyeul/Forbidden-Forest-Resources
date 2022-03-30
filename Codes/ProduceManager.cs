using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceManager : MonoBehaviour
{
    public GameObject[] Product = new GameObject[7]; //set product
    public GameObject ProductGroup; //get product group
    public GameObject GameManager; //get gamemanager
    public float ProduceTime = 3f; //set produce time
    bool produceB = true; //determine rather produce item

    void Update()
    {
        if (produceB && GameManager.GetComponent<GameManager>().GameOnGoing && GameManager.GetComponent<GameManager>().NotAlive == false)
        {
            produceB = false; //set false
            StartCoroutine("Produce"); //start produce coroutine
        }
    }

    IEnumerator Produce()
    {
        for (int i = 0; i <= 10; i++) //repeat 9 times
        {
            int ran = Random.Range(0, 7); //get random object number
            ProduceP(ran); //product produce
        }
        yield return new WaitForSeconds(ProduceTime);  //wait for produce time
        produceB = true; //set true to restart the coroutine
    }

    void ProduceP(int ran)
    {
        GameObject ProduceP = Instantiate(Product[ran]) as GameObject; //create product
        ProduceP.transform.parent = ProductGroup.transform; //get product location
        ProduceP.transform.position = new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(5.0f, 10.0f), 0); //change product x-axis & y-axis location randomly
        ProduceP.GetComponent<DropBehavior>().objectSpeed = Random.Range(10.0f, 25.0f); //set ramdom drop speed on dropbehavior script
        ProduceP.GetComponent<DropBehavior>().DropT = true; //let them drop
    }
}
