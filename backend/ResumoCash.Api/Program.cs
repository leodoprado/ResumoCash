using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using ResumoCash.Application.Categories.Create;
using ResumoCash.Application.Categories.Delete;
using ResumoCash.Application.Categories.GetAll;
using ResumoCash.Application.Categories.GetById;
using ResumoCash.Application.Categories.Update;
using ResumoCash.Application.Competences.Close;
using ResumoCash.Application.Competences.Create;
using ResumoCash.Application.Competences.GetAll;
using ResumoCash.Application.Competences.Reopen;
using ResumoCash.Application.Transactions.Completed;
using ResumoCash.Application.Transactions.Create;
using ResumoCash.Application.Transactions.Delete;
using ResumoCash.Application.Transactions.GetAll;
using ResumoCash.Application.Transactions.GetById;
using ResumoCash.Application.Transactions.Reopen;
using ResumoCash.Application.Transactions.Update;
using ResumoCash.Application.Users.Create;
using ResumoCash.Application.Users.GetCurrent;
using ResumoCash.Application.Users.Login;
using ResumoCash.Domain.Repositories;
using ResumoCash.Domain.Security;
using ResumoCash.Infrastructure;
using ResumoCash.Infrastructure.Repositories;
using ResumoCash.Infrastructure.Security;

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
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Informe o token JWT para autenticação."
    });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });
});

// Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// Dependências
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICompetenceRepository, CompetenceRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();

// Users
builder.Services.AddScoped<CreateUserService>();
builder.Services.AddScoped<LoginUserService>();
builder.Services.AddScoped<GetCurrentUserService>();

// Categories
builder.Services.AddScoped<CreateCategoryService>();
builder.Services.AddScoped<GetCategoryByIdService>();
builder.Services.AddScoped<GetCategoriesService>();
builder.Services.AddScoped<UpdateCategoryService>();
builder.Services.AddScoped<DeleteCategoryService>();

// Transactions
builder.Services.AddScoped<CreateTransactionService>();
builder.Services.AddScoped<GetTransactionsService>();
builder.Services.AddScoped<GetTransactionByIdService>();
builder.Services.AddScoped<UpdateTransactionService>();
builder.Services.AddScoped<DeleteTransactionService>();
builder.Services.AddScoped<CompletedTransactionService>();
builder.Services.AddScoped<ReopenTransactionService>();

// Competences
builder.Services.AddScoped<CreateCompetenceService>();
builder.Services.AddScoped<CloseCompetenceService>();
builder.Services.AddScoped<ReopenCompetenceService>();
builder.Services.AddScoped<GetCompetenceService>();

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException(
        "A chave JWT não foi configurada.");

var jwtIssuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException(
        "O emissor JWT não foi configurado.");

var jwtAudience = builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException(
        "A audiência JWT não foi configurada.");

if (Encoding.UTF8.GetByteCount(jwtKey) < 32)
    throw new InvalidOperationException(
        "A chave JWT deve ter pelo menos 32 bytes.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,

            ValidateAudience = true,
            ValidAudience = jwtAudience,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)),

            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("frontend");

// Mapeia as rotas dos Controllers
app.MapControllers();

app.Run();
