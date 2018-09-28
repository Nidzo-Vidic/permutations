using System;
using MathNet.Numerics;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using McMaster.Extensions.CommandLineUtils;

namespace permutations
{
    [Command(Name = "perm", Description = "Shows all permutations of two given numbers")]
    [HelpOption("-?")]
    class Program
    {
        static void Main(string[] args)
        {
            CommandLineApplication.Execute<Program>(args);
        }

        [Argument(0, "numbers")]
        [Required]
        public int Numbers { get; }

        [Argument(1, "choices")]
        [Required]
        public int Choices { get; }

        [Option(ShortName = "rf", Description = "Number of random fields")]
        public int RandomFields { get; }

        [Option(ShortName = "e")]
        public bool isEuro { get; }


        private void OnExecute()
        {


            if (RandomFields > 0)
            {
                if (Numbers < Choices)
                {
                    Console.WriteLine("First argument has to be smaller than the second");
                }

                Permutation perm = new Permutation(Numbers, Choices);
                double possibilities = SpecialFunctions.Binomial(Numbers, Choices);
                Console.WriteLine($"\n{RandomFields} random choices out of {possibilities}\n");
                perm.PrintRandomFields(RandomFields);
            }
        }
    }
}