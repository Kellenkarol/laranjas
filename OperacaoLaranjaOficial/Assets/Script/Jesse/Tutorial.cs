using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	int step=-1;
	bool click, canClick=true, t4end=true, t5end=true;
	public static bool TutorialOn;
	public GameObject t1, t1Out, t2, t2Out, t3, t3Out, t4, t4Out, t5, t5Out, 
	full_tc, t4Seta1, t4Seta2, t4Texto1, t4Texto2, t4Destaque1, t4Destaque2, 
	t4Papel, t4PapelTexto1, t4PapelTexto2, t4PapelTexto3, t4PapelTexto4,
	t5PapelTexto5, t5PapelTexto6, t5PapelTexto7, t4CardYellow, t4CardBlue, 
	t4CardRed, DeckSize, InfluenceSize; 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    	if(TutorialOn)
    	{

			click = false;
	        if(Input.GetMouseButtonDown(0) || step == -1)
	        {
		    	click = true;
	        	if(t4end&&t5end)
	        	{
		        	step++;
		        	if(step == 0)
		        	{
		        		full_tc.SetActive(true);
		        		t1.SetActive(true);
		        	}
		        	else if(step == 1)
		        	{
		        		t1.SetActive(false);
		        		t1Out.SetActive(true);
		        		t2.SetActive(true);
		        	}
		        	else if(step == 2)
		        	{
		        		t1Out.SetActive(false);
		        		t2.SetActive(false);
		        		t2Out.SetActive(true);
		        		t3.SetActive(true);
		        	}
		        	else if(step == 3)
		        	{
		        		t2Out.SetActive(false);
		        		t3.SetActive(false);
		        		t3Out.SetActive(true);
				        ShowT4();
		        		// t4.SetActive(true);
		        	}
		        	else if(step == 4)
		        	{
		        		t3Out.SetActive(false);
		        		t4.SetActive(false);
		        		t4Out.SetActive(true);
		        		ShowT5();
		        	}
	        	}
	        }
    	}
    }
    public static void StartTutorial()
    {
    	float waitAux=0;
    	while(waitAux < 3){waitAux += Time.deltaTime;}
    	print(waitAux);
    	if(waitAux >= 3){TutorialOn = true;}
    }

    public static void FinishTutorial()
    {
		
    	TutorialOn = false;
    }

    public void ShowT4()
    {
    	t4end = false;
    	StartCoroutine("_ShowT4");
    }

    public void ShowT5()
    {
    	t5end = false;
    	StartCoroutine("_ShowT5");
    }

    private IEnumerator _ShowT4()
    {
    	t4.SetActive(true);
		t4Papel.SetActive(true);
		yield return Wait(0.5f);
		t4PapelTexto1.SetActive(true);
		while(!click){ yield return null;}
		click = false;

		t4Texto1.SetActive(true);
		t4Seta1.SetActive(true);
		t4Destaque1.SetActive(true);
		InfluenceSize.SetActive(true);
		while(!click){ yield return null;}
		click = false;

		t4CardYellow.SetActive(true);
		yield return Wait(0.75f);
		t4PapelTexto2.SetActive(true);
		while(!click){ yield return null;}
		click = false;

		t4CardBlue.SetActive(true);
		yield return Wait(0.75f);
		t4PapelTexto3.SetActive(true);
		while(!click){ yield return null;}
		click = false;

		t4CardRed.SetActive(true);
		yield return Wait(0.75f);
		t4PapelTexto4.SetActive(true);
		while(!click){ yield return null;}
		click = false;

		t4Texto2.SetActive(true);
		t4Seta2.SetActive(true);
		t4Destaque2.SetActive(true);
		DeckSize.SetActive(true);
    	t4end = true;
    }

    private IEnumerator _ShowT5()
    {
        		
		yield return Wait(0.5f);
		click = false;
		t5.SetActive(true);
		t5PapelTexto5.SetActive(true);

		while(!click){ yield return null;}
		click = false;
		t5PapelTexto6.SetActive(true);

		while(!click){ yield return null;}
		click = false;
		t5PapelTexto7.SetActive(true);

    	t5end = true;
    	FinishTutorial();
		//Precisa achar um método para fechar o tutorial e reiniciar caso necessário
		//gameObject.SetActive(false);

	}    

    private WaitForSeconds Wait(float time)
    {
    	return new WaitForSeconds(time);	
    }

}
