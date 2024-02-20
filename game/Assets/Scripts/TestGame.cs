using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic; 

public class SpaceShooterGame : MonoBehaviour
{
    public GameObject spaceshipPrefab;
    public GameObject projectilePrefab;
    public GameObject meteorPrefab;

    private float spaceshipSpeed = 15f;
    private float projectileSpeed = 10f;
    private float meteorSpeed = 2.5f;
    private float meteorSpawnInterval = 1f;
    private float gameTime = 120f;

    private float meteorSpawnTimer = 0f;
    private float meteorTextTimer = 0f;
    private int score = 0;
    private bool gameRunning = true;

    private int lastMeteorDrop = 50; // arbitrary large numbert 'unlikely' to cause a clash on the first run
    private int secondlastMeteorDrop = 100; // same reasoning as previous
    private int correctTextThreshold; // every 4-7 meteors, the correct translation will be displayed
    private float lastFireTime = 0f;
    private float fireInterval = 0.5f;

    private Text scoreText;
    private Text timerText;
    private Text wordText;
    private string[] words = {"1","2","3","4","5","6","7","8","9","10"}; // to be replaced by english-french translation pairs from text files, dynamically loaded depending on which themes are selected
    private Queue<string> wordQueue = new Queue<string>();

    void Start()
    {
        scoreText = GameObject.Find("scoreText").GetComponent<Text>();
        timerText = GameObject.Find("timerText").GetComponent<Text>();
        wordText = GameObject.Find("wordText").GetComponent<Text>();
        correctTextThreshold = Random.Range(4,8);

        for (int word = 0; word < words.Length; word++)
        {
            wordQueue.Enqueue(words[word]);
        }

        UpdateScoreUI();
        UpdateTimeUI();
    }

    void Update()
    {
        if (!gameRunning)
            return;

        UpdateTime();

        // Collect spaceship movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        MoveSpaceship(horizontalInput);

        // Fire projectile
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastFireTime >= fireInterval)
        {
            FireProjectile();
            lastFireTime = Time.time;
        }

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

        DetectCollision();
    }

    void MoveSpaceship(float horizontalInput)
    {
        // Move spaceship left or right
        spaceshipPrefab.transform.Translate(Vector3.right * horizontalInput * spaceshipSpeed * Time.deltaTime);

        // Clamp spaceship within screen bounds
        float screenWidth = Camera.main.aspect * Camera.main.orthographicSize;
        float screenEdge = screenWidth - 0.5f;
        spaceshipPrefab.transform.position = new Vector3(Mathf.Clamp(spaceshipPrefab.transform.position.x, -screenEdge, screenEdge), spaceshipPrefab.transform.position.y, spaceshipPrefab.transform.position.z);
    }


    void FireProjectile()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, spaceshipPrefab.transform.position, Quaternion.identity);
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.velocity = Vector2.up * projectileSpeed;
        Destroy(newProjectile, 3f);
    }

    void SpawnMeteor(bool wordType) //wordType determines whether the spawned meteor will/ won't have the correct translation on it
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
            meteorText.text = words[Random.Range(0, words.Length)];
        
        // Adjust the position of the text to be relative to the meteor
        meteorText.transform.localPosition = new Vector3(0f, 0f, 0f);

        // Set up the rigidbody and velocity for the meteor
        Rigidbody2D rb = newMeteor.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.velocity = Vector2.down * meteorSpeed;

        // Destroy the meteor after a certain time
        Destroy(newMeteor, 4f);
}

void EndGame()
    {
        gameRunning = false;
        Application.Quit(); // To be replaced with a popup displaying score, correct and incorrect meteors hit
    }

    void DetectCollision()
    {
        GameObject[] meteors = GameObject.FindGameObjectsWithTag("Meteor");
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject meteor in meteors)
        {
            foreach (GameObject projectile in projectiles)
            {
                if (meteor.GetComponent<Collider2D>().bounds.Intersects(projectile.GetComponent<Collider2D>().bounds))
                {
                    CollisionDetected(meteor, projectile);
                    return;
                }
            }
        }
    }

    void CollisionDetected(GameObject meteor, GameObject projectile)
    {
        // Get the TextMeshPro component from the meteor
        TextMeshPro meteorText = meteor.GetComponentInChildren<TextMeshPro>();

        if (meteorText.text.Equals(wordText.text))
        {
            IncreaseScore();
            ChangeWord();
        }
        else
            DecreaseScore();
        Destroy(meteor);
        Destroy(projectile);
    }


    void UpdateTime()
    {
        if (!gameRunning)
            return;

        gameTime -= Time.deltaTime;
        if (gameTime <= 0)
        {
            gameTime = 0;
            EndGame();
        }

        UpdateTimeUI();
    }

    void IncreaseScore()
    {
        score += 10;
        UpdateScoreUI();
    }

    void DecreaseScore()
    {
        if (score != 0)
        {
            score -= 5;
            UpdateScoreUI();
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void UpdateTimeUI()
    {
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }

    void UpdateWordUI()
    {
        wordText.text = wordQueue.Peek();
    }

    void ChangeWord()
    {
        string word = wordQueue.Peek();
        wordQueue.Dequeue();
        wordQueue.Enqueue(word);
        word = wordQueue.Peek();
        wordText.text = word;
    }
}
