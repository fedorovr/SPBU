using System;

class Program
{
	public static void Main()
	{
		Console.WriteLine("Я – интеллектуальный калькулятор!");
		Console.WriteLine("Как тебя зовут?");
		var username = Console.ReadLine();
		var rnd = new Random();
		var first = rnd.Next(1, 11);
		var second = rnd.Next(1, 11);
		Console.WriteLine("Сколько будет {0} + {1}?", first, second);
		var userAnswer = 0;
		var isInt = Int32.TryParse(Console.ReadLine().Trim(), out userAnswer);
		if (isInt && userAnswer == first + second)
		{
			Console.WriteLine("Верно, {0}!", username);
		} 
		else
		{
			Console.WriteLine("{0}, ты не прав", username);
		}
	}
}