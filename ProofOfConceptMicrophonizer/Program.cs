using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProofOfConceptMicrophonizer
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    static class ProofOfConcept
    {
        //----------------------------------------------------------------------------------------------------------------------------------------------------//
        //                                                                     24.04.2018                                                                     //
        //----------------------------------------------------------------------------------------------------------------------------------------------------//



        //----------------------------------------------------------------------------------------------------------------------------------------------------//
        //                                                                     23.04.2018                                                                     //
        //----------------------------------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Vergleicht die Bytes ob sie 1 zu 1 ensprechen.
        /// Ergebnis:
        ///     Direct Übereinstimmung mit Mic: 414826
        ///     Direct Übereinstimmung mit RandomWav (Irgendeinem Lied oder so): 191746
        ///     Differenz: 223080
        /// Deutung:
        /// Die Differenz beweist, dass die beiden Signale auch ohne anpassungen schon Gemeinsamkeiten haben.
        ///     Übereinstimmung im Verhälltnis zur Gesammten Datei:
        ///     Differenz / GesamteDateiGröße = 223.080 / 19.784.522 = 0,01127548 = 1,127548%
        /// Übereinstimmung = 1,127548%
        /// 
        /// Auf Basis dieses Ergibnisses wurde die Methode GleichheitenPrüfenRange entwickelt.
        /// 
        /// </summary>
        public static void GleichheitenPrüfen()
        {
            string DirectPath = "C:\\Users\\Jannis\\Documents\\visual studio 2015\\Projects\\ProofOfConceptMicrophonizer\\Dokumente\\DirectSignal.wav";
            string MicPath = "C:\\Users\\Jannis\\Documents\\visual studio 2015\\Projects\\ProofOfConceptMicrophonizer\\Dokumente\\MicSignal.wav";

            //Test
            var TestMP = new Microphonizer(DirectPath, MicPath);
            var Matches = TestMP.CompareSignals();
            Console.WriteLine("Matches with Mic and Direct: " + Matches.Count);

            //------------------------------------------------------------------------------------------------------------------------------------

            string RandomWavPath = "C:\\Users\\Jannis\\Documents\\visual studio 2015\\Projects\\ProofOfConceptMicrophonizer\\Dokumente\\Random.wav";

            //Test
            var RandomMP = new Microphonizer(DirectPath, RandomWavPath);
            var MatchesWithRandomWav = RandomMP.CompareSignals();
            Console.WriteLine("Matches with RandomWave and Direct: " + MatchesWithRandomWav.Count);

            Console.WriteLine("Differenz (>Warscheinliche< 'Echte' Übereinstimmungen): " + (Matches.Count - MatchesWithRandomWav.Count));

            Console.ReadKey();
        }
        /// <summary>
        /// Ergebnis:
        /// Siehe Dokumente "Grafik Range [...]"
        /// </summary>
        /// <seealso cref="GleichheitenPrüfen"/>
        public static void GleichheitenPrüfenByRange(int Range)
        {
            Console.WriteLine();
            Console.WriteLine("-----------------");
            Console.WriteLine("Range: " + Range);

            string DirectPath = "C:\\Users\\Jannis\\Documents\\visual studio 2015\\Projects\\ProofOfConceptMicrophonizer\\Dokumente\\DirectSignal.wav";
            string MicPath = "C:\\Users\\Jannis\\Documents\\visual studio 2015\\Projects\\ProofOfConceptMicrophonizer\\Dokumente\\MicSignal.wav";

            //Test
            var TestMP = new Microphonizer(DirectPath, MicPath);
            var Matches = TestMP.CompareSignals(Range);
            //Console.WriteLine("Matches with Mic and Direct: " + Matches.Count);

            //------------------------------------------------------------------------------------------------------------------------------------

            string RandomWavPath = "C:\\Users\\Jannis\\Documents\\visual studio 2015\\Projects\\ProofOfConceptMicrophonizer\\Dokumente\\Random.wav";

            //Test
            var RandomMP = new Microphonizer(DirectPath, RandomWavPath);
            var MatchesWithRandomWav = RandomMP.CompareSignals(Range);
            //Console.WriteLine("Matches with RandomWave and Direct: " + MatchesWithRandomWav.Count);

            //Console.WriteLine("Differenz (>Warscheinliche< 'Echte' Übereinstimmungen): " + (Matches.Count - MatchesWithRandomWav.Count));
            Console.WriteLine("Prozentualle Übereinstimmung (Gesamtdateigröße): " + ((Convert.ToDouble(Matches.Count - MatchesWithRandomWav.Count)) / TestMP.Direct.Count()));
            
        }
        public static void LoopGleichheitenprüfen()
        {
            for (var x = 1; x < 21; x++)
            {
                ProofOfConcept.GleichheitenPrüfenByRange(x);
            }

            Console.ReadKey();
        }
    }
}
