using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.TestTools;
using System.Collections.Generic;
using TMPro;

namespace Tests.PlayMode
{
    public class RevisionTextControlerTests
    {

        private Dictionary<string, string> topic_1_vocab = new Dictionary<string, string>
        {
            { "a", "1" },
            { "b", "2" },
            { "c", "3" },
            { "d", "4" },
            { "e", "5" },
            { "f", "6" },
        };
        private Dictionary<string, string> topic_2_vocab = new Dictionary<string, string>
        {
            { "g", "7" },
            { "h", "8" },
            { "i", "9" },
            { "j", "10" },
            { "k", "11" },
            { "l", "12" },
        };

        private Vocabulary vocabulary = null;
        private GameObject vocabularyObject = null;

        [SetUp]
        public void Setup()
        {
            DestroyAllObjects();

            if (vocabularyObject == null)
            {
                vocabularyObject = new GameObject("Vocabulary");
                vocabulary = vocabularyObject.AddComponent<Vocabulary>();
                vocabulary.AddTopicVocab("topic1", topic_1_vocab);
                vocabulary.AddTopicVocab("topic2", topic_2_vocab);
            }

            SceneManager.LoadScene("LearnScene", LoadSceneMode.Single);
        }

        private void DestroyAllObjects()
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                Object.DestroyImmediate(obj);
            }
        }

        private List<TextMeshProUGUI> GetTextComponents()
        {
            GameObject canvasObject = GameObject.Find("Canvas");
            TextMeshProUGUI[] textMeshProComponents = canvasObject.GetComponentsInChildren<TextMeshProUGUI>(true);
            string targetTextMeshProName1 = "FrenchWord";
            string targetTextMeshProName2 = "EnglishWord";
            string targetTextMeshProName3 = "CurrentTopic";

            TextMeshProUGUI textComponent1 = null;
            TextMeshProUGUI textComponent2 = null;
            TextMeshProUGUI textComponent3 = null;


            foreach (TextMeshProUGUI textMeshPro in textMeshProComponents)
            {
                if (textMeshPro.gameObject.name == targetTextMeshProName1)
                {
                    textComponent1 = textMeshPro;
                }

                if (textMeshPro.gameObject.name == targetTextMeshProName2)
                {
                    textComponent2 = textMeshPro;
                }

                if (textMeshPro.gameObject.name == targetTextMeshProName3)
                {
                    textComponent3 = textMeshPro;
                }
            }

            return new List<TextMeshProUGUI> { textComponent1, textComponent2, textComponent3 };
        }

        private TextMeshProUGUI GetFrenchText()
        {
            return GetTextComponents()[0];
        }

        private TextMeshProUGUI GetEnglishText()
        {
            return GetTextComponents()[1];
        }

        private TextMeshProUGUI GetTopicText()
        {
            return GetTextComponents()[2];
        }

        private void ClickNext(int numClicks)
        {
            Button next = GameObject.Find("Next").GetComponent<Button>();
            for (int i = 0; i < numClicks; i++)
            {
                next.onClick.Invoke();
            }
        }

        private void ClickPrevious(int numClicks)
        {
            Button previous = GameObject.Find("Previous").GetComponent<Button>();
            for (int i = 0; i < numClicks; i++)
            {
                previous.onClick.Invoke();
            }
        }

        [UnityTest]
        public IEnumerator StartLearnScene_DoNothing_TranslationAndTopicAppear()
        {
            yield return null;

            Assert.AreEqual("a", GetFrenchText().text);
            Assert.AreEqual("1", GetEnglishText().text);
            Assert.AreEqual("Current Topic: topic1", GetTopicText().text);

        }

        [UnityTest]
        public IEnumerator NextCard_WhenNextExists_NextWordPairIsDisplayed()
        {
            yield return null;
            ClickNext(1);

            Assert.AreEqual("b", GetFrenchText().text);
            Assert.AreEqual("2", GetEnglishText().text);
            Assert.AreEqual("Current Topic: topic1", GetTopicText().text);
        }

        [UnityTest]
        public IEnumerator PrevCard_WhenPreviousExists_PreviousWordPairIsDisplayed()
        {
            yield return null;
            ClickNext(1);
            ClickPrevious(1);

            Assert.AreEqual("a", GetFrenchText().text);
            Assert.AreEqual("1", GetEnglishText().text);
            Assert.AreEqual("Current Topic: topic1", GetTopicText().text);
        }

        [UnityTest]
        public IEnumerator PrevCard_WhenPreviousDoesNotExist_DoesNothing()
        {
            yield return null;
            ClickPrevious(1);

            Assert.AreEqual("a", GetFrenchText().text);
            Assert.AreEqual("1", GetEnglishText().text);
            Assert.AreEqual("Current Topic: topic1", GetTopicText().text);
        }

        [UnityTest]
        public IEnumerator NextCard_WhenOnTopicBoundary_TopicIsUpdated()
        {
            yield return null;
            ClickNext(6);

            Assert.AreEqual("g", GetFrenchText().text);
            Assert.AreEqual("7", GetEnglishText().text);
            Assert.AreEqual("Current Topic: topic2", GetTopicText().text);
        }

        [UnityTest]
        public IEnumerator PrevCard_WhenOnTopicBoundary_TopicIsUpdated()
        {
            yield return null;
            ClickNext(6);
            ClickPrevious(1);

            Assert.AreEqual("f", GetFrenchText().text);
            Assert.AreEqual("6", GetEnglishText().text);
            Assert.AreEqual("Current Topic: topic1", GetTopicText().text);
        }

        [UnityTest]
        public IEnumerator NextCard_WhenNextDoesNotExist_DoesNothing()
        {
            yield return null;
            ClickNext(12);

            Assert.AreEqual("l", GetFrenchText().text);
            Assert.AreEqual("12", GetEnglishText().text);
            Assert.AreEqual("Current Topic: topic2", GetTopicText().text);
        }
    }
}