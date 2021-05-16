using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CardDisplay : MonoBehaviour
{
    [Tooltip("Dados iniciais do card")]
    [SerializeField] CardScriptable cardInfo;
    [Header("Card Dados Jogo")]
    [Tooltip("Construtor dos Cards no jogo com dados provenientes do CardScriptable")]
    public Card cardGame;
    [Header("Card Status ")]
    [Tooltip("Indentifica se a carta está ativa ou desativa")]public bool deadCard;
    [SerializeField]bool cardSelected=false;
    [Tooltip("Renderer da carta")] [SerializeField]SpriteRenderer spriteRenderer;
    [Tooltip("Texto da influencia da carta")] [SerializeField]TextMeshPro textValueInfluence;
    [Tooltip("Texto da influencia da carta")] [SerializeField] int _cardOrderDisplayNumber;

    [Header("Card Dissolve")]
    [ColorUsage(true,true)]
    [SerializeField] Color colorInitial;
    [ColorUsage(true, true)]
    [SerializeField] Color colorFinal;
    float timerDissolve = 0;
    public AudioSource AttackerSound, mySound;
    Vector3 cardScaleDefaut;
    public int CardOrderDisplay
    {
        get { return _cardOrderDisplayNumber; }
        set { _cardOrderDisplayNumber = value; }
    }

    void Start()
    {
        mySound = GameObject.Find("SFX/"+cardGame.Name).GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (cardScaleDefaut.x!=0)
        {
            if (!cardSelected)
                transform.localScale = new Vector3(cardScaleDefaut.x, cardScaleDefaut.y, cardScaleDefaut.z);
            else
                transform.localScale = new Vector3(cardScaleDefaut.x - 0.04f, cardScaleDefaut.y - 0.04f, cardScaleDefaut.z - 0.04f);
        }


        if (deadCard)
        {
            timerDissolve += Mathf.Clamp01(Time.deltaTime/2);
            spriteRenderer.material.SetFloat("_dissolveAmount", timerDissolve);
            Color colorLerp = Color.Lerp(colorInitial, colorFinal, timerDissolve);
            spriteRenderer.material.SetColor("_Color", colorLerp); 
            if (timerDissolve >= 1)
            {
                textValueInfluence.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
        }
    }
    public void ConfigCardDisplay(CardScriptable newCard)
    {
        cardInfo = newCard;
        spriteRenderer = GetComponent<SpriteRenderer>();
        textValueInfluence = GetComponentInChildren<TextMeshPro>();
        _cardOrderDisplayNumber = spriteRenderer.sortingOrder;
        cardGame = new Card(cardInfo.name, cardInfo.imageCard, cardInfo.baseCard, cardInfo.typeCard.ToString(),
                            UnityEngine.Random.Range(cardInfo.influence[0], cardInfo.influence[1]+1), UnityEngine.Random.Range(cardInfo.influenceEffect[0], cardInfo.influenceEffect[1]+1));
        spriteRenderer.sprite = cardGame.SpriteCard;
        if (cardGame.TypeCard == "Effect" || cardGame.TypeCard == "EffectAlly")
        {
            textValueInfluence.text = "" + cardGame.InfluenceEffect;
        }
        else
        {
            textValueInfluence.text = "" + cardGame.Influence;
        }
        spriteRenderer.material.SetTexture("_MainText", cardInfo.imageCard.texture);
    }
    public void GetDamage(int influenceDamage, GameObject attacker, bool effect)
    {
        cardGame.Influence -= influenceDamage;
        textValueInfluence.text = ""+cardGame.Influence;
        DeselectCard();
        if (cardGame.Influence <= 0)
        {
            try 
            {
                AttackerSound = attacker.GetComponent<CardDisplay>().mySound;
            }
            catch(Exception e)
            {
                AttackerSound = attacker.GetComponent<MartaPollaroid>().mySound;
            }
            cardGame.Influence = 0;
            AttackerSound.Play();
            EndCard();
        }
        else if(effect)
        {
            mySound.Play();
        }

    }
    public void GainLife(int influenceEffect) 
    {
        cardGame.Influence += influenceEffect;
        textValueInfluence.text = "" + cardGame.Influence;

    }

    public void EndCard()
    {
        deadCard = true;        
        textValueInfluence.gameObject.transform.position = new Vector3(1000,1000,1000);
        // textValueInfluence.gameObject.SetActive(false);
    }
    
    public void SelectCard()
    {
        cardSelected = true;
        
    }
    public void DeselectCard()
    {

        cardSelected = false;
        
    }

    public void initScale()
    {
        cardScaleDefaut = transform.lossyScale;
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
