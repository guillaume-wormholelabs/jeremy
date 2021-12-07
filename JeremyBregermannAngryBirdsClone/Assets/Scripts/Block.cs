using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite damaged;

    private GameObject gm;

    public int VelocityNeededToChangeSprite = 10;

    // Use this for initialization
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gm = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.relativeVelocity.magnitude > VelocityNeededToChangeSprite && spriteRenderer.sprite != damaged)
        {
            //Debug.Log(hit.relativeVelocity.magnitude);
            spriteRenderer.sprite = damaged;
            return;
        }
        if (hit.relativeVelocity.magnitude > VelocityNeededToChangeSprite && spriteRenderer.sprite == damaged)
        {
            gm.GetComponent<GameManager>().score += 50;
            //Debug.Log(hit.relativeVelocity.magnitude);
            Destroy(gameObject);
        }
    }
}