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
        // Move as long as the game is not over
        Move();
        // Destroy the obstacles when they reach the left bound
        DestroyObstacles();
        // Boost player forwards when right arrow is pressed for 1.5 seconds
        BoostPlayer();
    }

    void BoostPlayer()
    {
        // Boost player forwards when right arrow is pressed
        if (Input.GetKey(KeyCode.RightArrow) && playerControllerScript.gameOver == false)
        {
            // Set boost to true for bonus score
            playerControllerScript.boost = true;
            // Move the object to the left
            transform.Translate(Vector3.left * Time.deltaTime * speed * 1.5f);
        }
        else
        {
            // Set boost to false for no bonus score
            playerControllerScript.boost = false;
        }
    }

    void Move()
    {
        // Move unless the game is over
        if (playerControllerScript.gameOver == false)
        {
            // Move the objects to the left
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }

    void DestroyObstacles()
    {
        // Destroy the obstacle when it reaches the left bound
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
