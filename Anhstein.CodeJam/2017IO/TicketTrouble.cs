﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit.Abstractions;

namespace Anhstein.CodeJam
{
    public class TicketTrouble : CodeJamBase
    {
        public TicketTrouble(ITestOutputHelper output) : base(output)
        {
        }

        public override object Solve(StreamReader reader)
        {
            int f, s;
            string[] line;
            line = reader.ReadLine().Split(' ');
            f = int.Parse(line[0]);
            s = int.Parse(line[1]);

            return MaxSameRow(f, s, reader);
        }

        private int MaxSameRow(int f, int s, StreamReader reader)
        {
            Dictionary<int, List<int>> hash = new Dictionary<int, List<int>>();
            string[] line;
            int num1, num2;

            for (int i = 0; i < f; i++)
            {
                line = reader.ReadLine().Split(' ');
                num1 = int.Parse(line[0]);
                num2 = int.Parse(line[1]);

                AddToHash(hash, num1, num2);
                AddToHash(hash, num2, num1);
            }

            return hash.Values.OrderByDescending(v => v.Count).First().Count;
        }

        private void AddToHash(Dictionary<int, List<int>> hash, int row, int col)
        {
            if (hash.ContainsKey(row))
            {
                if (hash[row].Contains(col))
                    return;
            }
            else
                hash[row] = new List<int>();

            hash[row].Add(col);
        }
    }
}
