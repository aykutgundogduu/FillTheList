using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FillTheList
{
    class Program
    {
        private static int[] list = new int[9];
        private static int maxRandValue { get; set; } = 10;
        private static int minRandValue { get; set; } = 1;

        private static int randRunCount { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            FillList();

            Console.WriteLine("List : {0}, Random methodu {1} kere çalışmıştır.", String.Join(",", list), randRunCount);
        }

        private static void FillList()
        {
            for (int i = 0; i < list.Length; i++)
            {
                bool doResult = false;
                do
                {
                    int randValue = RunRandomForAbsolute();
                    if (IsValueValid(randValue))
                    {
                        list[i] = randValue;
                        doResult = true;
                    }
                }
                while (!doResult);
            }
        }


        private static int RunRandom(int minValue, int maxValue)
        {
            Random r = new Random();
            randRunCount++;
            return r.Next(minValue, maxValue);
        }

        /// <summary>
        /// Liste içindeki değerleri kontrol edip, eklenen değerler dışındakiler için random değer üretecektir.
        /// </summary>
        /// <returns>int value of random</returns>
        private static int RunRandomForAbsolute()
        {

            if (list.Any(x => x > 0)) //Diziye ilk değer girildi mi?
            {
                //Range belirler
                int generatedHighestValue = list.ToList().OrderByDescending(x => x).First();
                int generatedMinValue = FindUndenifiedMin();

                if (generatedHighestValue <= generatedMinValue) generatedHighestValue = maxRandValue;

                //Sleep kaldırılırsa random çok fazla sayıda aynı değeri üretecektir.
                //Random algoritması ile ilgili olabileceğini düşünüyorum. Date ticks ile çözümleniyorsa hızlı işlem problem yaratabilir.
                Thread.Sleep(100);

                return RunRandom(generatedMinValue, generatedHighestValue);
            }

            return RunRandom(minRandValue, maxRandValue);
        }

        private static int FindUndenifiedMin()
        {
            for (int i = minRandValue; i <= maxRandValue; i++)
            {
                if (IsValueValid(i)) return i;
            }

            return minRandValue;
        }

        /// <summary>
        /// Eklenecek yeni değer için listeyi kontrol eder.
        /// </summary>
        /// <param name="value">Yeni değer</param>
        /// <returns>If list contains this value returns false, otherwise true.</returns>
        private static bool IsValueValid(int value)
        {
            return !list.Any(x => x == value);
        }
    }
}
