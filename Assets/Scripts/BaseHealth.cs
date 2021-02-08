using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] Transform explosionSpawnParent;
    [SerializeField] Text healthText;
    [SerializeField] Text scoreText;

    private int score = 0;

    void Start() {
        healthText.text = health.ToString();
        scoreText.text = score.ToString();
    }

    void OnTriggerEnter(Collider other) {
        int dmg = other.GetComponent<Enemy>().GetEnemyDamage();
        health -= dmg;
        if (health <= 0) {
            health = 0;
            ProcessBaseDeath();
        }
        healthText.text = health.ToString();
    }

    public void AddToScore(int points) {
        score += points;
        scoreText.text = score.ToString();
    }

    private void ProcessBaseDeath() {
        float explosionDuration = explosion.main.duration;
        Destroy( Instantiate(explosion,transform.position,Quaternion.identity,explosionSpawnParent).gameObject, explosionDuration);
        Destroy(transform.gameObject);
    }
}
