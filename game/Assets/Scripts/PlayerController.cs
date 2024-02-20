using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 15f;
    
    // Update is called once per frame
    void Update()
    {
        // Collect spaceship movement input
        float hInput = Input.GetAxis("Horizontal");
        
        // Move the ship to the right
        transform.Translate(Vector2.right * (Time.deltaTime * hInput * moveSpeed));
    }

}
