using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float hInput;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the ship to the right
        hInput = Input.GetAxisRaw("Horizontal");
        
        transform.Translate(Vector2.right * (Time.deltaTime * hInput * moveSpeed));
    }

}