
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Interfaces;

public interface ITaskRepository
{
    void Add(TaskItem task);
    List<TaskItem> GetAll();
    TaskItem? GetById(int id);
    void Update(TaskItem task);
    void Delete(int id);
}
