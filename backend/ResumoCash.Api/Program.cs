using ResumoCash.Application.Categories.Create;
using ResumoCash.Application.Categories.Delete;
using ResumoCash.Application.Categories.GetAll;
using ResumoCash.Application.Categories.GetById;
using ResumoCash.Application.Categories.Update;
using ResumoCash.Application.Transactions.Create;
using ResumoCash.Application.Transactions.Delete;
using ResumoCash.Application.Transactions.GetAll;
using ResumoCash.Application.Transactions.Update;
using ResumoCash.Domain.Repositories;
using ResumoCash.Infrastructure;
using ResumoCash.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(
        new System.Text.Json.Serialization.JsonStringEnumConverter());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// Dependências
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

// Categories
builder.Services.AddScoped<CreateCategoryService>();
builder.Services.AddScoped<GetCategoryByIdService>();
builder.Services.AddScoped<GetCategoriesService>();
builder.Services.AddScoped<UpdateCategoryService>();
builder.Services.AddScoped<DeleteCategoryService>();

// Transactions
builder.Services.AddScoped<CreateTransactionService>();
builder.Services.AddScoped<GetTransactionsService>();
builder.Services.AddScoped<UpdateTransactionService>();
builder.Services.AddScoped<DeleteTransactionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("frontend");

// Mapeia as rotas dos Controllers
app.MapControllers();

app.Run();