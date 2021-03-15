using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
	public VideoPlayer video;
	public Button bt;
	public Text btText;
	public Image blackScreen;

	private Color textColor, bsColor;


    // Start is called before the first frame update
    void Start()
    {
		bt.enabled = false;  
		btText.gameObject.SetActive(false); 
		StartCoroutine("ShowSkipButton");
		StartCoroutine("Finished");
    }


    public void Skip()
    {
    	print("Video skiped");
    	StartCoroutine(HideVideoGradually(2));

    }


    private IEnumerator Finished()
    {
    	yield return new WaitForSeconds(18.5f);
    	Skip();
    }


    private IEnumerator ShowSkipButton()
    {
    	yield return new WaitForSeconds(6);
    	StartCoroutine(ShowSkipButtonGradually(2));
    }


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


    private IEnumerator HideVideoGradually(float time)
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
    }

}

