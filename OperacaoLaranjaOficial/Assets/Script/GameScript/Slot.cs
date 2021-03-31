using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot
{
    public enum TypeSlotEnum {Enemy,Ally,Bag}
    [SerializeField]Transform _positionTransform;
    [SerializeField] int _positionSlotArray;
    [SerializeField]TypeSlotEnum _typeSlot;




    public Transform PositionTransform
    {
        get
        {
            return _positionTransform;
        }
        set
        {
            _positionTransform = value;
        }
    }

    public int PositionSlotArray
    {
        get
        {
            return _positionSlotArray;
        }
        set
        {
            _positionSlotArray = value;
        }
    }

    public TypeSlotEnum TypeSlot
    {
        get { return _typeSlot; }
    }
}
