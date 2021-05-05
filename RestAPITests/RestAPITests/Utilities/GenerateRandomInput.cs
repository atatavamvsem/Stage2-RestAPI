using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPITests.Utilities
{
    class GenerateRandomInput
    {
        static readonly Random rndGen = new Random();

        static readonly char[] englishSymbols = "qwertyuiopasdfghjklzxcvbnm".ToCharArray();

        public static String GenerateEnglishString(int lengthPassword)
        {
            string result = "";
            for (int i = 0; i < lengthPassword; i++)
            {
                result += englishSymbols[rndGen.Next(englishSymbols.Length)].ToString();
            }
            return result;
        }
    }
}
