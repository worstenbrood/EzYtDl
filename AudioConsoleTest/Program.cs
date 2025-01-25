using System;
using System.IO;
using System.Linq;
using System.Threading;
using AudioTools;
using AudioTools.Dsp;
using SoundTouch;

namespace AudioConsoleTest
{
    internal class Program
    {
        private static void WriteAt(int x, int y, string text)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(x, y);
            Console.Write(new string(Enumerable.Repeat(' ', Console.BufferWidth - x).ToArray()));
            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);
        }

        private static void Enumerate(string path)
        {
            foreach (var fileInfo in Directory.EnumerateFiles(path, "*.mp3").Select(f => new FileInfo(f)))
            {
                if (fileInfo.Length > 50000000)
                    continue;

                using (var audioFile = new AudioFile(fileInfo.FullName))
                {
                    var bpmStart = DateTime.Now;
                    var bpm = audioFile.Bpm;
                    var bpmEnd = DateTime.Now - bpmStart;

                    var stStart = DateTime.Now;
                    var stBpm = audioFile.SoundTouchBpm;
                    var stEnd = DateTime.Now - stStart;

                    Console.WriteLine(
                        $"Track: {audioFile.Tag.Title} My BPM: {bpm} ({bpmEnd}) SoundTouch BPM: {stBpm} ({stEnd})");
                }
            }
        }

        static void Main(string[] args)
        {
            WriteAt(0,0,$"SoundTouch version: {SoundTouchProcessor.Version}");
            //Enumerate(@"C:\\Users\\tim.horemans\\Music");
            //return;
            
            using (var audioFile = new AudioFile(@"C:\Users\tim.horemans\Music\Various\Andre Crom “Mind Control” (Wehbba Remix) [431774235].mp3"))
            {
                var bpm = audioFile.Bpm;
                var stBpm = audioFile.SoundTouchBpm;
                var bcFrequency = 6000;
                short bcBits = 8;
                var bc = new BitCrusher(audioFile.WaveFormat.SampleRate, bcFrequency, bcBits);
                var wantedBpm = 140;
                var tempoChange = (float)wantedBpm / bpm * 100 - 100;
                audioFile.Play(tempoChange);
                
                WriteAt(0, 1, $"Track: {audioFile.Tag.Title} BPM: {bpm} ST BPM: {stBpm}");
                var timerBpm = new Timer(o => WriteAt(0, 2, $"SoundTouch BPM: {audioFile.CalculatedBpm}"), null, 0, 5000);
                var timerPosition = new Timer(o => WriteAt(0, 3, $"Current Position: {audioFile.CurrentTime :h':'mm':'ss}"), null, 0, 500);
                WriteAt(0, 4, $"BitCrusher Frequency: {bcFrequency}");
                WriteAt(0, 5, $"BitCrusher Bits: {bcBits}");
                WriteAt(0, 6, $"Tempo change: {tempoChange}%");

                ConsoleKeyInfo keyInfo;
                
                do
                {
                    keyInfo = Console.ReadKey(true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            bcFrequency += 10;
                            WriteAt(22, 4, $"{bcFrequency}");
                            bc.SetParameters(bcFrequency, bcBits);
                            break;

                        case ConsoleKey.DownArrow:
                            bcFrequency -= 10;
                            WriteAt(22, 4, $"{bcFrequency}");
                            bc.SetParameters(bcFrequency, bcBits);
                            break;

                        case ConsoleKey.Q:
                            if (bcBits + 1 <= 16)
                            {
                                bcBits++;
                                WriteAt(17, 5, $"{bcBits}");
                                bc.SetParameters(bcFrequency, bcBits);
                            }
                            break;

                        case ConsoleKey.W:
                            if (bcBits - 1 >= 1)
                            {
                                bcBits--;
                                WriteAt(17, 5, $"{bcBits}");
                                bc.SetParameters(bcFrequency, bcBits);
                            }
                            break;

                        case ConsoleKey.LeftArrow:
                            tempoChange -= 0.1f;
                            audioFile.TempoChange = tempoChange;
                            WriteAt(14, 6,$"{tempoChange}%");
                            break;

                        case ConsoleKey.RightArrow:
                            tempoChange += 0.1f;
                            audioFile.TempoChange = tempoChange;
                            WriteAt(14, 6, $"{tempoChange}%");
                            break;

                        case ConsoleKey.R:
                            audioFile.Dsp.Remove(0);
                            break;

                        case ConsoleKey.A:
                            audioFile.Dsp.Add(bc);
                            break;
                    }
                } while (keyInfo.Key != ConsoleKey.X && !audioFile.Wait(1));

                timerBpm.Dispose();
                timerPosition.Dispose();
                WriteAt(0, 5, $"Calculated SoundTouch BPM (on the fly during play): {audioFile.CalculatedBpm} ({(int)Math.Round(audioFile.CalculatedBpm)})");
            }
        }
    }
}
