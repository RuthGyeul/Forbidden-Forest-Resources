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
    
    public GameObject Background01; //background 01
    public GameObject Background02;
    public GameObject Background03;
    
    public GameObject LoadingPage; //black screen
    public GameObject OptionArea; //option area

    public bool DamageB = false; //damage trigger
    public bool HealB = false; //heal trigger
    public bool PointB = false; //point trigger

    public string GameType = ""; //determine game types
    public bool SCStat = false; //determine rather game is ongoing or not
    public bool GameOnGoing = false; //determine rather game is pause or not

    float HP = 100; //default hp
    float SC = 0; //default score

    Image HPBarI; //get hpbar image
    Text ScoreTextN; //get score text

    void Start()
    {
        LoadingPage.SetActive(true); //black screen turn on when start
    }
    
    void Update()
    {
        if (!SCStat) 
        {
            GetFile(); //get game file
        }
        
        if (GameOnGoing && GameType == "ItemRush") 
        {
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
        
        if (GameOnGoing && GameType == "stage02") 
        {
        }
        
        if (GameOnGoing && GameType == "stage03")
        {
        }
    }
    
    void GetFile()
    {
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 1) == "playing") {
            HP = 100; //default hp set
            SC = 0; //default score set
            HPBarI = HPBarCover.GetComponent<Image>(); //get hpbar
            ScoreTextN = ScoreText.GetComponent<Text>(); //get score text
            ScoreTextN.text = string.Format("{0:0}", SC); //score text format
            HPBarI.fillAmount = 1;
            Music01.SetActive(true); //music start
            Background01.GetComponent<BgMovement>().Speed = 0.1f; //background start to move
            GameType = "ItemRush";
            GameOnGoing = true;
            SCStat = true;
            LoadingPage.SetActive(false); //black screen turn off
        }
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 2) == "playing") {
            GameType = "stage02";
            GameOnGoing = true;
            SCStat = true;
            LoadingPage.SetActive(false); //black screen turn off
        }
        if (Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 3) == "playing") {
            GameType = "stage03";
            GameOnGoing = true;
            SCStat = true;
            LoadingPage.SetActive(false); //black screen turn off
        }
    }

    void Game()
    {
        if (HP <= 0) //if hp is 0 or down
        {
            HP = 0; //set hp 0
            SCStat = false; //game is over (turn off game)
            HPBarI.fillAmount = 0; //hpbar set to 0%
            ScoreTextN.text = string.Format("{0}", SC); //set score
            GameOverTextN.text = string.Format("{0}", SC); //set gameover score
        }

        if (SCStat == true) //if game is on
        {
            Point(1f); //gain 1 point per frame
            Damage(0.0005f); //loose 0.0005f hp per fram
        }
        else //if gameover
        {
            GameOver();
        }
    }

    void Damage(float damage) //if damge
    {
        if (HP - (damage * 100) <= 0) //if hp is same or lower than 0 (same code from game() function)
        {
            HP = 0;
            SCStat = false;
            HPBarI.fillAmount = 0;
            ScoreTextN.text = string.Format("{0}", SC);
            GameOverTextN.text = string.Format("{0}", SC);
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
            GameOverTextN.text = string.Format("{0}", SC);
        }
        else
        {
            HP += (heal * 100);
            HPBarI.fillAmount += heal;
            ScoreTextN.text = string.Format("{0:0}", SC);
            GameOverTextN.text = string.Format("{0:0}", SC);
        }
    }

    void Point(float point)
    {
        SC += point; //add point
        ScoreTextN.text = string.Format("{0}", SC); //score set
        GameOverTextN.text = string.Format("{0}", SC);
    }
    
    public void GameOver()
    {
        LoadingPage.SetActive(true); //black screen turn on
        
        if (GameType == "ItemRush")
        {
            Database.GetComponent<Database>().UpdateData("GameData", "Data", 1, "status", "'done'");
        }
        if (GameType == "stage02")
        {
            Database.GetComponent<Database>().UpdateData("GameData", "Data", 2, "status", "'done'");
        }
        if (GameType == "stage03")
        {
            Database.GetComponent<Database>().UpdateData("GameData", "Data", 3, "status", "'done'");
        }
        
        SceneManager.LoadScene("GameOverScene"); //change to game over scene
    }
    
    public void OptionMenu()
    {
        GameOnGoing = false;
        OptionArea.SetActvie(true);
    }
    
    public void ResumeGame()
    {
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
