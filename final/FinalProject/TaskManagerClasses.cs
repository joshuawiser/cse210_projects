using System;
using System.Collections.Generic;

public abstract class Task
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public bool IsCompleted { get; set; }


    protected Task(string title, string description, Priority priority)
    {
        Title = title;
        Description = description;
        Priority = priority;
        IsCompleted = false;
    }


    public abstract void DisplayTaskDetails();


    public virtual void MarkAsCompleted()
    {
        IsCompleted = true;
        Console.WriteLine($"Task '{Title}' marked as completed.");
    }
}


public enum Priority
{
    Low,
    Medium,
    High
}


public class WorkTask : Task
{
    public string ProjectName { get; set; }


    public WorkTask(string title, string description, Priority priority, string projectName)
        : base(title, description, priority)
    {
        ProjectName = projectName;
    }


    public override void DisplayTaskDetails()
    {
        Console.WriteLine($"Work Task: {Title} - {Description} (Project: {ProjectName}, Priority: {Priority}, Completed: {IsCompleted})");
    }
}


public class PersonalTask : Task
{
    // Constructor
    public PersonalTask(string title, string description, Priority priority)
        : base(title, description, priority)
    {
    }


    public override void DisplayTaskDetails()
    {
        Console.WriteLine($"Personal Task: {Title} - {Description} (Priority: {Priority}, Completed: {IsCompleted})");
    }
}

public class TaskManager
{
    private List<Task> tasks;

    public TaskManager()
    {
        tasks = new List<Task>();
    }


    public void AddTask(Task task)
    {
        tasks.Add(task);
        Console.WriteLine($"Task '{task.Title}' added to the Task Manager.");
    }

    public void MarkTaskAsCompleted(int index)
    {
        if (index >= 0 && index < tasks.Count)
        {
            tasks[index].MarkAsCompleted();
        }
        else
        {
            Console.WriteLine("Invalid task index.");
        }
    }


    public void DisplayAllTasks()
    {
        Console.WriteLine("All Tasks in Task Manager:");
        for (int i = 0; i < tasks.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            tasks[i].DisplayTaskDetails();
        }
    }


    public void SaveTasksToFile(string fileName)
    {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
        {
            foreach (var task in tasks)
            {
                file.WriteLine($"{task.GetType().Name},{task.Title},{task.Description},{task.Priority},{task.IsCompleted}");
            }
        }

        Console.WriteLine("Tasks saved to file successfully.");
    }


    public void LoadTasksFromFile(string fileName)
    {
        tasks.Clear();

        try
        {
            string[] lines = System.IO.File.ReadAllLines(fileName);
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 5)
                {
                    string taskType = parts[0];
                    string title = parts[1];
                    string description = parts[2];
                    Enum.TryParse(parts[3], out Priority priority);
                    bool isCompleted = bool.Parse(parts[4]);

                    Task newTask;
                    if (taskType.ToLower() == "worktask")
                    {
                        string projectName = description;
                        newTask = new WorkTask(title, description, priority, projectName);
                    }
                    else if (taskType.ToLower() == "personaltask")
                    {
                        newTask = new PersonalTask(title, description, priority);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid task type '{taskType}'. Skipping task.");
                        continue;
                    }

                    newTask.IsCompleted = isCompleted;
                    tasks.Add(newTask);
                }
                else
                {
                    Console.WriteLine("Invalid task format. Skipping task.");
                }
            }

            Console.WriteLine("Tasks loaded from file successfully.");
        }
        catch (System.IO.FileNotFoundException)
        {
            Console.WriteLine($"File '{fileName}' not found. No tasks loaded.");
        }
    }
}