using UnityEngine;
using UnityEngine.UI;
using System;

public class VolumeControl : MonoBehaviour
{
    public GameObject Database; //set database
    public Slider ASlider; //set slider
    public AudioSource Audio; //set audio file
    public float AVolume = 0.5f; //set default volume

    void Start()
    {
        AVolume = float.Parse(Database.GetComponent<Database>().ReadDataS("GameData", "Data", "status", 7)); //get volume data
        AVolume = PlayerPrefs.GetFloat("ASlider", AVolume); //set slider location to volume
        ASlider.value = AVolume; //slider value equals to current volume
        Audio.volume = ASlider.value; //set audio source volume
        Database.GetComponent<Database>().UpdateData("GameData", "Data", 7, "status", AVolume.ToString()); //import volume to database.
        AudioListener.volume = ASlider.value; //set liteners volume
    }

    void Update()
    {
        SoundSlider(); //slider active
    }

    public void SoundSlider()
    {
        Audio.volume = ASlider.value; //set audio source volume
        AudioListener.volume = ASlider.value; //set liteners volume
        AVolume = ASlider.value; //slider value equl to volume
        Database.GetComponent<Database>().UpdateData("GameData", "Data", 7, "status", AVolume.ToString()); //import volume to database.
        PlayerPrefs.SetFloat("AVolume", AVolume); //set liteners volume
    }
}
