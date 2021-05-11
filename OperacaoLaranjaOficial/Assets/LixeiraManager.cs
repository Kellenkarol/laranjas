using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LixeiraManager : MonoBehaviour
{
	public Sprite OnOver, OnOut;
	private SpriteRenderer mySpt;

	bool enter;
	GameObject card;


	void Start()
	{
		mySpt = GetComponent<SpriteRenderer>();
	}


	void Update()
	{
		if(enter)
		{
    		// mySpt.sprite = OnOver;
			if(!Input.GetMouseButton(0))
			{
				Destroy(card);
			}
		}
		else
		{
    		// mySpt.sprite = OnOut;
		}
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
    	if(col.gameObject.tag == "Card")
    	{
    		mySpt.sprite = OnOver;
    		card = col.gameObject;
    		enter = true;
    		print("Enter");
    	}
    }

    public void OnTriggerExit2D(Collider2D col)
    {
    	if(col.gameObject.tag == "Card")
    	{
    		mySpt.sprite = OnOut;
    		enter = false;
    		print("Out");
    	}
    }
}
