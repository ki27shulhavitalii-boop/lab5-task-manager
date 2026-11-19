
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Application.UseCases;
using TaskManager.Presentation.Cli;

var services = new ServiceCollection();

string filePath = Path.Combine(
    Directory.GetCurrentDirectory(),
    "tasks.json");

services.AddSingleton<ITaskRepository>(
    _ => new JsonTaskRepository(filePath));

services.AddTransient<CreateTaskUseCase>();
services.AddTransient<CompleteTaskUseCase>();

services.AddTransient<TaskManagerCli>();

var serviceProvider = services.BuildServiceProvider();

var app = serviceProvider.GetRequiredService<TaskManagerCli>();

app.Run();
