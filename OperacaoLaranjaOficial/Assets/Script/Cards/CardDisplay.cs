﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardDisplay : MonoBehaviour
{
    [Tooltip("Dados iniciais do card")]
    [SerializeField] CardScriptable cardInfo;
    [Header("Card Dados Jogo")]
    [Tooltip("Construtor dos Cards no jogo com dados provenientes do CardScriptable")]
    public Card cardGame;
    [Header("Card Status ")]
    [Tooltip("Indentifica se a carta está ativa ou desativa")]public bool deadCard;
    [Tooltip("Renderer da carta")] [SerializeField]SpriteRenderer spriteRenderer;
    [Tooltip("Texto da influencia da carta")] [SerializeField]TextMeshPro textValueInfluence;
    [Tooltip("Texto da influencia da carta")] [SerializeField] int _cardOrderDisplayNumber;
    public int CardOrderDisplay
    {
        get { return _cardOrderDisplayNumber; }
        set { _cardOrderDisplayNumber = value; }
    }
    private void Start()
    {

        
    }


    public void ConfigCardDisplay(CardScriptable newCard)
    {
        cardInfo = newCard;
        spriteRenderer = GetComponent<SpriteRenderer>();
        textValueInfluence = GetComponentInChildren<TextMeshPro>();
        _cardOrderDisplayNumber = spriteRenderer.sortingOrder;
        cardGame = new Card(cardInfo.name, cardInfo.imageCard, cardInfo.baseCard, cardInfo.typeCard.ToString(),
                            Random.Range(cardInfo.influence[0], cardInfo.influence[1]+1), Random.Range(cardInfo.influenceEffect[0], cardInfo.influenceEffect[1]+1));
        spriteRenderer.sprite = cardGame.SpriteCard;
        if (cardGame.TypeCard == "Effect" || cardGame.TypeCard == "EffectAlly")
        {
            textValueInfluence.text = "" + cardGame.InfluenceEffect;
        }
        else
        {
            textValueInfluence.text = "" + cardGame.Influence;
        }
        
    }
    public void GetDamage(int influenceDamage)
    {
        cardGame.Influence -= influenceDamage;
        textValueInfluence.text = ""+cardGame.Influence;
        if (cardGame.Influence <= 0)
        {
            cardGame.Influence = 0;
            EndCard();
        }
    }
    public void GainLife(int influenceEffect) 
    {
        cardGame.Influence += influenceEffect;
        textValueInfluence.text = "" + cardGame.Influence;

    }

    void EndCard()
    {
        deadCard = true;
    }

    [System.Serializable]
    public class Card{

        [SerializeField] string cardName;
        [SerializeField] Sprite imageCard;
        [SerializeField] Sprite baseCard;
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
        public Sprite BaseCard
        {
            get { return baseCard; }
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

        public Card(string newCardName, Sprite newSprite,Sprite newBaseCard, string newTypeCard, int newInfluence, int newInfluenceEffect)
        {
            cardName = newCardName;
            imageCard = newSprite;
            baseCard = newBaseCard;
            typeCard = newTypeCard;
            influence = newInfluence;
            influenceEffect = newInfluenceEffect;
        }



    }
}
