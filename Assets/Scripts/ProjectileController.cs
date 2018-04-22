using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float Damage;
    public float TimeToLive;
    public float Speed;


    void FixedUpdate()
    {
        transform.position += transform.forward * Speed * Time.fixedDeltaTime;

        TimeToLive -= Time.fixedDeltaTime;

        if(TimeToLive <= 0)
            gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        var dest = other.transform.GetComponent<Destructable>();

        if(dest != null)
            dest.DoDamage(Damage);

        var player = other.transform.GetComponent<PlayerCombatController>();

        if(player != null)
            player.TakeDamage(Damage);

        gameObject.SetActive(false);
    }


}
