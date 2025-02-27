using Service.Dtos.StudentDtos;
using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityAppAgain.Dtos.StudentDtos;
using Service.Dtos.GroupDtos;

namespace Service.Interfaces
{
    public interface IGroupService
    {
        int Create(GroupCreateDto createDto);
        PaginatedList<GroupGetDto> GetAllByPage(string? search = null, int page = 1, int size = 10);
        void Edit(int id, GroupUpdateDto editDto);
        void Delete(int id);
        GroupGetDto GetById(int id);
        List<GroupGetDto> GetAll(string? search = null);
    }
}
