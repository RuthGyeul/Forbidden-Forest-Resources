using UnityEngine;

public class FloorControl : MonoBehaviour
{
    public GameObject GameManager;

    public GameObject[] GrassObj = new GameObject[5];
    public GameObject[] BurnGrassObj = new GameObject[5];
    public GameObject[] LavaObj = new GameObject[5];

    public GameObject Grass;

    public GameObject BurnGrass;

    public GameObject Lava;

    public bool CreateSinkhole = false;
    public bool ImOnLava = false;

    public int oldN = 0;
    public int newN = 0;
    public float time = 0;
    public float btime = 0.1f;
    public float xtime = 0;
    public float waittime = 0.2f;

    GameManager GM;

    void Start()
    {
        GM = GameManager.GetComponent<GameManager>();
        BurnGrass.SetActive(true);
        Grass.SetActive(true);
        LavaObj[0].SetActive(false);
        LavaObj[1].SetActive(false);
        LavaObj[2].SetActive(false);
        LavaObj[3].SetActive(false);
        LavaObj[4].SetActive(false);
    }

    void Update()
    {
        if (GM.GameOnGoing && GM.NotAlive == false)
        {
            if (ImOnLava)
            {
                ImOnLava = false;
                BurnGrass.SetActive(true);
                Grass.SetActive(true);
                CreateSinkhole = true;
            }

            if (CreateSinkhole)
            {
                CreateSinkhole = false;
                DigSinkhole();
            }
        }
    }

    void DigSinkhole()
    {
        if (time < 4f)
        {
            newN = Random.Range(0, 5);
        }
        else // 약 3초
        {
            BurnGrassObj[oldN].SetActive(true);
            LavaObj[oldN].SetActive(false);
            GrassObj[oldN].SetActive(true);
            oldN = newN;
            if (xtime < btime) // 깜빡
            {
                GrassObj[newN].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - xtime * 10); //꺼졌다가
            }
            else
            {
                GrassObj[newN].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (xtime - (waittime + btime)) * 10);
                //켜졌다가
                if (xtime > waittime + btime * 2)
                {
                    xtime = 0;
                    waittime *= 0.8f; //깜빡이는 시간 줄어들기
                    if (waittime < 0.02f)
                    {
                        time = 0;
                        waittime = 0.2f;
                        LavaObj[newN].SetActive(true);
                        GrassObj[newN].SetActive(false);
                        BurnGrassObj[newN].SetActive(false);
                        CreateSinkhole = true;
                    }
                }
            }
            xtime += Time.deltaTime;
        }
        time += Time.deltaTime;
    }
}
