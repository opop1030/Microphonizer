using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProofOfConceptMicrophonizer
{
    
    class Wave
    {
        //Source: https://web.archive.org/web/20141213140451/https://ccrma.stanford.edu/courses/422/projects/WaveFormat/
        //The "RIFF" chunk descriptor
        public string ChunkID; //First 4
        public int ChunkSize; // next 4
        public string Format; // next 4
        //The "FMT Sub-chunk"
        public string SubChunk1ID; //next 4
        public int SubChunk1Size; //next 4
        public short AudioFormat; //next 2 => Must be 1 (Uncompressed)
        public short NumChannels; //next 2 => Must be 1 (Mono) || 2 would be Stereo
        public int SampleRate;//next 4 
        public int ByteRate;//next 4 =>
        public short BlockAlign;//next 2
        public short BitsPerSample;//next 2
        //The Data sub-chunk
        public string SubChunk2ID; //next 4
        public int SubChunk2Size;//next 4
        public byte[] DATA; //Rest

        public Wave(string path)
        {
            var AllData = File.ReadAllBytes(path);

            //RIFF
            ChunkID = Encoding.ASCII.GetString(AllData.Skip(0).Take(4).ToArray());
            ChunkSize = BitConverter.ToInt32(AllData.Skip(4).Take(4).ToArray(), 0);
            Format = Encoding.ASCII.GetString(AllData.Skip(8).Take(4).ToArray());

            //FMT
            SubChunk1ID = Encoding.ASCII.GetString(AllData.Skip(12).Take(4).ToArray());
            SubChunk1Size = BitConverter.ToInt32(AllData.Skip(16).Take(4).ToArray(), 0);
            AudioFormat = BitConverter.ToInt16(AllData.Skip(20).Take(2).ToArray(), 0);
            NumChannels = BitConverter.ToInt16(AllData.Skip(22).Take(2).ToArray(), 0);
            SampleRate = BitConverter.ToInt32(AllData.Skip(24).Take(4).ToArray(), 0);
            ByteRate = BitConverter.ToInt32(AllData.Skip(28).Take(4).ToArray(), 0);
            BlockAlign = BitConverter.ToInt16(AllData.Skip(32).Take(2).ToArray(), 0);
            BitsPerSample = BitConverter.ToInt16(AllData.Skip(34).Take(2).ToArray(), 0);

            //DATA
            SubChunk2ID = Encoding.ASCII.GetString(AllData.Skip(36).Take(4).ToArray()); //BUG: Sollte eigentlich "data" beinhalten
            SubChunk2Size = BitConverter.ToInt32(AllData.Skip(40).Take(4).ToArray(), 0);

            //Extract Data
            DATA = AllData.Skip(44).ToArray();
        }
    }
   
}
