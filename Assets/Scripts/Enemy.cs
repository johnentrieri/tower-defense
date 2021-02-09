using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] ParticleSystem deathExplosion;
    [SerializeField] ParticleSystem goalExplosion;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] AudioClip hitSFX;
    [SerializeField] AudioClip deathSFX;

    private int enemyHealth = 1;
    private int enemyDamage = 1;

    private Transform explosionSpawnParent;
    private BaseHealth baseHealth;
    private AudioSource audioSource;

    void Start() {
        baseHealth = FindObjectOfType<BaseHealth>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnParticleCollision(GameObject other) {

        hitEffect.Play();
        if ( --enemyHealth <= 0) {
            ProcessEnemyDeath(deathExplosion);
            AudioSource.PlayClipAtPoint(deathSFX,Camera.main.transform.position,0.2f);
        } else {
            audioSource.PlayOneShot(hitSFX);
        }
    }

    public void ProcessGoalReached() {
        ProcessEnemyDeath(goalExplosion);
    }

    private void ProcessEnemyDeath(ParticleSystem explosion) {
        float explosionDuration = explosion.main.duration;
        Destroy( Instantiate(explosion,transform.position,Quaternion.identity,explosionSpawnParent).gameObject, explosionDuration);
        Destroy(transform.parent.gameObject);
        baseHealth.AddToScore(1);
    }

    public void SetSpawnParent( Transform parent) {
        explosionSpawnParent = parent;
    }

    public void SetEnemyHealth( int hp) {
        enemyHealth = hp;
    }

    public void SetEnemyDamage( int dmg) {
        enemyDamage = dmg;
    }

    public int GetEnemyDamage() {
        return enemyDamage;
    }

}
