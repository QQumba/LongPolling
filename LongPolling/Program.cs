using LongPolling;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PollingHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapPost("/", (Message message, [FromServices] PollingHandler handler) =>
{
    handler.Notify(message.Text);
});

app.MapGet("/poll", async (CancellationToken ct, [FromServices] PollingHandler handler) =>
{
    while (!handler.Notified)
    {
        if (ct.IsCancellationRequested)
        {
            var error = "Connection was terminated";
            Console.WriteLine(error);
            return Results.BadRequest(error);
        }
        await Task.Delay(1000);
    }

    return Results.Ok(handler.Consume());
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

record Message(string Text);