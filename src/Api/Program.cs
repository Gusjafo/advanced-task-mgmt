using Infrastructure.Persistence;
using Application.Common;
using Microsoft.EntityFrameworkCore;
using Application.Tasks;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());
 
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateTaskCommand>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Endpoint CreateTask  
app.MapPost("/tasks", async (ISender sender, CreateTaskCommand cmd) =>
{
    var id = await sender.Send(cmd);
    return Results.Created($"/tasks/{id}", new { id });
});

app.Run();
