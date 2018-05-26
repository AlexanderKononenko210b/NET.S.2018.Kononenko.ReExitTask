using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace GeneratorCrossword
{
    /// <summary>
    /// Describe word in space
    /// </summary>
    public struct SpaceWord
    {
        #region Constructors

        internal SpaceWord(int x, int y, int length, Direction direction)
        {
            this.Coordinate = new Сoordinate(x, y);

            this.Direction = direction;

            this.Length = length;
        }

        #endregion

        #region Public Api

        internal Сoordinate Coordinate { get; set; }

        internal Direction Direction { get; set; }

        internal int Length { get; set; }

        #endregion
    }
}
