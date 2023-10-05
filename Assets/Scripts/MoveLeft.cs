using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    // `speed` is the speed at which the object will move
    private float speed = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object to the left
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}
