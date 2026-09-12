using ResumoCash.Application.Categories.Create;
using ResumoCash.Application.Categories.GetAll;
using ResumoCash.Application.Categories.GetById;
using ResumoCash.Domain.Repositories;
using ResumoCash.Infrastructure;
using ResumoCash.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// Dependências
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CreateCategoryService>();
builder.Services.AddScoped<GetCategoryByIdService>();
builder.Services.AddScoped<GetCategoriesService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Mapeia as rotas dos Controllers
app.MapControllers();

app.Run();