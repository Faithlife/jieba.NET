using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JiebaNet.Segmenter.Common;

namespace JiebaNet.Analyser
{
    public class IdfLoader
    {
        internal IDictionary<string, double> IdfFreq { get; set; }
        internal double MedianIdf { get; set; }

        public IdfLoader(Stream idfStream = null)
        {
            IdfFreq = new Dictionary<string, double>();
            MedianIdf = 0.0;
            if (idfStream != null)
            {
                var lines = idfStream.ReadAllLinesThenDispose();
                IdfFreq = new Dictionary<string, double>();
                foreach (var line in lines)
                {
                    var parts = line.Trim().Split(' ');
                    var word = parts[0];
                    var freq = double.Parse(parts[1]);
                    IdfFreq[word] = freq;
                }

                MedianIdf = IdfFreq.Values.OrderBy(v => v).ToList()[IdfFreq.Count / 2];
            }
        }
    }
}