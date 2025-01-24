using System;
using System.Diagnostics;
using System.Linq.Expressions;
using AudioTools;
using AudioTools.Dsp;

namespace AudioConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var audioFile = new AudioFile(@"C:\Users\tim.horemans\Music\Various\Generation [_iG2vZaVzdc].mp3"))
            {
                Console.WriteLine($"Track: {audioFile.Tag.Title} My BPM: {audioFile.Bpm} SoundTouch Bpm: {audioFile.SoundTouchBpm}");
                var currentCutoffFrequency = 500F;
                var hpf = new HighPassFilter(audioFile.WaveFormat.SampleRate, currentCutoffFrequency);
                audioFile.Dsp.Add(hpf);

                audioFile.Play(tempoChange: 4, time: TimeSpan.FromMinutes(2));
                
                ConsoleKeyInfo keyInfo;

                do
                {
                    keyInfo = Console.ReadKey(true);
                    float frequency = 0;
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            frequency = 5;
                            break;

                        case ConsoleKey.DownArrow:
                            frequency = -5;
                            break;

                        case ConsoleKey.R:
                            frequency = -currentCutoffFrequency + 15;
                            break;
                    }

                    if (frequency != 0)
                    {
                        currentCutoffFrequency += frequency;
                        if (currentCutoffFrequency > 0)
                        {
                            hpf.SetParameters(currentCutoffFrequency);
                            Console.WriteLine($"Cutoff Frequency: {currentCutoffFrequency}");
                        }
                    }
                } while (keyInfo.Key != ConsoleKey.X);
                
                Console.WriteLine($"Calculated SoundTouch BPM (on the fly during play): {audioFile.CalculatedBpm}");

            }
        }
    }
}
