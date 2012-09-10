using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaiveBayesianClassifier
{

    class DataSet
    {
        public int wordCount = 0;
        public int documentCount = 0;
        public int categoryCount = 0;

        Dictionary<string, Dictionary<string, int>> dataSet;
        public Dictionary<string, Dictionary<string, int>> getDataSet
        {
            get { return dataSet; }
        }


        Dictionary<string, int> categoryWordcount = new Dictionary<string, int>();

        public Dictionary<string, int> CategoryWordcount
        {
            get { return categoryWordcount; }
        }
        Dictionary<string, int> categoryDocumentCount = new Dictionary<string, int>();
        public Dictionary<string, int> CategoryDocumentCount
        {
            get { return categoryDocumentCount; }
        }

        Dictionary<string, int> vocabulary = new Dictionary<string, int>();
        public Dictionary<string, int> Vocabulary
        {
            get { return vocabulary; }
        }

        bool initialized;

        public DataSet()
        {
            dataSet = new Dictionary<string, Dictionary<string, int>>();
            initialized = false;
        }

        public Dictionary<string, Dictionary<string, int>> ReadFiles()
        {
            string[] categories = Directory.GetDirectories(Directory.GetCurrentDirectory() + "..\\..\\..\\20_newsgroups");
            string[] strippedCategories = new string[categories.Length];

            categoryCount = categories.Count();

            int i = 0;
            foreach (string category in categories)
            {
                string strippedCategory = category.Substring(category.LastIndexOf('\\') + 1, category.Length - 1 - category.LastIndexOf('\\'));
                strippedCategories[i] = strippedCategory;
                CategoryDocumentCount[strippedCategory] = 0;
                i++;
            }

            for (int t = 0; t < categories.Length; t++)
            {
                Dictionary<string, int> wordsInCategory = new Dictionary<string, int>();
                string[] files = Directory.GetFiles(categories[t]);
                documentCount += files.Count();

                // Set number of documents for each category.
                categoryDocumentCount[strippedCategories[t]] = files.Count();
                categoryWordcount[strippedCategories[t]] = 0;
                List<string[]> fileContents = new List<string[]>();
                foreach (string filePath in files)
                {
                    fileContents.Add(File.ReadAllLines(filePath));
                }
                foreach(string[] content in fileContents)
                {
                    wordsInCategory = ReturnWordCountFromFileContent(content, wordsInCategory);
                }

                foreach (KeyValuePair<string, int> pair in wordsInCategory)
                {
                    categoryWordcount[strippedCategories[t]] += pair.Value;
                }

                dataSet[strippedCategories[t]] = wordsInCategory;
            }

            Console.WriteLine("Generating vocab by function.");
            GenerateVocabulary();
            Console.WriteLine("Done generating vocab by function.");

            initialized = true;
            return dataSet;
        }

        // Reads words in a file and adds the count to an existing dictionary.
        public Dictionary<string, int> ReturnWordCountFromFilePath(string filePath, Dictionary<string, int> addToDictionary)
        {
            string[] fileContent = File.ReadAllLines(filePath);
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
                    /*if (!vocabulary.Contains(word) && word != "")
                    {
                        vocabulary.Add(word);
                        if (vocabulary.Count % 10000 == 0)
                            Console.WriteLine("Vocabulary size: " + vocabulary.Count);
                    }*/
                }


            }

            if (addToDictionary.ContainsKey(""))
            {
                addToDictionary.Remove("");
            }

            return addToDictionary;
        }

        public Dictionary<string, int> ReturnWordCountFromFileContent(string[] fileContent, Dictionary<string, int> addToDictionary)
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
                    /*
                    if (!vocabulary.Contains(word) && word != "")
                    {
                        vocabulary.Add(word);
                        if (vocabulary.Count % 10000 == 0)
                            Console.WriteLine("Vocabulary size: " + vocabulary.Count);
                    }*/
                }


            }

            if (addToDictionary.ContainsKey(""))
            {
                addToDictionary.Remove("");
            }

            return addToDictionary;
        }

        public void GenerateVocabulary()
        {
            foreach (KeyValuePair<string, Dictionary<string, int>> keyPair in dataSet)
            {
                foreach (KeyValuePair<string, int> sPair in keyPair.Value)
                {
                    if (!vocabulary.ContainsKey(sPair.Key))
                    {
                        vocabulary[sPair.Key] = 0;
                    }
                }
            }
        }
    }
}
