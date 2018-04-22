using System;
using UnityEngine;

public enum EnemyState
{
    MovingToTarget,
    Attacking
}

public class EnemyController : MonoBehaviour
{
    public float MinRange;
    public float InRange;
    public float Speed;
    public GameObject BoltPrefab;
    public Transform ProjectileSpawn;
    public float BoltCoolDown;
    public float BoltLiveTime;
    public float BoltDamage;
    public float BoltSpeed;
    public GameObject MeatFountainPrefab;

    public EnemyState State;

    private float _cooldown = 0f;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update ()
    {
        if (BossStats.Instance.HP <= 0)
        {
            var meat = PoolSystem.Instance.GetProjectile(MeatFountainPrefab);

            meat.transform.position = transform.position;
            meat.SetActive(true);

            gameObject.SetActive(false);
            return;
        }

        if (PlayerStats.Instance.HP <= 0)
            return;

        transform.LookAt(PlayerStats.Instance.transform);

	    switch (State)
	    {
	        case EnemyState.MovingToTarget:
	            _cooldown = 0f;

                if (Vector3.Distance(transform.position, PlayerStats.Instance.transform.position) < InRange)
	                State = EnemyState.Attacking;
	            else
	            {
	                var direction = (PlayerStats.Instance.transform.position - transform.position).normalized;

                    //_rigidbody.MovePosition(Vector3.Lerp(transform.position, PlayerStats.Instance.transform.position, Speed * Time.fixedDeltaTime));
	                _rigidbody.velocity = direction * Speed;
	            }
                break;
	        case EnemyState.Attacking:
	            _rigidbody.velocity = Vector3.zero;

                if (Vector3.Distance(transform.position, PlayerStats.Instance.transform.position) > MinRange)
	            {
	                State = EnemyState.MovingToTarget;
                }
	            else
	            {
	                if (_cooldown <= 0)
	                {
	                    _cooldown = BoltCoolDown;

	                    var bolt = PoolSystem.Instance.GetProjectile(BoltPrefab);   //Instantiate(BoltPrefab);

	                    bolt.transform.position = ProjectileSpawn.position;
	                    bolt.transform.rotation = ProjectileSpawn.rotation;
                        
	                    var ctrl = bolt.GetComponent<ProjectileController>();
	                    ctrl.Damage = BoltDamage;
	                    ctrl.TimeToLive = BoltLiveTime;
	                    ctrl.Speed = BoltSpeed;

	                    bolt.SetActive(true);
                    }
	            }
                break;
	        default:
	            throw new ArgumentOutOfRangeException();
	    }

	    
	}
}
