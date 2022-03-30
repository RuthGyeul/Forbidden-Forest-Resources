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
    float BSU = 0;
    float SC = 0;
    
    float CC = 0;
    float HC = 0;
    float DC = 0;

    Text BestST;
    Text NewST;

    void Start()
    {
        BestScoreTag.SetActive(false);
        PointS.SetActive(false);
        PointA.SetActive(false);
        PointB.SetActive(false);
        PointC.SetActive(false);
        PointD.SetActive(false);
        
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 1) == "done")
        {
            BS = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", 4);
            SC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", 1);
            CC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "cc", 1);
            HC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "hc", 1);
            DC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "dc", 1);
            
            float PTime = (SC - (CC*100 + HC*100))/1000; //play time (s)
            float PAvgCoin = CC/PTime; //coin per second
            float PAvgDamage = DC/PTime; //damage per second
            float PTotalHeal = HC*20; //total heal recieved
            float PTotalDamage = DC*100; //total damage recieved

            NewST = NewScore.GetComponent<Text>(); //get score text
            NewST.text = string.Format("{0:0}", SC); //score text format

            if (SC > BS)
            {
                Database.GetComponent<Database>().UpdateData("GameData", "Data", 4, "score", SC.ToString());
                Database.GetComponent<Database>().UpdateData("GameData", "Data", 4, "cc", CC.ToString());
                Database.GetComponent<Database>().UpdateData("GameData", "Data", 4, "hc", HC.ToString());
                Database.GetComponent<Database>().UpdateData("GameData", "Data", 4, "dc", DC.ToString());
                BSU = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", 4);
                BestST = BestScore.GetComponent<Text>();
                BestST.text = string.Format("{0:0}", BSU);
                BestScoreTag.SetActive(true);
            }
            else
            {
                BestST = BestScore.GetComponent<Text>();
                BestST.text = string.Format("{0:0}", BS);
            }
            
            if (PAvgDamage <= ) 
            {
                PointS.SetActive(true);
            }
            else if (PAvgDamage)
            {
                PointA.SetActive(true);
            }
            else if (PAvgDamage)
            {
                PointB.SetActive(true);
            }
            else if (PAvgDamage)
            {
                PointC.SetActive(true);
            }
            else
            {
                PointD.SetActive(true);
            }
        }

        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 2) == "done")
        {
            BS = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", 5);
            SC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", 2);

            NewST = NewScore.GetComponent<Text>(); //get score text
            NewST.text = string.Format("{0:0}", SC); //score text format

            if (SC > BS)
            {
                Database.GetComponent<Database>().UpdateData("GameData", "Data", 5, "score", SC.ToString());
                BSU = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", 5);
                BestST = BestScore.GetComponent<Text>();
                BestST.text = string.Format("{0:0}", BSU);
            }
            else
            {
                BestST = BestScore.GetComponent<Text>();
                BestST.text = string.Format("{0:0}", BS);
            }
        }

        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 3) == "done")
        {
            BS = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", 6);
            SC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", 3);

            NewST = NewScore.GetComponent<Text>(); //get score text
            NewST.text = string.Format("{0:0}", SC); //score text format

            if (SC > BS)
            {
                Database.GetComponent<Database>().UpdateData("GameData", "Data", 6, "score", SC.ToString());
                BSU = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", 6);
                BestST = BestScore.GetComponent<Text>();
                BestST.text = string.Format("{0:0}", BSU);
            }
            else
            {
                BestST = BestScore.GetComponent<Text>();
                BestST.text = string.Format("{0:0}", BS);
            }
        }
    }
    
    void Detail()
    {
    }

    void Reset()
    {
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 1) == "done")
        {
            Database.GetComponent<Database>().UpdateData("GameData", "Data", 1, "score, status, cc, hc, dc", "0, 'playing', 0, 0, 0");
        }
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 2) == "done")
        {
            Database.GetComponent<Database>().UpdateData("GameData", "Data", 2, "score, status", "0, 'playing'");
        }
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 3) == "done")
        {
            Database.GetComponent<Database>().UpdateData("GameData", "Data", 3, "score, status", "0, 'playing'");
        }
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
