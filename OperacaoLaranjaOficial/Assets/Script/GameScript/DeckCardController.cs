using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DeckCardController : MonoBehaviour
{

    [Tooltip("Cartas que estarão presentes na fase")] public CardScriptable[] CardDeck;
    [Tooltip("Text quant deck")] [SerializeField] TextMeshPro textValueCard;
    [Tooltip("Quantidade de cartas no deck")] [SerializeField] int numCard;
    int randomNumberCard;
    public GameObject CardObject, Position;
    [SerializeField] GameObject[] cardBaseDeck;
    [SerializeField] List<CardScriptable> newCards = new List<CardScriptable>();
    //[SerializeField]List<Dictionary<Transform,bool>> allPositions = new List<Dictionary<Transform,bool>>();
    [SerializeField] List<GameObject> spawnCardSlots = new List<GameObject>();
    public GameObject cardsSlots;
    bool clicado;
    [SerializeField]
    // Start is called before the first frame update
    public void InicializarDeck()
    {
        numCard = 40;
        AlterarUINumCard(numCard);
        Debug.Log(gameObject.name + " Inicializar teste");
        SortearNovaCartaInicial();
    }
    // Update is called once per frame
    void Update()
    {
        if (numCard > 0) {
            if (Input.GetMouseButtonDown(0) && !ManagerGame.Instance.LockPlayerActive)
            {
                checkHitObject();
            }
            if (Input.GetMouseButtonUp(0) && clicado && !Tutorial.TutorialOn)
            {
                clicado = false;
                bool isCardSpawn = false;
                for (int i = 0; i < spawnCardSlots.Count; i++)
                {
                    if (spawnCardSlots[i].gameObject.transform.childCount == 0)
                    {
                        isCardSpawn = true;
                        StartCoroutine(MoverNovaCarta(spawnCardSlots[i]));
                        newCards.RemoveAt(0);
                        changeDisplayCardUI();
                        i = spawnCardSlots.Count;
                        SortearNovaCartaSimples();
                        numCard--;
                        AlterarUINumCard(numCard);
                    }

                }
                if (!isCardSpawn)
                {
                    ManagerGame.Instance.LockPlayerActive = false;
                }
            }
        }
    }

    public void AlterarUINumCard(int number)
    {
        if (number == -1)
        {
            textValueCard.gameObject.SetActive(false);
        }
        else
        {
            textValueCard.gameObject.SetActive(true);
            textValueCard.text = "" + number;
        }
    }
    public void LimparTabuleiro()
    {
        newCards.RemoveRange(0, newCards.Count);
        Debug.Log(cardsSlots.transform.childCount);
        for (int i = 0; i < cardsSlots.transform.childCount; i++)
        {
            cardsSlots.transform.GetChild(i).GetComponent<SlotController>().RemoveCard();
        } 
        changeDisplayCardUI();
        AlterarUINumCard(-1);
    }
    public void SortearNovaCartaInicial()
    {
        foreach (GameObject slot in spawnCardSlots)
        {
            if (slot.gameObject.transform.childCount == 0)
            {
                randomNumberCard = Random.Range(0, CardDeck.Length);
                newCards.Add(CardDeck[randomNumberCard]);
            }
        }
        changeDisplayCardUI();

    }

    public void SortearNovaCartaSimples()
    {
        randomNumberCard = Random.Range(0, CardDeck.Length);
        newCards.Add(CardDeck[randomNumberCard]);
    }
    public IEnumerator MoverNovaCarta(GameObject slot)
    {
        GameObject cardTemp = Instantiate(CardObject, this.transform.GetChild(2).transform.position, slot.transform.rotation) as GameObject;
        cardTemp.GetComponent<CardDisplay>().ConfigCardDisplay(newCards[0]);
        cardTemp.GetComponent<CardMovement>().paiObjeto = slot;
        yield return null;
    }

    public void changeDisplayCardUI()
    {
        if (newCards.Count > 0) 
        { 
            if (newCards[0] != null)
            {
                cardBaseDeck[0].GetComponent<SpriteRenderer>().sprite = newCards[0].baseCard;
            }
            
        }
        else
        {
            cardBaseDeck[0].GetComponent<SpriteRenderer>().sprite = null;
        }
        if (newCards.Count > 1)
        {
            if (newCards[1] != null)
            {
                cardBaseDeck[1].GetComponent<SpriteRenderer>().sprite = newCards[1].baseCard;
            }
        }
        else
        {
            cardBaseDeck[1].GetComponent<SpriteRenderer>().sprite = null;
        }
        if (newCards.Count > 2)
        {
            if (newCards[2] != null)
            {
                cardBaseDeck[2].GetComponent<SpriteRenderer>().sprite = newCards[2].baseCard;
            }
        }
        else
        {
            cardBaseDeck[2].GetComponent<SpriteRenderer>().sprite = null;
        }
    }
    void checkHitObject()
    {
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (hit2D.collider != null && hit2D.collider.GetComponent<DeckCardController>() != null)
        {
            Debug.Log(hit2D.collider.name);
            hit2D.collider.gameObject.GetComponent<DeckCardController>().clicado = true;
            ManagerGame.Instance.LockPlayerActive = true;
        }
    }
}
