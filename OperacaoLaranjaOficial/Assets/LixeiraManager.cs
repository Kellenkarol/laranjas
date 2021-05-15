using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LixeiraManager : MonoBehaviour
{
	public Sprite[] spriteFill;
	private SpriteRenderer mySpt;
	private int numOfMovements = 0;

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
				enter = false;
				card.GetComponent<CardDisplay>().EndCard();
				// Destroy(card);
				numOfMovements = 0;
	    		mySpt.sprite = spriteFill[0];
			}
		}
		else
		{
    		// mySpt.sprite = OnOut;
		}


	}

    public void OnTriggerEnter2D(Collider2D col)
    {
    	if(col.gameObject.tag == "Card" && numOfMovements >= 4)
    	{
    		mySpt.sprite = spriteFill[5];
    		card = col.gameObject;
    		enter = true;
    		print("Enter");
    	}
    }


    public void OnTriggerExit2D(Collider2D col)
    {
    	if(col.gameObject.tag == "Card" && numOfMovements >= 4)
    	{
    		mySpt.sprite = spriteFill[4];
    		enter = false;
    		print("Out");
    	}
    }


    public void UpdateMovements()
    {
    	numOfMovements++;
		mySpt.sprite = spriteFill[numOfMovements <= 4 ? numOfMovements : 4];
    }

}
