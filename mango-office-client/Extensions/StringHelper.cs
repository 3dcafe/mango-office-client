using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MangoOfficeClient.Extensions
{
    public static class StringHelper
    {
        /// <summary>
        /// Replace the string of special characters
        /// </summary>
        /// <param name="str">Text</param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(this string str)
        {
            return Regex.Replace(str, "[^а-яА-Яa-zA-Z0-9_. «»]+", "", RegexOptions.Compiled);
        }
    }
}
