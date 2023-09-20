using System;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        int inputNumber = -1;
        while (inputNumber != 0)
        {
            Console.Write("Enter a positive number (0 to quit): ");
            string userResponse = Console.ReadLine();
            inputNumber = int.Parse(userResponse);

            if (inputNumber != 0)
            {
                numbers.Add(inputNumber);
            }
        }
    }
}