using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class CardMovement : MonoBehaviour
{
    [SerializeField]bool clicado;
    Vector3 touchPosition;
    [SerializeField]GameObject cardObjectiveSelected;
    [SerializeField]List<GameObject> cardsObjective;
    [SerializeField]List<GameObject> cardPosition;
    public bool cardInicializada;
    [HideInInspector]public GameObject paiObjeto;
    [SerializeField] bool actionMouseClick;
    [SerializeField] DeckCardController deckCardActive;
    // Start is called before the first frame update
    void Start()
    {
        clicado = false;
        this.name = GetComponent<CardDisplay>().cardGame.Name;
        cardInicializada = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cardInicializada)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
               
                checkHitObject();
            }
            if (Input.GetMouseButtonUp(0) && clicado)
            {
                clicado = false;
                int num = this.gameObject.GetComponent<CardDisplay>().CardOrderDisplay;
                this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = num;
                this.gameObject.GetComponentInChildren<TextMeshPro>().sortingOrder = num+1;
                if (cardsObjective.Count > 0 && cardObjectiveSelected.GetComponent<CardDisplay>() != null)
                {
                    if(cardObjectiveSelected.GetComponent<CardDisplay>().cardGame.TypeCard == "Enemy" && this.GetComponent<CardDisplay>().cardGame.TypeCard == "Effect")
                    {
                        actionMouseClick = true;
                        cardObjectiveSelected.GetComponent<CardDisplay>().GetDamage(this.GetComponent<CardDisplay>().cardGame.InfluenceEffect, this.gameObject, true);
                        Destroy(this.gameObject);

                    }
                    if (cardObjectiveSelected.GetComponent<CardDisplay>().cardGame.TypeCard == "Enemy" && this.GetComponent<CardDisplay>().cardGame.TypeCard == "Ally")
                    {
                        if (this.gameObject.GetComponentInParent<SlotController>().SlotObjective.TypeSlot.ToString() == "Ally")
                        {
                            int cardEnemyInfluenceInitial = cardObjectiveSelected.GetComponent<CardDisplay>().cardGame.Influence;
                            if(cardEnemyInfluenceInitial> this.GetComponent<CardDisplay>().cardGame.Influence)
                            {
                                deckCardActive.DamageMarta(cardEnemyInfluenceInitial - this.GetComponent<CardDisplay>().cardGame.Influence);
                            }
                            cardObjectiveSelected.GetComponent<CardDisplay>().GetDamage(this.GetComponent<CardDisplay>().cardGame.Influence, this.gameObject, false);
                            this.GetComponent<CardDisplay>().GetDamage(cardEnemyInfluenceInitial, cardsObjective[0], false);
                        }
                    }
                }

                if (cardsObjective.Count > 0 && cardsObjective[0].GetComponent<MartaPollaroid>() != null)
                {
                    if (cardsObjective[0].GetComponent<MartaPollaroid>() && this.GetComponent<CardDisplay>().cardGame.TypeCard == "Enemy")
                    {
                        int cardEnemyInfluenceAttack =GetComponent<CardDisplay>().cardGame.Influence;
                        GetComponent<CardDisplay>().GetDamage(cardsObjective[0].GetComponent<MartaPollaroid>().InfluenciaMarta, cardsObjective[0], false);
                        deckCardActive.DamageMarta(cardEnemyInfluenceAttack);
                    }


                    if (this.GetComponent<CardDisplay>().cardGame.TypeCard == "EffectAlly")
                    {
                        if (this.gameObject.GetComponentInParent<SlotController>().SlotObjective.TypeSlot.ToString() == "Ally" || this.gameObject.GetComponentInParent<SlotController>().SlotObjective.TypeSlot.ToString()== "Bag")
                        {
                            actionMouseClick = true;
                            cardsObjective[0].GetComponent<MartaPollaroid>().AlterarInfluenciaMarta(this.GetComponent<CardDisplay>().cardGame.InfluenceEffect);
                            this.GetComponent<CardDisplay>().mySound.Play();
                            Destroy(this.gameObject);
                        }
                    }
                }

                if (cardPosition.Count>0 && !actionMouseClick)
                {
                    if (!(GetComponent<CardDisplay>().cardGame.TypeCard.ToString() == "Enemy"))
                    {
                        changeCardPosition();
                    }

                }
                if(!actionMouseClick)
                {
                    this.transform.position = paiObjeto.transform.position;
                    this.transform.eulerAngles = paiObjeto.transform.eulerAngles;
                }
                cardPosition.RemoveRange(0, cardPosition.Count);
                cardsObjective.RemoveRange(0, cardsObjective.Count);
            }
            if (clicado)
            {
                Vector3 positionCard = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                positionCard.z = 0;
                transform.position = positionCard;
            }
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, paiObjeto.transform.position, 40f * Time.deltaTime);
            if (Vector3.Distance(this.transform.position, paiObjeto.transform.position) == 0)
            {
                cardInicializada = true;
                ManagerGame.Instance.LockPlayerActive = false;
                this.transform.SetParent(paiObjeto.transform);
                GetComponent<CardDisplay>().initScale();
                //GetComponent<CardDisplay>().GetDamage(100);
                //checkTypeCard();
            }
        }
    }
    public void DefineDeckCard(DeckCardController deck)
    {
        deckCardActive = deck;
    }
    void changeCardPosition()
    {
        for (int i = 0; i < cardPosition.Count; i++)
        {
            if (cardPosition[i].gameObject.transform.childCount == 0)
            {
                ActionChangeCardPosition(cardPosition[i]);
                cardPosition.RemoveRange(0, cardPosition.Count);
                i = cardPosition.Count;
            }

        }
    }
    void ActionChangeCardPosition(GameObject cardPosition)
    {
        if (cardPosition.GetComponent<SlotController>().SlotObjective.TypeSlot == Slot.TypeSlotEnum.Ally ||
                    cardPosition.GetComponent<SlotController>().SlotObjective.TypeSlot == Slot.TypeSlotEnum.Bag)
        {
            this.transform.SetParent(cardPosition.transform);
            this.transform.position = cardPosition.transform.position;
            this.transform.eulerAngles = cardPosition.transform.eulerAngles;
            paiObjeto = cardPosition;
            actionMouseClick = true;
        }
    }
    void checkHitObject()
    {
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (hit2D.collider != null && hit2D.collider.GetComponent<CardMovement>()!=null)
        {
            hit2D.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder = hit2D.collider.gameObject.GetComponent<CardDisplay>().CardOrderDisplay+2;
            hit2D.collider.gameObject.GetComponentInChildren<TextMeshPro>().sortingOrder = hit2D.collider.gameObject.GetComponent<CardDisplay>().CardOrderDisplay + 3;
            hit2D.collider.gameObject.GetComponent<CardMovement>().clicado = true;
            hit2D.collider.gameObject.GetComponent<CardMovement>().actionMouseClick = false;
            hit2D.collider.gameObject.transform.eulerAngles = new Vector3(0,0,0);
        }
    }
    void checkTypeCard()
    {
        if(GetComponent<CardDisplay>().cardGame.TypeCard == CardScriptable.TypeCard.Enemy.ToString())
        {
            Destroy(this.GetComponent<CardMovement>());
        }
    }

    bool checkcardsObjective(CardDisplay thisCard, CardDisplay cardCollision)
    {
        switch (thisCard.cardGame.TypeCard)
        {
            case"Ally" :
                string thisCardTypeSolt = thisCard.GetComponentInParent<SlotController>().SlotObjective.TypeSlot.ToString();
                if (cardCollision.cardGame.TypeCard== "Enemy")
                    if(thisCardTypeSolt == "Ally" || thisCardTypeSolt == "Bag")
                        return true;                
                break;
            case "Effect":
                Debug.Log("Testando");
                if (cardCollision.cardGame.TypeCard == "Enemy")
                    return true;
                break;
            case "EffectAlly":
                if (cardCollision.GetComponent<MartaPollaroid>())
                    return true;
                break;
        }
        return false;
    }

void OnTriggerEnter2D(Collider2D collision)
    {
        if (clicado)
        {
            if (collision.gameObject.GetComponent<SlotController>() != null)
            {
                cardPosition.Add(collision.gameObject);
            }
            else
            {
                Debug.Log(GetComponent<CardDisplay>()+ " , "+ collision.gameObject.GetComponent<CardDisplay>());
                if (checkcardsObjective(GetComponent<CardDisplay>(),collision.gameObject.GetComponent<CardDisplay>()))
                {
                    cardsObjective.Add(collision.gameObject);
                    if(GetComponent<CardDisplay>().cardGame.TypeCard=="Ally" || GetComponent<CardDisplay>().cardGame.TypeCard == "Effect")
                    {
                        Debug.Log("Estou segurando uma carta do tipo Aliada ou efeito no inimigo" );
                        foreach (GameObject card in cardsObjective)
                        {
                            card.gameObject.GetComponent<CardDisplay>().DeselectCard();
                        }
                        cardObjectiveSelected = SelectedLowestInfluence();
                        cardObjectiveSelected.GetComponent<CardDisplay>().CardSelect();
                    }
                    else
                    {
                        Debug.Log("Outro tipo de carta");
                    }

                }
            }

        }
        
    }
    GameObject SelectedLowestInfluence()
    {
        GameObject cardLowestInfluence=null;
        int lowestInfluence=0;
        if (cardsObjective.Count > 0)
        {
            cardLowestInfluence = cardsObjective[0];
            lowestInfluence = cardLowestInfluence.GetComponent<CardDisplay>().cardGame.Influence;
        }

        foreach (GameObject card in cardsObjective)
        {
            if(card.GetComponent<CardDisplay>().cardGame.Influence< lowestInfluence)
            {
                cardLowestInfluence = card;
                lowestInfluence = cardLowestInfluence.GetComponent<CardDisplay>().cardGame.Influence;
            }
        }

        return cardLowestInfluence;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (clicado)
        {
            if (collision.gameObject.GetComponent<SlotController>() != null)
            {
                cardPosition.Remove(collision.gameObject);
            }
            else
            {
                if (cardsObjective.Contains(collision.gameObject))
                {
                    foreach (GameObject card in cardsObjective)
                    {
                        card.gameObject.GetComponent<CardDisplay>().DeselectCard();
                    }
                    cardsObjective.Remove(collision.gameObject);
                    cardObjectiveSelected = SelectedLowestInfluence();
                    if (cardObjectiveSelected != null)
                    {
                        cardObjectiveSelected.GetComponent<CardDisplay>().CardSelect();
                    }

                }
            }
            
        }
    }
}
