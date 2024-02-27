using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class MenuSuite
    {

        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("TopicScene", LoadSceneMode.Single);

        }

        [UnityTest]
        public IEnumerator ClickStartGameButtonWithNoTopicsSelectedDoesNotLoadGameScene()
        {
            // Get the 'Start Game' button.
            var startGameButton = GameObject.Find("Start Game").GetComponent<Button>();
            Assert.IsNotNull(startGameButton, "'Start Game' button not found");

            // Simulate a click on the 'Start Game' button.
            startGameButton.onClick.Invoke();
            
            // Assert that an error message was logged.
            LogAssert.Expect(LogType.Error, "Vocabulary map is empty. Cannot start game.");

            // Assert that the current scene is still the same scene.
            Assert.AreEqual("TopicScene", SceneManager.GetActiveScene().name);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ClickStartGameButtonWithTopicsSelectedLoadsGameScene()
        {
            // Get the 'Content' GameObject.
            var content = GameObject.Find("Content");
            Assert.IsNotNull(content, "'Content' GameObject not found");

            // Get all 'Button' components in the children of the 'Content' GameObject.
            var topicButtons = content.GetComponentsInChildren<Button>();
            Assert.IsNotEmpty(topicButtons, "No 'Button' components found in the children of the 'Content' GameObject");

            // Select first topic button.
            var topicButton = topicButtons[0];

            // Move the mouse to the position of the 'Topic' button and simulate a click.
            topicButton.onClick.Invoke();

            yield return new WaitForSeconds(2f);

            // Get the 'Start Game' button.
            var startGameButton = GameObject.Find("Start Game").GetComponent<Button>();
            Assert.IsNotNull(startGameButton, "'Start Game' button not found");

            // Simulate a click on the 'Start Game' button.
            startGameButton.onClick.Invoke();

            // Wait for a short period of time to allow the scene to potentially change.
            yield return new WaitForSeconds(2f);

            // Assert that the current scene is the game scene.
            Assert.AreEqual("GameScene", SceneManager.GetActiveScene().name);

            yield return null;
        }

        [UnityTest]
        public IEnumerator NoTopicsSelected_TryToPressLearnButton_LearnButtonInactive()
        {
            // Arrange
            GameObject buttonManagerObject = new GameObject();
            buttonManagerObject.name = "ButtonManager";
            ButtonManager _ButtonManager = buttonManagerObject.AddComponent<ButtonManager>();

            GameObject learnButtonObject = new GameObject();
            learnButtonObject.name = "Learn";
            Button learnButton = learnButtonObject.AddComponent<Button>();
            _ButtonManager.learnButton = learnButton;

            _ButtonManager.LogButtonActivation();
            _ButtonManager.LogButtonDeactivation();

            Assert.IsFalse(learnButton.isActiveAndEnabled, "'Learn' button should be inactive");

            GameObject.Destroy(buttonManagerObject);
            GameObject.Destroy(learnButtonObject);

            yield return null;
        }

        [UnityTest]
        public IEnumerator SomeTopicsSelected_TryToPressLearnButton_LearnButtonActive()
        {
            GameObject buttonManagerObject = new GameObject();
            buttonManagerObject.name = "ButtonManager";
            ButtonManager _ButtonManager = buttonManagerObject.AddComponent<ButtonManager>();

            GameObject learnButtonObject = new GameObject(); 
            learnButtonObject.name = "Learn";
            Button learnButton = learnButtonObject.AddComponent<Button>();
            _ButtonManager.learnButton = learnButton;

            _ButtonManager.LogButtonActivation();

            Assert.IsTrue(learnButton.isActiveAndEnabled, "'Learn' button should be inactive");

            GameObject.Destroy(buttonManagerObject);
            GameObject.Destroy(learnButtonObject);

            yield return null;
        }
    }
}
