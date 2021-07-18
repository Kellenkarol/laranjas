using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	static int step=-1;
	bool click, t3end=true, t10end=true;
	static float waitAux;
	public static bool TutorialOn;
	public GameObject[] Tutoriais;
	public GameObject[] TutoriaisOut;
	public GameObject full_tc, full_tcOut, 
	t3PapelTexto1, t3PapelTexto2, t3PapelTexto3,
	t3CardYellow, t3CardBlue, 
	t3CardRed, skipButton, toquePC,
	t10PapelTexto1, t10PapelTexto2, t10CardDelator;

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
	        if(Input.GetMouseButtonDown(0) || step == -1 || step == 9)
	        {
		    	click = true;
	        	if(t3end&&t10end)
	        	{
		        	step++;

		        	// print("Step: "+step);
		        	if(step == 0)
		        	{
						// t5Out.SetActive(false);
						full_tcOut.SetActive(false);
		        		full_tc.SetActive(true);
		        		// t1.SetActive(true);
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
		        		if(step == 3)
		        		{
		        			HideT3();
		        		}
		        		else if(step >= 2)
		        		{
			        		T(1, step-2, false);
		        		}
		        		T(1, step-1, true);
		        		T(0, step-1, false);
		        		T(0, step, true);
		        	}
		        	else if(step == 10)
		        	{
		        		ShowT10();
		        	}
		        	else if(step == 11)
		        	{
		        		T(1, 9, true);
		        		T(0, 9, false);
						FinishTutorial();
		        	}
		        	else
		        	{
		        		if(step != 12)
		        		{
			        		T(1, step-1, true);
			        		T(0, step-1, false);
		        		}
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
		    //     		ShowT10();
		    //     	}
		    //     	else if(step == 5)
		    //     	{
		    //     		HideT10();
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
			t10end = t3end = true;
    		// step = 8;
    	}
    }
    public static void StartTutorial()
    {
		step = -1;
    	TutorialOn = true;
    }

    public static void StartTutorial2()
    {
		step = 9;
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
		// t3Papel.SetActive(false);
		t3PapelTexto1.SetActive(false);
		t3CardYellow.SetActive(false);
		t3PapelTexto2.SetActive(false);
		t3CardBlue.SetActive(false);
		t3PapelTexto3.SetActive(false);
		t3CardRed.SetActive(false);
    }

    public void HideT10()
    {
		t10PapelTexto1.SetActive(false);
		t10PapelTexto2.SetActive(false);
		t10CardDelator.SetActive(false);
	}

    public void ShowT10()
    {
    	t10end = false;
    	StartCoroutine("_ShowT10");
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

    private IEnumerator _ShowT10()
    {
		yield return Wait(1.6f);
		full_tc.SetActive(true);
		skipButton.SetActive(true);
		toquePC.SetActive(true);
		yield return Wait(0.3f);
    	Tutoriais[9].SetActive(true);
		yield return Wait(0.5f);
		t10PapelTexto1.SetActive(true);
		yield return Wait(0.8f);
		t10CardDelator.SetActive(true);
		yield return Wait(1.75f);
		t10PapelTexto2.SetActive(true);
		// while(!click){ yield return null;}
		// click = false;

		// while(!click){ yield return null;}
		// click = false;

    	t10end = true;
	}    


    private WaitForSeconds Wait(float time)
    {
    	return new WaitForSeconds(time);	
    }    


    private IEnumerator _FinishTutorial()
    {
		full_tc.SetActive(false);
		full_tcOut.SetActive(true);
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
    	print("BUUUUUUUUUUUUUUUUUUG");
    	StopAllCoroutines();
    	for(int c=0; c<Tutoriais.Length; c++)
    	{
			Tutoriais[c].SetActive(false);
			TutoriaisOut[c].SetActive(false);
    	}

		// Tutoriais[].SetActive(false);

		// Tutoriais[].SetActive(false);

		// Tutoriais[].SetActive(false);

		// Tutoriais[].SetActive(false);



		// t1Out.SetActive(false);

		// t2Out.SetActive(false);

		// t3Out.SetActive(false);

		// t3Out.SetActive(false);

		// t5Out.SetActive(false);


    	HideT3();
    	HideT10();
		full_tc.SetActive(false);
		full_tcOut.SetActive(true);
		TutorialOn = false;
		toquePC.SetActive(false);
		skipButton.SetActive(false);
    }

  

}
