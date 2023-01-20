using System;
using System.Linq;
using System.Text.RegularExpressions;

public class Script05b
{
    public void run()
    {
        var chars = new List<List<char>>()
        {
            new List<char>(){'N', 'R', 'G', 'P'},
            new List<char>(){'J', 'T', 'B', 'L', 'F', 'G', 'D', 'C'},
            new List<char>(){'M', 'S', 'V'},
            new List<char>(){'L', 'S', 'R', 'C', 'Z', 'P'},
            new List<char>(){'P', 'S', 'L', 'V', 'C', 'W', 'D', 'Q'},
            new List<char>(){'C', 'T', 'N', 'W', 'D', 'M', 'S'},
            new List<char>(){'H', 'D', 'G', 'W', 'P'},
            new List<char>(){'Z', 'L', 'P', 'H', 'S', 'C', 'M', 'V'},
            new List<char>(){'R', 'P', 'F', 'L', 'C', 'W', 'G', 'Z'}
        };


        Console.WriteLine(chars);

        // Read the file as array per line
        string[] lines = System.IO.File.ReadAllLines(@"C:\projects\adventofcode\data\2022\05\input.txt");

        int i = 0;
        foreach (string line in lines)
        {
            i++;
            if (i < 11) continue;//skip first lines - we are only interrested in moves
            //Console.WriteLine($"{line}");

            Regex re = new Regex(@"\d+");
            MatchCollection m = re.Matches(line);

            int from = Int32.Parse(m.ElementAt(1).Value);
            int to = Int32.Parse(m.ElementAt(2).Value);
            int size = Int32.Parse(m.ElementAt(0).Value);

            for (int j= size; j >0 ; j--)
            {
                chars[to - 1].Add(chars[from - 1].ElementAt(chars[from - 1].Count-j));
            }
            for (int j = size - 1; j >= 0; j--)
            {
                chars[from - 1].RemoveAt(chars[from - 1].Count - 1);
            }
        }

        foreach(List<char> x in chars)
        {
            Console.Write($"{x.Last()}");
        }
    }
}
