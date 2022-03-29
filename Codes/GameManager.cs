using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Player; //player
    public GameObject HPBarCover; //hpbar
    public GameObject ScoreText; //score
    public GameObject GameOver; //gameover screen
    public GameObject GameOverText; //gameover score text
    public GameObject Music; //game music
    public GameObject Background; //background

    public bool DamageB = false; //damage trigger
    public bool HealB = false; //heal trigger
    public bool PointB = false; //point trigger

    public string GameType = "ItemRush"; //determine game types
    public bool SCStat = true; //determine rather game is ongoing or not
    public bool GameOnGoing = true; //determine rather game is pause or not

    float HP = 100; //default hp
    float SC = 0; //default score

    Image HPBarI; //get hpbar image
    Text ScoreTextN; //get score text
    Text GameOverTextN; //get gameover score text

    void Start()
    {
        GameOver.SetActive(false); //gameover screen turn off when start
        HPBarI = HPBarCover.GetComponent<Image>(); //get hpbar
        ScoreTextN = ScoreText.GetComponent<Text>(); //get score text
        ScoreTextN.text = string.Format("{0:0}", SC); //score text format
        GameOverTextN = GameOverText.GetComponent<Text>(); //get gameover score text
        GameOverTextN.text = string.Format("{0:0}", SC); //gameover score text
        HPBarI.fillAmount = 1;
        GameOnGoing = true;
    }
    void Update()
    {
        Game(); //let the game begane
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
            GameOver.SetActive(true); //pull gameover screen
            Music.SetActive(false); //music stop
            Background.GetComponent<BgMovement>().Speed = 0f; //background movement stop
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

    public void Restart() //if press restart button
    {
        HP = 100; //default hp set
        SC = 0; //default score set
        SCStat = true; //let the game bagan
        GameOver.SetActive(false); //turn off gameover page
        Music.SetActive(true); //music start
        Background.GetComponent<BgMovement>().Speed = 0.1f; //background start to move
        ScoreTextN.text = string.Format("{0:0}", SC); //score
        GameOverTextN.text = string.Format("{0:0}", SC);
        HPBarI.fillAmount = 1; //hpbar set to 100%
    }
    public void Return() //if press return button
    {
        SceneManager.LoadScene("TitleScene"); //change to title scene
    }
}
