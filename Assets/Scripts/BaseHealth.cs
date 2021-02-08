using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] Transform explosionSpawnParent;

    void OnTriggerEnter(Collider other)
    {
        int dmg = other.GetComponent<Enemy>().GetEnemyDamage();
        health -= dmg;

        if (health <= 0) {
            ProcessBaseDeath();
        }
    }

    private void ProcessBaseDeath() {
        float explosionDuration = explosion.main.duration;
        Destroy( Instantiate(explosion,transform.position,Quaternion.identity,explosionSpawnParent).gameObject, explosionDuration);
        Destroy(transform.gameObject);
    }
}
