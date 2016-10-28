using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Parsers
{
    // Used to split a string a list of unique words and keep track of their occurences within the string
    public class SentenceParser
    {
        // Local helper class to keep track of word data
        private class WordStruct
        {
            private string _word; // Key value being tracked
            private int _count; // Count of the total instances of the key
            private List<int> _sentenceIndex; // List of unique sentance index

            // Constructor
            public WordStruct(string word)
            {
                _word = word;
                _count = 0;
                _sentenceIndex = new List<int>();
            }

            // Checks for positive count and adds the values
            public void AddValues(int index, int count)
            {
                if (count < 1)
                    return;

                _count += count;
                _sentenceIndex.AddRange(Enumerable.Repeat(index + 1, count));
            }

            // Outputs a formatted string from the work data
            public string Output()
            {
                return _word + " {" + _count + ":" + string.Join(",", _sentenceIndex) + "}";
            }
        }

        // Class members
        private string _originalString;
        private List<WordStruct> _words;
        private int _sentenceCount;

        // Accessors
        public int SentenceCount
        {
            get
            {
                return _sentenceCount;
            }
        }

        public int WordCount
        {
            get
            {
                return _words.Count;
            }
        }

        // Constructors
        public SentenceParser()
        {
            _originalString = string.Empty;
            _words = null;
            _sentenceCount = 0;
        }

        public SentenceParser(string parseValue)
            : this()
        {
            ParseString(parseValue);
        }

        // Builds word data and outputs information to console
        public void ParseString(string parseValue)
        {
            // Save the original value
            _originalString = parseValue;

            // Replace all breaks and set as lowercase value
            parseValue = Regex.Replace(parseValue, @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085]+", String.Empty).ToLower();

            // Creates a list of each sentence
            string[] sentences = GetSentences(parseValue);

            // Creates a list of unique words in alphabetical order
            string[] words = GetWords(parseValue);

            // Loop through the list of words
            _words = new List<WordStruct>();
            foreach (string word in words)
            {
                // Loop through the list of sentences adding to the word structure
                WordStruct currWord = new WordStruct(word);
                for (int i = 0; i < sentences.Length; i++)
                    currWord.AddValues(i, Regex.Matches(sentences[i].ToLower(), word).Count);

                // Add the completed structure to the list
                _words.Add(currWord);
            }
        }

        // Outputs information to console
        public void Output(bool showString = false)
        {
            if (_words == null)
                throw new Exception(Properties.Resources.ErrNoString);

            // Display the string if requested
            if (showString)
                Console.WriteLine(_originalString);

            // Console output
            foreach (WordStruct word in _words)
                Console.WriteLine(word.Output());
            Console.WriteLine("------------\n");
        }

        // Returns a list of sentences from within the input string
        private string[] GetSentences(string input)
        {
            string[] sentences = Regex.Matches(input, @"(\S.+?[.!?])(?=\s+|$)")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            _sentenceCount = sentences.Length;

            PrintLog(sentences);
            return sentences;
        }

        // Returns a sorted array of unique words within the input string (Drops non alpha chars)
        private string[] GetWords(string input)
        {
            string[] words = Regex.Replace(input, @"[^a-zA-Z -]", string.Empty)
                .Split(' ')
                .Where(d => d != "")
                .Distinct()
                .OrderBy(d => d)
                .ToArray();

            PrintLog(words);
            return words;
        }

        // Debug output
        [Conditional("DEBUG")]
        private void PrintLog(string[] displayValues)
        {
            foreach (string str in displayValues)
                Console.WriteLine(str);
        }
    }
}