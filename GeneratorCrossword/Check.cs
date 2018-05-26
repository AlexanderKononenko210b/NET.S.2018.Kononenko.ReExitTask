using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorCrossword
{
    /// <summary>
    /// Static class for check condition
    /// </summary>
    public static class Check
    {
        /// <summary>
        /// Check reference argument on null 
        /// </summary>
        /// <typeparam name="T">reference type</typeparam>
        /// <param name="value">value for check</param>
        /// <returns>value</returns>
        public static T NotNull<T>(T value) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException($"Argument {nameof(value)} is null");
            }

            return value;
        }

        /// <summary>
        /// Check nullable argument on null 
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="value">value for check</param>
        /// <returns>value</returns>
        public static T? NotNull<T>(T? value) where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException($"Argument {nameof(value)} is null");
            }

            return value;
        }

        /// <summary>
        /// Check whitespace or empty string input
        /// </summary>
        /// <param name="value">value for check</param>
        /// <returns>value</returns>
        public static string CheckString(string value)
        {
            if (value == String.Empty)
                throw new ArgumentOutOfRangeException($"Argument {value} is empty");

            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentOutOfRangeException($"Argument {value} is whitespace");

            return value;
        }
    }
}
