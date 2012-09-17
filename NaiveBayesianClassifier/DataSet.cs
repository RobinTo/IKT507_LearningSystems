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

        // Dictionary<Category name, Dictionary<word, times word occurs in category>>
        Dictionary<string, Dictionary<string, int>> dataSet;
        public Dictionary<string, Dictionary<string, int>> getDataSet
        {
            get { return dataSet; }
        }

        // Dictionary<Category Name, Number of words>
        Dictionary<string, int> categoryWordcount = new Dictionary<string, int>();
        public Dictionary<string, int> CategoryWordcount
        {
            get { return categoryWordcount; }
        }

        // Dictionary<Category name, number of documents>
        Dictionary<string, int> categoryDocumentCount = new Dictionary<string, int>();
        public Dictionary<string, int> CategoryDocumentCount
        {
            get { return categoryDocumentCount; }
        }

        // Dictionary containing the words in strings, int is irrelevant.
        // Makes creation of data set approximately thirty times faster than List<string>
        // due to speed of .Contains(string).
        Dictionary<string, int> vocabulary = new Dictionary<string, int>();
        public Dictionary<string, int> Vocabulary
        {
            get { return vocabulary; }
        }

        public DataSet()
        {
            dataSet = new Dictionary<string, Dictionary<string, int>>();
            ReadFiles();
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
                    wordsInCategory = DataReader.ReturnWordCountFromFileContent(content, wordsInCategory);
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

            return dataSet;
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
