using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    // public Toggle toggleSoundSFX;
    // public Toggle toggleSoundMusic;
    public Slider sliderVolumeSFX;
    public Slider sliderVolumeMusic;

    public GameObject GameObjectMusic, GameObjectSFX;
    private AudioSource[] AudioSourceMusic, AudioSourceSFX;

    // Start is called before the first frame update
    void Start()
    {
       if (ApplicationController.isFirstTime())
        {
            ApplicationController.SetDefaultConfigs();
        }
        
        AudioSourceMusic = GetAudio(GameObjectMusic);
        AudioSourceSFX = GetAudio(GameObjectSFX);
        sliderVolumeSFX.value = ApplicationController.GetVolumeSFX();
        sliderVolumeMusic.value = ApplicationController.GetVolumeMusic();
        SetVolumeSFX();
        SetVolumeMusic();
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void SetSFXSound ()
    {
        // if (toggleSoundSFX.isOn)
        //     ApplicationController.EnableSoundSFX();
        // else
        //     ApplicationController.DisableSoundSFX();
    }

    public void SetMusicSound()
    {
        // if (toggleSoundMusic.isOn)
        //     ApplicationController.EnableSoundMusic();
        // else
        //     ApplicationController.DisableSoundMusic();
    }

    public void SetVolumeSFX()
    {
        // print("VolumeSFX: "+sliderVolumeSFX.value);
        foreach(AudioSource _as in AudioSourceSFX)
        {
            _as.volume = sliderVolumeSFX.value; 

        }
        // GameObjectSFX.volume = sliderVolumeSFX.value; 
        ApplicationController.SetVolumeSFX(sliderVolumeSFX.value);
    
    }

    public void SetVolumeMusic()
    {
        // print("VolumeMusic: "+sliderVolumeMusic.value);
        // print("Debug here: ");
        foreach(AudioSource _as in AudioSourceMusic)
        {
            _as.volume = sliderVolumeMusic.value; 

        }
        // GameObjectMusic.volume = sliderVolumeMusic.value; 
        ApplicationController.SetVolumeMusic(sliderVolumeMusic.value);
    }

    private AudioSource[] GetAudio(GameObject MusicGameObject)
    {
        return MusicGameObject.GetComponentsInChildren<AudioSource>(); 
    }
}
