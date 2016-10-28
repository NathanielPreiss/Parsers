using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parsers;
using System;

namespace ParsersTest
{
    [TestClass]
    public class SentenceParserTest
    {
        // Shared list of string values
        string smString = "A Single Sentence.";
        string mdString = "This should test a few words. With more than one sentence. Let's try " +
            "something other than a period as well!";
        string lgString = "This will have even more words in it! With a few different things to " +
            "test possibly? Who knows what we should test just to be safe! I'm sure there are a " +
            "lot of edge cases that this test should probably cover. One more to be safe.";

        [TestMethod]
        [TestCategory("SentenceParser")]
        public void SentenceParser_NoParseString()
        {
            try
            {
                SentenceParser parser = new SentenceParser();
                parser.Output();
            }
            catch (Exception e)
            {
                Assert.AreEqual(Parsers.Properties.Resources.ErrNoString, e.Message, "Message doesn't match.");
                return;
            }
            Assert.Fail("No exception thrown.");
        }

        [TestMethod]
        [TestCategory("SentenceParser")]
        public void SentenceParser_ParseEmptyString()
        {
            try
            {
                SentenceParser parser;

                parser = new SentenceParser();
                parser.ParseString(string.Empty);
                Assert.AreEqual(parser.WordCount, 0, "Words parsed from empty string.");
                Assert.AreEqual(parser.SentenceCount, 0, "Sentences parsed from empty string.");
                parser.Output();

                parser = new SentenceParser(string.Empty);
                Assert.AreEqual(parser.WordCount, 0, "Words parsed from empty string.");
                Assert.AreEqual(parser.SentenceCount, 0, "Sentences parsed from empty string.");
                parser.Output();
            }
            catch (Exception e)
            {
                Assert.Fail("Exception should not be thrown from empty strings. " + e.Message);
            }
        }

        [TestMethod]
        [TestCategory("SentenceParser")]
        public void SentenceParser_ParseDifferentSizes()
        {
            try
            {
                SentenceParser parser = new SentenceParser();

                parser.ParseString(smString);
                Assert.AreEqual(parser.WordCount, 3, "Small sentence word count doesn't match.");
                Assert.AreEqual(parser.SentenceCount, 1, "Small sentence count doesn't match.");
                parser.Output();

                parser.ParseString(mdString);
                Assert.AreEqual(parser.WordCount, 18, "Medium sentence word count doesn't match.");
                Assert.AreEqual(parser.SentenceCount, 3, "Medium sentence count doesn't match.");
                parser.Output(false);

                parser.ParseString(lgString);
                Assert.AreEqual(parser.WordCount, 36, "Large sentence word count doesn't match.");
                Assert.AreEqual(parser.SentenceCount, 5, "Large sentence count doesn't match.");
                parser.Output(true);
            }
            catch (Exception e)
            {
                Assert.Fail("Exception should not be thrown from valid strings. " + e.Message);
            }
        }

        [TestMethod]
        [TestCategory("SentenceParser")]
        public void SentenceParser_ConstructorDifferentSizes()
        {
            try
            {
                SentenceParser smParser = new SentenceParser(smString);
                Assert.AreEqual(smParser.WordCount, 3, "Small sentence word count doesn't match.");
                Assert.AreEqual(smParser.SentenceCount, 1, "Small sentence count doesn't match.");
                smParser.Output();

                SentenceParser mdParser = new SentenceParser(mdString);
                Assert.AreEqual(mdParser.WordCount, 18, "Medium sentence word count doesn't match.");
                Assert.AreEqual(mdParser.SentenceCount, 3, "Medium sentence count doesn't match.");
                mdParser.Output(false);

                SentenceParser lgParser = new SentenceParser(lgString);
                Assert.AreEqual(lgParser.WordCount, 36, "Large sentence word count doesn't match.");
                Assert.AreEqual(lgParser.SentenceCount, 5, "Large sentence count doesn't match.");
                lgParser.Output(true);
            }
            catch (Exception e)
            {
                Assert.Fail("Exception should not be thrown from valid strings. " + e.Message);
            }
        }
    }
}