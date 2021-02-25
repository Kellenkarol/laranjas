using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonScript : MonoBehaviour
{
    public enum Button {Play,Niveis,Gravações,Configurações, Agentes_Especiais,Voltar_Menu,Fase1,Fase2,Fase3,Fase4 }
    public Button selectedButton;
    SpriteRenderer spriteRenderer;
    public Sprite[] status;
    CameraMovement camMove;
    private SmartPhoneAnimation animScript;
    
    private void Start()
    {
        spriteRenderer=GetComponentInChildren<SpriteRenderer>();
        animScript = GameObject.Find("SmartPhoneGameObject").GetComponent<SmartPhoneAnimation>();
        spriteRenderer.sprite = status[0];
        camMove = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
    }

    private void OnMouseDown()
    {
        if(!animScript.GetIfIsShowing())
        {
	        spriteRenderer.sprite = status[1];
        }
    }
    private void OnMouseUp()
    {
        Debug.Log(animScript.gameObject.name);
        if (!animScript.GetIfIsShowing())
        {
	        Debug.Log(this.gameObject.name);
	        spriteRenderer.sprite = status[0];
	        actionButton();
        }
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
            case Button.Voltar_Menu:
                camMove.SetDestiny(0);
                break;
        }
    }

    private void PhoneAnim()
    {
	    animScript.StartAnim();

    }
}
