using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	int step=-2;
	bool click;
	public GameObject t1, t1Out, t2, t2Out, t3, t3Out, full_tc; 
    // Start is called before the first frame update
    void Start()
    {
        
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
        		t1.SetActive(true);
        	}
        	else if(step == 1)
        	{
        		t1.SetActive(false);
        		t1Out.SetActive(true);
        		// full_tc.SetActive(true);
        		t2.SetActive(true);
        	}
        	else if(step == 2)
        	{
        		t2.SetActive(false);
        		t2Out.SetActive(true);
        		t3.SetActive(true);
        	}
        }
    }
}
