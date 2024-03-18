using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public float health;
    public float score;
    public Transform spawnPoint;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // This will make the game manager persist through scenes
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Start").transform; // find the spawn point
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This function will be called from other script as it is void
    public void AddScore(float scoreToAdd)
    {
        score += scoreToAdd;
    }
}