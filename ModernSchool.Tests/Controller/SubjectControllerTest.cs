using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modernschool_back.Contexts;
using modernschool_back.Controllers;
using modernschool_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernSchool.Tests.Controller
{
    public class SubjectControllerTest
    {
        private async Task<SchoolDBContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<SchoolDBContext>().
                UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var databaseContext = new SchoolDBContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Subjects.CountAsync() <= 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    databaseContext.Subjects.Add(new Subject()
                    {
                        Name = "DDD"
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
            var subjectRep = new SubjectController(dbContext);

            // Act

            var result = subjectRep.Get(id);

            // Assert

            result.Should().NotBeNull();
            result.Should().BeOfType<Task<ActionResult<Subject>>>();
        }
    }
}
