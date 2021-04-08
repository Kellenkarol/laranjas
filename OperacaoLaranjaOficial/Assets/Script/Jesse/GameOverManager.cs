using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
	public GameObject GameOver, BlackScreenIn, BlackScreenOut;
    public Camera MainCamera, SecondCamera;
    public CameraMovement CameraMovementScript;
    public AudioSource GameOverSound;
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
    public void FinishAnim(bool goMenu=true)
    {
        StartCoroutine(FinishGameOverAnim(goMenu));
    }


    private IEnumerator StartGameOverAnim()
    {
        yield return new WaitForSeconds(1);
        GameOverSound.Play();
        GameOver.SetActive(false);
        GameOver.SetActive(true);
        GameOver.transform.position = Camera.main.transform.position + new Vector3(-13.34f,-25.9f,20);
        BlackScreenIn.SetActive(false);
        BlackScreenOut.SetActive(false);
        yield return null;
    }


    private IEnumerator FinishGameOverAnim(bool goMenu)
    {
        BlackScreenIn.SetActive(true);
        yield return new WaitForSeconds(1f);
        if(goMenu)
        {
            CameraMovementScript.SetPosition(0);
        }
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

    public void FadeInOut()
    {
        StartCoroutine("_FadeInOut");
    }

    private IEnumerator _FadeInOut()
    {
        BlackScreenIn.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        BlackScreenOut.SetActive(true);
        BlackScreenIn.SetActive(false);
        yield return new WaitForSeconds(1f);
        BlackScreenOut.SetActive(false);
        
    }


}
