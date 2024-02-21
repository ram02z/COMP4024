using UnityEngine;
using UnityEngine.Serialization;

// This class is responsible for the behavior of the projectiles in the game.
public class Projectile : MonoBehaviour
{
    public float moveSpeed = 7f; // The speed at which the projectile moves.
    public GameObject explosionPrefab; // The prefab for the explosion that occurs when the projectile hits an enemy.
    private PointManager _pointManager; // Reference to the PointManager component.
    public static EnemyHitEvent onEnemyHit = new(); // The event that is invoked when the projectile hits an enemy.

    // This method is called once per frame.
    // It moves the projectile upwards at a speed determined by moveSpeed.
    void Update()
    {
        transform.Translate(Vector2.up * (Time.deltaTime * moveSpeed));
    }

    // This method is called when the projectile collides with another object.
    // If the other object is an enemy, an explosion is instantiated at the enemy's position, the score is updated, and both the enemy and the projectile are destroyed.
    // If the other object is the boundary, the projectile is destroyed.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Instantiate an explosion at the position of the ship
            Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);
            onEnemyHit.Invoke(other.gameObject);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}