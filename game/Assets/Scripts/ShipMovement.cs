using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 3f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the ship to the right
        transform.Translate(Vector2.right * (Time.deltaTime * moveSpeed));
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boundary"))
        {
            // Move the ship down
            var position = transform.position;
            position = new Vector3(position.x, position.y - 1, position.z);
            transform.position = position;
            // Reverse the direction
            moveSpeed *= -1;
        }
    }
}
