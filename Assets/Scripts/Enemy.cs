﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] ParticleSystem deathExplosion;
    [SerializeField] ParticleSystem goalExplosion;
    [SerializeField] ParticleSystem hitEffect;

    private int enemyHealth = 1;
    private Transform explosionSpawnParent;

    void OnParticleCollision(GameObject other) {

        hitEffect.Play();
        if ( --enemyHealth <= 0) {
            ProcessEnemyDeath(deathExplosion);
        }
    }

    public void ProcessGoalReached() {
        ProcessEnemyDeath(goalExplosion);
    }

    private void ProcessEnemyDeath(ParticleSystem explosion) {
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
