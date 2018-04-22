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

    public EnemyState State;

    private float _cooldown = 0f;
    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        if (BossStats.Instance.HP <= 0)
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

                   // transform.position = transform.position + direction * Speed * Time.fixedDeltaTime;

                    _rigidbody.MovePosition(Vector3.Lerp(transform.position, PlayerStats.Instance.transform.position, Speed * Time.fixedDeltaTime));
	            }
                break;
	        case EnemyState.Attacking:
	            if (Vector3.Distance(transform.position, PlayerStats.Instance.transform.position) > MinRange)
	            {
	                State = EnemyState.MovingToTarget;
                }
	            else
	            {
	                if (_cooldown <= 0)
	                {
	                    _cooldown = BoltCoolDown;

	                    var bolt = Instantiate(BoltPrefab);

	                    bolt.transform.position = ProjectileSpawn.position;
	                    bolt.transform.rotation = ProjectileSpawn.rotation;

                        bolt.SetActive(true);

	                    var ctrl = bolt.GetComponent<ProjectileController>();
	                    ctrl.Damage = BoltDamage;
	                    ctrl.TimeToLive = BoltLiveTime;
	                    ctrl.Speed = BoltSpeed;
	                }
	            }
                break;
	        default:
	            throw new ArgumentOutOfRangeException();
	    }

	    
	}
}
