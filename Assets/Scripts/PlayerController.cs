using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Declare a private Rigidbody variable named 'playerRb'
    private Rigidbody playerRb;
    // Declare a public float variable named 'jumpForce' and set it to 10
    public float jumpForce = 10;
    // Declare a public float variable named 'gravityModifier' and set it to 1
    public float gravityModifier = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Use GetComponent to assign the Rigidbody component of the GameObject to playerRb
        playerRb = GetComponent<Rigidbody>();
        // Set the gravity of the playerRb to multiply by the configurable gravityModifier
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // When the spacebar is pressed, the player should jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
