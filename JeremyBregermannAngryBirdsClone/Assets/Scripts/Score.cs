using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text score;

    private GameObject gm;

    // Use this for initialization
    private void Start()
    {
        score = this.gameObject.GetComponent<Text>();
        gm = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    private void Update()
    {
        score.text = "Score: " + gm.GetComponent<GameManager>().score.ToString();
    }
}