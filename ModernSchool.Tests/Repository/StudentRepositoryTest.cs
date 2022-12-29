using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ModernSchool.Worker;
using ModernSchool.Worker.Contexts;
using ModernSchool.Worker.Controllers;
using ModernSchool.Worker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernSchool.Tests.Repository
{
    public class StudentRepositoryTest
    {
        private async Task<SchoolDBContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<SchoolDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new SchoolDBContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Subjects.CountAsync() <= 0)
            {
                /*
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Subjects.Add(
                        new Subject()
                        {
                            Name = $"C#{i}"
                        });
                    await databaseContext.SaveChangesAsync();
                }*/

                
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Students.Add(
                        new Student()
                        {
                            Name = $"Art",
                            Surname = $"Gin",
                            Patronymic = $"Cool",
                            Age = 18,
                            Class = new Class()
                            {
                                Name = "A",
                                Number = 1,
                                Teacher = new Teacher()
                                {
                                    Name = "Petia",
                                    Surname = "AAA",
                                    Patronymic = "female",
                                    Age = 28,
                                    Subject = new Subject()
                                    {
                                        Name = "C#"
                                    }
                                }
                            },
                            Phone = "8921331133",
                            Subjects = new List<Subject>()
                            {
                                new Subject()
                                {
                                    Name = "Python"
                                }
                            }
                        });
                    await databaseContext.SaveChangesAsync();
                }
                
            }

            return databaseContext;
        }

        [Fact]
        public async void StudentRepository_Get_ReturnsStudents()
        {
            // Arrange
            var id = 1;
            var dbContext = await GetDatabaseContext();

            var studentRepository = new EFStudentRepository(dbContext);
            //var subjectsRep = new SubjectController(dbContext);



            // Act

            var result = studentRepository.Get(id);
            //var result = subjectsRep.Get(id);



            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Student>();
            //result.Should().BeOfType<Subject>();
        }

        

    }
}
