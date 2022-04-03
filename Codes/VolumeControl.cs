using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider ASlider; //set slider
    public AudioSource Audio; //set audio file
    public float AVolume = 0.5f; //set default volume

    void Start()
    {
        AVolume = PlayerPrefs.GetFloat("ASlider", AVolume); //set slider location to volume
        ASlider.value = AVolume; //slider value equals to current volume
        Audio.volume = ASlider.value; //set audio source volume
        AudioListener.volume = ASlider.value; //set liteners volume
    }

    private void Update()
    {
        SoundSlider(); //slider active
    }

    public void SoundSlider()
    {
        Audio.volume = ASlider.value; //set audio source volume
        AudioListener.volume = ASlider.value; //set liteners volume
        AVolume = ASlider.value; //slider value equl to volume
        PlayerPrefs.SetFloat("AVolume", AVolume); //set liteners volume
    }
}
