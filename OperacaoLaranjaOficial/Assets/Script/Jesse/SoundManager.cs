using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	// public AudioSource[] SFX, Music;
	public AudioSource GamePlay, Menu;
	private AudioSource audio1, audio2;
	bool IsMenuPlaying;

	// public void PlaySFX(int index)
	// {
	// 	SFX[index].Play();
	// }

	// public void PlayMusic(int index)
	// {
	// 	Music[index].Play();
	// }

	public void SwitGamePlayAndMenu()
	{	
		StartCoroutine(SwitFaded());
	}

	private IEnumerator SwitFaded()
	{
		audio1 = !IsMenuPlaying ? Menu : GamePlay;
		audio2 = IsMenuPlaying ? Menu : GamePlay;
		float v = 0,vMax = ApplicationController.GetVolumeMusic();
		// audio1.volume = vMax-v;
		audio2.volume = v;
		audio2.Play();
		while(v<=vMax)
		{
			v += Time.deltaTime/2;
			audio1.volume = vMax-v;
			audio2.volume = v >= vMax ? vMax : v;
			yield return null;
		}
		audio1.Stop();

		IsMenuPlaying = !IsMenuPlaying;
	}


	public void FadeInGameplaySound()
	{
		StartCoroutine(FadeInGameplay());
	}


	public void FadeOutGameplaySound()
	{
		StartCoroutine(FadeOutGameplay());
	}


	public void FadeInMenuSound()
	{
		StartCoroutine(FadeInMenu());
	}


	public void FadeOutMenuSound()
	{
		StartCoroutine(FadeOutMenu());
	}


	private IEnumerator FadeInGameplay()
	{
		float auxTime = 0, time = 0.5f;
		float maxV=GamePlay.volume;
    	while(auxTime <= time)
    	{
    		auxTime += Time.deltaTime;
    		GamePlay.volume = maxV - auxTime*maxV/time;
            yield return null;
    	}
	}

	private IEnumerator FadeOutGameplay()
	{
		float auxTime = 0, time = 0.5f;
		float maxV=ApplicationController.GetVolumeMusic();
    	while(auxTime <= time)
    	{
    		auxTime += Time.deltaTime;
    		GamePlay.volume = auxTime*maxV/time;
            yield return null;
    	}
	}


	private IEnumerator FadeInMenu()
	{
		float auxTime = 0, time = 0.5f;
		float maxV=GamePlay.volume;
    	while(auxTime <= time)
    	{
    		auxTime += Time.deltaTime;
    		Menu.volume = maxV - auxTime*maxV/time;
            yield return null;
    	}
	}

	private IEnumerator FadeOutMenu()
	{
		float auxTime = 0, time = 0.5f;
		float maxV=ApplicationController.GetVolumeMusic();
    	while(auxTime <= time)
    	{
    		auxTime += Time.deltaTime;
    		Menu.volume = auxTime*maxV/time;
            yield return null;
    	}
	}




}
