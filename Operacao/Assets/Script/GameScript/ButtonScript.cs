using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonScript : MonoBehaviour
{
    public enum Button {Play,Niveis,Gravações,Configurações, Agentes_Especiais,Fase1,Fase2,Fase3,Fase4 }
    public Button selectedButton;
    SpriteRenderer spriteRenderer;
    public Sprite[] status;

    private void Start()
    {
        spriteRenderer=GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = status[0];
    }
    private void OnMouseDown()
    {
        Debug.Log(this.gameObject.name);
        spriteRenderer.sprite = status[1];
    }
    private void OnMouseUp()
    {
        Debug.Log(this.gameObject.name);
        spriteRenderer.sprite = status[0];
    }


    void actionButton()
    {
        switch (selectedButton)
        {
            case Button.Play:
                break;
            case Button.Niveis:
                break;
            case Button.Gravações:
                break;
            case Button.Configurações:
                break;
            case Button.Agentes_Especiais:
                break;
            case Button.Fase1:
                break;
            case Button.Fase2:
                break;
            case Button.Fase3:
                break;
            case Button.Fase4:
                break;
        }
    }
}
