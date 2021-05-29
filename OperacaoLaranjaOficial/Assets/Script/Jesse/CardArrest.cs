using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardArrest : MonoBehaviour
{
    public GameObject Card_n1, Card_n2, Card_n3, Card_n4, Anim, BlackScreen; 
    public AudioSource[] audiosWin;
    public bool Test;
    public static bool IsActive;
    CameraMovement camMove;


    // private bool Finished=true;

    private GameObject cardTmp, animTmp; 
    private SpriteRenderer img;
    private Color imgColor;
    private bool IsArrested;
    private Dictionary<string, GameObject> Cards = new Dictionary<string, GameObject>();
    private SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        camMove = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        Cards.Add("Fase1", Card_n1);
        Cards.Add("Fase2", Card_n2);
        Cards.Add("Fase3", Card_n3);
        Cards.Add("Fase4", Card_n4);
    }


    void Update()
    {
        IsActive = IsArrested;
    }
   //   //Teste
   //   if(Test && Finished)
   //   {
      //       // StartCoroutine(_()); 
   //       Finished = false;
            // Test = false;
      //    Arrest(Camera.main, Card);
   //   }
   //   else
   //   {
            // Test = false;
   //   }

   //  }


    // Prende a carta --------------------------------------------------------------------------------
    public void Arrest(string nivel)
    {
        if(!IsArrested)
        {
            IsArrested = true;
            StartCoroutine(_Arrest(nivel));
        }
    }


    //Teste -------------------------------
    private IEnumerator _()
    {
        yield return new WaitForSeconds(2);
        // Arrest(1);
    }

    private IEnumerator _Arrest(string nivel)
    {
        BlackScreen.SetActive(false);
        yield return new WaitForSeconds(1);
        cardTmp                         = Instantiate(Cards[nivel]) as GameObject;
        animTmp                         = Instantiate(Anim) as GameObject;
        img                             = cardTmp.GetComponent<SpriteRenderer>();
        StartCoroutine("CardFadeIn");
        cardTmp.transform.position      = Camera.main.transform.position+new Vector3(0,0,20);
        // animTmp.transform.position       = currentCamera.transform.position;
        animTmp.transform.localPosition = Camera.main.transform.position+new Vector3(-12.92f,9.41f,40);
        cardTmp.transform.localScale    = new Vector3(1.41f,1.41f,1.41f);
        BlackScreen.transform.position  = Camera.main.transform.position+new Vector3(0,0,20);
        BlackScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        audiosWin[Random.Range(0, audiosWin.Length)].Play();
        StartCoroutine(DestroyCardAndAnim(cardTmp, animTmp));
    }

    // Faz a carta aparecer aos poucos -----------------------------
    private IEnumerator CardFadeIn()
    {
        float auxTime=0;
        imgColor = img.color;
        imgColor = new Color(imgColor[0],imgColor[1],imgColor[2],0);
            IsArrested = true;

        while(auxTime<=0.333f)
        {
            auxTime += Time.deltaTime;
            img.color = imgColor + new Color(0,0,0,auxTime*3.003f);
            yield return null;
        }

    }


    // Faz a carta desaparecer aos poucos e depois a destroi junto da animação
    private IEnumerator DestroyCardAndAnim(GameObject card, GameObject anim)
    {
        yield return new WaitForSeconds(3f);
        imgColor = img.color;
        float auxTime=0;
        int CurrentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        if(CurrentLevel < 4)
        {
            CurrentLevel++;
            if(CurrentLevel == 2)
            {
                Tutorial.StartTutorial2();
            }
            PlayerPrefs.SetInt("CurrentLevel",CurrentLevel);
            camMove.SetDestiny(CurrentLevel+1);
        }
        else
        {
            camMove.SetDestiny(6);
            soundManager.SwitGamePlayAndMenu();
        }
        while(auxTime<=0.5f)
        {
            auxTime += Time.deltaTime;
            img.color = imgColor - new Color(0,0,0,auxTime*2);
            yield return null;
        }
        Destroy(card);
        Destroy(anim);
        IsArrested = false;
        // Finished = true;
    }

}
