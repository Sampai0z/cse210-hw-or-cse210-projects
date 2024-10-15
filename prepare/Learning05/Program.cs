using System;

public class Assignment
{
    private string _studentName;
    private string _topic;

    // Construtor da classe base
    public Assignment(string studentName, string topic)
    {
        _studentName = studentName;
        _topic = topic;
    }

    // Método para retornar o resumo do trabalho
    public string GetSummary()
    {
        return $"{_studentName} - {_topic}";
    }

    // Método para obter o nome do estudante (para uso em classes derivadas)
    public string GetStudentName()
    {
        return _studentName;
    }
}


public class MathAssignment : Assignment
{
    private string _textbookSection;
    private string _problems;

    // Construtor para MathAssignment, chamando o construtor base para nome e tópico
    public MathAssignment(string studentName, string topic, string textbookSection, string problems)
        : base(studentName, topic)
    {
        _textbookSection = textbookSection;
        _problems = problems;
    }

    // Método para retornar a lista de tarefas de matemática
    public string GetHomeworkList()
    {
        return $"Section {_textbookSection} Problems {_problems}";
    }
}


public class WritingAssignment : Assignment
{
    private string _title;

    // Construtor para WritingAssignment, chamando o construtor base para nome e tópico
    public WritingAssignment(string studentName, string topic, string title)
        : base(studentName, topic)
    {
        _title = title;
    }

    // Método para retornar as informações da redação
    public string GetWritingInformation()
    {
        return $"{_title} by {GetStudentName()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Criando uma tarefa de Matemática
        MathAssignment mathAssignment = new MathAssignment("Roberto Rodriguez", "Fractions", "7.3", "8-19");
        Console.WriteLine(mathAssignment.GetSummary());          // Output: Roberto Rodriguez - Fractions
        Console.WriteLine(mathAssignment.GetHomeworkList());     // Output: Section 7.3 Problems 8-19

        // Criando uma tarefa de Redação
        WritingAssignment writingAssignment = new WritingAssignment("Mary Waters", "European History", "The Causes of World War II");
        Console.WriteLine(writingAssignment.GetSummary());       // Output: Mary Waters - European History
        Console.WriteLine(writingAssignment.GetWritingInformation()); // Output: The Causes of World War II by Mary Waters
    }
}
