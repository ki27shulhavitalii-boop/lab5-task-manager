
using System.Text.Json;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Infrastructure.Repositories;

public class JsonTaskRepository : ITaskRepository
{
    private readonly string _filePath;
    private List<TaskItem> _tasks;

    public JsonTaskRepository(string filePath)
    {
        _filePath = filePath;
        _tasks = Load();
    }

    private List<TaskItem> Load()
    {
        if (!File.Exists(_filePath))
            return new List<TaskItem>();

        string json = File.ReadAllText(_filePath);

        return JsonSerializer.Deserialize<List<TaskItem>>(json)
               ?? new List<TaskItem>();
    }

    private void Save()
    {
        string json = JsonSerializer.Serialize(
            _tasks,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText(_filePath, json);
    }

    public void Add(TaskItem task)
    {
        _tasks.Add(task);
        Save();
    }

    public List<TaskItem> GetAll()
    {
        return _tasks;
    }

    public TaskItem? GetById(int id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public void Update(TaskItem task)
    {
        var index = _tasks.FindIndex(t => t.Id == task.Id);

        if (index != -1)
        {
            _tasks[index] = task;
            Save();
        }
    }

    public void Delete(int id)
    {
        var task = GetById(id);

        if (task != null)
        {
            _tasks.Remove(task);
            Save();
        }
    }
}
