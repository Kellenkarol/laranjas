using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    [SerializeField] Slot slotObject;



    private void Update()
    {
        if(slotObject.TypeSlot.ToString() != "Enemy")
        {
            if (transform.childCount == 0)
            {
                GetComponent<Collider2D>().enabled = true;
            }
            else
            {
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
    public Slot SlotObjective
    {
        get { return slotObject; }
    }


    public void RemoveCard()
    {
        if (this.gameObject.transform.childCount > 0)
        {
            Destroy(this.gameObject.transform.GetChild(0).gameObject);
        }
    }
}
