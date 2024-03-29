﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Cards/New Card")]
public class CardScriptable : ScriptableObject
{
   
    public string cardName;
    public Sprite imageCard;
    public Sprite baseCard;
    public enum TypeCard { Ally, Enemy, Effect, EffectAlly,EffectBoth}
    public TypeCard typeCard;
    public int[] influence;
    public int[] influenceEffect;
    public int influenceBothEffect;
}
