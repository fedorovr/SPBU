namespace Drom
{
    public static class PalindromeChecker
    {
        private const string IgnoredCharacters = " _,.:;!?-()\"\'";
        public static bool IsPalindrome(string candidateForPalindrome)
        {
            if (candidateForPalindrome == null)
            {
                throw new System.ArgumentException("Unable to determine is null string a palindrome.");
            }
            var leftIndex = 0;
            var rightIndex = candidateForPalindrome.Length - 1;
            while (leftIndex <= rightIndex)
            {
                if (IsDelimiter(candidateForPalindrome[leftIndex]))
                {
                    leftIndex++;
                }
                else if (IsDelimiter(candidateForPalindrome[rightIndex]))
                {
                    rightIndex--;
                }
                else if (char.ToLower(candidateForPalindrome[leftIndex]) != char.ToLower(candidateForPalindrome[rightIndex]))
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
