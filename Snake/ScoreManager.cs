﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class ScoreManager
    {
        public static void SetBestScore(int bestScore)
        {
            string path = Environment.CurrentDirectory + @"\Scores\best_score.dat";
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                writer.Write(bestScore);
            }
        }

        public static int GetBestScoreFromBinary()
        {
            string path = Environment.CurrentDirectory + @"\Scores\best_score.dat";
            int result = 0;

            if (!File.Exists(path))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Scores");
                SetBestScore(0);
            }

            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    result = reader.ReadInt32();
                }
            }
            return result;
        }
    }
}
