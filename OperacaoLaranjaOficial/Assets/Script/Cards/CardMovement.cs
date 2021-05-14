﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class CardMovement : MonoBehaviour
{
    [SerializeField]bool clicado;
    Vector3 touchPosition;
    [SerializeField]List<GameObject> cardObjective;
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
                this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = this.gameObject.GetComponent<CardDisplay>().CardOrderDisplay;
                this.gameObject.GetComponentInChildren<TextMeshPro>().sortingOrder = this.gameObject.GetComponent<CardDisplay>().CardOrderDisplay+1;
                clicado = false;
                if (cardObjective.Count > 0 && cardObjective[0].GetComponent<CardDisplay>() != null)
                {
                    if(cardObjective[0].GetComponent<CardDisplay>().cardGame.TypeCard == "Enemy" && this.GetComponent<CardDisplay>().cardGame.TypeCard == "Effect")
                    {
                        actionMouseClick = true;
                        cardObjective[0].GetComponent<CardDisplay>().GetDamage(this.GetComponent<CardDisplay>().cardGame.InfluenceEffect, this.gameObject, true);
                        Destroy(this.gameObject);

                    }

                    /*if (cardObjective[0].GetComponent<CardDisplay>().cardGame.TypeCard == "EffectAlly" && this.GetComponent<CardDisplay>().cardGame.TypeCard == "Ally")
                    {
                        if (this.gameObject.GetComponentInParent<SlotController>().SlotObjective.TypeSlot.ToString() == "Ally")
                        {
                            Debug.Log("Vim aqui");
                            actionMouseClick = true;
                            this.GetComponent<CardDisplay>().GainLife(cardObjective[0].GetComponent<CardDisplay>().cardGame.InfluenceEffect);
                            cardPosition.Add(cardObjective[0].transform.parent.gameObject);
                            Destroy(cardObjective[0].gameObject);
                            if (cardPosition.Count > 0)
                            {
                                Debug.Log("Terei que mudar de posicao");
                                ActionChangeCardPosition(cardPosition[0]);
                            }
                        }
                    }*/
                    if (cardObjective[0].GetComponent<CardDisplay>().cardGame.TypeCard == "Enemy" && this.GetComponent<CardDisplay>().cardGame.TypeCard == "Ally")
                    {
                        if (this.gameObject.GetComponentInParent<SlotController>().SlotObjective.TypeSlot.ToString() == "Ally")
                        {
                            int cardEnemyInfluenceInitial = cardObjective[0].GetComponent<CardDisplay>().cardGame.Influence;
                            if(cardEnemyInfluenceInitial> this.GetComponent<CardDisplay>().cardGame.Influence)
                            {
                                deckCardActive.DamageMarta(cardEnemyInfluenceInitial - this.GetComponent<CardDisplay>().cardGame.Influence);
                            }
                            cardObjective[0].GetComponent<CardDisplay>().GetDamage(this.GetComponent<CardDisplay>().cardGame.Influence, this.gameObject, false);
                            this.GetComponent<CardDisplay>().GetDamage(cardEnemyInfluenceInitial, cardObjective[0], false);
                        }
                    }
                }

                if (cardObjective.Count > 0 && cardObjective[0].GetComponent<MartaPollaroid>() != null)
                {
                    if (cardObjective[0].GetComponent<MartaPollaroid>() && this.GetComponent<CardDisplay>().cardGame.TypeCard == "Enemy")
                    {
                        Debug.Log("Teste..."+ cardObjective[0]);
                        int cardEnemyInfluenceAttack =GetComponent<CardDisplay>().cardGame.Influence;
                        GetComponent<CardDisplay>().GetDamage(cardObjective[0].GetComponent<MartaPollaroid>().InfluenciaMarta, cardObjective[0], false);
                        deckCardActive.DamageMarta(cardEnemyInfluenceAttack);
                    }


                    if (this.GetComponent<CardDisplay>().cardGame.TypeCard == "EffectAlly")
                    {
                        print("DEBUG HERE 1");
                        if (this.gameObject.GetComponentInParent<SlotController>().SlotObjective.TypeSlot.ToString() == "Ally" || this.gameObject.GetComponentInParent<SlotController>().SlotObjective.TypeSlot.ToString()== "Bag")
                        {
                            print("DEBUG HERE 2");
                            actionMouseClick = true;
                            cardObjective[0].GetComponent<MartaPollaroid>().AlterarInfluenciaMarta(this.GetComponent<CardDisplay>().cardGame.InfluenceEffect);
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
                cardObjective.RemoveRange(0, cardObjective.Count);
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
            // Debug.Log("Vamos Posicao");
            this.transform.position = Vector3.MoveTowards(this.transform.position, paiObjeto.transform.position, 40f * Time.deltaTime);
            if (Vector3.Distance(this.transform.position, paiObjeto.transform.position) == 0)
            {
                // Debug.Log("Tem certeza que vim aqui:" + Vector3.Distance(this.transform.position, paiObjeto.transform.position));
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
        Debug.Log("teste: "+ cardPosition.Count);
        for (int i = 0; i < cardPosition.Count; i++)
        {
            Debug.Log("teste1: " + cardPosition.Count);
            if (cardPosition[i].gameObject.transform.childCount == 0)
            {
                Debug.Log("teste2: " + cardPosition.Count);
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
            Debug.Log("teste3: " + cardPosition.name);
            this.transform.SetParent(cardPosition.transform);
            this.transform.position = cardPosition.transform.position;
            this.transform.eulerAngles = cardPosition.transform.eulerAngles;
            paiObjeto = cardPosition;
            Debug.Log("Realizei a mudança");
            actionMouseClick = true;
        }
    }
    void checkHitObject()
    {
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (hit2D.collider != null && hit2D.collider.GetComponent<CardMovement>()!=null)
        {
            Debug.Log(hit2D.collider.name);
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
                cardObjective.Add(collision.gameObject);
                cardObjective[0].GetComponent<CardDisplay>().SelectCard();
            }

        }
        
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
                collision.gameObject.GetComponent<CardDisplay>().DeselectCard();
                cardObjective.Remove(collision.gameObject);
            }
            
        }
    }
}
