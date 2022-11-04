using Microsoft.EntityFrameworkCore;
using modernschool_back;
using modernschool_back.Contexts;
using modernschool_back.Interfaces;
using modernschool_back.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//string con = "Server=(localdb)\\mssqllocaldb;Database=usersdbstore;Trusted_Connection=True;";
//builder.Services.AddDbContext<StudentContext>(options => options.UseSqlServer(con));

//builder.Services.AddDbContext<SchoolDBContext>(opt => opt.UseNpgsql(@"Server=localhost;Port=5432;Database=dbname;User Id=postgres;Password=power11oikjmn91"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IStudent, EFStudentRepository>();
builder.Services.AddDbContext<SchoolDBContext>(options => options.UseNpgsql(@"Host=localhost;Port=5432;Database=modern;Username=postgres;Password=power11oikjmn91"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();