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
}
