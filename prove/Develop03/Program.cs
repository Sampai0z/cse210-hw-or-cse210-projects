using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // List of scriptures
        var scriptures = new List<Scripture>
        {
            new Scripture(new Reference("Proverbs", 3, 5, 6), 
                "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight."),
            new Scripture(new Reference("John", 3, 16), 
                "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."),
            new Scripture(new Reference("Alma", 37, 37), 
                " Counsel with the Lord in all thy doings, and he will direct thee for good; yea, when thou liest down at night lie down unto the Lord, that he may watch over you in your sleep; and when thou risest in the morning let thy heart be full of thanks unto God; and if ye do these things, ye shall be lifted up at the last day."),
            new Scripture(new Reference("1 Nephi", 3, 5), 
                "And now, behold thy brothers murmur, saying it is a hard thing which I have required of them; but behold I have not required it of them, but it is a commandment of the Lord.")
        };

        // Select a random scripture from the list
        var random = new Random();
        var scripture = scriptures[random.Next(scriptures.Count)];

        while (!scripture.AllWordsHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture);
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");

            if (Console.ReadLine()?.ToLower() == "quit")
                break;

            scripture.HideRandomWords();
        }

        Console.Clear();
        Console.WriteLine("All words are hidden! Program is ending.");
    }
}

public class Scripture
{
    private List<Word> _words;
    private Reference _reference;
    private Random _random = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public void HideRandomWords()
    {
        var visibleWords = _words.Where(w => !w.IsHidden).ToList();
        if (visibleWords.Count > 0)
        {
            visibleWords[_random.Next(visibleWords.Count)].Hide();
        }
    }

    public bool AllWordsHidden() => _words.All(w => w.IsHidden);

    public override string ToString() => $"{_reference}\n{string.Join(' ', _words)}";
}

public class Word
{
    public string Text { get; private set; }
    public bool IsHidden { get; private set; } = false;

    public Word(string text) => Text = text;

    public void Hide() => IsHidden = true;

    public override string ToString() => IsHidden ? "_____" : Text;
}

public class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse;

    public Reference(string book, int chapter, int startVerse, int endVerse = 0)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse == 0 ? startVerse : endVerse;
    }

    public override string ToString() => $"{_book} {_chapter}:{_startVerse}" + (_endVerse > _startVerse ? $"-{_endVerse}" : "");
}
