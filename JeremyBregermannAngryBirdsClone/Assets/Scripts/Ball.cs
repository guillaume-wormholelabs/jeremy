using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D hook;

    //public float number_ball = 1f;

    public float releaseTime = .15f;
    public float maxDragDistance = 2f;

    public GameObject nextBall;

    private bool isPressed = false;

    public bool released = false;

    public GameObject gm;

    public bool FirstHit = false;

    public AudioClip bird_launch;
    public AudioClip bird_hit;
    public AudioClip failed_level;
    public AudioSource audio;

    public int numberofhits;
    public GameObject gameOver;
    public GameObject reloadDestroy;
    public GameObject reloadDestroy2;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        gm = GameObject.FindWithTag("GameManager");
    }

    private void Update()
    {
        if (isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
                rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
            else
                rb.position = mousePos;
        }
    }

    private void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;
        gm.GetComponent<GameManager>().lives--;
        StartCoroutine(Release());
    }

    private void OnCollisionEnter2D(Collision2D colInfo)
    {
        if (colInfo.relativeVelocity.magnitude > 5)
        {
            audio.PlayOneShot(bird_hit);
        }
        FirstHit = true;
    }

    private IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        released = true;
        audio.PlayOneShot(bird_launch);
        GetComponent<SpringJoint2D>().enabled = false;
        this.enabled = false;

        yield return new WaitForSeconds(1f);

        if (nextBall != null)
        {
            nextBall.SetActive(true);
            //released = false;
            //number_ball++;
        }
        else
        {
            gameOver.SetActive(true);
            //gm.GetComponent<GameManager>().EnemiesAlive = 0;
        }
    }

    public void ReloadLevel()
    {
        StartCoroutine(Reload());
    }

    public void RestartGame()
    {
        Destroy(reloadDestroy);
        Destroy(reloadDestroy2);
        gameOver.SetActive(false);
        SceneManager.LoadScene(0);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(2f);
        audio.PlayOneShot(failed_level);
        Debug.Log("Reloading");
        Destroy(reloadDestroy);
        Destroy(reloadDestroy2);
        gameOver.SetActive(false);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}