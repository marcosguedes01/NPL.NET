using java.io;
using opennlp.tools.postag;
using opennlp.tools.sentdetect;
using opennlp.tools.tokenize;
using opennlp.tools.util;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/*
 * Models download: http://opennlp.sourceforge.net/models-1.5/
 * Fontes: http://seanglover.com/blog/2012/08/extracting-noun-phrases-with-contextual-relevance-in-net-using-opennlp/
 *         https://www.programcreek.com/2012/05/opennlp-tutorial/
 */
namespace NER_Library
{
    public class NLPLib
    {
        private const string PATH_MODELS_NPL = @"C:\OpenNPL\Models\";
        private List<Tag> lstTags;

        public NLPLib()
        {
            lstTags = new List<Tag>();
        }

        public List<Tag> GetTags(string paragraph)
        {
            var bin = GetFileStream("en-pos-maxent.bin");
            POSModel model = new POSModel(bin);
            POSTagger tagger = new POSTaggerME(model);

            var sentenceSpans = SentPosDetect(paragraph);
            List<Tag> tagsResult = new List<Tag>();

            foreach (var sentenceSpan in sentenceSpans)
            {
                var sentence = sentenceSpan.getCoveredText(paragraph).toString();
                var start = sentenceSpan.getStart();
                var end = sentenceSpan.getEnd();

                var tokenSpans = GetTokens(sentence);
                var tokens = new string[tokenSpans.Length];
                for (var i = 0; i < tokens.Length; i++)
                {
                    tokens[i] = tokenSpans[i].getCoveredText(sentence).toString();
                    var tag = tagger.tag(new[] { tokenSpans[i].getCoveredText(sentence).toString() }).FirstOrDefault();

                    tagsResult.Add(new Tag
                    {
                        startIndex = start,
                        endIndex = end,
                        category = tag
                    });
                }
            }

            return tagsResult;
        }
        
        public Span[] SentPosDetect(string paragraph)
        {
            var bin = GetFileStream("en-sent.bin");
            SentenceModel model = new SentenceModel(bin);
            SentenceDetectorME sdetector = new SentenceDetectorME(model);

            Span[] sentences = sdetector.sentPosDetect(paragraph);

            bin.close();

            return sentences;
        }

        public Span[] GetTokens(string paragraph)
        {
            var bin = GetFileStream("en-token.bin");
            TokenizerModel model = new TokenizerModel(bin);
            TokenizerME tokenizer = new TokenizerME(model);

            Span[] tokens = tokenizer.tokenizePos(paragraph);

            bin.close();

            return tokens;
        }

        private FileInputStream GetFileStream(string fileName)
        {
            return new FileInputStream(Path.Combine(PATH_MODELS_NPL, fileName));
        }
    }
}