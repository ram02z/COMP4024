using UnityEngine;

// This class represents the projectiles that enemies can fire.
public class EnemyProjectile : MonoBehaviour
{
    // The speed at which the projectile moves.
    public float speed = 2.5f;

    // Update is called once per frame.
    // In this method, the projectile is moved downwards at a speed defined by the 'speed' variable.
    void Update()
    {
        transform.Translate(Vector2.down * (Time.deltaTime * speed));
    }

    // This method is called when the projectile collides with another object.
    // If the projectile collides with an object tagged as "Player" or "Boundary", the projectile is destroyed.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}