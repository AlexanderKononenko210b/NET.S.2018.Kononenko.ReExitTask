using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorCrossword
{
    /// <summary>
    /// Class describe content word
    /// </summary>
    public class Content
    {
        #region Constructors

        internal Content(int length)
        {
            this.WordChars = new char[length];
        }

        #endregion

        #region Internal Api

        internal char[] WordChars { get; set; }

        #endregion
    }
}
