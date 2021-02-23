using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    [Tooltip("Dados iniciais do card")]
    [SerializeField] CardScriptable cardInfo;
    [Header("Card Dados Jogo")]
    [Tooltip("Construtor dos Cards no jogo com dados provenientes do CardScriptable")]
    public Card cardGame;
    [Header("Card Status ")]
    [Tooltip("Indentifica se a carta está ativa ou desativa")]public bool deadCard;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        

    }


    public void ConfigCardDisplay(CardScriptable newCard)
    {
        cardInfo = newCard;
        spriteRenderer = GetComponent<SpriteRenderer>();
        cardGame = new Card(cardInfo.name, cardInfo.imageCard, cardInfo.typeCard.ToString(), cardInfo.influence, cardInfo.influenceEffect);
        spriteRenderer.sprite = cardGame.SpriteCard;
    }
    void GetDamage(int influenceDamage)
    {
        cardGame.Influence -= influenceDamage;
        if (cardGame.Influence <= 0)
        {
            cardGame.Influence = 0;
            EndCard();
        }
    }


    void EndCard()
    {
        deadCard = true;
    }

    [System.Serializable]
    public class Card{

        [SerializeField] string cardName;
        [SerializeField] Sprite imageCard;
        [SerializeField] string typeCard;
        [SerializeField] int influence;
        [SerializeField] int influenceEffect;

        public string Name
        {
            get { return cardName; }
        }

        public Sprite SpriteCard
        {
            get { return imageCard; }
        }
        public string TypeCard
        {
            get { return typeCard; }
        }
        public int Influence
        {
            get { return influence; }
            set { influence = value;}
        }
        public int InfluenceEffect
        {
            get { return influenceEffect; }
        }

        public Card(string newCardName, Sprite newSprite, string newTypeCard, int newInfluence, int newInfluenceEffect)
        {
            cardName = newCardName;
            imageCard = newSprite;
            typeCard = newTypeCard;
            influence = newInfluence;
            influenceEffect = newInfluenceEffect;
        }



    }
}
