using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorCrossword
{
    /// <summary>
    /// Class for make crossword using array string and template crossword
    /// </summary>
    public static class Generator
    {
        #region Fields

        public const int SIZE_FIELD_CROSSWORD = 11;

        private static Collection<string> collectionStrings =
            new Collection<string> { "перелом", "подвывих", "закрытый", "ушиб", "открытый", "шина", "вывих" };

        private static SpaceWord[] spaceWords =
        {
            new SpaceWord(0, 2, 7, Direction.Horizontal),
            new SpaceWord(4, 3, 8, Direction.Horizontal),
            new SpaceWord(7, 0, 8, Direction.Horizontal),
            new SpaceWord(9, 2, 4, Direction.Horizontal),
            new SpaceWord(0, 7, 8, Direction.Vertical),
            new SpaceWord(4, 1, 4, Direction.Vertical),
            new SpaceWord(6, 4, 5, Direction.Vertical),
        };

        #endregion

        #region Public Api

        /// <summary>
        /// Method for generate crossword
        /// </summary>
        /// <returns>array char[,] with result</returns>
        public static char[,] Generate()
        {
            var template = new Template(spaceWords);

            foreach (var item in template.DictionaryWordHor)
            {
                WordInitializer(item.Value, collectionStrings);

                var collectionIntersect = GetIntersections(template.DictionaryIntersection, item.Value.Index);

                if (collectionIntersect == null)
                    throw new ArgumentOutOfRangeException($"Input template is not valid");

                foreach (Intersection intersect in collectionIntersect)
                {
                    if (!template[intersect.WordSecond.Index].IsUsed)
                    {
                        WordVertInitializer(template[intersect.WordSecond.Index], item.Value, collectionStrings, intersect);
                    }
                }
            }

            var isValid = IsValid(template);

            if (!isValid.Item1)
                throw new InvalidOperationException($"{isValid.Item2}");

            var resultCrossword = InitializeField(template);

            return resultCrossword;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initialize field crossword
        /// </summary>
        /// <param name="template">template crossword</param>
        /// <returns>char [,] contains crossword</returns>
        private static char[,] InitializeField(Template template)
        {
            char[,] field = new char[SIZE_FIELD_CROSSWORD, SIZE_FIELD_CROSSWORD];

            for (int i = 1; i < template.Count + 1; i++)
            {
                switch (template[i].SpaceWord.Direction)
                {
                    case Direction.Horizontal:
                        {
                            var indexYStartWord = template[i].SpaceWord.Coordinate.Y;

                            var indexXStartWord = template[i].SpaceWord.Coordinate.X;

                            for (int j = 0; j < template[i].Content.WordChars.Length; j++)
                            {
                                field[indexXStartWord, indexYStartWord] = template[i].Content.WordChars[j];

                                indexYStartWord++;
                            }
                            break;
                        }
                    case Direction.Vertical:
                        {
                            var indexXStartWord = template[i].SpaceWord.Coordinate.X;

                            var indexYStartWord = template[i].SpaceWord.Coordinate.Y;

                            for (int j = 0; j < template[i].Content.WordChars.Length; j++)
                            {
                                field[indexXStartWord, indexYStartWord] = template[i].Content.WordChars[j];

                                indexXStartWord++;
                            }
                            break;
                        }
                    default:
                        throw new InvalidOperationException($"Unknown type direction");
                }
            }

            return field;
        }

        /// <summary>
        /// Verify that all word`s in template are initialized
        /// </summary>
        /// <param name="template">template crossword</param>
        /// <returns>Item1 - bool result verify, Item2 - message</returns>
        private static (bool, string) IsValid(Template template)
        {
            for (int i = 1; i < template.Count; i++)
            {
                if (!template[i].IsUsed)
                    return (false, $"Not all words are initialized");
            }

            return (true, $"All word`s are initialized");
        }

        /// <summary>
        /// Initializer vertical word`s
        /// </summary>
        /// <param name="word">word for initializer</param>
        /// <param name="wordHor">horizontal word already initialize</param>
        /// <param name="strings">input collection string</param>
        /// <param name="intersect">instance intersect</param>
        private static void WordVertInitializer(Word word, Word wordHor, Collection<string> strings, Intersection intersect)
        {
            var indexCharIntersecInHorWord =
                intersect.PointIntersection.Y - wordHor.SpaceWord.Coordinate.Y;

            char charIntersect = wordHor.Content.WordChars[indexCharIntersecInHorWord];

            var indexCharIntersecInWord = wordHor.SpaceWord.Coordinate.X - word.SpaceWord.Coordinate.X;

            var concurrenceByLength = GetСoncurrencesByLength(strings, word.SpaceWord.Length);

            var concurrenceByChar = GetСoncurrencesByChar(concurrenceByLength, charIntersect, indexCharIntersecInWord);

            if (concurrenceByChar.Count == 0)
                throw new InvalidOperationException($"Input dictionary doesn`t contain word with necesary conditions");

            word.Content.WordChars = concurrenceByChar[0].ToCharArray();

            word.IsUsed = true;

            strings.Remove(concurrenceByChar[0]);
        }

        /// <summary>
        /// Get all intersect for current horizontal words
        /// </summary>
        /// <param name="dictionary">dictionary for all intersects</param>
        /// <param name="index">specify index word</param>
        /// <returns>collection all intersection for input index</returns>
        private static Collection<Intersection> GetIntersections(Dictionary<string, Intersection> dictionary, int index)
        {
            Check.NotNull(dictionary);

            Collection<Intersection> collectionIntersect = new Collection<Intersection>();

            foreach (KeyValuePair<string, Intersection> intersect in dictionary)
            {
                int indexWord;

                if (Int32.TryParse(intersect.Key.Split(',')[0], out indexWord))
                {
                    if (indexWord == index)
                        collectionIntersect.Add(intersect.Value);
                }
            }

            if (collectionIntersect.Count == 0)
                return null;

            return collectionIntersect;
        }

        /// <summary>
        /// Initializer word string content
        /// </summary>
        /// <param name="word">word for initialization</param>
        /// <param name="strings">collection string</param>
        /// <returns>word after initializer</returns>
        private static void WordInitializer(Word word, Collection<string> strings)
        {
            var firstWords = GetСoncurrencesByLength(strings, word.SpaceWord.Length);

            if (firstWords.Count == 0)
                throw new InvalidOperationException($"Input array word does not contains word`s with necesery length");

            word.Content.WordChars = firstWords[0].ToCharArray();

            word.IsUsed = true;

            strings.Remove(firstWords[0]);
        }

        /// <summary>
        /// Get concurrenses in input dictionary by length string
        /// </summary>
        /// <param name="strings">input collection</param>
        /// <param name="wordLength">length word for search</param>
        /// <returns>collection concurrences string by length</returns>
        private static Collection<string> GetСoncurrencesByLength(Collection<string> strings, int wordLength)
        {
            Collection<string> wordsConcorences = new Collection<string>();

            foreach (var item in strings.Where(item =>
                item.Length == wordLength))
            {
                wordsConcorences.Add(item);
            }

            return wordsConcorences;
        }

        /// <summary>
        /// Get concurrences string in input collection 
        /// type string which contains letter in index position
        /// </summary>
        /// <param name="inputCollection">string`s collection</param>
        /// <param name="letter">char letter</param>
        /// <param name="index">index where letter have to be</param>
        /// <returns>collection words find</returns>
        private static Collection<string> GetСoncurrencesByChar(Collection<string> inputCollection, char letter, int index)
        {
            Collection<string> wordsConcorences = new Collection<string>();

            foreach (var item in inputCollection)
            {
                if (item[index] == letter)
                {
                    wordsConcorences.Add(item);
                }
            }

            return wordsConcorences;
        }

        #endregion
    }
}
