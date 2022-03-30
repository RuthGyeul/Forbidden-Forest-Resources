using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Player; //player
    public GameObject Database; //database

    public GameObject HPBarCover; //hpbar
    public GameObject ScoreText; //score

    public GameObject Music01; //game music 01
    public GameObject Music02;
    public GameObject Music03;

    public GameObject Background01; //background 01
    public GameObject Background02;
    public GameObject Background03;

    public GameObject LoadingPage; //black screen
    public GameObject OptionArea; //option area

    public bool DamageB = false; //damage trigger
    public bool HealB = false; //heal trigger
    public bool PointB = false; //point trigger

    public string GameType = ""; //determine game types
    public bool CheckFile = false; //determine rather game is ongoing or not
    public bool NotAlive = false; //determin blah blah
    public bool GameOnGoing = false; //determine rather game is pause or not

    float HP = 100; //default hp
    float SC = 0; //default score

    Image HPBarI; //get hpbar image
    Text ScoreTextN; //get score text

    void Start()
    {
        CheckFile = false;
        NotAlive = false;
        GameOnGoing = false;
        //DISABLE EVERYTHING HERE!!!!!!!
        LoadingPage.SetActive(true); //black screen turn on when start
    }

    void Update()
    {
        if (NotAlive)
        {
            GameOver();
        }
        
        if (!CheckFile)
        {
            GetFile(); //get game file
        }

        if (GameType == "ItemRush")
        {
            if (GameOnGoing) {
            //PUT ALL MUSIC OR OBJECT SOURCE HERE!!!!!
                Game(); //let the game began

                if (DamageB) //if damage boolen triggered (on)
                {
                    DamageB = false; //damage boolen turn off
                    Damage(0.25f); //give damage
                    Point(-2000f); //change score
                }
                if (HealB)
                {
                    HealB = false;
                    Heal(0.1f);
                    Point(500f);
                }
                if (PointB)
                {
                    PointB = false;
                    Point(1000f);
                }
            }
        }

        if (GameType == "stage02")
        {
        }

        if (GameType == "stage03")
        {
        }
    }

    void GetFile()
    {
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 1) == "playing")
        {
            HP = 100; //default hp set
            SC = 0; //default score set
            HPBarI = HPBarCover.GetComponent<Image>(); //get hpbar
            ScoreTextN = ScoreText.GetComponent<Text>(); //get score text
            ScoreTextN.text = string.Format("{0:0}", SC); //score text format
            HPBarI.fillAmount = 1;
            Background01.GetComponent<BgMovement>().Speed = 0.1f; //background start to move
            GameType = "ItemRush";
            CheckFile = true;
            GameOnGoing = true;
            LoadingPage.SetActive(false); //black screen turn off
        }
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 2) == "playing")
        {
            GameType = "stage02";
            CheckFile = true;
            GameOnGoing = true;
            LoadingPage.SetActive(false); //black screen turn off
        }
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 3) == "playing")
        {
            GameType = "stage03";
            CheckFile = true;
            GameOnGoing = true;
            LoadingPage.SetActive(false); //black screen turn off
        }
    }

    void Game()
    {
        if (HP <= 0) //if hp is 0 or down
        {
            HP = 0; //set hp 0
            GameOnGoing = false;
            NotAlive = true;
            HPBarI.fillAmount = 0; //hpbar set to 0%
            ScoreTextN.text = string.Format("{0}", SC); //set score
        }

        if (NotAlive == false) //if alive
        {
            Point(1f); //gain 1 point per frame
            Damage(0.0005f); //loose 0.0005f hp per fram
        }
        else //if gameover
        {
            GameOnGoing = false;
            NotAlive = true;
        }
    }

    void Damage(float damage) //if damge
    {
        if (HP - (damage * 100) <= 0) //if hp is same or lower than 0 (same code from game() function)
        {
            HP = 0;
            HPBarI.fillAmount = 0;
            ScoreTextN.text = string.Format("{0}", SC);
            GameOnGoing = false;
            NotAlive = true;
        }
        else
        {
            HP -= (damage * 100); //give damage
            HPBarI.fillAmount -= damage; //loose hp by damage
        }
    }

    void Heal(float heal)
    {
        if (HP + (heal * 100) >= 100)
        {
            HP = 100;
            HPBarI.fillAmount = 1;
            ScoreTextN.text = string.Format("{0}", SC);
        }
        else
        {
            HP += (heal * 100);
            HPBarI.fillAmount += heal;
            ScoreTextN.text = string.Format("{0:0}", SC);
        }
    }

    void Point(float point)
    {
        SC += point; //add point
        ScoreTextN.text = string.Format("{0}", SC); //score set
    }

    public void GameOver()
    {
        LoadingPage.SetActive(true); //black screen turn on

        if (GameType == "ItemRush")
        {
            Database.GetComponent<Database>().UpdateData("GameData", "Data", 1, "score, status", SC + ", 'done'");
            Database.GetComponent<Database>().UpdateData("GameData", "Data", 1, "cc", Player.GetComponent<PlayerStat>().PointC.ToString());
            Database.GetComponent<Database>().UpdateData("GameData", "Data", 1, "hc", Player.GetComponent<PlayerStat>().HealC.ToString());
            Database.GetComponent<Database>().UpdateData("GameData", "Data", 1, "dc", Player.GetComponent<PlayerStat>().DamageC.ToString());
        }
        if (GameType == "stage02")
        {
            Database.GetComponent<Database>().UpdateData("GameData", "Data", 2, "score, status", SC + ", 'done'");
        }
        if (GameType == "stage03")
        {
            Database.GetComponent<Database>().UpdateData("GameData", "Data", 3, "score, status", SC + ", 'done'");
        }

        SceneManager.LoadScene("GameOverScene"); //change to game over scene
    }

    public void OptionMenu()
    {
        GameOnGoing = false;
        OptionArea.SetActive(true);
    }

    public void ResumeGame()
    {
        GameOnGoing = true;
        OptionArea.SetActive(false);
    }

    public void HowToPlay()
    {
    }

    public void ReturnToLobby()
    {
        SceneManager.LoadScene("TitleScene"); //send to tile scene
    }
}
