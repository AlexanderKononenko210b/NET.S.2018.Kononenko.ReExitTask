using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorCrossword
{
    /// <summary>
    /// Extansion method for IEnumerable<SpaceWord></SpaceWord>
    /// </summary>
    public static class ExtansionMethods
    {
        /// <summary>
        /// Extension method IEnumerable type SpaceWord
        /// </summary>
        /// <param name="spaceWords">IEnumerable type SpaceWord</param>
        /// <returns>IEnumerable type Word</returns>
        public static IEnumerable<Word> GetWords(this IEnumerable<SpaceWord> spaceWords)
        {
            var index = 1;

            foreach (var item in spaceWords)
            {
                yield return new Word(item, index++);
            }
        }

        /// <summary>
        /// Get array type int by array type int
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static int[] GetIntArray(this string[] strings)
        {
            Collection<int> helper = new Collection<int>();

            int number;

            for (int i = 0; i < strings.Length; i++)
            {
                if(Int32.TryParse(strings[i], out number))
                    helper.Add(number);
                else
                    throw new ArgumentOutOfRangeException($"Key intersection is invalid");
            }

            return helper.ToArray();
        }
    }
}
