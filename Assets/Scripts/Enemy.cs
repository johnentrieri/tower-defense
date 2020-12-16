using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    private int enemyHealth = 1;
    private Transform explosionSpawnParent;

    void OnParticleCollision(GameObject other) {

        if ( --enemyHealth <= 0) {
            ProcessEnemyDeath();
        }
    }

    private void ProcessEnemyDeath() {
        Instantiate(explosion,transform.position,Quaternion.identity,explosionSpawnParent);
        Destroy(transform.parent.gameObject);
    }

    public void SetSpawnParent( Transform parent) {
        explosionSpawnParent = parent;
    }

    public void SetEnemyHealth( int hp) {
        enemyHealth = hp;
    }

}
