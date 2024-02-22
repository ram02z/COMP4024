using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 15f;
    private InputAction _movementAction;
    private PlayerControls _inputActionReference;
    private float _playersMovementDirection = 0; //this will give the direction of the players movement.

    private void Start()
    {
        //Getting the reference of the players rigid body.
        _inputActionReference = new PlayerControls();
        //enabling the Input actions
        _inputActionReference.Enable();
        //reading the values of the player movement direction for the players movement.
        _inputActionReference.Spaceship.Move.performed += moving =>
        {
            _playersMovementDirection = moving.ReadValue<float>();
        };
        _inputActionReference.Spaceship.Move.canceled += moving =>
        {
            _playersMovementDirection = 0;
        };
    }


    private void FixedUpdate()
    {
        //Moving player using player rigid body.
        transform.Translate(Vector2.right * (Time.deltaTime * _playersMovementDirection * moveSpeed));
        // Clamp player within screen bounds
        float screenWidth = Camera.main.aspect * Camera.main.orthographicSize;
        float screenEdge = screenWidth - 0.5f;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -screenEdge, screenEdge), transform.position.y, transform.position.z);
    }
}