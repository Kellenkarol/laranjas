using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardArrest : MonoBehaviour
{
	public GameObject Card, Anim; //Card apenas para teste
	public AudioSource audio;
	public bool Test; 
	private bool Finished=true;

	private GameObject cardTmp, animTmp; 
	private SpriteRenderer img;
	private Color imgColor;


    // Start is called before the first frame update
    // void Start()
    // {
    
    // }


    void Update()
    {
    	//Teste
    	if(Test && Finished)
    	{
	        // StartCoroutine(_()); 
    		Finished = false;
			Test = false;
	    	Arrest(Camera.main, Card);
    	}
    	else
    	{
			Test = false;
    	}

    }


    // Prende a carta --------------------------------------------------------------------------------
    public void Arrest(Camera currentCamera, GameObject card)
    {
    	audio.Play();
    	cardTmp 						= Instantiate(card) as GameObject;
    	animTmp 						= Instantiate(Anim) as GameObject;
    	img 							= cardTmp.GetComponent<SpriteRenderer>();
    	StartCoroutine("CardFadeIn");
    	cardTmp.transform.position 		= currentCamera.transform.position+new Vector3(0,0,20);
    	// animTmp.transform.position 		= currentCamera.transform.position;
    	animTmp.transform.localPosition 		= currentCamera.transform.position+new Vector3(-12.92f,9.41f,40);
    	cardTmp.transform.localScale 	= new Vector3(0.58f,0.58f,0.58f);
    	StartCoroutine(DestroyCardAndAnim(cardTmp, animTmp));
    }


    //Teste -------------------------------
    private IEnumerator _()
    {
    	yield return new WaitForSeconds(2);
    	Arrest(Camera.main, Card);
    }


    // Faz a carta aparecer aos poucos -----------------------------
    private IEnumerator CardFadeIn()
    {
    	float auxTime=0;
    	imgColor = img.color;
    	imgColor = new Color(imgColor[0],imgColor[1],imgColor[2],0);

    	while(auxTime<=0.333f)
    	{
    		auxTime += Time.deltaTime;
    		img.color = imgColor + new Color(0,0,0,auxTime*3.003f);
    		yield return null;
    	}

    }


    // Faz a carta desaparecer aos poucos e depois a destroi junto da animação
    private IEnumerator DestroyCardAndAnim(GameObject card, GameObject anim)
    {
    	yield return new WaitForSeconds(2.5f);
    	imgColor = img.color;
    	float auxTime=0;
    	while(auxTime<=0.5f)
    	{
    		auxTime += Time.deltaTime;
    		img.color = imgColor - new Color(0,0,0,auxTime*2);
    		yield return null;
    	}
    	Destroy(card);
    	Destroy(anim);
		Finished = true;
    }

}
