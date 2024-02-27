using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests.PlayMode
{

    public class GameSuite : InputTestFixture
    {
        private Keyboard keyboard;

        [SetUp]
        public override void Setup()
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            base.Setup();

            keyboard = InputSystem.AddDevice<Keyboard>();
        }

        [UnityTest]
        public IEnumerator PlayerDoesNotExceedRightBoundary()
        {
            // Get the player GameObject.
            var player = GameObject.FindWithTag("Player");
            // Get the player GameObject.
            Assert.IsNotNull(player, "Player GameObject not found");
        
            // Set the initial x position of the player.
            player.transform.position = new Vector2(0, 0);
        
            // Simulate pressing the right arrow key.
            Press(keyboard.dKey);
            yield return new WaitForSeconds(1f);
            Release(keyboard.dKey);
        
            // Wait for a frame to allow the PlayerController to process the input.
            yield return null;
        
            // Get the new x position of the player.
            var newXPosition = player.transform.position.x;
        
            // Assert that the new x position is not equal to the initial x position.
            Assert.Less(0, newXPosition);
            
            // Assert that the new x position is not greater than the right edge of the screen.
            float screenWidth = Camera.main.aspect * Camera.main.orthographicSize;
            float screenEdge = screenWidth - 0.5f;
            Assert.LessOrEqual(newXPosition, screenEdge);
        }

        [UnityTest]
        public IEnumerator PlayerDoesNotExceedLeftBoundary()
        {
            // Get the player GameObject.
            var player = GameObject.FindWithTag("Player");
            Assert.IsNotNull(player, "Player GameObject not found");
        
            // Set the initial x position of the player.
            player.transform.position = new Vector2(0, 0);
        
            // Simulate pressing the left arrow key.
            Press(keyboard.aKey);
            yield return new WaitForSeconds(1f);
            Release(keyboard.aKey);
        
            // Wait for a frame to allow the PlayerController to process the input.
            yield return null;
        
            // Get the new x position of the player.
            var newXPosition = player.transform.position.x;
        
            // Assert that the new x position is not equal to the initial x position.
            Assert.Greater(0, newXPosition);
            
            // Assert that the new x position is not greater than the left edge of the screen.
            float screenWidth = Camera.main.aspect * Camera.main.orthographicSize;
            float screenEdge = screenWidth - 0.5f;
            Assert.GreaterOrEqual(newXPosition, -screenEdge);
        }
        
        [UnityTest]
        public IEnumerator ProjectileSpawnedWhenSpacebarIsPressed()
        {
            // Get the player GameObject.
            var player = GameObject.FindWithTag("Player");
            Assert.IsNotNull(player, "Player GameObject not found");

            // Simulate pressing the spacebar.
            Press(keyboard.spaceKey);
            yield return null; // Wait for a frame to allow the PlayerController to process the input.
            Release(keyboard.spaceKey);

            // Find the projectile GameObject in the scene.
            var projectile = GameObject.FindWithTag("Projectile");

            // Assert that the projectile GameObject is not null.
            Assert.IsNotNull(projectile, "Projectile GameObject not found");

            yield return null;
        }
        
        [UnityTest]
        public IEnumerator PlayerDestroysEnemyWhenSpacebarIsPressed()
        {
            // Wait for an enemy GameObject to spawn.
            yield return new WaitUntil(() => GameObject.FindWithTag("Enemy") != null);

            // Get the enemy GameObject.
            var enemy = GameObject.FindWithTag("Enemy");
            Assert.IsNotNull(enemy, "Enemy GameObject not found");

            // Get the player GameObject.
            var player = GameObject.FindWithTag("Player");
            Assert.IsNotNull(player, "Player GameObject not found");

            // Move the player to the x position of the enemy.
            player.transform.position = new Vector2(enemy.transform.position.x, player.transform.position.y);

            // Simulate pressing the spacebar.
            Press(keyboard.spaceKey);
            yield return null; // Wait for a frame to allow the PlayerController to process the input.
            Release(keyboard.spaceKey);

            // Wait for a short period of time to allow the projectile to hit the enemy.
            yield return new WaitForSeconds(2f);
            
            // Check if the original enemy GameObject is still in the scene.
            var isOriginalEnemyDestroyed = enemy == null;

            // Assert that the original enemy GameObject is null.
            Assert.IsTrue(isOriginalEnemyDestroyed, "Original Enemy GameObject was not destroyed");
        }
        
        [UnityTest]
        public IEnumerator ProjectileGetsDestroyedWhenItCollidesWithEnemy()
        {
            // Wait for an enemy GameObject to spawn.
            yield return new WaitUntil(() => GameObject.FindWithTag("Enemy") != null);

            // Get the enemy GameObject.
            var enemy = GameObject.FindWithTag("Enemy");
            Assert.IsNotNull(enemy, "Enemy GameObject not found");

            // Get the player GameObject.
            var player = GameObject.FindWithTag("Player");
            Assert.IsNotNull(player, "Player GameObject not found");
            
            // Move the player to the x position of the enemy.
            player.transform.position = new Vector2(enemy.transform.position.x, player.transform.position.y);

            // Simulate pressing the spacebar.
            Press(keyboard.spaceKey);
            yield return null; // Wait for a frame to allow the PlayerController to process the input.
            Release(keyboard.spaceKey);
            
            // Get the projectile GameObject.
            var projectile = GameObject.FindWithTag("Projectile");
            Assert.IsNotNull(projectile, "Projectile GameObject not found");

            // Wait for a short period of time to allow the projectile to hit the enemy.
            yield return new WaitForSeconds(2f);

            // Check if the projectile GameObject is still in the scene.
            var isProjectileDestroyed = projectile == null;

            // Assert that the projectile GameObject is null.
            Assert.IsTrue(isProjectileDestroyed, "Projectile GameObject was not destroyed");
        }
        
        [UnityTest]
        public IEnumerator ScoreIncreasesWhenEnemyWithMatchingTextTagIsDestroyed()
        {
            // Get the 'Word' GameObject.
            var word = GameObject.Find("Word");
            Assert.IsNotNull(word, "Word GameObject not found");

            // Get the text of the 'Word' GameObject.
            var wordText = word.GetComponent<TMP_Text>().text;
            
            // Wait for an enemy GameObject with a matching text tag to spawn.
            yield return new WaitUntil(() =>
            {
                
                var obj = GameObject.FindWithTag("Enemy");
                if (obj == null) return false;
                var enemyTextTag = obj.GetComponentInChildren<TMP_Text>().text;
                return enemyTextTag == wordText;

            });

            // Get the enemy GameObject.
            var enemy = GameObject.FindWithTag("Enemy");
            Assert.IsNotNull(enemy, "Enemy GameObject not found");
            
            // Get the player GameObject.
            var player = GameObject.FindWithTag("Player");
            Assert.IsNotNull(player, "Player GameObject not found");

            // Move the player to the x position of the enemy.
            player.transform.position = new Vector2(enemy.transform.position.x, player.transform.position.y);

            // Simulate pressing the spacebar.
            Press(keyboard.spaceKey);
            yield return null; // Wait for a frame to allow the PlayerController to process the input.
            Release(keyboard.spaceKey);

            // Wait for a short period of time to allow the projectile to hit the enemy.
            yield return new WaitForSeconds(2f);

            // Check if the enemy GameObject is still in the scene.
            var isEnemyDestroyed = enemy == null;

            // Assert that the enemy GameObject is null.
            Assert.IsTrue(isEnemyDestroyed, "Enemy GameObject was not destroyed");
            
            // Get the PointManager GameObject.
            var pointManager = GameObject.Find("PointManager");
            Assert.IsNotNull(pointManager, "PointManager GameObject not found");

            // Get the PointManager component.
            var pointManagerComponent = pointManager.GetComponent<PointManager>();
            Assert.IsNotNull(pointManagerComponent, "PointManager component not found");

            // Get the current score.
            var currentScore = pointManagerComponent.score;

            // Assert that the score is at ten.
            Assert.AreEqual(10, currentScore);

            yield return null;
        }
        
                [UnityTest]
        public IEnumerator ScoreDoesNotChangeWhenEnemyWithNonMatchingTextTagIsDestroyed()
        {
            // Get the 'Word' GameObject.
            var word = GameObject.Find("Word");
            Assert.IsNotNull(word, "Word GameObject not found");

            // Get the text of the 'Word' GameObject.
            var wordText = word.GetComponent<TMP_Text>().text;
            
            // Wait for an enemy GameObject with a different text tag to spawn.
            yield return new WaitUntil(() =>
            {
                
                var obj = GameObject.FindWithTag("Enemy");
                if (obj == null) return false;
                var enemyTextTag = obj.GetComponentInChildren<TMP_Text>().text;
                return enemyTextTag != wordText;

            });

            // Get the enemy GameObject.
            var enemy = GameObject.FindWithTag("Enemy");
            Assert.IsNotNull(enemy, "Enemy GameObject not found");
            
            // Get the player GameObject.
            var player = GameObject.FindWithTag("Player");
            Assert.IsNotNull(player, "Player GameObject not found");

            // Move the player to the x position of the enemy.
            player.transform.position = new Vector2(enemy.transform.position.x, player.transform.position.y);

            // Simulate pressing the spacebar.
            Press(keyboard.spaceKey);
            yield return null; // Wait for a frame to allow the PlayerController to process the input.
            Release(keyboard.spaceKey);

            // Wait for a short period of time to allow the projectile to hit the enemy.
            yield return new WaitForSeconds(2f);

            // Check if the enemy GameObject is still in the scene.
            var isEnemyDestroyed = enemy == null;

            // Assert that the enemy GameObject is null.
            Assert.IsTrue(isEnemyDestroyed, "Enemy GameObject was not destroyed");
            
            // Get the PointManager GameObject.
            var pointManager = GameObject.Find("PointManager");
            Assert.IsNotNull(pointManager, "PointManager GameObject not found");

            // Get the PointManager component.
            var pointManagerComponent = pointManager.GetComponent<PointManager>();
            Assert.IsNotNull(pointManagerComponent, "PointManager component not found");

            // Get the current score.
            var currentScore = pointManagerComponent.score;

            // Assert that the score is still zero.
            Assert.AreEqual(0, currentScore);

            yield return null;
        }
        
        [UnityTest]
        public IEnumerator EnemyGetsDestroyedWhenTouchesPlayer()
        {
            // Wait for an enemy GameObject to spawn.
            yield return new WaitUntil(() => GameObject.FindWithTag("Enemy") != null);

            // Get the enemy GameObject.
            var enemy = GameObject.FindWithTag("Enemy");
            Assert.IsNotNull(enemy, "Enemy GameObject not found");

            // Get the player GameObject.
            var player = GameObject.FindWithTag("Player");
            Assert.IsNotNull(player, "Player GameObject not found");

            // Move the player to the position of the enemy.
            player.transform.position = enemy.transform.position;

            // Wait for a short period of time to allow the player to collide with the enemy.
            yield return new WaitForSeconds(0.5f);

            // Check if the enemy GameObject is still in the scene.
            var isEnemyDestroyed = enemy == null;

            // Assert that the enemy GameObject is null.
            Assert.IsTrue(isEnemyDestroyed, "Enemy GameObject was not destroyed");

            yield return null;
        }
        
        [TearDown]
        public override void TearDown()
        {
             base.TearDown();
        }
    }
}