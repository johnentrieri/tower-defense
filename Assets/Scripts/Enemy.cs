using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosion;
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
        Destroy( Instantiate(explosion,transform.position,Quaternion.identity,explosionSpawnParent), 3.0f );
        Destroy(transform.parent.gameObject);
    }

    public void SetSpawnParent( Transform parent) {
        explosionSpawnParent = parent;
    }

    public void SetEnemyHealth( int hp) {
        enemyHealth = hp;
    }

}
