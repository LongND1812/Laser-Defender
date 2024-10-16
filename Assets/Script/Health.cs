using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] bool isBoss;
    [SerializeField] int health = 50;
    [SerializeField] int score = 0;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    Scorekeeper scorekeeper;
    LevelManager levelManager;
    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindAnyObjectByType<AudioPlayer>();
        scorekeeper = FindAnyObjectByType<Scorekeeper>();
        levelManager = FindAnyObjectByType<LevelManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamge(damageDealer.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamgeClip();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    void TakeDamge(int damage)
    {
        health -= damage;
        if (isPlayer)
        {
            UIDisplay.Instance.HealthUI();
        }
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isPlayer && !isBoss)
        {
            scorekeeper.ModifyScore(score);

        }
        else if (isBoss)
        {
            scorekeeper.ModifyScore(score);
            levelManager.LoadSceneNextLevel();
        }
        else if (isPlayer)
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);

    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
    public int GetHealth()
    {
        return health;
    }
}
