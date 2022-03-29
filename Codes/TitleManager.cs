using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject BackGround; //background
    public GameObject ButtonArea; //button area
    public GameObject Start_Key; //start button
    public GameObject Option_Key; //tutorial button
    public GameObject InfoArea; //info area
    public GameObject Description; //info des area
    public GameObject DReturn_Key; //return to title

    public Text Title;
    public string TitleText = "FORBIDDEN FOREST";
    public float TextSpeed = 0.2f;

    void Start() //when start
    { 
        BackGround.SetActive(true); //background on
        ButtonArea.SetActive(true); //button area on
        InfoArea.SetActive(false); //turn off info(tutorial) area
        StartCoroutine(Typing(Title, TitleText, TextSpeed));
    }

    IEnumerator Typing(Text textBox, string msg, float speed)
    {
        for (int i = 0; i < msg.Length; i++)
        {
            textBox.text = msg.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }

    }

    public void StartK() //if press start
    {
        SceneManager.LoadScene("LoadingScene"); //change to loading scene
    }
    public void OptionK() //if press tutorial 
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
