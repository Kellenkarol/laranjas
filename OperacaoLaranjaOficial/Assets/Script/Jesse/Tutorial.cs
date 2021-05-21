﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	static int step=-1;
	bool click, t3end=true, t5end=true;
	static float waitAux;
	public static bool TutorialOn;
	public GameObject[] Tutoriais;
	public GameObject[] TutoriaisOut;
	public GameObject t1, t1Out, t2, t2Out, t3, t3Out, t4, t4Out, t5, t5Out, 
	full_tc, full_tcOut, t3Seta1, t3Seta2, t3Texto1, t3Texto2, t3Destaque1, t3Destaque2, 
	t3Papel, t3PapelTexto1, t3PapelTexto2, t3PapelTexto3,
	t5Papel, t5PapelTexto5, t5PapelTexto6, t5PapelTexto7, t5PapelTexto8, t3CardYellow, t3CardBlue, 
	t3CardRed, DeckSize, InfluenceSize, skipButton, toquePC; 

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
	        	if(t3end&&t5end)
	        	{
		        	step++;

		        	// print("Step: "+step);
		        	if(step == 0)
		        	{
						// t5Out.SetActive(false);
						full_tcOut.SetActive(false);
		        		full_tc.SetActive(true);
		        		t1.SetActive(true);
		        		T(0, 0, true);
		        		skipButton.SetActive(true);
		        		toquePC.SetActive(true);
		        	}
		        	else if(step == 2)
		        	{
		        		T(1, step-2, false);
		        		T(1, step-1, true);
		        		T(0, step-1, false);
		        		// T(0, step, true);
		        		print("DEBUG!!!!");
		        		ShowT3();
		        	}
		        	else if(step<9)
		        	{
		        		if(step >= 2)
		        		{
			        		T(1, step-2, false);
		        		}
		        		T(1, step-1, true);
		        		T(0, step-1, false);
		        		T(0, step, true);

		        	}
		        	else
		        	{
		        		T(1, step-1, true);
		        		T(0, step-1, false);
						full_tc.SetActive(false);
						full_tcOut.SetActive(true);
						FinishTutorial();
		        	}
		    //     	else if(step == 1)
		    //     	{
		    //     		T(0, 0, false);
		    //     		T(1, 0, true);
		    //     		T(0, 1, true);
		    //     		// t1.SetActive(false);
		    //     		// t1Out.SetActive(true);
		    //     		// t2.SetActive(true);
		    //     	}
		    //     	else if(step == 2)
		    //     	{
		    //     		T(1, 0, false);
		    //     		T(0, 1, false);
		    //     		T(1, 1, true);
		    //     		T(0, 2, true);
		        	
		    //     		// t1Out.SetActive(false);
		    //     		// t2.SetActive(false);
		    //     		// t2Out.SetActive(true);
		    //     		// t3.SetActive(true);
		    //     	}
		    //     	else if(step == 3)
		    //     	{
		    //     		T(1, 1, false);
		    //     		T(0, 2, false);
		    //     		T(1, 2, true);
		    //     		T(0, 3, true);
		        	
		    //     		// t2Out.SetActive(false);
		    //     		// t3.SetActive(false);
		    //     		// t3Out.SetActive(true);
				  //       ShowT4();
		    //     		// t3.SetActive(true);
		    //     	}
		    //     	else if(step == 4)
		    //     	{
		    //     		T(1, 2, false);
		    //     		T(0, 3, false);
		    //     		T(1, 3, true);
		        	
		    //     		// t3Out.SetActive(false);
		    //     		// t3.SetActive(false);
		    //     		// t3Out.SetActive(true);
		    //     		HideT4();
		    //     		ShowT5();
		    //     	}
		    //     	else if(step == 5)
		    //     	{
		    //     		HideT5();
						// t3Out.SetActive(false);
						// full_tc.SetActive(false);
						// t5.SetActive(false);
						// t5Out.SetActive(true);
						// full_tcOut.SetActive(true);
						// FinishTutorial();
		    //     	}
	        	}
	        }
    	}
    	else
    	{
			t5end = t3end = true;
    		step = -1;
    	}
    }
    public static void StartTutorial()
    {
    	TutorialOn = true;
    }

    public void FinishTutorial()
    {
    	StartCoroutine("_FinishTutorial");
    }

    public void ShowT3()
    {
    	t3end = false;
    	StartCoroutine("_ShowT3");
    }

    private void T(int tp, int n, bool v)
    {
    	if(tp == 0)
    	{
			Tutoriais[n].SetActive(v);
    	}
    	else
    	{
    		TutoriaisOut[n].SetActive(v);
    	}
    }

    public void HideT3()
    {
		t3Papel.SetActive(false);
		t3PapelTexto1.SetActive(false);
		t3CardYellow.SetActive(false);
		t3PapelTexto2.SetActive(false);
		t3CardBlue.SetActive(false);
		t3PapelTexto3.SetActive(false);
		t3CardRed.SetActive(false);
    }

    public void HideT5()
    {
		t5PapelTexto5.SetActive(false);
		t5PapelTexto6.SetActive(false);
		t5PapelTexto7.SetActive(false);
	}

    public void ShowT5()
    {
    	t5end = false;
    	StartCoroutine("_ShowT5");
    }

    private IEnumerator _ShowT3()
    {
    	Tutoriais[2].SetActive(true);
		// t3Papel.SetActive(true);
		t3CardRed.SetActive(true);
		yield return Wait(0.5f);
		t3PapelTexto1.SetActive(true);
		while(!click){ yield return null;}
		click = false;

		t3CardBlue.SetActive(true);
		yield return Wait(0.75f);
		t3PapelTexto2.SetActive(true);
		while(!click){ yield return null;}
		click = false;

		t3CardYellow.SetActive(true);
		yield return Wait(0.75f);
		t3PapelTexto3.SetActive(true);
		// while(!click){ yield return null;}
		// click = false;

    	t3end = true;
    }

    private IEnumerator _ShowT5()
    {
		yield return Wait(0.5f);
		click = false;
		t5.SetActive(true);
        t5Papel.SetActive(true);
		yield return Wait(0.5f);
		t5PapelTexto5.SetActive(true);

		while(!click){ yield return null;}
		click = false;
		t5PapelTexto6.SetActive(true);

		while(!click){ yield return null;}
		click = false;
		t5PapelTexto7.SetActive(true);

		while(!click){ yield return null;}
		click = false;
		t5PapelTexto8.SetActive(true);

    	t5end = true;
	}    


    private WaitForSeconds Wait(float time)
    {
    	return new WaitForSeconds(time);	
    }    


    private IEnumerator _FinishTutorial()
    {
		toquePC.SetActive(false);
		skipButton.SetActive(false);
    	// yield return new WaitForSeconds(0.5f);
    	TutorialOn = false;
    	step = -1;
    	yield return null;
    	// print("Finished Tutorial");
    }    


    public void Skip()
    {
    	StopAllCoroutines();
		t1.SetActive(false);

		t2.SetActive(false);

		t3.SetActive(false);

		t3.SetActive(false);

		t5.SetActive(false);



		t1Out.SetActive(false);

		t2Out.SetActive(false);

		t3Out.SetActive(false);

		t3Out.SetActive(false);

		t5Out.SetActive(false);


    	HideT3();
    	HideT5();
		full_tc.SetActive(false);
		full_tcOut.SetActive(true);
		TutorialOn = false;
		toquePC.SetActive(false);
		skipButton.SetActive(false);
    }

  

}
