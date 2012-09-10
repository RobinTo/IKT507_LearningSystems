using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NaiveBayesianClassifier
{
    class DataAnalyzer
    {
        Dictionary<string, Dictionary<string, double>> categoryWithWWL = new Dictionary<string, Dictionary<string, double>>();

        public void calcCWWWL(DataSet dataset)
        {

            Dictionary<string, int> vocabulary = dataset.Vocabulary;
            foreach (KeyValuePair<string, Dictionary<string, int>> categoryWords in dataset.getDataSet)
            {
                Dictionary<string, int> categoryWithDocumentCount = dataset.CategoryDocumentCount;
                Dictionary<string, double> wordWithLikelyhood = new Dictionary<string, double>();

                foreach (KeyValuePair<string, int> w in vocabulary)
                {
                    if (categoryWords.Value.ContainsKey(w.Key))
                        wordWithLikelyhood[w.Key] = (categoryWords.Value[w.Key] + 1.0) / (dataset.CategoryWordcount[categoryWords.Key] + vocabulary.Count);
                    else
                        wordWithLikelyhood[w.Key] = (1.0) / (dataset.CategoryWordcount[categoryWords.Key] + vocabulary.Count);
                }
                categoryWithWWL[categoryWords.Key] = wordWithLikelyhood;
            }
        }

        public string Analyze(String filename, DataSet dataSet)
        {
            // Fetch file, parse it and wordify it as a Dictionary
            Dictionary<String, int> candidateDocument = DataReader.ReturnWordCountFromFile(filename);

            // Determine which category it belongs to, and calculate the probability for this
            String results = this.CalculateCategory(candidateDocument, dataSet);

            return results;
        }

        private String CalculateCategory(Dictionary<string, int> candidateDocument, DataSet dataset)
        {
            // Vocabulary <- All distinct words in aall documents.
            Dictionary<string, int> vocabulary = dataset.Vocabulary;
            Dictionary<string, double> highestEffect = new Dictionary<string, double>();

            double pH = 0;
            string max_group = "";
            double max_p = 0;
            foreach (KeyValuePair<string, Dictionary<string, int>> categoryWords in dataset.getDataSet)
            {
                Dictionary<string, int> categoryWithDocumentCount = dataset.CategoryDocumentCount;
                pH = (double)categoryWithDocumentCount[categoryWords.Key] / (double)dataset.documentCount;
                Dictionary<string, double> wordWithLikelyhood = new Dictionary<string, double>();

                wordWithLikelyhood = categoryWithWWL[categoryWords.Key];


                //Finds group with max P(O | H) * P(H)

                //Calculates P(O | H) * P(H) for candidate group
                double p = 0;
                foreach (KeyValuePair<string, int> wordPair in candidateDocument)
                {
                    if (vocabulary.ContainsKey(wordPair.Key))
                    {

                        p += Math.Log(wordPair.Value * (wordWithLikelyhood[wordPair.Key]));
                    }
                }
                p *= (pH);
                if (p > max_p || max_p == 0)
                {
                    max_p = p;
                    max_group = categoryWords.Key;
                }

            }



            return "Category: " + max_group + ". Likelyhood: " + max_p + ".";
        }
    }
}
