using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var scriptures = new List<Scripture>
        {
            new Scripture("John 3:16", "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting ilife."),
            new Scripture("Psalms 23:1", "The Lord is my shepherd; I shall not want."),
            new Scripture("Proverbs 3:5-6", "5 Trust in the Lord with all thine heart; and lean not unto thine own understanding. 6 In all thy ways acknowledge him, and he shall direct thy paths.")
        };

        var memorizer = new ScriptureMemorizer(scriptures);
        memorizer.Run();
    }
}

class Scripture
{
    public string Reference { get; }
    public string Text { get; }

    public Scripture(string reference, string text)
    {
        Reference = reference;
        Text = text;
    }
}

class ScriptureMemorizer
{
    private List<Scripture> scriptures;
    private Random random = new Random();

    public ScriptureMemorizer(List<Scripture> scriptures)
    {
        this.scriptures = scriptures;
    }

    public void Run()
    {
        Console.WriteLine("Welcome to Scripture Memorizer!");

        while (true)
        {
            DisplayScriptureMenu();
            var choice = GetUserChoice();

            if (choice < 0 || choice >= scriptures.Count)
            {
                Console.WriteLine("Invalid choice. Please select a valid option.");
                continue;
            }

            MemorizeScripture(scriptures[choice]);
        }
    }

    private void DisplayScriptureMenu()
    {
        Console.Clear();
        Console.WriteLine("Choose a Scripture to Memorize:");
        for (int i = 0; i < scriptures.Count; i++)
        {
            Console.WriteLine($"{i}. {scriptures[i].Reference}");
        }
    }

    private int GetUserChoice()
    {
        Console.Write("Enter the number of the scripture to memorize (or type 'quit' to exit): ");
        string input = Console.ReadLine();
        if (input.ToLower() == "quit")
        {
            Environment.Exit(0);
        }
        if (int.TryParse(input, out int choice))
        {
            return choice;
        }
        return -1;
    }

    private void MemorizeScripture(Scripture scripture)
    {
        var hiddenWords = new bool[scripture.Text.Split(' ').Length];

        while (!AllWordsHidden(hiddenWords))
        {
            DisplayScripture(scripture, hiddenWords);
            Console.WriteLine("Press Enter to continue or type 'quit' to exit.");
            var input = Console.ReadLine().ToLower();
            if (input == "quit")
            {
                break;
            }
            else if (string.IsNullOrWhiteSpace(input))
            {
                HideRandomWords(hiddenWords);
            }
        }
    }

    private void DisplayScripture(Scripture scripture, bool[] hiddenWords)
    {
        Console.Clear();
        var words = scripture.Text.Split(' ');

        Console.WriteLine(scripture.Reference);
        for (int i = 0; i < words.Length; i++)
        {
            if (hiddenWords[i])
                Console.Write(new string('_', words[i].Length) + " ");
            else
                Console.Write(words[i] + " ");
        }
        Console.WriteLine();
    }

    private bool AllWordsHidden(bool[] hiddenWords)
    {
        return hiddenWords.All(w => w);
    }

    private void HideRandomWords(bool[] hiddenWords)
    {
        for (int i = 0; i < hiddenWords.Length; i++)
        {
            if (random.Next(2) == 1 && !hiddenWords[i])
            {
                hiddenWords[i] = true;
            }
        }
    }
}

