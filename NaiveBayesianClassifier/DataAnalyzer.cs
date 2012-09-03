using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NaiveBayesianClassifier
{
    class DataAnalyzer
    {
        public string Analyze(String filename, Dictionary<String, Dictionary<String, int>> dataset)
        {
            // Fetch file, parse it and wordify it as a Dictionary
            Dictionary<String, int> candidateDocument = this.ReadData(filename);

            // Determine which category it belongs to, and calculate the probability for this
            String results = this.CalculateCategory(candidateDocument, dataset);

            return results;
        }

        private Dictionary<String, int> ReadData(String filename)
        {
            // Read the file and parse it to a string
            StreamReader streamReader = new StreamReader(filename);
            string input = streamReader.ReadToEnd();
            streamReader.Close();

            // Separate each word in the string, and put it into the proper category
            Dictionary<String, int> candidateDocument = new Dictionary<String, int>();
            
            /*
            for (int counter = 0; counter < input.)
            {

            }
            */
            return candidateDocument;
        }

        private String CalculateCategory(Dictionary<String, int> candidateDocument, Dictionary<String, Dictionary<String, int>> dataset)
        {
            // Do Magic



            return "Category, XX % Probability";
        }
    }
}
