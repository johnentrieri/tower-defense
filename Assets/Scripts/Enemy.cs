using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem hitEffect;

    private int enemyHealth = 1;
    private Transform explosionSpawnParent;

    void OnParticleCollision(GameObject other) {

        hitEffect.Play();
        if ( --enemyHealth <= 0) {
            ProcessEnemyDeath();
        }
    }

    private void ProcessEnemyDeath() {
        float explosionDuration = explosion.main.duration;
        Destroy( Instantiate(explosion,transform.position,Quaternion.identity,explosionSpawnParent).gameObject, explosionDuration);
        Destroy(transform.parent.gameObject);
    }

    public void SetSpawnParent( Transform parent) {
        explosionSpawnParent = parent;
    }

    public void SetEnemyHealth( int hp) {
        enemyHealth = hp;
    }

}
