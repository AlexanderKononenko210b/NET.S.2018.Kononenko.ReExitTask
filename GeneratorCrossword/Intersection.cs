using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorCrossword
{
    /// <summary>
    /// Class describe intersection two words
    /// </summary>
    internal class Intersection
    {
        #region Constructors

        internal Intersection(Сoordinate pointIntersection, Word wordFirst, Word wordSecond)
        {
            this.PointIntersection = pointIntersection;

            this.WordFirst = wordFirst;

            this.WordSecond = wordSecond;
        }

        #endregion

        #region Internal Api

        internal Сoordinate PointIntersection { get; set; }

        internal Word WordFirst { get; set; }

        internal Word WordSecond { get; set; }

        /// <summary>
        /// Get key for using in dictionary
        /// </summary>
        /// <returns>key type string</returns>
        internal string GetKey() => $"{WordFirst.Index},{WordSecond.Index}";

        #endregion
    }
}
