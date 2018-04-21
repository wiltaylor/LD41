using UnityEngine;

public enum WeaponType
{
    None,
    Bolt,
    Beam
}


public class PlayerCombatController : MonoBehaviour
{

    private static PlayerCombatController _instance;

    public static PlayerCombatController Instance
    {
        get { return _instance; }
    }

    public WeaponType CurrentWeapon = WeaponType.None;
    public float Damage;
    public float Ammount;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }
	
	void Update ()
	{

	}
}
