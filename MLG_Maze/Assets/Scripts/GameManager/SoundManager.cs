using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicvolume", 1);
        }

        else
        {
            Load();
        }
    }

    //Volume slider
    public void ChangeVolume()
        {
            AudioListener.volume = volumeSlider.value;
            Save();
        }

    //Load sounf
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");

    }

    //Save settings for restart app
    private void Save()
    {
        PlayerPrefs.SetFloat("musicvolume", volumeSlider.value);
    }
}
