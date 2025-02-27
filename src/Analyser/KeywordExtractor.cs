using System.Collections.Generic;
using System.IO;
using JiebaNet.Segmenter.Common;

namespace JiebaNet.Analyser
{
    public abstract class KeywordExtractor
    {
        protected static readonly List<string> DefaultStopWords = new List<string>()
        {
            "the", "of", "is", "and", "to", "in", "that", "we", "for", "an", "are",
            "by", "be", "as", "on", "with", "can", "if", "from", "which", "you", "it",
            "this", "then", "at", "have", "all", "not", "one", "has", "or", "that"
        };

        protected virtual ISet<string> StopWords { get; set; }

        public void SetStopWords(Stream stopWordsStream)
        {
            StopWords = new HashSet<string>();

            if (stopWordsStream != null)
            {
                var lines = stopWordsStream.ReadAllLines();
                foreach (var line in lines)
                {
                    StopWords.Add(line.Trim());
                }
            }
        }

        public void AddStopWord(string word)
        {
            if (!StopWords.Contains(word))
            {
                StopWords.Add(word.Trim());
            }
        }

        public void AddStopWords(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                AddStopWord(word);
            }
        }

        public abstract IEnumerable<string> ExtractTags(string text, int count = 20, IEnumerable<string> allowPos = null);
        public abstract IEnumerable<WordWeightPair> ExtractTagsWithWeight(string text, int count = 20, IEnumerable<string> allowPos = null);
    }
}