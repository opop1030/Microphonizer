using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProofOfConceptMicrophonizer
{
    class Microphonizer
    {
        public byte[] HeaderInformation;
        public byte[] Direct;
        public byte[] Mic;

        public Microphonizer(string PathToDirect, string PathToMic)
        {
            Wave test = new Wave(PathToDirect);
            Direct = File.ReadAllBytes(PathToDirect);
            Mic = File.ReadAllBytes(PathToMic);
        }

        public Microphonizer(byte[] Direct, byte[] Mic)
        {
            this.Direct = Direct;
            this.Mic = Mic;
        }



        //------------------------------------------------------------------------------------------------------------//
        //                                              Proof Of Concept                                              //

        // 23.04.2018
        public List<IndexAndValue> CompareSignals()
        {
            List<IndexAndValue> Matches = new List<IndexAndValue>();

            for (var x = 0; x < Direct.Length; x++)
            {
                if (Direct[x] != 0 && Direct[x] == Mic[x])
                {
                    Matches.Add(new IndexAndValue(x,Direct[x]));
                }
            }
            return Matches;
        }

        public List<IndexAndValue> CompareSignals(int Range)
        {
            List<IndexAndValue> Matches = new List<IndexAndValue>();

            for (var x = 0; x < Direct.Length; x++)
            {
                if (
                    Direct[x] != 0 &&  
                    (Direct[x]-Range) <= Mic[x] &&
                    (Direct[x]+Range) >= Mic[x]
                )
                {
                    Matches.Add(new IndexAndValue(x, Direct[x]));
                }
            }
            return Matches;
        }

        //                                              Proof Of Concept                                              //
        //------------------------------------------------------------------------------------------------------------//
    }




    class IndexAndValue
    {
        public IndexAndValue(int Index, byte Value)
        {
            this.Index = Index;
            this.Value = Value;
        }

        public int Index;
        public byte Value;
    }
}
