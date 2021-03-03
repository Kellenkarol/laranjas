using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public GameObject[] bolosCarta;
    // Start is called before the first frame update
    public void inicializarFase(int fase)
    {
        if (fase >= 0)
        {
            bolosCarta[fase].GetComponent<DeckCardController>().enabled = true;
            bolosCarta[fase].GetComponent<DeckCardController>().InicializarDeck();
        }

    }

    public void desativarFase()
    {
        foreach(GameObject bolodeCartas in bolosCarta)
        {
            if (bolodeCartas.GetComponent<DeckCardController>().enabled)
            {
                bolodeCartas.GetComponent<DeckCardController>().enabled = false;
            }
        }
    }
}
