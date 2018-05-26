using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorCrossword;

namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var field = Generator.Generate();

                VisualCrossword(field);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Visual crossword
        /// </summary>
        /// <param name="field">massive char</param>
        public static void VisualCrossword(char[,] field)
        {
            for (int i = 0; i < Generator.SIZE_FIELD_CROSSWORD; i++)
            {
                for (int j = 0; j < Generator.SIZE_FIELD_CROSSWORD; j++)
                {
                    if (field[i, j] == default(char))
                    {
                        Console.Write("");
                    }
                    Console.Write("{0,5}", field[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
