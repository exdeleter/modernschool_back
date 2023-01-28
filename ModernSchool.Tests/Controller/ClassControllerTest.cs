using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernSchool.Worker.Contexts;
using ModernSchool.Worker.Controllers;
using ModernSchool.Worker.Models;

namespace ModernSchool.Tests.Controller
{
    public class ClassControllerTest
    {
        
        private async Task<SchoolDBContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<SchoolDBContext>().
                UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var databaseContext = new SchoolDBContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Class.CountAsync() <= 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    databaseContext.Class.Add(new Class()
                    {
                        Name = "A",
                        Number = 10,
                        Teacher = new Teacher()
                        {
                            Name = "Petia",
                            Surname = "Ivanov",
                            Patronymic = "female",
                            Age = 28,
                            //Subject = new Subject()
                            //{
                            //    Name = "Matan"
                           // }
                        }
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }


        [Fact]
        private async void ClassController_Get_ReturnsClasses()
        {
            // Arrange

            var id = 1;
            var dbContext = await GetDatabaseContext();
            var classRep = new ClassController(dbContext);

            // Act

            var result = classRep.Get(id);

            // Assert

            result.Should().NotBeNull();
            result.Should().BeOfType<Task<ActionResult<Class>>>();
        }
        
    }
}
