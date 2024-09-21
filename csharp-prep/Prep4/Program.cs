using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>(); // Lista para armazenar os números
        int number;

        // Pedir números ao usuário até ele digitar 0
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        do
        {
            string input;
            bool isValidNumber;

            do
            {
                Console.Write("Enter number: ");
                input = Console.ReadLine();  // Recebe a entrada do usuário

                // Tenta converter a entrada para um número inteiro
                isValidNumber = int.TryParse(input, out number);

                if (!isValidNumber)
                {
                    Console.WriteLine("Invalid input, please enter a valid integer.");
                }

            } while (!isValidNumber);  // Continua até uma entrada válida ser recebida

            if (number != 0)  // Adiciona o número à lista se for válido e diferente de 0
            {
                numbers.Add(number);
            }

        } while (number != 0);  // Para quando o usuário digitar 0

        // Requisitos principais: Calcular soma, média e maior número
        if (numbers.Count > 0)
        {
            int sum = 0;
            int largest = numbers[0];
            int? smallestPositive = null;

            // Soma, encontra o maior e o menor número positivo
            foreach (int num in numbers)
            {
                sum += num;

                if (num > largest)
                {
                    largest = num;
                }

                if (num > 0 && (smallestPositive == null || num < smallestPositive))
                {
                    smallestPositive = num;
                }
            }

            // Calcula a média
            double average = (double)sum / numbers.Count;

            // Exibe soma, média e o maior número
            Console.WriteLine($"The sum is: {sum}");
            Console.WriteLine($"The average is: {average}");
            Console.WriteLine($"The largest number is: {largest}");

            // Desafio: Exibe o menor número positivo
            if (smallestPositive.HasValue)
            {
                Console.WriteLine($"The smallest positive number is: {smallestPositive.Value}");
            }
            else
            {
                Console.WriteLine("No positive numbers were entered.");
            }

            // Desafio: Ordena e exibe a lista de números
            numbers.Sort();
            Console.WriteLine("The sorted list is:");
            foreach (int num in numbers)
            {
                Console.WriteLine(num);
            }
        }
        else
        {
            Console.WriteLine("No numbers were entered.");
        }
    }
}
