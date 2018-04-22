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
    private bool _doBeamFixed = false;
    private bool _doBeam = false;
    public float BeamLength = 14f;
    public GameObject BeamEmiter;
    public GameObject ProjectileSpawn;
    public GameObject BoltPrefab;

    public float BoltCooldown = 0.1f;


    private float _currentBoltCoolDown = 0f;

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

	    if (Ammount <= 0)
	    {
	        Ammount = 0;
	        CurrentWeapon = WeaponType.None;
	    }

	    _currentBoltCoolDown -= Time.deltaTime;

	    if (_currentBoltCoolDown < 0)
	        _currentBoltCoolDown = 0;

        BeamEmiter.SetActive(_doBeam);
	    _doBeam = false;
	}

    public void Shoot()
    {
        if (CurrentWeapon == WeaponType.Beam)
        {
            _doBeam = true;
            _doBeamFixed = true;
        }

        if (CurrentWeapon == WeaponType.Bolt && _currentBoltCoolDown <= 0)
        {
            var bolt = Instantiate(BoltPrefab);
            bolt.transform.position = ProjectileSpawn.transform.position;
            bolt.transform.rotation = ProjectileSpawn.transform.rotation;
            bolt.SetActive(true);

            var controller = bolt.GetComponent<ProjectileController>();
            controller.Damage = Damage;
            controller.TimeToLive = 10f;
            controller.Speed = 31;

            _currentBoltCoolDown = BoltCooldown;

            Ammount -= 1;
        }


    }

    void FixedUpdate()
    {
        if (_doBeamFixed)
        {
            var ray = new Ray(transform.position, transform.forward);

            var hits = Physics.RaycastAll(ray, BeamLength);

            foreach (var h in hits)
            {
                var dest = h.transform.GetComponent<Destructable>();

                if(dest == null)
                    continue;

                dest.DoDamage(Damage * Time.fixedDeltaTime);
            }

            _doBeamFixed = false;

            Ammount -= Time.fixedDeltaTime;
        }
    }


}
