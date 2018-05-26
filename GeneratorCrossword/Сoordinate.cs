using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorCrossword
{
    /// <summary>
    /// Struct describe coordinate word in crossword
    /// </summary>
    public struct Сoordinate
    {
        #region Constructors

        internal Сoordinate(int x, int y)
        {
            this.X = x;

            this.Y = y;
        }

        #endregion

        #region Internal Api

        internal int X { get; set; }

        internal int Y { get; set; }

        #endregion
    }
}
