using System;
using System.Collections.Generic;
using System.IO;

namespace JournalApp
{
    // Represents a single journal entry
    public class Entry
    {
        public string Prompt { get; set; }
        public string Response { get; set; }
        public string Date { get; set; }

        // Constructor to initialize an entry
        public Entry(string prompt, string response)
        {
            Prompt = prompt;
            Response = response;
            Date = DateTime.Now.ToString("MM/dd/yyyy");
        }

        // Display the entry
        public void Display()
        {
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine($"Prompt: {Prompt}");
            Console.WriteLine($"Response: {Response}\n");
        }

        // Converts the entry to a string for saving
        public override string ToString()
        {
            return $"{Date}|{Prompt}|{Response}";
        }

        // Creates an Entry from a string
        public static Entry FromString(string entryData)
        {
            string[] parts = entryData.Split('|');
            if (parts.Length == 3)
            {
                Entry entry = new Entry(parts[1], parts[2]);
                entry.Date = parts[0]; // Keep the original date
                return entry;
            }
            return null;
        }
    }

    // Represents the journal containing multiple entries
    public class Journal
    {
        private List<Entry> _entries = new List<Entry>();

        // Random prompts list
        private static readonly List<string> Prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        // Adds a new entry to the journal
        public void AddEntry()
        {
            Random rand = new Random();
            string prompt = Prompts[rand.Next(Prompts.Count)];

            Console.WriteLine($"Prompt: {prompt}");
            Console.Write("Your response: ");
            string response = Console.ReadLine();

            Entry newEntry = new Entry(prompt, response);
            _entries.Add(newEntry);

            Console.WriteLine("Entry added!\n");
        }

        // Displays all entries in the journal
        public void DisplayJournal()
        {
            if (_entries.Count == 0)
            {
                Console.WriteLine("The journal is empty.");
            }
            else
            {
                foreach (Entry entry in _entries)
                {
                    entry.Display();
                }
            }
        }

        // Saves the journal to a file
        public void SaveJournal()
        {
            Console.Write("Enter the filename to save: ");
            string filename = Console.ReadLine();

            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Entry entry in _entries)
                {
                    writer.WriteLine(entry.ToString());
                }
            }

            Console.WriteLine("Journal saved successfully!\n");
        }

        // Loads the journal from a file
        public void LoadJournal()
        {
            Console.Write("Enter the filename to load: ");
            string filename = Console.ReadLine();

            if (File.Exists(filename))
            {
                _entries.Clear(); // Clear current entries
                string[] lines = File.ReadAllLines(filename);

                foreach (string line in lines)
                {
                    Entry entry = Entry.FromString(line);
                    if (entry != null)
                    {
                        _entries.Add(entry);
                    }
                }

                Console.WriteLine("Journal loaded successfully!\n");
            }
            else
            {
                Console.WriteLine("File not found!\n");
            }
        }
    }

    // The main Program class
    class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();
            bool running = true;

            while (running)
            {
                Console.WriteLine("Journal Menu:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Save the journal to a file");
                Console.WriteLine("4. Load the journal from a file");
                Console.WriteLine("5. Quit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        journal.AddEntry();
                        break;
                    case "2":
                        journal.DisplayJournal();
                        break;
                    case "3":
                        journal.SaveJournal();
                        break;
                    case "4":
                        journal.LoadJournal();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        break;
                }
            }

            Console.WriteLine("Goodbye!");
        }
    }
}
