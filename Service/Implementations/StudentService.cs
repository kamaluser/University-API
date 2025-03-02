using AutoMapper;
using Core.Entities;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Service.Dtos;
using Service.Dtos.GroupDtos;
using Service.Dtos.StudentDtos;
using Service.Exceptions;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityAppAgain.Dtos.StudentDtos;

namespace Service.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public int Create(StudentCreateDto createDto)
        {
            if(_repository.Exists(x=>x.Email == createDto.Email))
            {
                throw new RestException(StatusCodes.Status400BadRequest, "Email", "Email is already exists.");
            }

            Student student = _mapper.Map<Student>(createDto);

            _repository.Add(student);
            _repository.Save();

            return student.Id;

        }

        public void Delete(int id)
        {
            Student student = _repository.Get(x => x.Id == id && !x.IsDeleted);
            if(student == null)
            {
                throw new RestException(StatusCodes.Status400BadRequest, "Student not found by given Id.");
            }

            student.IsDeleted = true;
            _repository.Delete(student);
            _repository.Save();
        }

        public void Edit(int id, StudentUpdateDto editDto)
        {
            Student student = _repository.Get(x => x.Id == id && !x.IsDeleted);
            if (student == null)
            {
                throw new RestException(StatusCodes.Status404NotFound, "Student not found by given Id!");
            }

            _mapper.Map(editDto, student);

            _repository.Save();
        }

        public List<StudentGetDto> GetAll(string? search = null)
        {
            IQueryable<Student> query = _repository.GetAll(x => !x.IsDeleted);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.FullName.Contains(search));
            }

            List<Student> students = query.ToList();
            return _mapper.Map<List<StudentGetDto>>(students);
        }

        public PaginatedList<StudentGetDto> GetAllByPage(string? search = null, int page = 1, int size = 10)
        {
            var query = _repository.GetAll(x => search == null || x.FullName.Contains(search), "Group");
            var paginated = PaginatedList<Student>.Create(query, page, size);
            return new PaginatedList<StudentGetDto>(_mapper.Map<List<StudentGetDto>>(paginated.Items), paginated.TotalPages, page, size);
        }

        public StudentGetDto GetById(int id)
        {

            Student student = _repository.Get(x => x.Id == id && !x.IsDeleted);
            if (student == null)
            {
                throw new RestException(StatusCodes.Status404NotFound, "Student not found by given Id!");
            }

            var result = _mapper.Map<StudentGetDto>(student);

            return result;
        }
    }
}
