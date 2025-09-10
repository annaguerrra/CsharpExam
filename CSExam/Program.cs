using CSExam.Entites;
using CSExam.Features.CreateTrip;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CSExamDbContext>(options => {
    var sqlConn = Environment.GetEnvironmentVariable("SQL_CONNECTION");
    options.UseSqlServer(sqlConn);
});



builder.Services.AddScoped<CreateTripUseCase>();


var app = builder.Build();

app.Run();
