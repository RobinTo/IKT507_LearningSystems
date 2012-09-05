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
            Dictionary<String, int> candidateDocument = DataReader.ReturnWordCountFromFile(filename);

            // Determine which category it belongs to, and calculate the probability for this
            String results = this.CalculateCategory(candidateDocument, dataset);

            return results;
        }

        private String CalculateCategory(Dictionary<String, int> candidateDocument, Dictionary<String, Dictionary<String, int>> dataset)
        {
            double ph = 





            //Python code from the lecture.

            /*# Calculates P(O | H)
            p_word_given_group = {}
            for group in posts.keys():
            p_word_given_group[group] = {}
            # Counts the number of words
            for word in vocabulary.keys():
            p_word_given_group[group][word] = 1.0
            for word in posts[group]:
            if vocabulary.has_key(word):
            p_word_given_group[group][word] += 1.0
            # Calculates probabilities
            for word in vocabulary.keys():
            p_word_given_group[group][word] /= len(posts[group]) +
            len(vocabulary) */

            
            /*# Finds group with max P(O | H) * P(H)
            max_group = 0
            max_p = 1
            for candidate_group in posts.keys():
            # Calculates P(O | H) * P(H) for candidate group
            p = math.log(p_group[candidate_group])
            for word in post_to_be_classified:
            if vocabulary.has_key(word):
            p += math.log(p_word_given_group[candidate_group][word])
            if p > max_p or max_p == 1:
            max_p = p
            max_group = candidate_group*/

            return "Category, XX % Probability";
        }
    }
}
