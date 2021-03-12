using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonScript : MonoBehaviour
{
    public enum Button {Play,Niveis,Gravações,Configurações, Agentes_Especiais,Voltar_Menu,
        Fase1,Fase2,Fase3,Fase4,
        Agente_Kellen, Agente_Bruna, Agente_Matheus, Agente_Leandro, Agente_Edilson, Agente_Jesse
    }
    public Button selectedButton;
    SpriteRenderer spriteRenderer;
    public Sprite[] status;
    CameraMovement camMove;
    private SmartPhoneAnimation animScript;
    [SerializeField]DeckCardController deckCard;
    private void Start()
    {
        spriteRenderer=GetComponentInChildren<SpriteRenderer>();
        animScript = GameObject.Find("SmartPhoneGameObject/SmartPhone").GetComponent<SmartPhoneAnimation>();
        spriteRenderer.sprite = status[0];
        camMove = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
    }

    private void OnMouseDown()
    {
        if(!animScript.GetIfIsShowing() && !ManagerGame.Instance.LockPlayerActive)
        {
	        spriteRenderer.sprite = status[1];
        }
    }
    private void OnMouseUp()
    {
        Debug.Log(animScript.gameObject.name);
        if (!animScript.GetIfIsShowing() && !ManagerGame.Instance.LockPlayerActive)
        {
	        Debug.Log(this.gameObject.name);
	        actionButton();
        }
        spriteRenderer.sprite = status[0];
        // print("selectedButton: "+selectedButton);
    }

    void actionButton()
    {
        switch (selectedButton)
        {
            case Button.Play:
                camMove.SetDestiny(2);
                break;
            case Button.Niveis:
                Debug.Log("Testando");
                camMove.SetDestiny(1);
                break;
            case Button.Gravações:
                camMove.SetDestiny(7);
                break;
            case Button.Configurações:
                PhoneAnim();
                break;
            case Button.Agentes_Especiais:
                camMove.SetDestiny(6);
                break;
            case Button.Voltar_Menu:
                camMove.SetDestiny(0);
                if(deckCard)
                {
                    deckCard.LimparTabuleiro();
                }
                break;
            case Button.Fase1:
                camMove.SetDestiny(2);
                break;
            case Button.Fase2:
                camMove.SetDestiny(3);
                break;
            case Button.Fase3:
                camMove.SetDestiny(4);
                break;
            case Button.Fase4:
                camMove.SetDestiny(5);
                break;
            case Button.Agente_Kellen:
                Application.OpenURL("https://www.instagram.com/sophilah.art/");
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
}
