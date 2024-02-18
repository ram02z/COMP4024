using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectSpawner : MonoBehaviour
{
    public GameObject enemyProjectilePrefab;
    public float spawnTimer;
    public float spawnMax = 10f;
    public float spawnMin = 5f;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Random.Range(spawnMin, spawnMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer <= 0)
        {
            Instantiate(enemyProjectilePrefab, transform.position, Quaternion.identity);
            spawnTimer = Random.Range(spawnMin, spawnMax);
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
    }
}
