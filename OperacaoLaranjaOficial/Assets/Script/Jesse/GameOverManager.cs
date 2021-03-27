using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
	public GameObject GameOver, BlackScreenIn, BlackScreenOut;
    public Camera MainCamera, SecondCamera;
    public CameraMovement CameraMovementScript;
    [HideInInspector]
    public bool IsActive;

    // Start is called before the first frame update
    void Start()
    {
        // ShowGameOverAnim();
    }


    // Função principal que chama a animação do GameOver
    public void ShowGameOverAnim()
    {
        if(!IsActive)
        {
            IsActive = true;
            StartCoroutine("StartGameOverAnim");
        }
    }


    //  Encerra a animação do GameOver -----
    public void FinishAnim()
    {
        StartCoroutine("FinishGameOverAnim");
    }


    private IEnumerator StartGameOverAnim()
    {
        yield return new WaitForSeconds(1);
        GameOver.SetActive(false);
        GameOver.SetActive(true);
        GameOver.transform.position = Camera.main.transform.position + new Vector3(-13.34f,-25.9f,20);
        BlackScreenIn.SetActive(false);
        BlackScreenOut.SetActive(false);
        yield return null;
    }


    private IEnumerator FinishGameOverAnim()
    {
        BlackScreenIn.SetActive(true);
        yield return new WaitForSeconds(1f);
        CameraMovementScript.SetPosition(0);
        BlackScreenOut.SetActive(true);
        BlackScreenIn.SetActive(false);
        GameOver.SetActive(false);
        yield return new WaitForSeconds(1f);
        BlackScreenOut.SetActive(false);
        IsActive = false;
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
