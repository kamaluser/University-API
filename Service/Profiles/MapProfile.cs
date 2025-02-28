using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Service.Dtos.GroupDtos;
using Service.Dtos.StudentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityAppAgain.Dtos.StudentDtos;

namespace Service.Profiles
{
    public class MapProfile:Profile
    {

        public MapProfile()
        {
            CreateMap<Group, GroupGetDto>();
            CreateMap<GroupCreateDto, Group>();
            CreateMap<GroupUpdateDto, Group>();

            CreateMap<StudentCreateDto, Student>();
            CreateMap<StudentUpdateDto, Student>();
            CreateMap<Student, StudentGetDto>();
        }


    }
}
