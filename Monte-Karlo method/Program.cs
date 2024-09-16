using System;
using System.Collections.Generic;
using System.Linq;

namespace Monte_Carlo_method
{
    internal class Program
    {
        // Второй вариант
        public static int BinaryToDecimal(string binary)
        {
            int _decimal = 0;
            for (int i = 0; i < 15; ++i)
            {
                _decimal += int.Parse(binary[i].ToString()) * Convert.ToInt32(Math.Pow(2, 14 - i));
            }
            return _decimal;
        }

        // Третий вариант
        public static int QuadraticAdaptationFunction(string binary)
        {
            int decimalVal = BinaryToDecimal(binary);
            return Convert.ToInt32(Math.Pow((decimalVal - Convert.ToInt32(Math.Pow(2, 15 - 1))), 2));
        }

        static void Main(string[] args)
        {
            Random rnd = new Random();
            List<List<string>> searchSpace = new List<List<string>>();

            int numEncodings = Convert.ToInt32(Math.Pow(2, 15));

            while (true)
            {
                Console.WriteLine("1 - Generate encoding list");
                Console.WriteLine("2 - Run the algorithm");
                Console.WriteLine("3 - Exit the program");

                int path = int.Parse(Console.ReadLine());

                switch (path)
                {
                    case 1:
                        searchSpace.Clear();
                        HashSet<string> uniqueEncodings = new HashSet<string>();
                        // Генерация кодировок
                        while (uniqueEncodings.Count < numEncodings)
                        {
                            string encoding = null;

                            for (int j = 0; j < 15; ++j)
                            {
                                encoding += rnd.Next(0, 2).ToString();
                            }

                            if (uniqueEncodings.Add(encoding))
                            {
                                List<string> encodingData = new List<string>();
                                encodingData.Add(encoding);

                                encodingData.Add(QuadraticAdaptationFunction(encoding).ToString());
                                searchSpace.Add(encodingData);
                            }

                        }

                        Console.WriteLine("The list has been successfully generated!");
                        break;

                    case 2:
                        if (searchSpace.Count > 0)
                        {
                            HashSet<int> randIndexes = new HashSet<int>();
                             while(randIndexes.Count < 32)
                            {
                                randIndexes.Add(rnd.Next(0, numEncodings));
                            }

                            List<int> indexes = randIndexes.ToList();

                            for (int i = 0; i < 32; ++i)
                            {
                                Console.WriteLine("[" +  indexes[i] + "] " + searchSpace[indexes[i]][0] + " - " + searchSpace[indexes[i]][1]);
                            }

                            Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+\n");

                            // Монте-Карло
                            int max = 0;
                            string maxS = "Empty".PadRight(20);

                            Console.WriteLine("Step".PadRight(5) + "Max Adaptation".PadRight(15) + "Best Encoding".PadRight(20) + "Selected Encoding".PadRight(20) + "Selected Adaptation".PadRight(20));

                            for (int i = 0; i < 32; ++i)
                            {
                                string step = (i + 1).ToString().PadRight(5);
                                string maxAdaptation = max.ToString().PadRight(15);
                                string bestEncoding = maxS.PadRight(20);
                                string selectedEncoding = searchSpace[indexes[i]][0].PadRight(20);
                                string selectedAdaptation = searchSpace[indexes[i]][1].PadRight(20);

                                Console.Write(step + maxAdaptation + bestEncoding + selectedEncoding + selectedAdaptation);

                                if (max < int.Parse(searchSpace[indexes[i]][1]))
                                {
                                    max = int.Parse(searchSpace[indexes[i]][1]);
                                    maxS = searchSpace[indexes[i]][0];
                                    Console.Write("<-- The maximum has changed!".PadLeft(20));
                                }

                                Console.WriteLine();
                            }

                            Console.WriteLine("\nFinal best encoding: " + maxS);
                            Console.WriteLine("Final maximum adaptation: " + max);
                            Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+\n");
                        }
                        else
                        {
                            Console.WriteLine("The list is empty!");
                        }
                        break;

                    case 3:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Incorrect input!");
                        break;
                }
            }
        }
    }
}
