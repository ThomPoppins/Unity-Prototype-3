using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    // The start postion of the background
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        // Set the start position to the current position at the start of the game
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x - 50)
        {
            // Reset the position of the background to the start position
            transform.position = startPos;
        }
    }
}
