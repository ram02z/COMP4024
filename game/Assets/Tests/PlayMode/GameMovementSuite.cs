using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Tests.PlayMode
{

    public class GameMovementSuite : InputTestFixture
    {
        private Keyboard keyboard;

        [SetUp]
        public override void Setup()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
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
        
        [TearDown]
        public override void TearDown()
        {
             base.TearDown();
        }
    }
}