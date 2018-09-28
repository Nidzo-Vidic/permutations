using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace permutations
{
    public class Permutation
    {
        private int[] field;
        private int numbers;
        private int choices;
        private StringBuilder allPermutations = new StringBuilder("");

        public Permutation(int numbers, int choices)
        {
            this.numbers = numbers;
            this.choices = choices;
            this.field = new int[choices];

            string kombinations = $"{' ',2}{this.choices} aus {this.numbers} Kombinationen\n";

            field = Enumerable.Range(1, field.Length).ToArray();

            allPermutations
                .Append($"{kombinations,15}")
                .AppendLine()
                .AppendJoin($"{' '}", field)
                .AppendLine();
        }

        public void PrintRandomFieldsFromFile(int fields)
        {
            string fileName = $"{choices}aus{numbers}.txt";
            Random rng = new Random(); ;
            int randomNumber;
            if (!File.Exists(fileName))
            {
                WriteToFile(fileName);
            }

            var lines = File.ReadLines(fileName);
            int lineCount = lines.Count();

            for (int i = 0; i < fields; i++)
            {
                randomNumber = rng.Next(3, lineCount - 1);
                string[] line = lines.Skip(randomNumber + 1).Take(1).First().Split($"{' '}");
                Console.WriteLine($"Feld Nr. {i + 1,-8}");
                Console.WriteLine($"Zeile {randomNumber}\n");
                Console.WriteLine(new StringBuilder().AppendJoin($"{' ',-6}", line));
                Console.WriteLine();
            }
        }

        public void PrintRandomFields(int fields)
        {
            CreateAllPermutations();
            Random rng = new Random();
            int randomNumber;
            string permutations = allPermutations.ToString();
            string[] lines = permutations.Split("\n");
            int lineCount = lines.Count();
            for (int i = 0; i < fields; i++)
            {
                randomNumber = rng.Next(3, lineCount);
                string line = lines[randomNumber];
                Console.Write($"Field Nr. {i + 1}  ||  ");
                // Console.WriteLine($"Zeile {randomNumber}\n");
                StringBuilder field = new StringBuilder().AppendJoin($"{' ',-8}", line.Split($"{' ',-10}"));
                Console.WriteLine(field);
                Console.WriteLine();
            }
        }

        public void PrintResult()
        {
            CreateAllPermutations();
            Console.Write(allPermutations);
        }

        public void WriteToFile(string fileName)
        {
            string currentDir = Directory.GetCurrentDirectory();
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(currentDir, fileName)))
            {
                CreateAllPermutations();
                outputFile.Write(allPermutations);
            }
        }

        private void CreateAllPermutations()
        {
            for (int i = this.field.Length; i > 0; i--)
            {
                Increment(i);
            }
        }

        private void Increment(int pivot)
        {
            if (field.Length == pivot)
            {
                while (field[pivot - 1] < numbers)
                {
                    field[pivot - 1] += 1;
                    allPermutations
                        .AppendJoin($"{' '}", field)
                        .AppendLine();
                }
            }
            else
            {
                int i = 0;
                while (i < numbers - field.Length)
                {
                    field[pivot] = field[pivot - 1] + 1;
                    field[pivot - 1] += 1;
                    Increment(pivot + 1);
                    i++;
                }
            }
        }
    }
}