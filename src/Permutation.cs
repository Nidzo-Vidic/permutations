using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

            field = Enumerable.Range(1, field.Length).ToArray();

            allPermutations
                .AppendJoin($"{' '}", field)
                .AppendLine();
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

        public void CreatePermutationsRecursive()
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


        public void CreatePermutationsIterative()
        {
            int pivot = field.Length;
            int fieldEnd = getFieldEnd(pivot);
            int currentField = field[pivot - 1];

            while (pivot != 0 && currentField != fieldEnd)
            {
                field[pivot - 1]++;
                currentField = field[pivot - 1];

                if (pivot < field.Length && currentField != fieldEnd)
                {
                    pivot++;
                    for (int i = pivot - 1; i < field.Length; i++)
                    {
                        field[i] = field[i - 1] + 1;
                    }
                    if (pivot < field.Length)
                    {
                        if (field[pivot] < getFieldEnd(pivot + 1))
                        {
                            pivot++;
                        }
                    }
                    fieldEnd = getFieldEnd(pivot);
                }

                allPermutations
                    .AppendJoin($"{' '}", field)
                    .AppendLine();

                if (currentField == fieldEnd)
                {
                    pivot--;
                    fieldEnd = getFieldEnd(pivot);
                }

            }
        }

        public int getFieldEnd(int pivot)
        {
            return numbers - choices + pivot;
        }

    }
}