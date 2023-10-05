using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    // The start postion of the background
    private Vector3 startPos;
    // The width the background will be repeated at
    private float repeatWidth;

    // Start is called before the first frame update
    void Start()
    {
        // Set the start position to the current position at the start of the game
        startPos = transform.position;
        // Set the repeat width to half the width of the background
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x - repeatWidth)
        {
            // Reset the position of the background to the start position
            transform.position = startPos;
        }
    }
}
