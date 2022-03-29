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
    
    float BS = 0;
    float SC = 0;
    
    Text BestST;
    Text NewST;
    
    void Start()
    {
        BestST = BestScore.GetComponent<Text>(); //get bestscore text
        BestST.text = string.Format("{0:0}", BS); //best score text format
            
        int bestS = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", 4);
        int newS = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", 1);
    }
    
    void Update()
    {
    }
    
    void Reset()
    {
    }
    
    public void Restart()
    {
        Reset();
        SceneManager.LoadScene("LoadingScene");
    }
    
    public void ReturnToLobby()
    {
        Reset();
        SceneManager.LoadScene("TitleScene");
    }
}
