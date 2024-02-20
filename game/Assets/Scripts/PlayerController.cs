using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 15f;
    
    // Update is called once per frame
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
