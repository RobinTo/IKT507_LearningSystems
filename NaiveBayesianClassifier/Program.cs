using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaiveBayesianClassifier
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Dictionary<string, int>> dataSet = new Dictionary<string, Dictionary<string, int>>();

            dataSet = DataReader.ReadFiles();

            //dataAnalyzer.Analyze("filnavn", dataSet);
        }
    }
}
