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
    [SerializeField] List<GameObject> spawnCardSlots = new List<GameObject>();
    const int initialMartaInfluence = 15;
    public GameObject cardsSlots;
    public GameOverManager gov;
    public CardArrest cardArrest;
    public AudioSource cardSound;
    bool clicado;
    [SerializeField]MartaPollaroid marta;

    CameraMovement camMove;
    bool canRun, isCardSpawn;
    float delayAux;

    void Start()
    {
        camMove = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
    }


    // Update is called once per frame
    void Update()
    {   
        if(canRun && !Tutorial.TutorialOn && !camMove.GetIsMoving())
        {

            if (numCard > 0) {
                
                if (marta.InfluenciaMarta > 0)
                {
                    delayAux += Time.deltaTime;
                    if(!isCardSpawn && delayAux >= 1f)
                    {

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
                else
                {
                    //Declarar derrota
                    delayAux = 0;
                    Debug.Log("Derrota");
                    gov.ShowGameOverAnim();
                    canRun = false;
                    
                }
            }
            else
            {
                // Declarar Vitoria
                bool vitoriaDetected = true;
                foreach (GameObject spawnSlot in spawnCardSlots)
                {
                    if (spawnSlot.transform.childCount > 0)
                    {
                        if (spawnSlot.GetComponentInChildren<CardDisplay>().cardGame.TypeCard == "Enemy")
                        {
                            vitoriaDetected = false;
                        }
                    }

                }

                if (vitoriaDetected)
                {
                    delayAux = 0;
                    Debug.Log("Vitoria fase uhu");
                    cardArrest.Arrest(transform.parent.name);
                    canRun = false;
                }

            }
        }
        else
        {
            delayAux = 0;
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
    public void InicializarDeck()
    {
        canRun = true;
        numCard = 40;
        AlterarUINumCard(numCard);
        marta.AlterarInfluenciaMarta(initialMartaInfluence);
        Debug.Log(gameObject.name + " Inicializar teste");
        SortearNovaCartaInicial();
    }
    public void LimparTabuleiro()
    {
        canRun = false;
        newCards.RemoveRange(0, newCards.Count);
        for (int i = 0; i < cardsSlots.transform.childCount; i++)
        {
            cardsSlots.transform.GetChild(i).GetComponent<SlotController>().RemoveCard();
        } 
        changeDisplayCardUI();
        AlterarUINumCard(-1);
        marta.AlterarInfluenciaMarta(-marta.InfluenciaMarta);
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
        cardSound.Play();
        GameObject cardTemp = Instantiate(CardObject, this.transform.GetChild(2).transform.position, slot.transform.rotation) as GameObject;
        cardTemp.GetComponent<CardDisplay>().ConfigCardDisplay(newCards[0]);
        cardTemp.GetComponent<CardMovement>().paiObjeto = slot;
        cardTemp.GetComponent<CardMovement>().DefineDeckCard(this.GetComponent<DeckCardController>());
        yield return new WaitForSeconds(0.5f);
        isCardSpawn = false;
        yield return null;
    }

    public void DamageMarta(int damageValue)
    {
        marta.AlterarInfluenciaMarta(-damageValue);
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
