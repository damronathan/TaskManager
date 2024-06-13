using System;
using System.Collections.Generic;
using System.Linq;
public class Task
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsComplete { get; set; }
    // Default constructor
    public Task() { }
    // Constructor to initialize all properties
    public Task(int id, string title, string description, bool isComplete)
    {
        ID = id;
        Title = title;
        Description = description;
        IsComplete = isComplete;
    }

    public override string ToString()
    {
        return $"{ID}/{Title}/{Description}/{IsComplete}";
    }
    public static Task FromString(string taskString)
    {
        var parts = taskString.Split('/');
        return new Task
        {
            ID = int.Parse(parts[0]),
            Title = parts[1],
            Description = parts[2],
            IsComplete = bool.Parse(parts[3])
        };
    }
    
}


public class MenuMethods
{
    private static TaskManager taskManager = new TaskManager();
    public static Task PromptForTask()
    {
        Console.WriteLine();
        Console.Write("Enter task title: ");
        string title = Console.ReadLine();
        Console.Write("Enter task description: ");
        string description = Console.ReadLine();
        bool isComplete = false;
        int id = 0;
        return new Task(id, title, description, isComplete);
    }
    public static int DisplayMenuAndGetSelection(List<string> menu)
    {
        foreach (string item in menu)
        {
            Console.WriteLine(item);
        }
        string selectedNum = Console.ReadLine();
        while (selectedNum != "1" && selectedNum != "2" && selectedNum != "3" && selectedNum != "4" && selectedNum != "5")
        {
            Console.WriteLine();
            Console.WriteLine("INCORRECT INPUT");
            foreach (string item in menu)
            {
                Console.WriteLine(item);
            }
            selectedNum = Console.ReadLine();
        }
        return Convert.ToInt32(selectedNum);
    }
    public static void AddTask()
    {
        while (true)
        {
            Task newTask = PromptForTask();
            taskManager.AddTask(newTask);
            Console.Write("Do you want to add another task? (yes/no): ");
            string continueInput = Console.ReadLine();
            if (continueInput.ToLower() != "yes")
            {
                break;
            }
        }
    }
    public static void ViewTasks()
    {
        var tasks = taskManager.GetTasks();
        if (tasks.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine("Tasks:");
            foreach (Task task in tasks)
            {
                if (task.IsComplete)
                {
                    string completionStatus = task.IsComplete ? "yes" : "no";
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(task.ID + ". Title: " + task.Title + " / Description: " + task.Description + " / Completed: " + completionStatus);
                    Console.ResetColor();
                }
                else if (task.IsComplete == false)
                {
                    string completionStatus = task.IsComplete ? "yes" : "no";
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(task.ID + ". Title: " + task.Title + " / Description: " + task.Description + " / Completed: " + completionStatus);
                    Console.ResetColor();
                }

            }

            Console.WriteLine("Enter any key to continue");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("There are no tasks.");
        }
    }
    public static void MarkComplete()
    {
        var tasks = taskManager.GetTasks();
        if (tasks.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine("Tasks:");
            foreach (Task task in tasks)
            {
                if (task.IsComplete)
                {
                    string completionStatus = task.IsComplete ? "yes" : "no";
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(task.ID + ". Title: " + task.Title + " / Description: " + task.Description + " / Completed: " + completionStatus);
                    Console.ResetColor();
                }
                else if (task.IsComplete == false)
                {
                    string completionStatus = task.IsComplete ? "yes" : "no";
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(task.ID + ". Title: " + task.Title + " / Description: " + task.Description + " / Completed: " + completionStatus);
                    Console.ResetColor();
                }

            }
            Console.WriteLine();
            Console.Write("Enter the number of the task you would like to mark completed: ");
            string stringID = Console.ReadLine();
            int selectedID;
            if (!int.TryParse(stringID, out selectedID))
            {
                Console.WriteLine();
                Console.WriteLine("INCORRECT INPUT");
                MarkComplete();
                return;
            }
            Task selectedTask = tasks.FirstOrDefault(t => t.ID == selectedID);
            if (selectedTask != null)
            {
                selectedTask.IsComplete = true;
                taskManager.UpdateTask(selectedTask);
                Console.WriteLine();
                Console.WriteLine(selectedTask.Title + " is now marked as completed.");
                Console.WriteLine();
                Console.Write("Would you like to mark another task completed? (yes/no): ");
                string answer = Console.ReadLine();
                if (answer.ToLower() == "yes")
                {
                    MarkComplete();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Task not found.");
            }
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("There are no tasks.");
        }
    }
    public static void DeleteTask()
    {
        var tasks = taskManager.GetTasks();
        if (tasks.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine("Tasks:");
            foreach (Task task in tasks)
            {
                if (task.IsComplete)
                {
                    string completionStatus = task.IsComplete ? "yes" : "no";
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(task.ID + ". Title: " + task.Title + " / Description: " + task.Description + " / Completed: " + completionStatus);
                    Console.ResetColor();
                }
                else if (task.IsComplete == false)
                {
                    string completionStatus = task.IsComplete ? "yes" : "no";
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(task.ID + ". Title: " + task.Title + " / Description: " + task.Description + " / Completed: " + completionStatus);
                    Console.ResetColor();
                }

            }
            Console.WriteLine();
            Console.Write("Enter the number of the task you would like to delete: ");
            string stringID = Console.ReadLine();
            int selectedID;
            if (!int.TryParse(stringID, out selectedID))
            {
                Console.WriteLine();
                Console.WriteLine("INCORRECT INPUT");
                DeleteTask();
                return;
            }
            Task selectedTask = tasks.FirstOrDefault(t => t.ID == selectedID);
            if (selectedTask != null)
            {
                taskManager.DeleteTask(selectedID);
                Console.WriteLine();
                Console.WriteLine(selectedTask.Title + " has been deleted.");


                Console.WriteLine();
                Console.Write("Would you like to delete another task? (yes/no): ");
                string answer = Console.ReadLine();
                if (answer.ToLower() == "yes")
                {
                    DeleteTask();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Task not found.");
            }
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("There are no tasks.");
        }
    }
}

