using UnityEngine;

// This class is responsible for controlling the player's movement.
public class PlayerController : MonoBehaviour
{
    // The speed at which the player moves.
    public float moveSpeed = 15f;

    // Update is called once per frame.
    // In this method, the player's horizontal input is collected and used to move the player.
    // The player's position is also clamped within the screen bounds.
    void Update()
    {
        // Collect player movement input
        float hInput = Input.GetAxis("Horizontal");

        // Move the player to the right
        transform.Translate(Vector2.right * (Time.deltaTime * hInput * moveSpeed));

        // Clamp player within screen bounds
        float screenWidth = Camera.main.aspect * Camera.main.orthographicSize;
        float screenEdge = screenWidth - 0.5f;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -screenEdge, screenEdge), transform.position.y, transform.position.z);
    }
}