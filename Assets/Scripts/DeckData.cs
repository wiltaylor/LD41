using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[CreateAssetMenu(fileName = "Deck", menuName = "ScriptObject/Deck", order = 1)]
public class DeckData : ScriptableObject
{
    public DeckItem[] Items;
}

