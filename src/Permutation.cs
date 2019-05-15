using System;
using System.Linq;
using System.Text;

namespace permutations
{
    public class Permutation
    {
        private int numbers;
        private int choices;
        private StringBuilder allPermutations = new StringBuilder("");

        public Permutation(int numbers, int choices)
        {
            this.numbers = numbers;
            this.choices = choices;
        }

        public void PrintRandomFields(int fields)
        {
            RandomGenerator rng = new RandomGenerator();
            int randomNumber;
            string permutations = allPermutations.ToString();
            ReadOnlySpan<string> lines = permutations.Split("\n").AsSpan();
            int lineCount = lines.Length - 1;
            for (int i = 0; i < fields; i++)
            {
                randomNumber = rng.Next(0, lineCount);
                string line = lines[randomNumber];
                Console.WriteLine(line);
            }
        }

        public void PrintResult()
        {
            Console.Write(allPermutations);
        }

        public int getFieldEnd(in int pivot)
        {
            return numbers - choices + pivot + 1;
        }
        public void CreatePermutationsIterative()
        {
            int[] field = Enumerable.Range(1, choices).ToArray();

            allPermutations
                .AppendJoin($"{' '}", field)
                .AppendLine();

            int pivot = field.Length - 1;
            int fieldEnd = getFieldEnd(pivot);
            int currentField = field[pivot];

            while (pivot != -1 && currentField != fieldEnd)
            {
                field[pivot]++;
                currentField = field[pivot];

                if (pivot < field.Length - 1 && currentField != fieldEnd)
                {
                    pivot++;
                    for (int i = pivot; i < field.Length; i++)
                    {
                        field[i] = field[i - 1] + 1;
                    }
                    while (pivot < field.Length - 1)
                    {
                        fieldEnd = getFieldEnd(pivot + 1);
                        if (field[pivot] < fieldEnd)
                        {
                            pivot++;
                        }
                    }
                    fieldEnd = getFieldEnd(pivot);
                }

                if (currentField == fieldEnd)
                {
                    pivot--;
                    fieldEnd = getFieldEnd(pivot);
                }

                allPermutations
                    .AppendJoin($"{' '}", field)
                    .AppendLine();
            }
        }
    }
}