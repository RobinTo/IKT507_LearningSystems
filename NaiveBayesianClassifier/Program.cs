using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaiveBayesianClassifier
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;

            Console.WriteLine("Creating dataset from test files.");

            DataSet data = new DataSet();
            data.ReadFiles();

            DateTime end = DateTime.Now;
            TimeSpan duration = end - start;

            Console.WriteLine("Dataset created in {0} seconds.", String.Format("{0:0.00}", duration.TotalSeconds));

            Console.WriteLine("Starting to analyze standard document.");

            DataAnalyzer dataAnalyzer = new DataAnalyzer();
            dataAnalyzer.calcCWWWL(data);

            Console.WriteLine(dataAnalyzer.Analyze(@"C:\Users\Robin\Desktop\IKT507_LearningSystems\NaiveBayesianClassifier\20_newsgroups\alt.atheism\51123", data));


            Console.WriteLine("To analyze another document, enter a filepath. To analyze 20 documents from each category, enter \"analyzeAll\" Exit to end.");

            bool done = false;
            while (!done)
            {
                string command = Console.ReadLine();

                if (command.ToLower() == "exit")
                    done = true;
                else if (command.ToLower() == "analyzeall")
                {
                    float correct = 0.0f;
                    float wrong = 0.0f;
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
                        int analyzedCorrectly = 0;
                        int analyzedWrong = 0;
                        Dictionary<string, int> wordsInCategory = new Dictionary<string, int>();
                        string[] files = Directory.GetFiles(categories[t]);

                        for (int q = 0; q < files.Length; q++)
                        {
                            string analyzed = dataAnalyzer.Analyze(files[q], data);
                            if (analyzed.Contains(strippedCategories[t]))
                                analyzedCorrectly++;
                            else
                                analyzedWrong++;
                            //Console.WriteLine(strippedCategories[t] + " document analyzed as: " + analyzed);
                        }
                        correct += analyzedCorrectly;
                        wrong += analyzedWrong;
                        Console.WriteLine("Out of " + (analyzedWrong + analyzedCorrectly) + " documents in " + strippedCategories[t] + ".");
                        Console.WriteLine(analyzedCorrectly + " were correctly classified.");
                        Console.WriteLine(analyzedWrong + " were wrongly classified.");
                    }
                    float percent = ((correct/(correct + wrong)) * 100.0f);
                    Console.WriteLine("Correctly classified: " + correct + " of " + (correct+wrong) + " - " + percent + "%");
                }
                else
                    Console.WriteLine(dataAnalyzer.Analyze(command, data));

            }

            Console.ReadLine();
        }


    }
}
