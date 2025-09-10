using CSExam.Entites;
using CSExam.Features.CreateTrip;
using CSExam.Features.Login;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CSExamDbContext>(options => {
    var sqlConn = Environment.GetEnvironmentVariable("SQL_CONNECTION");
    options.UseSqlServer(sqlConn);
});



builder.Services.AddScoped<CreateTripUseCase>();
builder.Services.AddScoped<LoginUseCase>();

var app = builder.Build();

app.Run();
