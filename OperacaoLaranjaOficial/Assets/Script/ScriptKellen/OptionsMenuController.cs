using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    public Toggle toggleSoundSFX;
    public Toggle toggleSoundMusic;
    public Slider sliderVolumeSFX;
    public Slider sliderVolumeMusic;

<<<<<<< HEAD
    public GameObject GameObjectMusic, GameObjectSFX;
    private AudioSource[] AudioSourceMusic, AudioSourceSFX;

=======
>>>>>>> parent of dd378f1 (SmartPhone)
    // Start is called before the first frame update
    void Start()
    {
       if (ApplicationController.isFirstTime())
        {
            ApplicationController.SetDefaultConfigs();
        }
        
<<<<<<< HEAD
        AudioSourceMusic = GetAudio(GameObjectMusic);
        AudioSourceSFX = GetAudio(GameObjectSFX);
=======
        toggleSoundSFX.isOn = ApplicationController.IsMuttedSoundSFX ();
        toggleSoundMusic.isOn = ApplicationController.IsMuttedSoundMusic ();
>>>>>>> parent of dd378f1 (SmartPhone)
        sliderVolumeSFX.value = ApplicationController.GetVolumeSFX();
        sliderVolumeMusic.value = ApplicationController.GetVolumeMusic();
        SetVolumeSFX();
        SetVolumeMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSFXSound ()
    {
        if (toggleSoundSFX.isOn)
            ApplicationController.EnableSoundSFX();
        else
            ApplicationController.DisableSoundSFX();
    }

    public void SetMusicSound()
    {
        if (toggleSoundMusic.isOn)
            ApplicationController.EnableSoundMusic();
        else
            ApplicationController.DisableSoundMusic();
    }

    public void SetVolumeSFX()
    {
<<<<<<< HEAD
        // print("VolumeSFX: "+sliderVolumeSFX.value);
        foreach(AudioSource _as in AudioSourceSFX)
        {
            _as.volume = sliderVolumeSFX.value; 

        }
        // GameObjectSFX.volume = sliderVolumeSFX.value; 
        ApplicationController.SetVolumeSFX(sliderVolumeSFX.value);
=======
>>>>>>> parent of dd378f1 (SmartPhone)
    
    }

    public void SetVolumeMusic()
    {
<<<<<<< HEAD
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
=======
    
}
>>>>>>> parent of dd378f1 (SmartPhone)
}
