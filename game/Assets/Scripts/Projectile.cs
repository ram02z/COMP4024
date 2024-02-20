using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 7f;
    public GameObject explosionPrefab;
    private PointManager pointManager;
    // Start is called before the first frame update
    void Start()
    {
        pointManager = GameObject.Find("PointManager").GetComponent<PointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * (Time.deltaTime * moveSpeed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Instantiate an explosion at the position of the ship
            Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);
            pointManager.UpdateScore(other.gameObject);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}
