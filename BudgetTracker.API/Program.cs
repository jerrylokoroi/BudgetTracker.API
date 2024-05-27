using BudgetTrackerApp.Data;
using BudgetTrackerApp.Data.Repository;
using BudgetTrackerApp.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with SQL Server
builder.Services.AddDbContext<BudgetTrackerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the repository
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<DataSeeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dataSeeder = services.GetRequiredService<DataSeeder>();
    dataSeeder.Seed();
}

// Define the API endpoints
app.MapGet("/api/transactions", (ITransactionRepository repository) =>
{
    return Results.Ok(repository.GetAll());
})
.WithName("GetAllTransactions");

app.MapGet("/api/transactions/{id}", (int id, ITransactionRepository repository) =>
{
    var transaction = repository.GetById(id);
    return transaction is not null ? Results.Ok(transaction) : Results.NotFound();
})
.WithName("GetTransactionById");

app.MapPost("/api/transactions", (BudgetTransaction transaction, ITransactionRepository repository) =>
{
    repository.Add(transaction);
    return Results.Created($"/api/transactions/{transaction.Id}", transaction);
})
.WithName("CreateTransaction");

app.MapPut("/api/transactions/{id}", (int id, BudgetTransaction transaction, ITransactionRepository repository) =>
{
    var existingTransaction = repository.GetById(id);
    if (existingTransaction is null)
    {
        return Results.NotFound();
    }

    existingTransaction.Amount = transaction.Amount;
    existingTransaction.Description = transaction.Description;
    existingTransaction.Date = transaction.Date;
    existingTransaction.Category = transaction.Category;

    repository.Update(existingTransaction);
    return Results.NoContent();
})
.WithName("UpdateTransaction");

app.MapDelete("/api/transactions/{id}", (int id, ITransactionRepository repository) =>
{
    var existingTransaction = repository.GetById(id);
    if (existingTransaction is null)
    {
        return Results.NotFound();
    }

    repository.Delete(id);
    return Results.NoContent();
})
.WithName("DeleteTransaction");

app.Run();
