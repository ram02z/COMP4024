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
        private string validCSVFile = "Tests/CSV/valid";
        private string invalidCSVFile = "Tests/CSV/invalid";
        private string emptyCSVFile = "Tests/CSV/empty";
        private string invalidCSVPath = "Tests/CSV/non_existent";
        private string testCSVDirectory = "Tests/CSV/";
        private string invalidDirectory = "Tests/non_existent";



        private List<string> filenames = new List<string> {"empty", "invalid", "valid"};

        private Dictionary<string, string> validCSVContent = new Dictionary<string, string>
        {
            {"a","a"},
            {"b","b"},
            {"c","c"},
        };

        private Dictionary<string, string> CSVExtraCommaContent = new Dictionary<string, string>
        {
            {"a","a, a, a"},
            {"b","b, b, b"},
            {"c","c, c, c"},
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
        public void ReadCSVFile_WhenFileHasMoreThanTwoColumns_CombinesDataToSecondColumn()
        {
            Dictionary<string, string> csvContent = FileUtil.ReadCSVFile(invalidCSVFile);
            Assert.AreEqual(csvContent, CSVExtraCommaContent);
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
        public void GetFileNames_WhenDirectoryDoesNotExist_ReturnsEmpty()
        {
            List<string> filenameContent = FileUtil.GetFileNames(invalidDirectory);
            Assert.IsEmpty(filenameContent);
        }
    }
}
