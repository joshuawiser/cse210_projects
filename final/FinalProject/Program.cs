using System;

class Program
{
    static void Main()
    {
        TaskManager taskManager = new TaskManager();

        while (true)
        {
            Console.WriteLine("Task Manager Menu:");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Mark Task as Completed");
            Console.WriteLine("3. Display All Tasks");
            Console.WriteLine("4. Save Tasks to File");
            Console.WriteLine("5. Load Tasks from File");
            Console.WriteLine("6. Exit");

            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask(taskManager);
                    break;
                case "2":
                    MarkTaskAsCompleted(taskManager);
                    break;
                case "3":
                    taskManager.DisplayAllTasks();
                    break;
                case "4":
                    Console.Write("Enter file name to save tasks: ");
                    string saveFileName = Console.ReadLine();
                    taskManager.SaveTasksToFile(saveFileName);
                    break;
                case "5":
                    Console.Write("Enter file name to load tasks: ");
                    string loadFileName = Console.ReadLine();
                    taskManager.LoadTasksFromFile(loadFileName);
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void AddTask(TaskManager taskManager)
    {
        Console.WriteLine("Enter task details:");

        Console.Write("Title: ");
        string title = Console.ReadLine();

        if (title.ToLower() == "exit")
        {
            Environment.Exit(0);
        }

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.Write("Priority (Low/Medium/High): ");
        Enum.TryParse(Console.ReadLine(), out Priority priority);

        Console.Write("Task type (Work/Personal): ");
        string taskType = Console.ReadLine();

        Task newTask;
        if (taskType.ToLower() == "work")
        {
            Console.Write("Project Name: ");
            string projectName = Console.ReadLine();
            newTask = new WorkTask(title, description, priority, projectName);
        }
        else if (taskType.ToLower() == "personal")
        {
            newTask = new PersonalTask(title, description, priority);
        }
        else
        {
            Console.WriteLine("Invalid task type. Defaulting to Personal Task.");
            newTask = new PersonalTask(title, description, priority);
        }

        taskManager.AddTask(newTask);
    }

    static void MarkTaskAsCompleted(TaskManager taskManager)
    {
        Console.Write("Enter the index of the task to mark as completed: ");
        if (int.TryParse(Console.ReadLine(), out int index))
        {
            taskManager.MarkTaskAsCompleted(index - 1);
        }
        else
        {
            Console.WriteLine("Invalid index. Please enter a valid numeric index.");
        }
    }
}