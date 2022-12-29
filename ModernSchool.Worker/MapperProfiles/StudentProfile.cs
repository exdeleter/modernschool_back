using AutoMapper;
using ModernSchool.Worker.Dtos;
using ModernSchool.Worker.Models;

namespace ModernSchool.Worker.MapperProfiles;

public class StudentProfile : Profile
{
    public StudentProfile()
    {			
        CreateMap<Student, StudentDto>()
            .ForMember(x => x.TeacherFIO, 
                d => d
                    .MapFrom(s => string.Join(' ',
                            s.Class.Teacher.Surname,
                            s.Class.Teacher.Name,
                            s.Class.Teacher.Patronymic
                            )
                    )
            );
    }
}