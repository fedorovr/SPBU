namespace Drom
{
    class PalindromeChecker
    {
        private const string IgnoredCharacters = " _,.:;!?-()\"\'";
        public static bool IsPalindrome(string str)
        {
            str = str.ToLower();
            var leftIndex = 0;
            var rightIndex = str.Length - 1;
            while (leftIndex <= rightIndex)
            {
                if (IsDelimiter(str[leftIndex]))
                {
                    leftIndex++;
                }
                else if (IsDelimiter(str[rightIndex]))
                {
                    rightIndex--;
                }
                else if (str[leftIndex] != str[rightIndex])
                {
                    return false;
                }
                else {
                    ++leftIndex;
                    --rightIndex;
                }                
            }
            return true;
        }

        private static bool IsDelimiter(char chr)
        {
            return IgnoredCharacters.Contains(chr.ToString());
        }
    }
}
