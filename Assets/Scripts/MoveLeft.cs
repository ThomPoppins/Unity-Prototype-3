using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    // `speed` is the speed at which the object will move
    private float speed = 30;
    // PlayerController script
    private PlayerController playerControllerScript;
    // Left bound
    private float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        // Connect with the PlayerController script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Stop moving the objects when the game is over
        GameOver();
        // Destroy the obstacles when they reach the left bound
        DestroyObstacles();
    }

    void GameOver()
    {
        // Stop moving the object when the game is over
        if (playerControllerScript.gameOver == false)
        {
            // Move the object to the left
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }

    void DestroyObstacles()
    {
        // Destroy the object when it reaches the left bound
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
