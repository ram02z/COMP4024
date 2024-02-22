using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.EditMode
{
    public class FileUtilTetsts
    {
        private string validCSVFile = "Assets/Tests/EditMode/TestCSVs/valid";
        private string invalidCSVFile = "Assets/Tests/EditMode/TestCSVs/invalid";
        private string emptyCSVFile = "Assets/Tests/EditMode/TestCSVs/empty";
        private string invalidCSVPath = "Assets/Tests/EditMode/TestCSVs/non_existent";
        private string testCSVDirectory = "Assets/Tests/EditMode/TestCSVs";
        private string invalidDirectory = "Assets/Tests/EditMode/non_existent";



        private List<string> filenames = new List<string> {"empty", "invalid", "valid"};

        private Dictionary<string, string> validCSVContent = new Dictionary<string, string>
        {
            {"a","a"},
            {"b","b"},
            {"c","c"},
        };

        [Test]
        public void ReadCSVFile_WhenFileIsValid_ReturnsDataInDictionary()
        {
            Dictionary<string, string> csvContent = FileUtil.ReadCSVFile(validCSVFile);
            Assert.AreEqual(csvContent, validCSVContent);
        }

        [Test]
        public void ReadCSVFile_WhenFileIsEmpty_ReturnsEmptyDictionary()
        {
            Dictionary<string, string> csvContent = FileUtil.ReadCSVFile(emptyCSVFile);
            Assert.AreEqual(csvContent, new Dictionary<string, string>());
        }

        [Test]
        public void ReadCSVFile_WhenFileHasMoreThanTwoColumns_ReturnsFirstTwoColumnsInDictionary()
        {
            Dictionary<string, string> csvContent = FileUtil.ReadCSVFile(invalidCSVFile);
            Assert.AreEqual(csvContent, validCSVContent);
        }

        [Test]
        public void ReadCSVFile_WhenFileDoesNotExist_ReturnsEmptyDictionary()
        {
            Dictionary<string, string> csvContent = FileUtil.ReadCSVFile(invalidCSVPath);
            Assert.AreEqual(csvContent, new Dictionary<string, string>());
        }

        [Test]
        public void GetFileNames_WhenDirectoryExists_ReturnsListOfFiles()
        {
            List<string> filenameContent = FileUtil.GetFileNames(testCSVDirectory);
            Assert.AreEqual(filenames, filenameContent);
        }

        [Test]
        public void GetFileNames_WhenDirectoryDoesNotExist_ReturnsNull()
        {
            List<string> filenameContent = FileUtil.GetFileNames(invalidDirectory);
            Assert.Null(filenameContent);
        }
    }
}
