using NUnit.Framework;
using System.Collections.Generic;

/*
 * Models download: http://opennlp.sourceforge.net/models-1.5/
 * Fontes: http://seanglover.com/blog/2012/08/extracting-noun-phrases-with-contextual-relevance-in-net-using-opennlp/
 *         https://www.programcreek.com/2012/05/opennlp-tutorial/
 */

namespace NER_Library.Tests
{
    [TestFixture]
    internal class TagsTests
    {
        private const string TEXT = "Mugica said on Wednesday that Smartmatic detected the overstated turnout " +
                "because of Venezuela's automated election system. \"We estimate the difference between the " +
                "actual participation and the one announced by authorities is at least 'm votes,\" he said. " +
                "\"We know, without any doubt, that the turnout of the recent election for a national " +
                "constituent assembly was manipulated.\nThe turnout figure had previously been contested.";

        [Test]
        public void ShouldReturnsDisambiguateTags()
        {
            Tag _tag = new Tag();
            List<Tag> tags = _tag.DisambiguateTags(new List<string> { TEXT });
            Assert.IsTrue(tags?.Count > 0);
        }
    }
}
