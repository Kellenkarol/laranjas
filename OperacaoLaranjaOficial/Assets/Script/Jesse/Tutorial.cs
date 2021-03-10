using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	int step=-2;
	bool click;
	public GameObject t1, t1Out, t2, t2Out, t3, t3Out, t4, t4Out, t5, t5Out, 
	full_tc, t4Seta1, t4Seta2, t4Texto1, t4Texto2, t4Destaque1, t4Destaque2, 
	t4Papel, t4PapelTexto1, t4PapelTexto2, t4PapelTexto3, t4PapelTexto4, 
	t4CardYellow, t4CardBlue, t4CardRed; 
    // Start is called before the first frame update
    void Start()
    {
        ShowT4();
    }

    // Update is called once per frame
    void Update()
    {
    	click = false;
        if(Input.GetMouseButtonDown(0))
        {
        	step++;
	    	click = true;
        }

        if(click)
        {
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
        		t2.SetActive(false);
        		t2Out.SetActive(true);
        		t3.SetActive(true);
        	}
        	else if(step == 3)
        	{
        		t3.SetActive(false);
        		t3Out.SetActive(true);
        		t4.SetActive(true);
        	}
        	else if(step == 4)
        	{
        		t4.SetActive(false);
        		t4Out.SetActive(true);
        		t5.SetActive(true);
        	}
        }
    }

    public void ShowT4()
    {
    	StartCoroutine("_ShowT4");
    }

    private IEnumerator _ShowT4()
    {
		t4Papel.SetActive(true);
		yield return Wait(0.5f);
		t4PapelTexto1.SetActive(true);
		yield return Wait(1.4f);
		t4Texto1.SetActive(true);
		t4Seta1.SetActive(true);
		// t4Destaque1.SetActive(true);
		yield return Wait(1);
		t4CardYellow.SetActive(true);
		yield return Wait(0.5f);
		t4PapelTexto2.SetActive(true);
		yield return Wait(2);
		t4CardBlue.SetActive(true);
		yield return Wait(0.5f);
		t4PapelTexto3.SetActive(true);
		yield return Wait(1.5f);
		t4CardRed.SetActive(true);
		yield return Wait(0.5f);
		t4PapelTexto4.SetActive(true);
		yield return Wait(1);
		t4Texto2.SetActive(true);
		t4Seta2.SetActive(true);
		// t4Destaque2.SetActive(true);
    }

    private WaitForSeconds Wait(float time)
    {
    	return new WaitForSeconds(time);	
    }
}
