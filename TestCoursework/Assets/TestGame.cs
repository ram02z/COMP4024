using UnityEngine;
using UnityEngine.UI;

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

    private float timer = 0f;
    private int score = 0;
    private bool gameRunning = true;

    private float lastFireTime = 0f;
    private float fireInterval = 0.5f;

    private Text scoreText;
    private Text timerText;
    private Text meteorText;
    private int[] randomNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    void Start()
    {
        scoreText = GameObject.Find("scoreText").GetComponent<Text>();
        timerText = GameObject.Find("timerText").GetComponent<Text>();

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
        timer += Time.deltaTime;
        if (timer >= meteorSpawnInterval)
        {
            timer = 0f;
            SpawnMeteor();
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

    void SpawnMeteor()
    {
        float screenWidth = Camera.main.aspect * Camera.main.orthographicSize;
        float randomX = Random.Range(-screenWidth + 0.5f, screenWidth - 0.5f);
        float randomY = Camera.main.orthographicSize + 2f; // Add 2f to ensure meteors spawn above the screen

        // Instantiate the meteor prefab
        GameObject newMeteor = Instantiate(meteorPrefab, new Vector3(randomX, randomY, 0f), Quaternion.identity);

        // Find the Text component in the Canvas child of the meteor
        meteorText = GameObject.Find("Word").GetComponent<Text>();

        // Select a random number from the array and set it as the text of the Word object in the meteor's canvas
        int randomIndex = Random.Range(0, randomNumbers.Length);
        int randomNumber = randomNumbers[randomIndex];
        if (meteorText != null)
        {
            meteorText.text = randomNumber.ToString();
        }

        // Set up the rigidbody and velocity for the meteor
        Rigidbody2D rb = newMeteor.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.velocity = Vector2.down * meteorSpeed;

        // Destroy the meteor after a certain time
        Destroy(newMeteor, 6f);
    }


    void EndGame()
    {
        gameRunning = false;
        Debug.Log("Game Over!");
        // Add end game logic
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
        Destroy(meteor);
        Destroy(projectile);
        IncreaseScore();
    }


    void UpdateTime()
    {
        gameTime -= Time.deltaTime;
        UpdateTimeUI();

        if (gameTime <= 0)
        {
            EndGame();
        }
    }

    void IncreaseScore()
    {
        score += 10;
        UpdateScoreUI();
    }

    void DecreaseScore()
    {
        score -= 5; 
        UpdateScoreUI();
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
}
