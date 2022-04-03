using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject Database; //get database

    public GameObject BestScoreTag; //best tag obj
    public GameObject PointS; //point s mark
    public GameObject PointA;
    public GameObject PointB;
    public GameObject PointC;
    public GameObject PointD;

    public GameObject BestScore; //best score box
    public GameObject NewScore; //new score box

    public GameObject DCC; //coin count obj
    public GameObject DHC; //heal count obj
    public GameObject DDC; //damage count obj

    int stage = 0; //which stage for result
    int stageB = 0; //wich best score stage for result
    int stPointOff = 0; //any point reduce?

    float BS = 0; //best score
    float BSU = 0; //new best score

    float SC = 0; //score
    float CC = 0; //coin count
    float HC = 0; //heal count
    float DC = 0; //damage count

    Text BestST; //text 
    Text NewST;

    Text DCCT;
    Text DHCT;
    Text DDCT;

    void Start()
    {
        BestScoreTag.SetActive(false); //turn off best tag
        PointS.SetActive(false); //turn off all point marks
        PointA.SetActive(false);
        PointB.SetActive(false);
        PointC.SetActive(false);
        PointD.SetActive(false);

        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 1) == "done") //if stage 1
        {
            stage = 1; //give stage number
            stageB = 4; //give best stage number
            stPointOff = 0; //any point reduction?
        }
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 2) == "done")
        {
            stage = 2;
            stageB = 5;
            stPointOff = 15000;
        }
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 3) == "done")
        {
            stage = 3;
            stageB = 6;
            stPointOff = 75000;
        }

        BS = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", stageB); //get best score
        SC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", stage); //get score
        CC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "cc", stage); //get coin count
        HC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "hc", stage); //get heal count
        DC = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "dc", stage); //get damage count

        NewST = NewScore.GetComponent<Text>(); //text component
        BestST = BestScore.GetComponent<Text>();
        NewST.text = string.Format("{0:0}", 0); //set new score as 0
        BestST.text = string.Format("{0:0}", 0); //set best score as 0

        SetDetail(); //set detail area

        StartCoroutine(StartCount("score", SC, 0, 2)); //start point show coroutine for 2s

        if (SC > BS) //if score is higher than the best score
        {
            BestScoreTag.SetActive(true); //turn on best score tag
            Database.GetComponent<Database>().UpdateData("GameData", "Data", stageB, "score", SC.ToString()); //update best score database datas
            Database.GetComponent<Database>().UpdateData("GameData", "Data", stageB, "cc", CC.ToString());
            Database.GetComponent<Database>().UpdateData("GameData", "Data", stageB, "hc", HC.ToString());
            Database.GetComponent<Database>().UpdateData("GameData", "Data", stageB, "dc", DC.ToString());
            BSU = Database.GetComponent<Database>().ReadDataI("GameData", "Data", "score", stageB);
            StartCoroutine(StartCount("best", BSU, 0, 2)); //start point show coroutine for 2s
        }
        else
        {
            StartCoroutine(StartCount("best", BS, 0, 2)); //or just print out best score
        }

        if (SC >= (200000 - stPointOff)) //determine rathe the game was good or bad: if score is above 200K
        {
            PointS.SetActive(true); //givv S
        }
        else if (SC >= (150000 - stPointOff) && SC < (200000 - stPointOff)) //if score is below 200K and higher than 150K
        {
            PointA.SetActive(true); //give A
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
            PointD.SetActive(true); //or below, give D
        }
    }

    IEnumerator StartCount(string type, float target, float count, float duration)
    {
        float done = (target - count) / duration; //count time

        while (count < target) //time given
        {
            count += done * Time.deltaTime; //time is running
            if (type == "score") //if type is score
            {
                NewST.text = string.Format("{0:0}", count); //change score text to score point
            }
            if (type == "best") //if type is best
            {
                BestST.text = string.Format("{0:0}", count); //change best score text to best score point
            }
            yield return null;
        } 

        count = target; //if done
        if (type == "score") //if type is score
        {
            NewST.text = string.Format("{0:0}", count); //change score text to score point
        }
        if (type == "best") //if type is best
        {
            BestST.text = string.Format("{0:0}", count); //change best score text to best score point
        }
    }

    void SetDetail()
    {
        DCCT = DCC.GetComponent<Text>(); //get component
        DHCT = DHC.GetComponent<Text>();
        DDCT = DDC.GetComponent<Text>();
        DCCT.text = string.Format("{0:0}", CC); //write out data
        DHCT.text = string.Format("{0:0}", HC);
        DDCT.text = string.Format("{0:0}", DC);
    }

    void Reset()
    {
        Database.GetComponent<Database>().UpdateData("GameData", "Data", stage, "score, status, cc, hc, dc", "0, 'playing', 0, 0, 0"); //reset game data to prevent restart

        stage = 0; //reset all point datas to 0
        stageB = 0;
        stPointOff = 0;

        BS = 0;
        BSU = 0;

        SC = 0;
        CC = 0;
        HC = 0;
        DC = 0;
    }

    public void Restart() //whe resart game
    {
        Reset(); //reset result screen
        SceneManager.LoadScene("LoadingScene"); //load new game
    }

    public void ReturnToLobby() //when return to lobby
    {
        Reset(); //reset sesult screen
        SceneManager.LoadScene("TitleScene"); //.oad title page
    }
}
