using System;
using System.Collections.Generic;
using System.Linq;

/*
 * Models download: http://opennlp.sourceforge.net/models-1.5/
 * Fontes: http://seanglover.com/blog/2012/08/extracting-noun-phrases-with-contextual-relevance-in-net-using-opennlp/
 *         https://www.programcreek.com/2012/05/opennlp-tutorial/
 */

namespace NER_Library
{
    public sealed class Tag
    {
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public string category { get; set; }

        public List<Tag> DisambiguateTags(List<string> annotations)
        {
            NLPLib nlp = new NLPLib();
            List<Tag> tags = new List<Tag>();
            foreach (string annotation in annotations)
            {
                List<Tag> tagsTemp = nlp.GetTags(annotation);
                tags.AddRange(
                    tagsTemp.GroupBy(x => x.GetHash()).Select(y => y.First())
                    .Where(l => Char.IsLetter(l.category[0]))
                    );
            }

            return tags;
        }

        public override string ToString()
        {
            return category ?? base.ToString();
        }

        public override bool Equals(object obj)
        {
            return GetHash().Equals((obj as Tag).GetHash());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private string GetHash()
        {
            return string.Concat(startIndex, endIndex, category);
        }
    }
}
