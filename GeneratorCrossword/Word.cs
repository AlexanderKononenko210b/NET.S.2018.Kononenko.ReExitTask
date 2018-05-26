using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorCrossword
{
    /// <summary>
    /// Class describe word in crossword
    /// </summary>
    public class Word
    {
        #region Constructors

        public Word(SpaceWord spaceWord, int index)
        {
            this.Content = new Content(spaceWord.Length);

            this.SpaceWord = spaceWord;

            this.Index = index;
        }

        #endregion

        #region Internal Api

        internal int Index { get; set; }

        internal Content Content { get; set; }

        internal SpaceWord SpaceWord { get; set; }

        internal bool IsUsed { get; set; }

        #endregion
    }
}
