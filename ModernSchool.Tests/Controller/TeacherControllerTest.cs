using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modernschool_back.Contexts;
using modernschool_back.Controllers;
using modernschool_back.Models;

namespace ModernSchool.Tests.Controller
{
    public class TeacherControllerTest
    {
        private async Task<SchoolDBContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<SchoolDBContext>().
                UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var databaseContext = new SchoolDBContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Teachers.CountAsync() <= 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    databaseContext.Teachers.Add(new Teacher()
                    {
                        Name = "A",
                        Age = 25,
                        Patronymic = "male",
                        Surname = "Petrovich",
                        Subject = new Subject()
                        {
                            Name = "BBB"
                        }
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }


        [Fact]
        private async void TeacherController_Get_ReturnsClasses()
        {
            // Arrange

            var id = 1;
            var dbContext = await GetDatabaseContext();
            var teacherRep = new TeacherController(dbContext);

            // Act

            var result = teacherRep.Get(id);

            // Assert

            result.Should().NotBeNull();
            result.Should().BeOfType<Task<ActionResult<Teacher>>>();
        }
    }
}
