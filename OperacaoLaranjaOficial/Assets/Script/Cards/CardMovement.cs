using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CardMovement : MonoBehaviour
{
    [SerializeField]bool clicado;
    
    // Start is called before the first frame update
    void Start()
    {
        clicado = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            clicado = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            clicado = false;
        }
        if (clicado)
        {
            Vector3 positionCard = Camera.main.ScreenToWorldPoint(Input.mousePosition) ;
            positionCard.z = 0;
            transform.position = positionCard;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (clicado)
        {
            Debug.Log(collision.name);
        }
        
    }
}
