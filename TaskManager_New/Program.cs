using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TaskManager_New.Data;
using TaskManager_New.Services;
using TaskManager_New.Services.Auth;
using TaskManager_New.Services.Task;
using TaskManager_New.Services.Users;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()                      
                  .AllowAnyHeader();                     
        });
});


var key = Encoding.UTF8.GetBytes("123456789_task_manager_123456789");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "TaskManager",
            ValidAudience = "TaskManagerUsers",
            IssuerSigningKey = new SymmetricSecurityKey(key)

        };
    });
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Введите токен: Bearer [ваш_токен]"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddAuthorization();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITaskServices, TaskServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseCors("AllowAngular");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


