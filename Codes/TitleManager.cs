using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Start() //when start
    {
        BackGround.SetActive(true); //background on
        ButtonArea.SetActive(true); //button area on
        InfoArea.SetActive(false); //turn off info(tutorial) area
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
