using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    [SerializeField] Slot slotObject;

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
