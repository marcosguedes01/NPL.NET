using NUnit.Framework;
using opennlp.tools.util;
using System.Collections.Generic;

namespace NER_Library.Tests
{
    [TestFixture]
    internal class NLPLibTests
    {
        private const string TEXT = "Mugica said on Wednesday that Smartmatic detected the overstated turnout " +
                "because of Venezuela's automated election system. \"We estimate the difference between the " +
                "actual participation and the one announced by authorities is at least 'm votes,\" he said. " +
                "\"We know, without any doubt, that the turnout of the recent election for a national " +
                "constituent assembly was manipulated.\nThe turnout figure had previously been contested.";

        [Test]
        public void ShouldReturnsTags()
        {
            NLPLib nlp = new NLPLib();
            List<Tag> tags = nlp.GetTags(TEXT);
            Assert.IsTrue(tags?.Count > 0);
        }

        [Test]
        public void ShouldReturnsSentencesPosDetect()
        {
            NLPLib nlp = new NLPLib();
            Span[] sentences = nlp.SentPosDetect(TEXT);
            Assert.IsTrue(sentences?.Length > 0);
        }

        [Test]
        public void ShouldReturnsTokens()
        {
            NLPLib nlp = new NLPLib();
            Span[] sentences = nlp.GetTokens(TEXT);
            Assert.IsTrue(sentences?.Length > 0);
        }
    }
}
