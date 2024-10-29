using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    // Base class for Goal
    public abstract class Goal
    {
        protected string description;
        protected int points;
        protected bool isComplete;

        public Goal(string description, int points)
        {
            this.description = description;
            this.points = points;
            this.isComplete = false;
        }

        public abstract int RecordEvent(); // Record progress on the goal and return points

        public abstract string GetStatus(); // Get current goal status

        public virtual bool IsComplete()
        {
            return isComplete;
        }
    }

    // SimpleGoal class (One-time goal)
    public class SimpleGoal : Goal
    {
        public SimpleGoal(string description, int points) : base(description, points) { }

        public override int RecordEvent()
        {
            if (!isComplete)
            {
                isComplete = true;
                return points;
            }
            return 0;
        }

        public override string GetStatus()
        {
            return isComplete ? "[X] " + description : "[ ] " + description;
        }
    }

    // EternalGoal class (Repeating goal)
    public class EternalGoal : Goal
    {
        public EternalGoal(string description, int points) : base(description, points) { }

        public override int RecordEvent()
        {
            return points; // Always gives points when recorded
        }

        public override string GetStatus()
        {
            return "[∞] " + description;
        }
    }

    // ChecklistGoal class (Multiple times goal with a bonus)
    public class ChecklistGoal : Goal
    {
        private int targetCount;
        private int currentCount;
        private int bonusPoints;

        public ChecklistGoal(string description, int points, int targetCount, int bonusPoints)
            : base(description, points)
        {
            this.targetCount = targetCount;
            this.currentCount = 0;
            this.bonusPoints = bonusPoints;
        }

        public override int RecordEvent()
        {
            if (isComplete) return 0;
            currentCount++;
            if (currentCount >= targetCount)
            {
                isComplete = true;
                return points + bonusPoints;
            }
            return points;
        }

        public override string GetStatus()
        {
            return isComplete ? $"[X] {description} - Completed {currentCount}/{targetCount}" :
                                $"[ ] {description} - Completed {currentCount}/{targetCount}";
        }
    }

    // User class to manage goals and score
    public class User
    {
        private List<Goal> goals;
        private int score;

        public User()
        {
            goals = new List<Goal>();
            score = 0;
        }

        public void AddGoal(Goal goal)
        {
            goals.Add(goal);
        }

        public void RecordEvent(int goalIndex)
        {
            if (goalIndex >= 0 && goalIndex < goals.Count)
            {
                score += goals[goalIndex].RecordEvent();
            }
        }

        public void ShowGoals()
        {
            Console.WriteLine("Your Goals:");
            for (int i = 0; i < goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {goals[i].GetStatus()}");
            }
        }

        public void ShowScore()
        {
            Console.WriteLine($"Score: {score}");
        }

        public void SaveProgress(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(score);
                foreach (var goal in goals)
                {
                    writer.WriteLine(goal.GetStatus());
                }
            }
        }

        public void LoadProgress(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    if (int.TryParse(reader.ReadLine(), out int loadedScore))
                    {
                        score = loadedScore;
                    }
                    // For simplicity, just reading goals' statuses without recreating goal objects.
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            User user = new User();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nEternal Quest Menu:");
                Console.WriteLine("1. Add a Simple Goal");
                Console.WriteLine("2. Add an Eternal Goal");
                Console.WriteLine("3. Add a Checklist Goal");
                Console.WriteLine("4. Record an Event");
                Console.WriteLine("5. Show Goals");
                Console.WriteLine("6. Show Score");
                Console.WriteLine("7. Save Progress");
                Console.WriteLine("8. Load Progress");
                Console.WriteLine("9. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter description: ");
                        string desc1 = Console.ReadLine();
                        Console.Write("Enter points: ");
                        int points1 = int.Parse(Console.ReadLine());
                        user.AddGoal(new SimpleGoal(desc1, points1));
                        break;

                    case 2:
                        Console.Write("Enter description: ");
                        string desc2 = Console.ReadLine();
                        Console.Write("Enter points: ");
                        int points2 = int.Parse(Console.ReadLine());
                        user.AddGoal(new EternalGoal(desc2, points2));
                        break;

                    case 3:
                        Console.Write("Enter description: ");
                        string desc3 = Console.ReadLine();
                        Console.Write("Enter points per event: ");
                        int points3 = int.Parse(Console.ReadLine());
                        Console.Write("Enter target count: ");
                        int targetCount = int.Parse(Console.ReadLine());
                        Console.Write("Enter bonus points: ");
                        int bonusPoints = int.Parse(Console.ReadLine());
                        user.AddGoal(new ChecklistGoal(desc3, points3, targetCount, bonusPoints));
                        break;

                    case 4:
                        user.ShowGoals();
                        Console.Write("Enter goal number to record event: ");
                        int goalIndex = int.Parse(Console.ReadLine()) - 1;
                        user.RecordEvent(goalIndex);
                        break;

                    case 5:
                        user.ShowGoals();
                        break;

                    case 6:
                        user.ShowScore();
                        break;

                    case 7:
                        user.SaveProgress("progress.txt");
                        break;

                    case 8:
                        user.LoadProgress("progress.txt");
                        break;

                    case 9:
                        running = false;
                        break;
                }
            }
        }
    }
}
