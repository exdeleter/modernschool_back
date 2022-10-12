using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using modernschool_back;
using modernschool_back.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<SchoolDbContext>(opt => opt.UseNpgsql(@"Server=localhost;Port=5432;Database=dbname;User Id=Id;Password=password"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IStudent, EFStudentRepository>();
//builder.Services.AddDbContext<SchoolDbContext>(options => options.UseNpgsql("Server=localhost;Port=5432;Database=myDataBase;User Id=myUsername;Password=myPassword;"));

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