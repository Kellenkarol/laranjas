using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCardController : MonoBehaviour
{

    [Tooltip("Cartas que estarão presentes na fase")]public CardScriptable[] CardDeck;
    int randomNumberCard;
    public GameObject CardObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            sortearNovaCarta();

        }
    }

    public void sortearNovaCarta()
    {
        randomNumberCard = Random.Range(0, CardDeck.Length);
        Instantiate(CardObject,Vector3.zero,Quaternion.identity);
        Debug.Log(CardDeck[randomNumberCard]);
        CardObject.GetComponent<CardDisplay>().ConfigCardDisplay(CardDeck[randomNumberCard]);
    }
}
