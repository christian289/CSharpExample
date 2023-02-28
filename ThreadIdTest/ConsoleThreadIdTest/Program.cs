Console.WriteLine($"Thread Id : {Environment.CurrentManagedThreadId}");

await Task.Run(() =>
{
    Console.WriteLine($"Thread Id : {Environment.CurrentManagedThreadId}");
});

Console.WriteLine($"Thread Id : {Environment.CurrentManagedThreadId}");