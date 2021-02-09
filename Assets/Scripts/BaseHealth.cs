using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] Transform explosionSpawnParent;
    [SerializeField] AudioClip hitSFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] Text healthText;
    [SerializeField] Text scoreText;

    private AudioSource audioSource;
    private int score = 0;

    void Start() {
        healthText.text = health.ToString();
        scoreText.text = score.ToString();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) {
        int dmg = other.GetComponent<Enemy>().GetEnemyDamage();
        health -= dmg;
        if (health <= 0) {
            health = 0;
            AudioSource.PlayClipAtPoint(deathSFX,Camera.main.transform.position,0.2f);
            ProcessBaseDeath();
        } else {
            audioSource.PlayOneShot(hitSFX);
        }
        healthText.text = health.ToString();
    }

    public void AddToScore(int points) {
        score += points;
        scoreText.text = score.ToString();
    }

    private void ProcessBaseDeath() {
        float explosionDuration = explosion.main.duration;        
        Destroy(Instantiate(explosion,transform.position,Quaternion.identity,explosionSpawnParent).gameObject, explosionDuration);        
        Destroy(transform.gameObject);
    }
}
