using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
	public Camera camPrincipal, camSecundaria;
	public VideoPlayer videoIntro, videoAbertura;
	public Button bt;
	public Text btText;
    public AudioSource menuMusic;
	// public GameObject blackScreen;

	private Color textColor, bsColor;
	private AsyncOperation asyncOperation;
	private bool aux;

    // Start is called before the first frame update
    void Start()
    {
		bt.enabled = false;  
		camPrincipal.enabled = false;  
		camSecundaria.enabled = true;  
		btText.gameObject.SetActive(false); 
    	StartCoroutine("AllScript");
    }

   	void Update()
   	{
   		// if(!aux){
	    //     asyncOperation = SceneManager.LoadSceneAsync(1);
	    //     asyncOperation.allowSceneActivation = false;
	    //     StartCoroutine(LoadScene());        
	    //     aux = true;
   		// }
   	}

    public void Skip()
    {
    	print("Video skiped");
    	// while(!asyncOperation.isDone)
    	// {
	    // 	yield return null;
    	// }
    	StartCoroutine(HideVideoGradually(videoIntro, 2, true));

    }


    private IEnumerator Finished()
    {
    	yield return new WaitForSeconds(19f);
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
    	float auxTime=0, maxV=0;
        if(lastVideo){maxV=menuMusic.volume;menuMusic.volume=0;menuMusic.Play();}
            
    	// bsColor = blackScreen.color;
    	// bsColor = new Color(bsColor[0],bsColor[1],bsColor[2],0);
    	// blackScreen.color = bsColor;

    	while(auxTime <= time)
    	{
    		auxTime += Time.deltaTime;
    		video.targetCameraAlpha = 1-auxTime/time;
    		// blackScreen.color = bsColor + new Color(0,0,0,auxTime/time);
    		video.SetDirectAudioVolume(0, 1-auxTime/time);
    		if(lastVideo){menuMusic.volume=auxTime/time*maxV > maxV ? maxV : auxTime/time*maxV;}
            yield return null;
    	}
    	if(lastVideo)
    	{
			camPrincipal.enabled = true;  
			camSecundaria.enabled = false;
			videoIntro.gameObject.SetActive(false); 
            yield return new WaitForSeconds(2);


	    	// SceneManager.LoadScene(1);
	        // asyncOperation.allowSceneActivation = true;
	        print("Last");
    	}
    }


    private IEnumerator ShowVideoGradually(VideoPlayer video, float time, float delay)
    {
    	// print("DEBUG HERE");
		bt.enabled = false;   
    	float auxTime=0;
		video.targetCameraAlpha = 0;
        video.gameObject.SetActive(true);
    	// bsColor = blackScreen.color;
    	// bsColor = new Color(bsColor[0],bsColor[1],bsColor[2],1);
    	// blackScreen.color = bsColor;
    	yield return new WaitForSeconds(delay);
    	while(auxTime <= time)
    	{
    		auxTime += Time.deltaTime;
    		// blackScreen.color = bsColor - new Color(0,0,0,auxTime/time);
            video.targetCameraAlpha = auxTime/time;
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
    	StartCoroutine(ShowVideoGradually(videoAbertura, 1, 0));
    	yield return new WaitForSeconds(14.5f);

    	StartCoroutine(HideVideoGradually(videoAbertura, 1, false));
    	yield return new WaitForSeconds(1);
		
		videoAbertura.gameObject.SetActive(false); 
		videoIntro.gameObject.SetActive(true); 
    	StartCoroutine(ShowVideoGradually(videoIntro, 1, 0));
		StartCoroutine("Finished");
            
        yield return new WaitForSeconds(6);
    	StartCoroutine(ShowSkipButtonGradually(2));

    }



    IEnumerator LoadScene()
    {
        // yield return null;

        while (!asyncOperation.isDone)
        {
	        asyncOperation.allowSceneActivation = false;
            // if (asyncOperation.progress >= 0.9f)
            // {
            //     asyncOperation.allowSceneActivation = true;
            // }

            yield return null;
        }
    }


    // public static IEnumerator ShowGameOverAnim()
    // {
    // 	_blackScreen.SetActive(true);
    // 	StartCoroutine(ShowVideoGradually(_videoGameOver, 1, 0));
    // }





}

