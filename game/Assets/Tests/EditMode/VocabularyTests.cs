using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.EditMode
{
    public class VocabularyTests
    {

        private Vocabulary vocabularyObject;
        private Dictionary<string, string> testVocabulary;
        private string testTopicName = "testTopic";
        Dictionary<string, string> topic1;
        Dictionary<string, string> topic2;
        Dictionary<string, string> topic3;

        [SetUp]
        public void Setup()
        {
            // Create a Vocabulary object to be tested
            vocabularyObject = new GameObject().AddComponent<Vocabulary>();
            // Run the SetupVocabMap to bypass instatiation by Awake method call
            vocabularyObject.SetupVocabMap();
            // Create a testVocabulary object
            testVocabulary = new Dictionary<string, string>
            {
                { "french", "english" }
            };

            // Create "dummy" topic vocabulary
            topic1 = new Dictionary<string, string>();
            topic2 = new Dictionary<string, string>();
            topic3 = new Dictionary<string, string>();

            // Populate the vocabularyObject with "dummy" topics
            vocabularyObject.AddTopicVocab("topic1", topic1);
            vocabularyObject.AddTopicVocab("topic2", topic2);
            vocabularyObject.AddTopicVocab("topic3", topic3);
        }

        [Test]
        public void AddTopicVocab_WhenTopicNotInVocabulary_AddsEntry()
        {
            vocabularyObject.AddTopicVocab(testTopicName, testVocabulary);
            Assert.IsTrue(vocabularyObject.IsTopicInVocabulary(testTopicName));
        }

        [Test]
        public void AddTopicVocab_WhenTopicInVocabulary_DoesNothing()
        {
            Dictionary<string, string> elementBeforeAddTopicVocab = vocabularyObject.GetVocabMap()["topic1"];
            vocabularyObject.AddTopicVocab("topic1", testVocabulary);
            Dictionary<string, string> elementAfterAddTopicVocab = vocabularyObject.GetVocabMap()["topic1"];

            Assert.AreEqual(elementBeforeAddTopicVocab, elementAfterAddTopicVocab);
        }

        [Test]
        public void RemoveTopicVocab_WhenTopicInVocabulary_RemovesEntry()
        {
            vocabularyObject.RemoveTopicVocab("topic1");
            Assert.False(vocabularyObject.IsTopicInVocabulary("topic1"));
        }

        [Test]
        public void RemoveTopicVocab_WhenTopicNotInVocabulary_DoesNothing()
        {
            Dictionary<string, Dictionary<string, string>> dictionaryBeforeRemoveTopicVocab = vocabularyObject.GetVocabMap();
            vocabularyObject.RemoveTopicVocab("non existent topic");
            Dictionary<string, Dictionary<string, string>> dictionaryAfterRemoveTopicVocab = vocabularyObject.GetVocabMap();
            Assert.AreEqual(dictionaryBeforeRemoveTopicVocab, dictionaryAfterRemoveTopicVocab);
        }

        [Test]
        public void GetVocabMap_WhenNotNull_ReturnsDictionary()
        {
            vocabularyObject.SetupVocabMap();
            Dictionary<string, Dictionary<string, string>> dictionaryAfterSetupVocabMap = vocabularyObject.GetVocabMap();
            Assert.AreEqual(dictionaryAfterSetupVocabMap, new Dictionary<string, Dictionary<string, string>>());
        }


        [Test]
        public void IsTopicInVocabulary_WhenTopicExists_ReturnsTrue()
        {
            Assert.True(vocabularyObject.IsTopicInVocabulary("topic1"));
        }

        [Test]
        public void IsTopicInVocabulary_WhenTopicDoesNotExist_ReturnsFalse()
        {
            Assert.False(vocabularyObject.IsTopicInVocabulary("non existent topic"));
        }

        [Test]
        public void TopicButtonClicked_WhenTopicNotInVocabulary_AddsTopicAndReturnsTrue()
        {
            Assert.True(vocabularyObject.TopicButtonClicked("Access"));
            Assert.True(vocabularyObject.IsTopicInVocabulary("Access"));
        }

        [Test]
        public void TopicButtonClicked_WhenTopicInVocabulary_RemovesTopicAndReturnsFalse()
        {
            Assert.False(vocabularyObject.TopicButtonClicked("topic1"));
            Assert.False(vocabularyObject.IsTopicInVocabulary("topic1"));
        }
    }
}
