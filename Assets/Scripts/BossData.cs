using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss", menuName = "ScriptObject/Boss", order = 1)]
public class BossData : ScriptableObject
{
    public string Name;
    public Sprite Picture;
    public float MaxHP;
    public float MaxMana;
    public float ManaChargeRate;
    public float HPChargeRate;
    public float CoolDown;
    public DeckData Deck;
    public DialogueEntry[] IntroText;
    public DialogueEntry[] DeathText;
    public DialogueEntry[] OnHitText;

}

