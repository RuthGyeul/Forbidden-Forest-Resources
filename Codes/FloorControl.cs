using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorControl : MonoBehaviour
{
    public GameObject GameManager;
    
    public GameObject[] GrassObj = new GameObject[7];
    public GameObject[] BurnGrassObj = new GameObject[7];
    public GameObject[] LavaObj = new GameObject[7];
    
    public GameObject Grass;
    public GameObject Grass01;
    public GameObject Grass02;
    public GameObject Grass03;
    public GameObject Grass04;
    public GameObject Grass05;
    public GameObject Grass06;
    public GameObject Grass07;
    
    public GameObject BurnGrass;
    public GameObject BurnGrass01;
    public GameObject BurnGrass02;
    public GameObject BurnGrass03;
    public GameObject BurnGrass04;
    public GameObject BurnGrass05;
    public GameObject BurnGrass06;
    public GameObject BurnGrass07;
    
    public GameObject Lava;
    public GameObject Lava01;
    public GameObject Lava02;
    public GameObject Lava03;
    public GameObject Lava04;
    public GameObject Lava05;
    public GameObject Lava06;
    public GameObject Lava07;
    
    public bool CreateSinkhole = false;
    public bool ImOnLava = false;
    public float LavaDuration = 5.0f;
    
    GameManager GM;
    
    void Start()
    {
        GM = GameManager.GetComponent<GameManager>();
        Lava.SetActive(false);
        BurnGrass.SetActive(true);
        Grass.SetActive(true);
    }
    
    void Update()
    {
        if (GM.GameOnGoing && GM.NotAlive == false)
        {
            if (ImOnLava)
            {
                ImOnLava = false;
                StopCoroutine("Sinkhole");
                BurnGrass.SetActive(true);
                Grass.SetActive(true);
                Lava.SetActive(false);
                CreateSinkhole = true;
            }
            
            if (CreateSinkhole)
            {
                CreateSinkhole = false;
                StartCoroutine("Sinkhole");
            }
        }
    }
    
    IEnumerator Sinkhole()
    {
        int ran = Random.Range(0, 7);
        
        for (int i = 0; i <= 10; i++)
        {
            GrassObj[ran].SetActive(false);
            BurnGrassObj[ran].SetActive(true);
            GrassObj[ran].SetActive(true);
            BurnGrassObj[ran].SetActive(false);
        }
        GrassObj[ran].SetActive(false);
        LavaObj[ran].SetActive(true);
        BurnGrassObj[ran].SetActive(false);
        yield return new WaitForSeconds(LavaDuration);
        CreateSinkhole = true;
    }
}
