using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Declare a private Rigidbody variable named 'playerRb'
    private Rigidbody playerRb;
    // Player animation
    private Animator playerAnim;
    // Explosn particle effect
    public ParticleSystem explosionParticle;
    // Declare a public float variable named 'jumpForce' and set it to 10
    public float jumpForce = 10;
    // Declare a public float variable named 'gravityModifier' and set it to 1
    public float gravityModifier = 1;
    // Declare a public bool variable named 'isOnGround' and set it to true
    public bool isOnGround = true;
    // Is game over?
    public bool gameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        // Use GetComponent to assign the Rigidbody component of the GameObject to playerRb
        playerRb = GetComponent<Rigidbody>();
        // Set the gravity of the playerRb to multiply by the configurable gravityModifier
        Physics.gravity *= gravityModifier;
        // Connect with the Animator component
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // When the spacebar is pressed, the player should jump when it's on the ground and the game is not over
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            // Apply an upward force to the Rigidbody
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // The player is no longer on the ground
            isOnGround = false;
            // Set the animation trigger for jumping
            playerAnim.SetTrigger("Jump_trig");
        }
    }

    // When the player collides with the ground, isOnGround should be set to true
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // When the player collides with the ground, isOnGround should be set to true
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // When the player collides with an obstacle, the game should be over
            Debug.Log("Game Over!");
            gameOver = true;

            // Animate the player dying
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            // Play the explosion particle effect
            explosionParticle.Play();
        }
    }
}
