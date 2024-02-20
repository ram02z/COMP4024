using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    public float meteorSpeed = 2.5f;
    public float meteorSpawnInterval = 1f;
    public TMP_Text wordText;
    private float meteorSpawnTimer = 0f;
    private float meteorTextTimer = 0f;
    private int lastMeteorDrop = 50; // arbitrary large number 'unlikely' to cause a clash on the first run
    private int secondlastMeteorDrop = 100; // same reasoning as previous
    private int correctTextThreshold; // every 4-7 meteors, the correct translation will be displayed
    private WordManager wordManager;

    public void Start()
    {
        wordManager = GameObject.Find("WordManager").GetComponent<WordManager>();
        correctTextThreshold = Random.Range(4, 8);
    }

    void Update()
    {
        // Meteor spawning interval
        meteorTextTimer += Time.deltaTime;
        meteorSpawnTimer += Time.deltaTime;
        if (meteorSpawnTimer >= meteorSpawnInterval)
        {
            meteorSpawnTimer = 0f;
            if (meteorTextTimer >= correctTextThreshold)
            {
                meteorTextTimer = 0f;
                correctTextThreshold = Random.Range(4, 8);
                SpawnMeteor(true);
            }
            else
                SpawnMeteor(false);
        }
    }
     // wordType determines whether the spawned meteor will/ won't have the correct translation on it
    void SpawnMeteor(bool wordType)
    {
        // Calculate meteor drop location Y
        float Y = Camera.main.orthographicSize + 2f; // Add 2f to ensure meteors spawn above the screen

        // Instantiate the meteor prefab
        GameObject newMeteor = Instantiate(meteorPrefab, Vector3.zero, Quaternion.identity);

        // Calculate meteor size
        float meteorSize = newMeteor.GetComponent<SpriteRenderer>().bounds.size.x;

        // Find the TextMeshPro component in the Canvas child of the meteor
        TextMeshPro meteorText = newMeteor.GetComponentInChildren<TextMeshPro>();

        // Set the position of the text to match the meteor's position
        Vector3 textPosition = new Vector3(newMeteor.transform.position.x, newMeteor.transform.position.y, 0f);
        meteorText.transform.position = textPosition;

        // Calculate meteor drop location X
        float screenWidth = Camera.main.aspect * Camera.main.orthographicSize * 2;
        int numOfColumns = (int)(screenWidth / meteorSize);
        int negColumns = -numOfColumns / 2;
        int posColumns = numOfColumns / 2;

        // Calculate drop location X
        int randomX = Random.Range(negColumns, posColumns);
        while (randomX == lastMeteorDrop || randomX == secondlastMeteorDrop)
        {
            randomX = Random.Range(negColumns, posColumns);
        }
        float dropLocationX = randomX * meteorSize;

        // Adjust drop location for the centre of the meteor
        dropLocationX += meteorSize / 2;

        // Set the position of the meteor
        newMeteor.transform.position = new Vector3(dropLocationX, Y, 0f);

        // Re-shuffle previous meteor locations
        secondlastMeteorDrop = lastMeteorDrop;
        lastMeteorDrop = randomX;

        // Decide whether meteor text should be correct translation or not - based on wordType flag
        if (wordType)
            meteorText.text = wordText.text;
        else
            meteorText.text = wordManager.GetRandomWord();

        // Adjust the position of the text to be relative to the meteor
        meteorText.transform.localPosition = new Vector3(0f, 0f, 0f);
    }
}