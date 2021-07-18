using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class CapitulosManager : MonoBehaviour
{

    public Camera camPrincipal, camSecundaria;
    public VideoPlayer[] videos;
    public SoundManager soundManager;
	public Button bt;
	public Image btBackText;
	public Text btText;
	public GameObject BlackScreenIn, BlackScreenOut;
	public GravacoesManager GM;
    [HideInInspector]
	public bool Skiped;
    [HideInInspector]
	public Coroutine CurrentCoroutine;

    public static int MaxLevel=0;


    private int CurrentLevel_;
    private CameraMovement camMove;
	private Color BacktextColor, textColor, bsColor;
	private bool IsPreview;


    // Start is called before the first frame update
    void Start()
    {
        camMove = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator ShowVideo(int CurrentLevel, bool preview=false)
    {
		print(CapitulosManager.MaxLevel+"   "+(CurrentLevel));
    	Skiped = false;
    	bool secondTime=false;
    	IsPreview = preview;
    	if (!IsPreview)
    	{
	    	GM.UnlockNew(CurrentLevel+1);
			bt.gameObject.SetActive(false); 

    	}
    	if(IsPreview || CapitulosManager.MaxLevel > CurrentLevel)
    	{
    		secondTime = true;
    		StartCoroutine(ShowSkipButtonGradually(3.5f));
    	}

		BlackScreenOut.SetActive(false);
		BlackScreenIn.SetActive(true);

    	// CurrentLevel-= 2;

    	CurrentLevel_ = CurrentLevel;
    	if (CurrentLevel_ == 0 || IsPreview){
	        soundManager.FadeInMenuSound();
    	}
    	else{
	        soundManager.FadeInGameplaySound();
    	}

        yield return new WaitForSeconds(1.4f);
        
        camPrincipal.enabled = false;  
        camSecundaria.enabled = true;
        videos[CurrentLevel].gameObject.SetActive(true);
        StartCoroutine(ShowVideoGradually(videos[CurrentLevel], 1));
        float lengthy = (float) videos[CurrentLevel].clip.length;
        if(!secondTime)
        {
	        StartCoroutine(ShowSkipButtonGradually(10));
        }
        yield return new WaitForSeconds(lengthy+1);
    	Skip();
    }

    public void Skip()
    {
        if(CurrentLevel_ == 0 || IsPreview){
	        soundManager.FadeOutMenuSound();
	    }
    	StartCoroutine(HideVideoGradually(videos[CurrentLevel_], 1));
    }

    public void IncrementMaxLevel()
    {
    	StartCoroutine(IncrementMaxLevel_());
    }

    public IEnumerator IncrementMaxLevel_()
    {
    	yield return new WaitForSeconds(0.5f);
    	MaxLevel++;
    }

    public void SetMaxLevel(int v)
    {
    	if(v >= CapitulosManager.MaxLevel)
    	{
	    	StartCoroutine(SetMaxLevel_(v));
    	}
    }

    public IEnumerator SetMaxLevel_(int v)
    {
    	yield return new WaitForSeconds(0.5f);
    	MaxLevel = v;
    }


    private IEnumerator HideVideoGradually(VideoPlayer video, float time)
    {
    	StartCoroutine(HideSkipButtonGradually(0));
    	float auxTime=0;

		float volumeSFX = ApplicationController.GetVolumeSFX();

    	while(auxTime <= time)
    	{
    		auxTime += Time.deltaTime;
    		video.targetCameraAlpha = 1-auxTime/time;
    		video.SetDirectAudioVolume(0, volumeSFX-auxTime/time*volumeSFX);
            yield return null;
    	}
        videos[CurrentLevel_].gameObject.SetActive(false);
        camPrincipal.enabled = true;  
        camSecundaria.enabled = false;
		BlackScreenIn.SetActive(false);
		BlackScreenOut.SetActive(true);
		bt.gameObject.SetActive(false);   
		yield return new WaitForSeconds(1.6f);
        if(!IsPreview){
	        if(CurrentLevel_ == 0){
	        	camMove.SetDestiny(2);
	            soundManager.SwitGamePlayAndMenu();
	        }
	    	else{
		        soundManager.FadeOutGameplaySound();
	    	}
        }

    	Skiped = true;
    	GM.ShowingVideo = false;
    	StopCoroutine(CurrentCoroutine);
    	CurrentCoroutine = null;
    }


    private IEnumerator ShowVideoGradually(VideoPlayer video, float time)
    {
    	float auxTime=0;
		float volumeSFX = ApplicationController.GetVolumeSFX();
		if(volumeSFX < 0.25f){volumeSFX = 0.25f;}

    	while(auxTime <= time)
    	{
    		auxTime += Time.deltaTime;
    		video.targetCameraAlpha = auxTime/time;
    		video.SetDirectAudioVolume(0, auxTime/time*volumeSFX);
            yield return null;
    	}
    }



    private IEnumerator ShowSkipButtonGradually(float WaitTime)
    {
    	// print("DEBUG HERE");
    	// print("WaitTime:"+WaitTime);
    	yield return new WaitForSeconds(WaitTime);
    	float auxTime=0;
		btText.gameObject.SetActive(false); 
    	textColor = btText.color;
    	textColor = new Color(textColor[0],textColor[1],textColor[2],0);
    	btText.color = textColor; 
    	BacktextColor = btBackText.color;
    	BacktextColor = new Color(BacktextColor[0],BacktextColor[1],BacktextColor[2],0);
    	btBackText.color = BacktextColor;
		bt.gameObject.SetActive(true);   

		btText.gameObject.SetActive(true); 

    	while(auxTime <= 2)
    	{
    		auxTime += Time.deltaTime;
    		btText.color = textColor + new Color(0,0,0,auxTime/2);
    		btBackText.color = BacktextColor + new Color(0,0,0,auxTime/2*0.55f);
    		yield return null;
    	}
    }



    private IEnumerator HideSkipButtonGradually(float WaitTime=0f)
    {
    	// print("DEBUG HERE");
    	// print("WaitTime:"+WaitTime);
    	yield return new WaitForSeconds(WaitTime);
    	float auxTime=0;
		bt.gameObject.SetActive(true);   
		btText.gameObject.SetActive(true); 
    	textColor = btText.color;
    	textColor = new Color(textColor[0],textColor[1],textColor[2],1);
    	btText.color = textColor;

    	BacktextColor = btBackText.color;
    	BacktextColor = new Color(BacktextColor[0],BacktextColor[1],BacktextColor[2],0.55f);
    	btBackText.color = BacktextColor;    	

    	while(auxTime <= 1)
    	{
    		auxTime += Time.deltaTime;
    		btText.color = textColor - new Color(0,0,0,auxTime);
    		btBackText.color = BacktextColor - new Color(0,0,0,auxTime*0.55f);
    		yield return null;
    	}
    }




}
