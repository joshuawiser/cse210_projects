using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        int secretNumber = randomGenerator.Next(1, 100);

        int guess = 0;

        while (guess != secretNumber)
        {
            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());
            if (secretNumber > guess)
            {
                Console.WriteLine("Higher");
            }
            else if (secretNumber < guess)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }
        }
    }
}