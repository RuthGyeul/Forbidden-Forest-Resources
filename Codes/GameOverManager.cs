using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject Database;

    public GameObject DetailArea;

    public GameObject BestScoreTag;
    public GameObject PointS;
    public GameObject PointA;
    public GameObject PointB;
    public GameObject PointC;
    public GameObject PointD;

    public GameObject BestScore;
    public GameObject NewScore;
    
    public GameObject DSC; //score
    public GameObject DCC; //coin count
    public GameObject DHC; //heal count
    public GameObject DDC; //damage count
    
    public GameObject DBSC; //Best score
    public GameObject DBCC; //best coin count
    public GameObject DBHC; //best heal count
    public GameObject DBDC; //best damage count
    
    public GameObject DSG; //score graph
    public GameObject DCG; //coin count graph
    public GameObject DHG; //heal count graph
    public GameObject DDG; //damage count graph
    
    public GameObject DBSG; //Best score
    public GameObject DBCG; //best coin count
    public GameObject DBHG; //best heal count
    public GameObject DBDG; //best damage count
    
    public GameObject 

    int stage = 0;
    int stageB = 0;

    float BS = 0;
    float BSU = 0;
    float SC = 0;

    float CC = 0;
    float HC = 0;
    float DC = 0;

    Text BestST;
    Text NewST;
    
    Text DSCT;
    Text DCCT;
    Text DHCT;
    Text DDCT; 
    
    Text DBSCT;
    Text DBCCT;
    Text DBHCT;
    Text DBDCT;
    
    Image DSGI;
    Image DCGI;
    Image DHGI;
    Image DDGI;
    
    Image DBSGI;
    Image DBCGI;
    Image DBHGI;
    Image DBDGI;
    

    void Start()
    {
        DetailArea.SetActive(false);
        BestScoreTag.SetActive(false);
        PointS.SetActive(false);
        PointA.SetActive(false);
        PointB.SetActive(false);
        PointC.SetActive(false);
        PointD.SetActive(false);

        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 1) == "done")
        {
            stage = 1;
            stageB = 4;
        }
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 2) == "done")
        {
            stage = 2;
            stageB = 5;
        }
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 3) == "done")
        {
            stage = 3;
            stageB = 6;
        }

        BS = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", stageB);
        SC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", stage);
        CC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "cc", stage);
        HC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "hc", stage);
        DC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "dc", stage);
        
        float PTime = (SC - (CC * 100 + HC * 100)) / 1000;
        
        NewST = NewScore.GetComponent<Text>();
        BestST = BestScore.GetComponent<Text>();
        NewST.text = string.Format("{0:0}", 0);
        BestST.text = string.Format("{0:0}", 0);
        
        SetDetail();

        StartCoroutine(StartCount("score", SC, 0, 2));
        
        if (SC > BS)
        {
            BestScoreTag.SetActive(true);
            Database.GetComponent<Database>().UpdateData("GameData", "Data", stageB, "score", SC.ToString());
            Database.GetComponent<Database>().UpdateData("GameData", "Data", stageB, "cc", CC.ToString());
            Database.GetComponent<Database>().UpdateData("GameData", "Data", stageB, "hc", HC.ToString());
            Database.GetComponent<Database>().UpdateData("GameData", "Data", stageB, "dc", DC.ToString());
            BSU = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", stageB);
            StartCoroutine(StartCount("best", BSU, 0, 2));
            //best 갱신 시 기존 베스트는 화면에서 떨어트리고 새로운걸로 교체 에니메이션?
        } 
        else 
        {
            StartCoroutine(StartCount("best", BS, 0, 2));
        }
        
        if (SC >= (200000 - stPointOff))
        {
            PointS.SetActive(true);
        }
        else if (SC >= (150000 - stPointOff) && SC < (200000 - stPointOff))
        {
            PointA.SetActive(true);
        }
        else if (SC >= (100000 - stPointOff) && SC < (150000 - stPointOff))
        {
            PointB.SetActive(true);
        }
        else if (SC >= (70000 - stPointOff) && SC < (100000 - stPointOff))
        {
            PointC.SetActive(true);
        }
        else
        {
            PointD.SetActive(true);
        }
    }

    IEnumerator StartCount(string type, float target, float count, float duration)
    {
        float done = (target - count) / duration;

        while (count < target)
        {
            count += done * Time.deltaTime;
            if (type == "score")
            {
                NewST.text = string.Format("{0:0}", count);
            }
            if (type == "best")
            {
                BestST.text = string.Format("{0:0}", count);
            }
            yield return null;
        }

        count = target;
        if (type == "score")
        {
            NewST.text = string.Format("{0:0}", count);
        }
        if (type == "best")
        {
            BestST.text = string.Format("{0:0}", count);
        }
    }
    
    void SetDetail()
    {
        DSCT = DSC.GetComponent<Text>();
        
        DSCT.text = string.Format("{0:0}", 0);
    }

    void Reset()
    {
        Database.GetComponent<Database>().UpdateData("GameData", "Data", stage, "score, status, cc, hc, dc", "0, 'playing', 0, 0, 0");
    }

    public void Detail()
    {
        DetailArea.SetActive(true);
    }

    public void Return()
    {
        DetailArea.SetActive(false);
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
