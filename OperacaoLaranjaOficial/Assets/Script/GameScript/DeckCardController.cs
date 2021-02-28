using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCardController : MonoBehaviour
{

    [Tooltip("Cartas que estarão presentes na fase")]public CardScriptable[] CardDeck;
    int randomNumberCard;
    public GameObject CardObject, Position;

    private static List<Dictionary<Transform,bool>> allPositions = new List<Dictionary<Transform,bool>>();

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


    // // Test
     public void sortearNovaCarta()
     {
         randomNumberCard = Random.Range(0, CardDeck.Length);
         Transform pos = FindPosition(0);
         GameObject cardTmp = Instantiate(CardObject) as GameObject;
         CardObject.GetComponent<CardDisplay>().ConfigCardDisplay(CardDeck[randomNumberCard]);
         cardTmp.transform.position = pos.position;
         cardTmp.transform.eulerAngles = pos.eulerAngles;
         SetEmptyOrNot(0, pos, false);
    }

    /*
    public void sortearNovaCarta()
    {
        randomNumberCard = Random.Range(0, CardDeck.Length);
        Instantiate(CardObject,Vector3.zero,Quaternion.identity);
        CardObject.GetComponent<CardDisplay>().ConfigCardDisplay(CardDeck[randomNumberCard]);
    }
    */

    // Encontra uma posição vazia, caso não tenha retorna null
    public static Transform FindPosition(int nivel)
    {
        foreach(KeyValuePair<Transform,bool> trs in allPositions[nivel])
        {
            if(trs.Value)
            {
                return trs.Key;
            }
        }
        return null;
    }


    // Percorre todos os gameObjects e adiciona o Transform a lista allPositions
    private void SetAllPositions()
    {
        int num = Position.transform.childCount;
        for(int c=0; c<num; c++)
        {
            GameObject gobTmp = Position.transform.GetChild(c).gameObject;
            int num2 = gobTmp.transform.childCount;
            Dictionary<Transform, bool> dictTmp = new Dictionary<Transform, bool>();
            for(int b=0; b<num2; b++)
            {
                Transform trsTmp = gobTmp.transform.GetChild(b).transform;
                dictTmp.Add(trsTmp, true);
            }
            allPositions.Add(dictTmp);
        }
    }


    // Define se um slot está em uso ou não
    // true = vazio
    public static void SetEmptyOrNot(int nivel, Transform pos, bool v)
    {
        allPositions[nivel][pos] = v;
    }
}
