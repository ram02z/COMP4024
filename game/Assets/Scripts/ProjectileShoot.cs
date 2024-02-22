using UnityEngine;

// This class is responsible for shooting projectiles.
public class ProjectileShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // The prefab for the projectile that will be shot.
    public float fireRate = 0.5f; // The rate of fire, in seconds.
    private float _nextFire; // The time when the next shot can be fired.

    // Update is called once per frame.
    // In this method, the game checks if the "Fire1" button is pressed and if enough time has passed since the last shot.
    // If both conditions are met, a projectile is instantiated at the current position of the game object this script is attached to.
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > _nextFire)
        {
            _nextFire = Time.time + fireRate;
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
    }
}