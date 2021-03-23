using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
	public Image image;
	public bool test;
	private AsyncOperation asyncOperation;

    // Start is called before the first frame update
    void Start()
    {
    	if(!test)
    	{

        StartCoroutine("LoadScene");
        StartCoroutine("LoadSceneAux");
    	}
    	else
    	{
    		SceneManager.LoadScene(0);
    	}
    }


    IEnumerator LoadScene()
    {
    	// yield return null;
    	yield return new WaitForSeconds(1f);
        asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            else
            {
	        	image.fillAmount = asyncOperation.progress;
            }

            yield return null;
        }
    }

    IEnumerator LoadSceneAux()
    {
    	yield return new WaitForSeconds(1.1f);
    	float aux=0;
    	while(true)
    	{

	        if (asyncOperation.progress >= 0.9f)
	        {
	        	aux += UnityEngine.Random.Range(0,0.001f);
	        	// print(aux);
	        	image.fillAmount = asyncOperation.progress+aux;
	        }

            yield return null;
    	}

    }

}
