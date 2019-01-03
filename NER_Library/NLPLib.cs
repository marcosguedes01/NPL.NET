using java.io;
using opennlp.tools.chunker;
using opennlp.tools.postag;
using opennlp.tools.sentdetect;
using opennlp.tools.tokenize;
using opennlp.tools.util;
using System.Collections.Generic;
using System.Linq;

/*
 * Models download: http://opennlp.sourceforge.net/models-1.5/
 */
namespace NER_Library
{
    public class NLPLib
    {
        private List<Tag> lstTags;

        public NLPLib()
        {
            lstTags = new List<Tag>();
        }

        public List<Tag> GetTags(string paragraph)
        {
            InputStream bin = new FileInputStream(@"C:\OpenNPL\Models\en-pos-maxent.bin");
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

        public string[] SentDetect(string paragraph)
        {
            // always start with a model, a model is learned from training data
            InputStream bin = new FileInputStream(@"C:\OpenNPL\Models\en-sent.bin");
            SentenceModel model = new SentenceModel(bin);
            SentenceDetectorME sdetector = new SentenceDetectorME(model);

            string[] sentences = sdetector.sentDetect(paragraph);

            bin.close();

            return sentences;
        }

        public Span[] SentPosDetect(string paragraph)
        {
            // always start with a model, a model is learned from training data
            InputStream bin = new FileInputStream(@"C:\OpenNPL\Models\en-sent.bin");
            SentenceModel model = new SentenceModel(bin);
            SentenceDetectorME sdetector = new SentenceDetectorME(model);

            Span[] sentences = sdetector.sentPosDetect(paragraph);

            bin.close();

            return sentences;
        }

        public Span[] GetTokens(string paragraph)
        {
            // always start with a model, a model is learned from training data
            InputStream bin = new FileInputStream(@"C:\OpenNPL\Models\en-token.bin");
            TokenizerModel model = new TokenizerModel(bin);
            TokenizerME tokenizer = new TokenizerME(model);

            Span[] tokens = tokenizer.tokenizePos(paragraph);

            bin.close();

            return tokens;
        }

        public Span[] GetChunker(string[] tokens, string[] tags)
        {
            InputStream bin = new FileInputStream(@"C:\OpenNPL\Models\en-chunker.bin");
            ChunkerModel model = new ChunkerModel(bin);
            ChunkerME tokenizer = new ChunkerME(model);

            Span[] result = tokenizer.chunkAsSpans(tokens, tags);

            bin.close();

            return result;
        }
    }
}
