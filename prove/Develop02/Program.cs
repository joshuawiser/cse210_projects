using System;
using System.Collections.Generic;
using System.IO;

class Entry
{
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public override string ToString()
    {
        return $"Date: {Date}\nTitle: {Title}\nContent: {Content}\n";
    }
}

class Journal
{
    private List<Entry> entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        entries.Add(entry);
    }

    public void DisplayEntries()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine(entry.ToString());
        }
    }

    public void LoadEntries(string fileName)
    {
        if (File.Exists(fileName))
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    string dateStr = reader.ReadLine();
                    string title = reader.ReadLine();
                    string content = reader.ReadLine();

                    if (DateTime.TryParse(dateStr, out DateTime date))
                    {
                        Entry entry = new Entry
                        {
                            Date = date,
                            Title = title,
                            Content = content
                        };
                        entries.Add(entry);
                    }
                    else
                    {
                        Console.WriteLine("Error parsing date from the file.");
                    }
                }
            }
            Console.WriteLine("Loaded Entries:");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    public void SaveEntries(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (var entry in entries)
            {
                writer.WriteLine(entry.Date);
                writer.WriteLine(entry.Title);
                writer.WriteLine(entry.Content);
            }
            Console.WriteLine("Entries saved to the file.");
        }
    }
}

class Prompt
{
    private static string[] journalPrompts = new string[]
    {
        "What are you grateful for today?",
        "Describe a challenge you faced and how you overcame it.",
        "Write about a memorable dream or aspiration.",
        "Reflect on a meaningful conversation you had recently.",
        "Share a favorite quote and explain why it resonates with you."
    };

    public static int GetChoice()
    {
        Console.WriteLine("Please select one of the following choices:");
        Console.WriteLine("1. Write");
        Console.WriteLine("2. Read");
        Console.WriteLine("3. Load");
        Console.WriteLine("4. Save");
        Console.WriteLine("5. Quit");

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
        {
            Console.WriteLine("Invalid choice. Please enter a valid option (1-5).");
        }

        return choice;
    }

    public static Entry GetJournalEntry()
    {
        Console.WriteLine("Random Journal Prompt: ");
        Random rand = new Random();
        int randomIndex = rand.Next(journalPrompts.Length);
        Console.WriteLine(journalPrompts[randomIndex]);

        Console.WriteLine("Enter the title for your journal entry:");
        string title = Console.ReadLine();

        Console.WriteLine("Enter text:");
        string content = Console.ReadLine();

        return new Entry
        {
            Date = DateTime.Now,
            Title = title,
            Content = content
        };
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();

        Console.WriteLine("Welcome to your journal!");

        while (true)
        {
            int choice = Prompt.GetChoice();

            switch (choice)
            {
                case 1:
                    Entry newEntry = Prompt.GetJournalEntry();
                    journal.AddEntry(newEntry);
                    break;
                case 2:
                    journal.DisplayEntries();
                    break;
                case 3:
                    Console.WriteLine("Enter file name to load: ");
                    string loadFileName = Console.ReadLine();
                    journal.LoadEntries(loadFileName);
                    break;
                case 4:
                    Console.WriteLine("Enter file name to save: ");
                    string saveFileName = Console.ReadLine();
                    journal.SaveEntries(saveFileName);
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
