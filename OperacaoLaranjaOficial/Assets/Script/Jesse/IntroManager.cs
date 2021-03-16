using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
	public VideoPlayer videoIntro, videoAbertura;
	public Button bt;
	public Text btText;
	public Image blackScreen;

	private Color textColor, bsColor;


    // Start is called before the first frame update
    void Start()
    {
		bt.enabled = false;  
		btText.gameObject.SetActive(false); 
    	StartCoroutine("AllScript");
    }


    public void Skip()
    {
    	print("Video skiped");
    	StartCoroutine(HideVideoGradually(videoIntro, 2, true));

    }


    private IEnumerator Finished()
    {
    	yield return new WaitForSeconds(18.5f);
    	Skip();
    }


    // private IEnumerator ShowSkipButton()
    // {
    // }


    private IEnumerator ShowSkipButtonGradually(float time)
    {
    	// print("DEBUG HERE");
		bt.enabled = true;   
    	float auxTime=0;
    	textColor = btText.color;
    	textColor = new Color(textColor[0],textColor[1],textColor[2],0);
    	btText.color = textColor;

		btText.gameObject.SetActive(true); 

    	while(auxTime <= time)
    	{
    		auxTime += Time.deltaTime;
    		btText.color = textColor + new Color(0,0,0,auxTime/time);
    		yield return null;
    	}
    }


    private IEnumerator HideVideoGradually(VideoPlayer video, float time, bool lastVideo)
    {
    	// print("DEBUG HERE");
		bt.enabled = false;   
    	float auxTime=0;
    	bsColor = blackScreen.color;
    	bsColor = new Color(bsColor[0],bsColor[1],bsColor[2],0);
    	blackScreen.color = bsColor;

    	while(auxTime <= time)
    	{
    		auxTime += Time.deltaTime;
    		blackScreen.color = bsColor + new Color(0,0,0,auxTime/time);
    		video.SetDirectAudioVolume(0, 1-auxTime/time);
    		yield return null;
    	}
    	if(lastVideo)
    	{
	    	SceneManager.LoadScene(1);
    	}
    }


    private IEnumerator ShowVideoGradually(VideoPlayer video, float time)
    {
    	// print("DEBUG HERE");
		bt.enabled = false;   
    	float auxTime=0;
    	bsColor = blackScreen.color;
    	bsColor = new Color(bsColor[0],bsColor[1],bsColor[2],1);
    	blackScreen.color = bsColor;

    	while(auxTime <= time)
    	{
    		auxTime += Time.deltaTime;
    		blackScreen.color = bsColor - new Color(0,0,0,auxTime/time);
    		video.SetDirectAudioVolume(0, auxTime/time);
    		yield return null;
    	}
    }


    private IEnumerator StartChangeVideo()
    {
    	yield return new WaitForSeconds(15);
    }

    private IEnumerator AllScript()
    {
    	yield return new WaitForSeconds(14.5f);

    	StartCoroutine(HideVideoGradually(videoAbertura, 2, false));
    	yield return new WaitForSeconds(2);
		
		videoAbertura.gameObject.SetActive(false); 
		videoIntro.gameObject.SetActive(true); 
    	StartCoroutine(ShowVideoGradually(videoIntro, 2));
		StartCoroutine("Finished");
    	
    	yield return new WaitForSeconds(6);
    	StartCoroutine(ShowSkipButtonGradually(2));



    }

}

