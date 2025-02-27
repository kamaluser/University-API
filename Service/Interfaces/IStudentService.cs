using Service.Dtos;
using Service.Dtos.StudentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityAppAgain.Dtos.StudentDtos;

namespace Service.Interfaces
{
    public interface IStudentService
    {
        int Create(StudentCreateDto createDto);
        PaginatedList<StudentGetDto> GetAllByPage(string? search = null, int page = 1, int size = 10);
        void Edit(int id, StudentUpdateDto editDto);
        void Delete(int id);
        StudentGetDto GetById(int id);
        List<StudentGetDto> GetAll(string? search = null);
    }
}
