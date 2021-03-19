using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
	public GameObject BlackScreen;
	public VideoPlayer GameOver;
    public Camera MainCamera, SecondCamera;

    // Start is called before the first frame update
    void Start()
    {
        ShowGameOverAnim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowGameOverAnim()
    {
        StartCoroutine("StartGameOverAnim");
    }


    private IEnumerator StartGameOverAnim()
    {
        BlackScreen.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        MainCamera.enabled = false;
        SecondCamera.enabled = true;
        GameOver.gameObject.SetActive(true);
        StartCoroutine(ShowVideoGradually(GameOver, 0.5f, 0));
    }


    public void FinishAnim()
    {
        BlackScreen.SetActive(false);
        GameOver.gameObject.SetActive(false);
        MainCamera.enabled = true;
        SecondCamera.enabled = false;
    }




    private IEnumerator ShowVideoGradually(VideoPlayer video, float time, float delay)
    {
        float auxTime=0;
        yield return new WaitForSeconds(delay);
        while(auxTime <= time)
        {
            auxTime += Time.deltaTime;
            video.targetCameraAlpha = auxTime/time;
            yield return null;
        }
    }
}
