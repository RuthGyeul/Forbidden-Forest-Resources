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

    Text BestST;
    Text NewST;

    void Start()
    {
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 1) == "done")
        {
            BS = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", 4);
            SC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", 1);

            NewST = NewScore.GetComponent<Text>(); //get score text
            NewST.text = string.Format("{0:0}", SC); //score text format

            if (SC > BS)
            {
                Database.GetComponent<Database>().UpdateData("GameData", "Data", 4, "score", SC.ToString());
                BSU = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", 4);
                BestST = BestScore.GetComponent<Text>();
                BestST.text = string.Format("{0:0}", BSU);
            }
            else
            {
                BestST = BestScore.GetComponent<Text>();
                BestST.text = string.Format("{0:0}", BSU);
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
                BestST.text = string.Format("{0:0}", BSU);
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
                BestST.text = string.Format("{0:0}", BSU);
            }
        }
    }

    void Reset()
    {
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 1) == "done")
        {
            Database.GetComponent<Database>().UpdateData("GameData", "Data", 1, "score, status", "0, 'playing'");
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
