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

        public static Dictionary<string, int> ReturnWordCountFromFileContent(string[] fileContent, Dictionary<string, int> addToDictionary)
        {
            List<string> wordsInFile = new List<string>();

            char[] delimiters = new char[] { '\r', '\n', ' ' };
            foreach (string line in fileContent)
            {
                string[] list = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                foreach (string w in list)
                    wordsInFile.Add(w.ToLower());
            }

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

        // Used to read just a single file and create a single dictionary.
        public static Dictionary<string, int> ReturnWordCountFromFile(string filePath)
        {
            Dictionary<string, int> wordsInCategory = new Dictionary<string, int>();
            
            string[] fileContent = File.ReadAllLines(filePath);
            wordsInCategory = ReturnWordCountFromFileContent(fileContent, wordsInCategory);

            return wordsInCategory;
        }
    }
}
