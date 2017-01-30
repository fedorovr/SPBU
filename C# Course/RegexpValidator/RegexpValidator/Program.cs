namespace RegexValidator
{
    class Program
    {
        private const string Description = "Enter e-mail, Ru/Us zip code or Ru phone to validate";

        static void Main(string[] args)
        {
            System.Console.WriteLine(Description);
            System.Console.WriteLine(Validator.Validate(System.Console.ReadLine()));
        }
    }
}
