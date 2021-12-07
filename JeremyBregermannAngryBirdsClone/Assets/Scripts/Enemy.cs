using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    public GameObject deathEffect;

    private float health = 4f;

    //public int EnemiesAlive = 0;

    public GameObject gm;

    private SpriteRenderer spriteRenderer;
    public Sprite damaged;

    public AudioSource audio;

    public AudioClip[] pig_noises;
    private AudioClip pig_noise;

    public AudioClip[] death_noises;
    private AudioClip death_noise;

    private void Start()
    {
        Invoke("PlayPigNoises", (UnityEngine.Random.Range(5f, 17f)));
        audio = GetComponent<AudioSource>();
        gm.GetComponent<GameManager>().EnemiesAlive++;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D colInfo)
    {
        if (colInfo.relativeVelocity.magnitude > health)
        {
            Die();
        }

        if (colInfo.relativeVelocity.magnitude > 2)
        {
            //Debug.Log(hit.relativeVelocity.magnitude);
            spriteRenderer.sprite = damaged;
            Debug.Log("HURT!");
            health = health / 2f;
        }
    }

    public void Die()
    {
        Debug.Log("Dead");

        gm.GetComponent<GameManager>().score += 100;

        int death_index = UnityEngine.Random.Range(0, death_noises.Length);
        death_noise = death_noises[death_index];
        gm.GetComponent<GameManager>().PlayDeathSound(death_noise);

        Instantiate(deathEffect, transform.position, Quaternion.identity);
        gm.GetComponent<GameManager>().EnemiesAlive--;
        if (gm.GetComponent<GameManager>().EnemiesAlive <= 0)
        {
            Invoke("LoadNextLevel", 2.75f);
        }
        gameObject.SetActive(false);
    }

    public void LoadNextLevel()
    {
        Debug.Log("GAME SET!");
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) + 1);
    }

    public void PlayPigNoises()
    {
        float delay = UnityEngine.Random.Range(5f, 17f);
        int index = UnityEngine.Random.Range(0, pig_noises.Length);
        pig_noise = pig_noises[index];
        audio.PlayOneShot(pig_noise);
        Invoke("PlayPigNoises", delay);
    }
}