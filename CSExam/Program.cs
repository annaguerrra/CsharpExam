using System.Text;
using CSExam.Endpoints;
using CSExam.Entites;
using CSExam.Features.CreateTrip;
using CSExam.Features.EditTrip;
using CSExam.Features.Login;
using CSExam.Features.ShowTrip;
using CSExam.Services.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CSExamDbContext>(options => {
    var sqlConn = Environment.GetEnvironmentVariable("SQL_CONNECTION");
    options.UseSqlServer(sqlConn);
});

builder.Services.AddTransient<IJWTService, EFJWTService>();

builder.Services.AddScoped<CreateTripUseCase>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<EditTripUseCase>();
builder.Services.AddScoped<ShowTripUseCase>();

var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
var keyBytes = Encoding.UTF8.GetBytes(jwtSecret);
var key = new SymmetricSecurityKey(keyBytes);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidIssuer = "Turism",
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = key,
        };
    });

var app = builder.Build();

app.ConfigureTripEndpoints();
app.ConfigureUserEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
