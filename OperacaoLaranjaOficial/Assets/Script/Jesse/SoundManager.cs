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







}
