using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject BackGround; //background
    public GameObject ButtonArea; //button area
    public GameObject StageArea;
    public GameObject Start_Key; //start button
    public GameObject Option_Key; //tutorial button
    public GameObject InfoArea; //info area
    public GameObject Description; //info des area
    public GameObject DReturn_Key; //return to title

    public GameObject Database; //database

    public Text Title; //title box
    public string TitleText = "FORBIDDEN FOREST"; //title text
    public float TextSpeed = 0.2f; //text typing speed

    void Start() //when start
    {
        BackGround.SetActive(true); //background on
        ButtonArea.SetActive(true); //button area on
        InfoArea.SetActive(false); //turn off info(tutorial) area
        StageArea.SetActive(false); //turn off stage area
        Database.GetComponent<Database>().UpdateData("GameData", "Data", 1, "score, status, cc, hc, dc", "0, 'ready', 0, 0, 0");
        Database.GetComponent<Database>().UpdateData("GameData", "Data", 2, "score, status", "0, 'ready'");
        Database.GetComponent<Database>().UpdateData("GameData", "Data", 3, "score, status", "0, 'ready'");
        StartCoroutine(Typing(Title, TitleText, TextSpeed)); //start typing coroutine
    }

    IEnumerator Typing(Text textBox, string msg, float speed)
    {
        for (int i = 0; i < msg.Length; i++)
        {
            textBox.text = msg.Substring(0, i + 1); //start type by single letter at a time
            yield return new WaitForSeconds(speed); //wait for speed control
        }
    }

    public void StartK01() //if press stage01
    {
        Database.GetComponent<Database>().UpdateData("GameData", "Data", 1, "status", "'playing'"); //file ready
        SceneManager.LoadScene("LoadingScene"); //change to loading scene
    }
    public void StartK02() //if press stage02
    {
        Database.GetComponent<Database>().UpdateData("GameData", "Data", 2, "status", "'playing'");
        SceneManager.LoadScene("LoadingScene");
    }
    public void StartK03() //if press stage03
    {
        Database.GetComponent<Database>().UpdateData("GameData", "Data", 3, "status", "'playing'");
        SceneManager.LoadScene("LoadingScene");
    }
    public void StageK() 
    {
        StageArea.SetActive(true);
    }
    public void OptionK() //when press tutorial 
    {
        ButtonArea.SetActive(false); //turn off button area
        InfoArea.SetActive(true); //pull tutorial area
        Description.SetActive(true); //pull des area
    }
    public void ReturnK() //when press return
    {
        BackGround.SetActive(true); //turn on background
        ButtonArea.SetActive(true); //turn on button area
        InfoArea.SetActive(false); //turn off infoarea
    }
}
