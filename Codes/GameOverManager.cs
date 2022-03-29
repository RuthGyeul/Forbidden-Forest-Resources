using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject Database;
    
    public GameObject BestScoreTag;
    public GameObject PointS;
    public GameObject PointA;
    public GameObject PointB;
    public GameObject PointC;
    public GameObject PointD;
    
    public GameObject BestScore;
    public GameObject NewScore;
    
    Text BestST;
    Text NewST;
    
    void Start()
    {
    }
    
    void Update()
    {
        int bestS = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", 4);
        int newS = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", 1);
    }
