using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // The prefab to be spawned
    public GameObject obstaclePrefab;
    // The position at which the prefab will be spawned
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    // The delay between each spawn
    private float startDelay = 2;
    // The delay between each spawn after the first spawn
    private float repeatDelay = 2;
    // The PlayerController script
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Connect with the PlayerController script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        // Call the SpawnObstacle function every `repeatDelay` seconds after `startDelay` seconds
        InvokeRepeating("SpawnObstacle", startDelay, repeatDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawn the obstacle prefab at the spawn position
    private void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
}
