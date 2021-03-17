using System.Collections;
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
    bool actionMouseClick;
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
        Debug.Log("Card Inicializada: "+ cardInicializada);
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
                if (cardObjective.Count > 0)
                {
                    if (cardObjective[0].GetComponent<CardDisplay>().cardGame.TypeCard == "Enemy" && this.GetComponent<CardDisplay>().cardGame.TypeCard == "Effect")
                    {
                        actionMouseClick = true;
                        cardObjective[0].GetComponent<CardDisplay>().GetDamage(this.GetComponent<CardDisplay>().cardGame.InfluenceEffect);
                        Destroy(this.gameObject);

                    }

                    if (cardObjective[0].GetComponent<CardDisplay>().cardGame.TypeCard == "Ally" && this.GetComponent<CardDisplay>().cardGame.TypeCard == "EffectAlly")
                    {
                        actionMouseClick = true;
                        cardObjective[0].GetComponent<CardDisplay>().GainLife(this.GetComponent<CardDisplay>().cardGame.InfluenceEffect);
                        Destroy(this.gameObject);
                    }
                }
                if(cardPosition.Count>0 && !actionMouseClick)
                {
                    if (cardPosition.Count > 0)
                    {
                        for(int i =0;i< cardPosition.Count; i++)
                        {
                            if (cardPosition[i].gameObject.transform.childCount == 0)
                            {
                                if (cardPosition[i].GetComponent<SlotController>().SlotObjective.TypeSlot == Slot.TypeSlotEnum.Ally ||
                                    cardPosition[i].GetComponent<SlotController>().SlotObjective.TypeSlot == Slot.TypeSlotEnum.Bag)
                                {
                                    this.transform.SetParent(cardPosition[i].transform);
                                    this.transform.position = cardPosition[i].transform.position;
                                    this.transform.eulerAngles = cardPosition[i].transform.eulerAngles;
                                    paiObjeto = cardPosition[i];
                                    cardPosition.RemoveRange(0, cardPosition.Count);
                                    i = cardPosition.Count;
                                    actionMouseClick = true;
                                }
                            }

                        }

                    }
                }
                if(!actionMouseClick)
                {
                    this.transform.position = paiObjeto.transform.position;
                    this.transform.eulerAngles = paiObjeto.transform.eulerAngles;
                }
                Debug.Log("OLAAAA");
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
            Debug.Log("Vamos Posicao");
            this.transform.position = Vector3.MoveTowards(this.transform.position, paiObjeto.transform.position, 30f * Time.deltaTime);
            if (Vector3.Distance(this.transform.position, paiObjeto.transform.position) == 0)
            {
                Debug.Log("Tem certeza que vim aqui:" + Vector3.Distance(this.transform.position, paiObjeto.transform.position));
                cardInicializada = true;
                ManagerGame.Instance.LockPlayerActive = false;
                this.transform.SetParent(paiObjeto.transform);
                GetComponent<CardDisplay>().GetDamage(100);
                checkTypeCard();
            }
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
                cardObjective.Remove(collision.gameObject);
            }
            
        }
    }
}
