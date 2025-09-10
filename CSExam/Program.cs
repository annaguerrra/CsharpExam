using CSExam.Entites;
using CSExam.Features.CreateTrip;
using CSExam.Features.EditTrip;
using CSExam.Features.Login;
using CSExam.Features.ShowTrip;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CSExamDbContext>(options => {
    var sqlConn = Environment.GetEnvironmentVariable("SQL_CONNECTION");
    options.UseSqlServer(sqlConn);
});



builder.Services.AddScoped<CreateTripUseCase>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<EditTripUseCase>();
builder.Services.AddScoped<ShowTripUseCase>();

var app = builder.Build();

app.Run();
