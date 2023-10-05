using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    // `speed` is the speed at which the object will move
    private float speed = 30;
    // PlayerController script
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Connect with the PlayerController script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            // Move the object to the left
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}
