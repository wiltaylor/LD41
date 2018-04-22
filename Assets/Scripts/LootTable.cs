using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[CreateAssetMenu(fileName = "LootTable", menuName = "ScriptObject/Loot Table", order = 1)]
public class LootTable : ScriptableObject
{
    public LootTableItem[] Items;
}
