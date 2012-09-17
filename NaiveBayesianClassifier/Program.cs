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

            DateTime end = DateTime.Now;
            TimeSpan duration = end - start;

            Console.WriteLine("Dataset created in {0} seconds.", String.Format("{0:0.00}", duration.TotalSeconds));
            Console.WriteLine("Calculating P(w|h) for each word in each category using given dataset.");
            DataAnalyzer dataAnalyzer = new DataAnalyzer();
            dataAnalyzer.calcCWWWL(data);

            Console.Clear();
            Console.WriteLine("Dataset is ready.");
            Console.WriteLine("Enter command for execution - \"help\" for help.");

            bool done = false;
            while (!done)
            {
                Console.Write("> ");
                string command = Console.ReadLine();
                string[] commandSplit = command.Split(' ');

                switch (commandSplit[0].ToLower())
                {
                    case "exit":
                        {
                            done = true;
                            break;
                        }
                    case "analyzeall":
                        {
                            Console.Clear();
                            AnalyzeAll(dataAnalyzer, data);
                            break;
                        }
                    case "analyze":
                        {
                            Console.Clear();
                            try
                            {
                                if (commandSplit.Length > 1)
                                    Console.WriteLine(dataAnalyzer.Analyze(commandSplit[1], data));
                                else
                                    Console.WriteLine("Enter a path as well. Syntax \"analyze filepath\".");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Unable to analyze " + commandSplit[1] + ". Check that file is located at this path.");
                            }
                            break;
                        }
                    case "help":
                        {
                            Console.Clear();
                            Console.WriteLine("Commands:");
                            Console.WriteLine("Exit - exits the program.");
                            Console.WriteLine("analyze filePath - attempts to categorize file located at given filepath.");
                            Console.WriteLine("analyzeAll - attempts to categorize all files in training set.");
                            break;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Unable to recognize command - write help for list of commands.");
                            break;
                        }
                }
            }
        }

        // Runs the dataAnalyzers analyze function on all files in training set.
        public static void AnalyzeAll(DataAnalyzer dataAnalyzer, DataSet data)
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

                for (int q = files.Length - 300; q < files.Length; q++)
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
            float percent = ((correct / (correct + wrong)) * 100.0f);
            Console.WriteLine("Correctly classified: " + correct + " of " + (correct + wrong) + " - " + percent + "%");
        }
    }
}
