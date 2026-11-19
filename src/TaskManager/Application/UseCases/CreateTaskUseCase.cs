
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.UseCases;

public class CreateTaskUseCase
{
    private readonly ITaskRepository _repository;

    public CreateTaskUseCase(ITaskRepository repository)
    {
        _repository = repository;
    }

    public void Execute(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Назва завдання обов'язкова.");

        var task = new TaskItem(
            Random.Shared.Next(1, int.MaxValue),
            title
        );

        _repository.Add(task);
    }
}
