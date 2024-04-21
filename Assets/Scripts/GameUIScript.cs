using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIScript : MonoBehaviour
{

    public TMP_Text scoreText;
    public TMP_Text healthText;
    public GameManagerScript gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>(); // find the game manager and get the script
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + gameManager.score;
        healthText.text = "Health: " + gameManager.health;
    }
}
