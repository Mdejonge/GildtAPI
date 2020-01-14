using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GildtAPI
{
    static class Whitelister
    {
        static readonly Regex ALPHA = new Regex("[^ [:alpha:]]+");
        /// <summary>
        /// Gets string filtered through regex only allowing alphabet characters and spaces.
        /// </summary>
        public static string GetAlphaFiltered(string input)
        {
            return ALPHA.Replace(input, "");
        }
    }
}
