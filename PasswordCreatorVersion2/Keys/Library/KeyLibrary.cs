using System.Collections.Generic;

namespace PasswordCreatorVersion2.Keys.Library
{
    public class KeyLibrary
    {
        private static readonly char[] EnglishUpChars = new[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
            'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        private static readonly char[] EnglishDownChars = new[]
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
            'v', 'w', 'x', 'y', 'z'
        };

        private static readonly char[] NumberChars = new[]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        public static char[] AddNewKeyLibrary(bool numberIsOn, bool englishDownKeysIsOn, bool englishUpKeysIsOn)
        {
            int elementCount = 0;
            var newArray = new List<char>();
            if (numberIsOn)
            {
                newArray.AddRange(NumberChars);
                elementCount += NumberChars.Length;
            }

            if (englishDownKeysIsOn)
            {
                newArray.AddRange(EnglishDownChars);
                elementCount += EnglishDownChars.Length;
            }

            if (englishUpKeysIsOn)
            {
                newArray.AddRange(EnglishUpChars);
                elementCount += EnglishUpChars.Length;
            }

            char[] arr = new char[elementCount];
            int element = 0;
            foreach (var item in newArray)
            {
                arr[element] = item;
                element++;
            }

            return arr;
        }
    }
}