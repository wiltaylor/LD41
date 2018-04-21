using UnityEngine;
using UnityEngine.Events;

public enum ActionOnCast
{
    Discard,
    MoveToBuffs,
    Target
}

[CreateAssetMenu(fileName = "Card", menuName = "ScriptObject/Card", order = 1)]
public class CardData : ScriptableObject
{
    public string Name;
    public string Type;
    public string Description;
    public Sprite Picture;
    public int Cost;
    public float Timer;
    public ActionOnCast CastAction;
    public CardTargetType TargetType;
    public UnityEvent OnCast;
    public UnityEvent OnActivation;
    public UnityEvent OnExpire;

    
    public void AddManaResource(int ammount)
    {
        PlayerStats.Instance.MaxMana += ammount;
        PlayerStats.Instance.Mana += ammount;
    }

    public void AddHealthResource(int ammount)
    {
        PlayerStats.Instance.MaxHP += ammount;
        PlayerStats.Instance.HP += ammount;
    }

    public void SetAttackType(WeaponType type)
    {
        PlayerCombatController.Instance.CurrentWeapon = type;
    }

    public void SetAttackDamage(float ammount)
    {
        PlayerCombatController.Instance.Damage = ammount;
    }

    public void SetAttackQty(float ammount)
    {
        PlayerCombatController.Instance.Ammount = ammount;
    }

    public void DestroyTarget()
    {
        CardSystemController.Instance.CurrentTarget.Discard();
    }
}

