using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // The prefab for the projectile that will be shot.
    public float fireRate = 0.5f; // The rate of fire, in seconds.
    private float _nextFire; // The time when the next shot can be fired.
    private PlayerControls _playerControls; // The PlayerControls instance to handle player input.
    private InputAction _fireAction; // The InputAction to handle the shooting action.

    private void Awake()
    {
        // Initialize the PlayerControls and the fire action.
        _playerControls = new PlayerControls();
        _fireAction = _playerControls.Spaceship.Shoot;
    }

    private void OnEnable()
    {
        // Enable the fire action when the script is enabled.
        _fireAction.Enable();
    }

    private void OnDisable()
    {
        // Disable the fire action when the script is disabled.
        _fireAction.Disable();
    }

    // Update is called once per frame.
    void Update()
    {
        // Check if the fire action is triggered and if enough time has passed since the last shot.
        if (_fireAction.triggered && Time.time > _nextFire)
        {
            // Update the time for the next shot.
            _nextFire = Time.time + fireRate;
            // Instantiate a new projectile at the current position of the game object this script is attached to.
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
    }
}