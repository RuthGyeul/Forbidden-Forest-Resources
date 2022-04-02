using System.Collections;
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

    public GameObject DCC; //coin count
    public GameObject DHC; //heal count
    public GameObject DDC; //damage count

    int stage = 0;
    int stageB = 0;
    int stPointOff = 0;

    float BS = 0;
    float BSU = 0;

    float SC = 0;
    float CC = 0;
    float HC = 0;
    float DC = 0;

    Text BestST;
    Text NewST;

    Text DCCT;
    Text DHCT;
    Text DDCT;

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
            stage = 1;
            stageB = 4;
            stPointOff = 0;
        }
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 2) == "done")
        {
            stage = 2;
            stageB = 5;
            stPointOff = 10000;
        }
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 3) == "done")
        {
            stage = 3;
            stageB = 6;
            stPointOff = 20000;
        }

        BS = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", stageB);
        SC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", stage);
        CC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "cc", stage);
        HC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "hc", stage);
        DC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "dc", stage);

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
        DCCT = DCC.GetComponent<Text>();
        DHCT = DHC.GetComponent<Text>();
        DDCT = DDC.GetComponent<Text>();
        DCCT.text = string.Format("{0:0}", CC);
        DHCT.text = string.Format("{0:0}", HC);
        DDCT.text = string.Format("{0:0}", DC);
    }

    void Reset()
    {
        Database.GetComponent<Database>().UpdateData("GameData", "Data", stage, "score, status, cc, hc, dc", "0, 'playing', 0, 0, 0");

        stage = 0;
        stageB = 0;
        stPointOff = 0;

        BS = 0;
        BSU = 0;

        SC = 0;
        CC = 0;
        HC = 0;
        DC = 0;
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
