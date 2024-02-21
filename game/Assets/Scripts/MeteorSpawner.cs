using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

// This class is responsible for spawning meteors in the game.
public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab; // The meteor prefab to be spawned.
    public float meteorSpawnInterval = 1f; // The interval at which meteors are spawned.
    public TMP_Text wordText; // The text that will be displayed on the meteor.
    private float meteorSpawnTimer = 0f; // Timer to control the spawning of meteors.
    private float meteorTextTimer = 0f; // Timer to control the display of text on the meteors.
    private int lastMeteorDrop = 50; // The last position where a meteor was dropped.
    private int secondlastMeteorDrop = 100; // The second last position where a meteor was dropped.
    private int correctTextThreshold; // The threshold for displaying the correct translation on the meteor.
    private WordManager wordManager; // Reference to the WordManager component.

    // This method is called at the start of the game.
    public void Start()
    {
        wordManager = GameObject.Find("WordManager").GetComponent<WordManager>();
        correctTextThreshold = Random.Range(4, 8);
    }

    // This method is called once per frame.
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

    // This method spawns a meteor.
    // wordType determines whether the spawned meteor will/ won't have the correct translation on it
    void SpawnMeteor(bool wordType)
    {
        // Calculate meteor drop location Y
        float y = Camera.main.orthographicSize + 2f; // Add 2f to ensure meteors spawn above the screen

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
        newMeteor.transform.position = new Vector3(dropLocationX, y, 0f);

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