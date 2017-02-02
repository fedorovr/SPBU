namespace Drom
{
    internal sealed class Program
    {
        private const string GetInput = "Type a string to check, is it a palindrome or not";
        private const string IsPalindrome = "is a palindrome";
        private const string IsNotPalindrome = "isn't a palindrome";

        static void Main(string[] args)
        {
            while (true)
            {
                System.Console.WriteLine(GetInput);
                var input = System.Console.ReadLine();
                System.Console.WriteLine(input + " " + (PalindromeChecker.IsPalindrome(input) ? IsPalindrome : IsNotPalindrome));
            }
        }
    }
}
