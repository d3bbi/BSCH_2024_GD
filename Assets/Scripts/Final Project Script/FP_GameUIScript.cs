using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FP_GameUIScript : MonoBehaviour
{

    public TMP_Text scoreText;
    public TMP_Text bombText;
    public FP_GameManagerScript gameManager;
    public FP_Player_Movement player;
    public BombController bombScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FP_GameManagerScript>(); // find the game manager and get the script
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<FP_Player_Movement>();
        bombScript = GameObject.FindGameObjectWithTag("Player").GetComponent<BombController>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" + gameManager.score;
        bombText.text = ""+bombScript.bombsRemaining;
    }
}
