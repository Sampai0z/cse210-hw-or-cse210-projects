using System.Threading;
using System;

public class MindfulnessActivity
{
    private string _name;
    private string _description;

    public MindfulnessActivity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    // Exibe a mensagem inicial e pede a duração da atividade
    public int StartActivity()
    {
        Console.WriteLine($"Starting {_name}...");
        Console.WriteLine(_description);
        Console.Write("Enter the duration of the activity in seconds: ");
        int _duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        PauseWithSpinner(3);  // Pausa por 3 segundos
        return _duration;
    }

    // Exibe a mensagem final
    public void EndActivity(int duration)
    {
        Console.WriteLine("Well done!");
        Console.WriteLine($"You have completed the {_name} for {duration} seconds.");
        PauseWithSpinner(3);  // Pausa por 3 segundos
    }

    // Método que mostra uma pausa com uma animação de spinner
    public void PauseWithSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write("/");
            Thread.Sleep(500);
            Console.Write("\b-");
            Thread.Sleep(500);
            Console.Write("\b\\");
            Thread.Sleep(500);
            Console.Write("\b|");
            Thread.Sleep(500);
            Console.Write("\b");
        }
        Console.WriteLine();
    }
}

public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() : base("Breathing Activity", 
        "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    // Método específico da atividade de respiração
    public void StartBreathing(int duration)
    {
        int timePerBreath = 4;  // Cada ciclo de respiração dura 4 segundos (2 inspirar, 2 expirar)
        int cycles = duration / timePerBreath;

        for (int i = 0; i < cycles; i++)
        {
            Console.WriteLine("Breathe in...");
            PauseWithSpinner(2);
            Console.WriteLine("Breathe out...");
            PauseWithSpinner(2);
        }
    }
}


public class ReflectionActivity : MindfulnessActivity
{
    private List<string> _prompts = new List<string>()
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> _questions = new List<string>()
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times?",
        "What is your favorite thing about this experience?",
        "What did you learn from this experience?"
    };

    public ReflectionActivity() : base("Reflection Activity", 
        "This activity will help you reflect on times in your life when you have shown strength and resilience.")
    {
    }

    // Método específico da atividade de reflexão
    public void StartReflection(int duration)
    {
        Random random = new Random();
        Console.WriteLine(_prompts[random.Next(_prompts.Count)]);

        int elapsedTime = 0;
        while (elapsedTime < duration)
        {
            string question = _questions[random.Next(_questions.Count)];
            Console.WriteLine(question);
            PauseWithSpinner(5);  // Pausa para reflexão
            elapsedTime += 5;
        }
    }
}

public class ListingActivity : MindfulnessActivity
{
    private string[] _prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing Activity", 
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    // Método específico da atividade de listagem
    public void StartListing(int duration)
    {
        Random random = new Random();
        Console.WriteLine(_prompts[random.Next(_prompts.Length)]);
        Console.WriteLine("You have a few seconds to start thinking...");
        PauseWithSpinner(3);

        int count = 0;
        int elapsedTime = 0;
        while (elapsedTime < duration)
        {
            Console.Write("List an item: ");
            Console.ReadLine();
            count++;
            elapsedTime += 5;  // Estima 5 segundos por item
        }

        Console.WriteLine($"You listed {count} items.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            if (choice == "4")
            {
                break;
            }

            MindfulnessActivity activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    continue;
            }

            int duration = activity.StartActivity();

            if (activity is BreathingActivity)
            {
                ((BreathingActivity)activity).StartBreathing(duration);
            }
            else if (activity is ReflectionActivity)
            {
                ((ReflectionActivity)activity).StartReflection(duration);
            }
            else if (activity is ListingActivity)
            {
                ((ListingActivity)activity).StartListing(duration);
            }

            activity.EndActivity(duration);
        }
    }
}
