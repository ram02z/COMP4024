using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 2.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * (Time.deltaTime * speed));
    }
    
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
