using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcome(); 
        string userName = PromptUserName();
        int userNumber = PromptUserNumber(); 
        int squaredNumber = SquareNumber(userNumber);
        DisplayResult(userName, squaredNumber);
    }

    // Welcome
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }

    // User Name
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();
        return name;
    }

    // Favorite number
    static int PromptUserNumber()
    {
        int number;
        bool isValidNumber;

        do
        {
            Console.Write("Please enter your favorite number: ");
            string userNumber = Console.ReadLine(); 

            isValidNumber = int.TryParse(userNumber, out number);

            if (!isValidNumber)
            {
                Console.WriteLine("Invalid number, please enter a valid integer.");
            }

        } while (!isValidNumber);  // Continua até um número válido ser inserido

        return number;  // Retorna o número válido
    }

    // Square
    static int SquareNumber(int number)
    {
        return number * number;
    }

    // Display 
    static void DisplayResult(string name, int squaredNumber)
    {
        Console.WriteLine($"{name}, the square of your number is {squaredNumber}");
    }
}
