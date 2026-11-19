
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.UseCases;

public class CompleteTaskUseCase
{
    private readonly ITaskRepository _repository;

    public CompleteTaskUseCase(ITaskRepository repository)
    {
        _repository = repository;
    }

    public void Execute(int id)
    {
        var task = _repository.GetById(id);

        if (task == null)
            throw new Exception("Завдання не знайдено.");

        task.IsCompleted = true;
        _repository.Update(task);
    }
}
