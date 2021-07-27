using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonScript : MonoBehaviour
{
    public enum Button {Play,Niveis,Gravações,Configurações, Agentes_Especiais,Voltar_Menu,GamePlay_Reiniciar,GameOver_Reiniciar,GameOver_Voltar_Menu,
        Fase1,Fase2,Fase3,Fase4,
        Agente_Kellen, Agente_Bruna, Agente_Matheus, Agente_Leandro, Agente_Edilson, Agente_Jesse
    }
    public Button selectedButton;
    SpriteRenderer spriteRenderer;
    public Sprite[] status;
    CameraMovement camMove;
    public GameOverManager gameOverScript;
    private SmartPhoneAnimation animScript;
    private AudioSource clickSound, gamePlaySound, menuSound;
    private CapitulosManager CM;
    private GravacoesManager GM;
    GameControllerScript gm;
    bool Restating;
    SoundManager soundScript;
    [SerializeField]DeckCardController deckCard;
    private void Start()
    {
        spriteRenderer=GetComponentInChildren<SpriteRenderer>();
        CM = GameObject.Find("CapitulosManager").GetComponent<CapitulosManager>();
        animScript = GameObject.Find("SmartPhoneGameObject/SmartPhone").GetComponent<SmartPhoneAnimation>();
        soundScript = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        clickSound = GameObject.Find("SFX/Click").GetComponent<AudioSource>();
        GM = GameObject.Find("BGCompleto/Gravações").GetComponent<GravacoesManager>();
        spriteRenderer.sprite = status[0];
        camMove = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
        gm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameControllerScript>();
    }

    private void OnMouseDown()
    {
        if(CanClick())
        {
            if (GetComponent<FasesButtonScript>() != null)
            {
                if (GetComponent<FasesButtonScript>().iteractableFaseButton)
                {
                    clickSound.Play();
                    spriteRenderer.sprite = status[1];
                }
            }
            else
            {
                clickSound.Play();
                spriteRenderer.sprite = status[1];
            }
        }
    }
    private void OnMouseUp()
    {
        // Debug.Log(animScript.gameObject.name);
        if (CanClick())
        {
            if (GetComponent<FasesButtonScript>() != null)
            {
                if (GetComponent<FasesButtonScript>().iteractableFaseButton)
                {
                    actionButton();
                }
            }
            else
            {
                actionButton();
            }

        }
        spriteRenderer.sprite = status[0];
    }

    void actionButton()
    {
        switch (selectedButton)
        {
            case Button.Play:
                PlayerPrefs.SetInt("CurrentLevel", 1);
                // StartCoroutine(CM.ShowVideo(0));
	            CM.CurrentCoroutine = StartCoroutine(CM.ShowVideo(0));
	            CM.SetMaxLevel(1);

                // soundScript.SwitGamePlayAndMenu();
                ClearAllDecks();
                // camMove.SetDestiny(2);
                // camMove.SetDestiny(PlayerPrefs.GetInt("CurrentLevel", 1)+1);
                break;
            case Button.Niveis:
                ClearAllDecks();
                camMove.SetDestiny(1);
                break;
            case Button.Gravações:
                camMove.SetDestiny(7);
                GM.ShowPolaroids();
                break;
            case Button.Configurações:
                PhoneAnim();
                break;
            case Button.Agentes_Especiais:
                camMove.SetDestiny(6);
                break;
            case Button.Voltar_Menu:
                ClearAllDecks();
                if(CameraMovement.__cenaEmTela != "ForaJogo"){soundScript.SwitGamePlayAndMenu();}
                camMove.SetDestiny(0);
                // if(deckCard)
                // {
                //     gm.desativarFase();
                //     deckCard.LimparTabuleiro();
                // }
                break;
            case Button.GamePlay_Reiniciar:
                // Reinicia nivel atual
                if(gameOverScript)
                {
                    Restating = true;
                    StartCoroutine(Restart(1));
                    gameOverScript.FadeInOut(); //goMenu=false
                }
                break;
            case Button.GameOver_Reiniciar:
                // Reinicia nivel atual
                if(gameOverScript)
                {
                    StartCoroutine(Restart(0));
                    gameOverScript.FinishAnim(false); //goMenu=false
                }
                break;
            case Button.GameOver_Voltar_Menu:
                soundScript.SwitGamePlayAndMenu();
                // gm.desativarFase();
                // GameObject.Find("BoloDeCartas"+PlayerPrefs.GetInt("CurrentLevel",1)).GetComponent<DeckCardController>().LimparTabuleiro();
                if(gameOverScript)
                {
                    gameOverScript.FinishAnim();
                }
                break;
            case Button.Fase1:
	            CM.CurrentCoroutine = StartCoroutine(CM.ShowVideo(0)); 
	            CM.SetMaxLevel(1);
                // soundScript.SwitGamePlayAndMenu();
                // camMove.SetDestiny(2);
                PlayerPrefs.SetInt("CurrentLevel", 1);
                break;
            case Button.Fase2:
		        if(CapitulosManager.MaxLevel >= 2)
		        {
		        	Tutorial.StartTutorial2();
	                soundScript.SwitGamePlayAndMenu();
	                camMove.SetDestiny(3);
	                PlayerPrefs.SetInt("CurrentLevel", 2);
		        }
                break;
            case Button.Fase3:
		        if(CapitulosManager.MaxLevel >= 3)
		        {
	                soundScript.SwitGamePlayAndMenu();
		            camMove.SetDestiny(4);
	                PlayerPrefs.SetInt("CurrentLevel", 3);
		        }
                break;
            case Button.Fase4:
		        if(CapitulosManager.MaxLevel >= 4)
		        {
	                soundScript.SwitGamePlayAndMenu();
	                camMove.SetDestiny(5);
	                PlayerPrefs.SetInt("CurrentLevel", 4);
		        }
                break;
            case Button.Agente_Kellen:
                Application.OpenURL("https://www.instagram.com/ascronicasdekellen/");
                break;
            case Button.Agente_Matheus:
                Application.OpenURL("https://www.instagram.com/miauzfuzzy/");
                break;
            case Button.Agente_Bruna:
                Application.OpenURL("https://www.instagram.com/brusnogueira/");
                break;
            case Button.Agente_Leandro:
                Application.OpenURL("https://www.instagram.com/gueandro/");
                break;
            case Button.Agente_Edilson:
                Application.OpenURL("https://www.linkedin.com/in/edilson-chavesws/");
                break;
            case Button.Agente_Jesse:
                Application.OpenURL("https://www.instagram.com/jesse_s.c/");
                break;

        }
    }


    private void PhoneAnim()
    {
        animScript.StartAnim();
    }


    private bool CanClick()
    {
        if(!animScript.GetIfIsShowing() && !ManagerGame.Instance.LockPlayerActive 
        	&& !Tutorial.TutorialOn && !camMove.GetIsMoving() 
        	&& !Restating && !CardArrest.IsActive
        	&& !GM.ShowingPolaroidImage
        	&& !GM.SpawningPolaroids
        	&& !GM.ShowingVideo)
        {
            if(gameOverScript)
            {
                if(gameOverScript.IsActive)
                {
                    return (selectedButton == Button.GameOver_Voltar_Menu || selectedButton == Button.GameOver_Reiniciar);
                }
            }
            return true;
        }
        return false;
    }

    private IEnumerator Restart(float delay)
    {
        yield return new WaitForSeconds(delay);
        gm.desativarFase();
        GameObject.Find("BoloDeCartas"+PlayerPrefs.GetInt("CurrentLevel",1)).GetComponent<DeckCardController>().LimparTabuleiro();
        yield return new WaitForSeconds(0.3f);
        gm.inicializarFase(PlayerPrefs.GetInt("CurrentLevel",1)-1);
        yield return new WaitForSeconds(1.5f);
        Restating = false;
    }

    private void ClearAllDecks()
    {
        GameObject.Find("BoloDeCartas1").GetComponent<DeckCardController>().LimparTabuleiro();
        GameObject.Find("BoloDeCartas2").GetComponent<DeckCardController>().LimparTabuleiro();
        GameObject.Find("BoloDeCartas3").GetComponent<DeckCardController>().LimparTabuleiro();
        GameObject.Find("BoloDeCartas4").GetComponent<DeckCardController>().LimparTabuleiro();
    }
}
