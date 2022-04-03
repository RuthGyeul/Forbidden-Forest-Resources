using UnityEngine;

public class FloorControl : MonoBehaviour
{
    public GameObject GameManager; //get gamemanager

    public GameObject[] GrassObj = new GameObject[5]; //get grass obj
    public GameObject[] BurnGrassObj = new GameObject[5]; //get burned grass obj
    public GameObject[] LavaObj = new GameObject[5]; //get lava obj

    public GameObject Grass; //get all grass obj
    public GameObject BurnGrass; //get all burned grass obj
    public GameObject Lava; //get all lava obj

    public bool CreateSinkhole = false; //determine rather create hole or not
    public bool ImOnLava = false; //determine rather player is on lave or not

    public int oldN = 0; //which object to change to grass
    public int newN = 0; //which object to change to lava
    public float time = 0; //time count
    public float btime = 0.1f; //blinking time
    public float xtime = 0; //time restriction
    public float waittime = 0.2f; //wait time

    GameManager GM;

    void Start()
    {
        GM = GameManager.GetComponent<GameManager>(); //get gmaemanager
        BurnGrass.SetActive(true); //turn on burned grass obj
        Grass.SetActive(true); //turn on grass obj
        LavaObj[0].SetActive(false); //turn off lava obj
        LavaObj[1].SetActive(false);
        LavaObj[2].SetActive(false);
        LavaObj[3].SetActive(false);
        LavaObj[4].SetActive(false);
    }

    void Update()
    {
        if (GM.GameOnGoing && GM.NotAlive == false) //when game is ongoing and player is alive
        {
            if (ImOnLava) //if player is on lave
            {
                ImOnLava = false; //turn off bool
                BurnGrass.SetActive(true); //reset burn grass
                Grass.SetActive(true); //reset grass
            }

            if (CreateSinkhole) //if creating sinkhole is allowed
            {
                CreateSinkhole = false; //turn off to prevent infinite creation of sinkhole
                DigSinkhole(); //process to creat sinkhole
            }
        }
    }

    void DigSinkhole()
    {
        if (time < 3f) //cool time set to 3s
        {
            newN = Random.Range(0, 5); //select random obj to create lava
        }
        else //if 3s passed
        {
            BurnGrassObj[oldN].SetActive(true); //turn on old burn grass
            LavaObj[oldN].SetActive(false); //turn off old lava obj
            GrassObj[oldN].SetActive(true); //turn on old grass obj
            oldN = newN; //change old number to new number
            if (xtime < btime) //starts to blink
            {
                GrassObj[newN].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - xtime * 10); //make grass darker
            }
            else
            {
                GrassObj[newN].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (xtime - (waittime + btime)) * 10); //make grass brighter
                if (xtime > waittime + btime * 2) //if wait time is over
                {
                    xtime = 0; //reset time restriction
                    waittime *= 0.8f; //reduce blinking time
                    if (waittime < 0.02f) //if done with bliking
                    {
                        time = 0; //reset time
                        waittime = 0.2f; //reset wait time
                        LavaObj[newN].SetActive(true); //turn on lave
                        GrassObj[newN].SetActive(false); //turn off grass
                        BurnGrassObj[newN].SetActive(false); //turn off burned grass
                        CreateSinkhole = true; //start to process new hole
                    }
                }
            }
            xtime += Time.deltaTime; //keep time up
        }
        time += Time.deltaTime;
    }
}
