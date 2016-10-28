using Parsers;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Program
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SentenceParser parser = new SentenceParser();

                parser.ParseString("A test of words. And another Sentence.");
                parser.Output(true);

                parser.ParseString(@"Hello world! How are you? I am fine. Make sure to test for new lines and those 
                pesky 12.34.56.78 numbers as well!");
                parser.Output(true);

                parser.ParseString("Given an arbitrary text document written in English, write a program that will generate a " +
                    "concordance, i.e. an alphabetical list of all word occurrences, labeled with word frequencies. Bonus: " +
                    "label each word with the sentence numbers in which each occurrence appeared.");
                parser.Output(true);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}