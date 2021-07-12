using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPollaroid : MonoBehaviour
{

    [SerializeField] DeckCardController cardDeck;

    public void DecreaseNumberOfCards(int amount)
    {
        cardDeck.DecreaseCards(amount);
    }
}
