
using TaskManager.Application.UseCases;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Presentation.Cli;

public class TaskManagerCli
{
    private readonly ITaskRepository _repository;
    private readonly CreateTaskUseCase _createTask;
    private readonly CompleteTaskUseCase _completeTask;

    public TaskManagerCli(
        ITaskRepository repository,
        CreateTaskUseCase createTask,
        CompleteTaskUseCase completeTask)
    {
        _repository = repository;
        _createTask = createTask;
        _completeTask = completeTask;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n===== TASK MANAGER =====");
            Console.WriteLine("1. Переглянути завдання");
            Console.WriteLine("2. Створити завдання");
            Console.WriteLine("3. Завершити завдання");
            Console.WriteLine("0. Вихід");
            Console.Write("Ваш вибір: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowTasks();
                    break;

                case "2":
                    CreateTask();
                    break;

                case "3":
                    CompleteTask();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Невірний вибір!");
                    break;
            }
        }
    }

    private void ShowTasks()
    {
        var tasks = _repository.GetAll();

        Console.WriteLine("\n===== СПИСОК ЗАВДАНЬ =====");

        if (tasks.Count == 0)
        {
            Console.WriteLine("Завдань поки немає.");
            return;
        }

        foreach (var task in tasks)
        {
            string status = task.IsCompleted ? "Виконано" : "Не виконано";

            Console.WriteLine(
                $"ID: {task.Id} | {task.Title} | {status}");
        }
    }

    private void CreateTask()
    {
        Console.Write("Введіть назву завдання: ");
        string? title = Console.ReadLine();

        try
        {
            _createTask.Execute(title ?? "");
            Console.WriteLine("Завдання успішно створено!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    private void CompleteTask()
    {
        Console.Write("Введіть ID завдання: ");
        string? input = Console.ReadLine();

        if (!int.TryParse(input, out int id))
        {
            Console.WriteLine("Неправильний ID!");
            return;
        }

        try
        {
            _completeTask.Execute(id);
            Console.WriteLine("Завдання виконано!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}
