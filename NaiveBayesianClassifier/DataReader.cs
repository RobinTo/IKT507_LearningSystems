using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaiveBayesianClassifier
{
    class DataReader
    {
        public static Dictionary<string, Dictionary<string, int>> ReadFiles()
        {
            Dictionary<string, Dictionary<string, int>> dataSet = new Dictionary<string, Dictionary<string, int>>();


            string[] categories = Directory.GetDirectories(Directory.GetCurrentDirectory() + "..\\..\\..\\20_newsgroups");
            string[] strippedCategories = new string[categories.Length];

            int i = 0;
            foreach (string category in categories)
            {
                string strippedCategory = category.Substring(category.LastIndexOf('\\') + 1, category.Length - 1 - category.LastIndexOf('\\'));
                strippedCategories[i] = strippedCategory;
                i++;
            }

            for (int t = 0; t < categories.Length; t++)
            {
                Dictionary<string, int> wordsInCategory = new Dictionary<string, int>();
                string[] files = Directory.GetFiles(categories[t]);

                foreach (string filePath in files)
                {
                    wordsInCategory = ReturnWordCountFromFile(filePath, wordsInCategory);
                }

                dataSet[strippedCategories[t]] = wordsInCategory;
            }


            return dataSet;
        }

        // Reads words in a file and adds the count to an existing dictionary.
        public static Dictionary<string, int> ReturnWordCountFromFile(string filePath, Dictionary<string, int> addToDictionary)
        {
            string fileContent = File.ReadAllText(filePath).ToLower();

            Regex rgx = new Regex("[^a-zA-Z0-9-]");
            fileContent = rgx.Replace(fileContent, ".");
            string[] wordsInFile = fileContent.Split('.');

            foreach (string word in wordsInFile)
            {
                if (addToDictionary.ContainsKey(word))
                {
                    addToDictionary[word] = addToDictionary[word] + 1;
                }
                else
                {
                    addToDictionary[word] = 1;
                }
            }

            if (addToDictionary.ContainsKey(""))
            {
                addToDictionary.Remove("");
            }

            return addToDictionary;
        }

        // Same as previous function, except it starts with an empty dictionary.
        public static Dictionary<string, int> ReturnWordCountFromFile(string filePath)
        {
            Dictionary<string, int> wordsInCategory = new Dictionary<string, int>();
            string fileContent = File.ReadAllText(filePath).ToLower();
            Regex rgx = new Regex("[^a-zA-Z0-9-]");
            fileContent = rgx.Replace(fileContent, ".");
            string[] wordsInFile = fileContent.Split('.');

            foreach (string word in wordsInFile)
            {
                if (wordsInCategory.ContainsKey(word))
                {
                    wordsInCategory[word] = wordsInCategory[word] + 1;
                }
                else
                {
                    wordsInCategory[word] = 1;
                }
            }

            if (wordsInCategory.ContainsKey(""))
            {
                wordsInCategory.Remove("");
            }

            return wordsInCategory;
        }
    }
}
