namespace Tests.PlayMode
{
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

    public class TimeManagerTests
    {
        [UnityTest]
        public IEnumerator TimeManager_StartTimer_DecrementsTimeRemaining()
        {
            // Arrange
            GameObject timeManagerObject = new GameObject();
            TimeManager timeManager = timeManagerObject.AddComponent<TimeManager>();
            timeManager.timeRemaining = 120f;

            // Act
            timeManager.StartCoroutine(timeManager.StartTimer());

            // Wait for 2 seconds
            yield return new WaitForSeconds(2);

            // Assert
            Assert.AreEqual(118f, timeManager.timeRemaining);
        }

        [UnityTest]
        public IEnumerator TimeManager_EndGame_LoadsTopicScene()
        {
            // Arrange
            GameObject timeManagerObject = new GameObject();
            TimeManager timeManager = timeManagerObject.AddComponent<TimeManager>();
            
            timeManager.timeRemaining = 0f;

            // Wait for the scene to load
            yield return null;

            // Assert
            Assert.AreEqual("TopicScene", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}
