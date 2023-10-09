using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Declare a private Rigidbody variable named 'playerRb'
    private Rigidbody playerRb;
    // Player animation
    private Animator playerAnim;
    // Audio source
    private AudioSource playerAudio;
    // Sound for jumping
    public AudioClip jumpSound;
    // Sound for crashing
    public AudioClip crashSound;
    // MoveLeft script
    private MoveLeft moveLeftScript;
    // Explosion particle effect
    public ParticleSystem explosionParticle;
    // Particle effect for dirt
    public ParticleSystem dirtParticle;
    // Player score
    public float score = 0;
    // Is the player boosted?
    public bool boost = false;
    // Declare a public float variable named 'jumpForce' and set it to 10
    public float jumpForce = 10;
    // Boolean variable to define if someone already double jumped
    private bool doubleJumped = false;
    // Declare a public float variable named 'gravityModifier' and set it to 1
    public float gravityModifier = 1;
    // Declare a public bool variable named 'isOnGround' and set it to true
    public bool isOnGround = true;
    // Is game over?
    public bool gameOver = false;
    // Starting point
    public Transform startingPoint;
    // Lerp speed
    public float lerpSpeed;


    // Start is called before the first frame update
    void Start()
    {
        // Use GetComponent to assign the Rigidbody component of the GameObject to playerRb
        playerRb = GetComponent<Rigidbody>();
        // Set the gravity of the playerRb to multiply by the configurable gravityModifier
        Physics.gravity *= gravityModifier;
        // Connect with the Animator component
        playerAnim = GetComponent<Animator>();
        // Connect with the AudioSource component
        playerAudio = GetComponent<AudioSource>();
        // Get MoveLeft script
        moveLeftScript = GameObject.Find("Player").GetComponent<MoveLeft>();

        gameOver = true;
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        // Subscribe to space bar press event in the Jump method
        Jump();

        // Track the player's score
        ScoreTracker();
    }

    IEnumerator PlayIntro()
    {
        // Player start position
        Vector3 startPos = transform.position;
        // Player end position
        Vector3 endPos = startingPoint.position;
        // Journey length
        float journeyLength = Vector3.Distance(startPos, endPos);
        // Start time
        float startTime = Time.time;
        // Distance covered
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        // The part the journey is at
        float fractionOfJourney = distanceCovered / journeyLength;
        // Set the animation trigger for running speed to 0.5
        playerAnim.SetFloat("Speed_Multiplier", 0.5f);
        // Move the player from the start position to the end position at a constant speed and 
        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }
        // After the player has reached the end position, set the animation trigger for running speed to 1
        playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        // Start moving left
        gameOver = false;
    }


    private void Jump()
    {
        // When the space bar is pressed, the player should jump when it's on the ground and the game is not over
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            // Apply an upward force to the Rigidbody
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // The player is no longer on the ground
            isOnGround = false;
            // Set the animation trigger for jumping
            playerAnim.SetTrigger("Jump_trig");
            // Stop the dirt particle effect
            dirtParticle.Stop();
            // Play the jump sound
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJumped && !gameOver)
        {
            // Apply an upward force to the Rigidbody
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // The player has double jumped
            doubleJumped = true;
            // Set the animation trigger for jumping
            playerAnim.SetTrigger("Jump_trig");
            // Stop the dirt particle effect
            dirtParticle.Stop();
            // Play the jump sound
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    public void ScoreTracker()
    {
        // Score tracker
        if (gameOver == false)
        {
            if (boost)
            {
                // Increase the score by 1 every second
                score += Time.deltaTime * 0.01f * 4.0f;
            }
            else
            {
                // Increase the score by 1 every second
                score += Time.deltaTime * 0.01f;
            }
        }
        // Display score rounded to 2 decimal places
        Debug.Log("Score: " + score.ToString("F2"));
    }

    // When the player collides with the ground, isOnGround should be set to true
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // When the player collides with the ground, isOnGround should be set to true
            isOnGround = true;
            // Play the dirt particle effect
            dirtParticle.Play();
            // The player has not double jumped yet
            doubleJumped = false;
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
            // Stop the dirt particle effect
            dirtParticle.Stop();
            // Play the crash sound
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
