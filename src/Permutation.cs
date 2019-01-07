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
                .AppendJoin($"{' '}", field)
                .AppendLine();
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
                randomNumber = rng.Next(0, lineCount);
                string line = lines[randomNumber];
                StringBuilder field = new StringBuilder().AppendJoin($"{' ',-8}", line.Split($"{' ',-10}"));
                Console.WriteLine(field);
            }
        }

        public void PrintResult()
        {
            CreateAllPermutations();
            Console.Write(allPermutations);
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