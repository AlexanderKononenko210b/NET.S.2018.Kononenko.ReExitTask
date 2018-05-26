using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorCrossword
{
    /// <summary>
    /// Class describe template crossword
    /// </summary>
    public class Template
    {
        #region Fields

        private readonly Dictionary<int, Word> dictionaryWordHor = new Dictionary<int, Word>();

        private readonly Dictionary<int, Word> dictionaryWordVert = new Dictionary<int, Word>();

        private readonly Dictionary<string, Intersection> dictionaryIntersection = new Dictionary<string, Intersection>();

        #endregion

        #region Constructors

        public Template(IEnumerable<SpaceWord> spaceWords)
        {
            foreach (var item in spaceWords.GetWords())
            {
                if (item.SpaceWord.Direction == Direction.Horizontal)
                    dictionaryWordHor.Add(item.Index, item);
                else
                    dictionaryWordVert.Add(item.Index, item);
            }

            GetIntersections();
        }

        #endregion

        #region Internal Api

        /// <summary>
        /// Get dictionary horisontal word`s
        /// </summary>
        internal Dictionary<int, Word> DictionaryWordHor => dictionaryWordHor;

        /// <summary>
        /// Get dictionary vertical word`s
        /// </summary>
        internal Dictionary<int, Word> DictionaryWordVert => dictionaryWordVert;

        /// <summary>
        /// Get dictionary word`s intersection
        /// </summary>
        internal Dictionary<string, Intersection> DictionaryIntersection => dictionaryIntersection;

        /// <summary>
        /// Get count word`s in horizontal and vertical dictionary
        /// </summary>
        internal int Count => dictionaryWordHor.Count + dictionaryWordVert.Count;

        /// <summary>
        /// Get word by index
        /// </summary>
        /// <param name="index">index word</param>
        /// <returns>instanse type word</returns>
        internal Word this[int index]
        {
            get
            {
                if (index < 1 || index > Count)
                    throw new ArgumentOutOfRangeException($"Argument index have to more than 0");

                if (dictionaryWordHor.ContainsKey(index))
                    return dictionaryWordHor[index];
                return dictionaryWordVert[index];
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Get all intersections in template
        /// </summary>
        private void GetIntersections()
        {
            if (dictionaryWordHor.Count == 0 || dictionaryWordVert.Count == 0)
                return;

            foreach (KeyValuePair<int, Word> itemH in dictionaryWordHor)
            {
                foreach (KeyValuePair<int, Word> itemV in dictionaryWordVert)
                {
                    var intersection = GetIntersection(itemH.Value, itemV.Value);

                    if (intersection != null)
                        dictionaryIntersection.Add(intersection.GetKey(), intersection);
                }
            }
        }

        /// <summary>
        /// Method for get intersection between two word
        /// </summary>
        /// <param name="wordH">horisontal word</param>
        /// <param name="wordV">vertical word</param>
        /// <returns>instance intersection two word or null if word`s are not intersection</returns>
        private Intersection GetIntersection(Word wordH, Word wordV)
        {
            Check.NotNull(wordH);

            Check.NotNull(wordV);

            if (wordH.SpaceWord.Coordinate.X < wordV.SpaceWord.Coordinate.X)
                return null;

            if (wordH.SpaceWord.Coordinate.X > wordV.SpaceWord.Coordinate.X + wordV.SpaceWord.Length)
                return null;

            if (wordH.SpaceWord.Coordinate.X < wordV.SpaceWord.Coordinate.X + wordV.SpaceWord.Length
                && wordV.SpaceWord.Coordinate.Y < wordH.SpaceWord.Coordinate.Y)
                return null;

            if (wordH.SpaceWord.Coordinate.X < wordV.SpaceWord.Coordinate.X + wordV.SpaceWord.Length
                && wordV.SpaceWord.Coordinate.Y > wordH.SpaceWord.Coordinate.Y + wordH.SpaceWord.Length)
                return null;

            return new Intersection(
                new Сoordinate(wordH.SpaceWord.Coordinate.X, wordV.SpaceWord.Coordinate.Y),
                wordH, wordV);
        }

        #endregion
    }
}
