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
    // Start is called before the first frame update
    void Start()
    {
        clicado = false;
        this.name = GetComponent<CardDisplay>().cardGame.Name;
        checkTypeCard();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            checkHitObject();
        }
        if (Input.GetMouseButtonUp(0) && clicado)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            this.gameObject.GetComponentInChildren<TextMeshPro>().sortingOrder = 2;
            clicado = false;
            if (cardObjective.Count > 0)
            {
                if(cardObjective[0].GetComponent<CardDisplay>().cardGame.TypeCard=="Enemy" && this.GetComponent<CardDisplay>().cardGame.TypeCard== "Effect")
                {
                    cardObjective[0].GetComponent<CardDisplay>().GetDamage(this.GetComponent<CardDisplay>().cardGame.InfluenceEffect);
                    Destroy(this.gameObject);
                }
            }

        }
        if (clicado)
        {
            Vector3 positionCard = Camera.main.ScreenToWorldPoint(Input.mousePosition) ;
            positionCard.z = 0;
            transform.position = positionCard;
        }
    }

    public void MovimentacaoInicial(Transform posicaoCarta)
    {

        
    }
    void checkHitObject()
    {
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition)); ;
        if (hit2D.collider != null && hit2D.collider.GetComponent<CardMovement>()!=null)
        {
            Debug.Log(hit2D.collider.name);
            hit2D.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
            hit2D.collider.gameObject.GetComponentInChildren<TextMeshPro>().sortingOrder = 6;
            hit2D.collider.gameObject.GetComponent<CardMovement>().clicado = true;
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
        Debug.Log("Teste");
        if (clicado)
        {
            cardObjective.Add(collision.gameObject);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (clicado)
        {
            cardObjective.Remove(collision.gameObject);
        }
    }
}
