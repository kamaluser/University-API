using AutoMapper;
using Core.Entities;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Service.Dtos;
using Service.Dtos.GroupDtos;
using Service.Exceptions;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class GroupService:IGroupService
    {
        private readonly IGroupRepository _repository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public int Create(GroupCreateDto dto)
        {
            
            if(_repository.Exists(x=>x.No == dto.No && !x.IsDeleted))
            {
                throw new RestException(StatusCodes.Status400BadRequest, "No", "No already taken.");
            }
            Group group = _mapper.Map<Group>(dto);

            _repository.Add(group);
            _repository.Save();

            return group.Id;
        }

        public void Delete(int id)
        {
            Group group = _repository.Get(x => x.Id == id && !x.IsDeleted);
            if (group == null)
            {
                throw new RestException(StatusCodes.Status404NotFound, "Group not found by given Id!");
            }

            group.IsDeleted = true;
            _repository.Delete(group);
            _repository.Save();
        }

        public void Edit(int id, GroupUpdateDto editDto)
        {
            Group group = _repository.Get(x => x.Id == id && !x.IsDeleted);
            if (group == null)
            {
                throw new RestException(StatusCodes.Status404NotFound, "Group not found by given Id!");
            }

            _mapper.Map(editDto,group);

            _repository.Save();
        }

        public List<GroupGetDto> GetAll(string? search = null)
        {
            IQueryable<Group> query = _repository.GetAll(x => !x.IsDeleted);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.No.Contains(search));
            }

            List<Group> groups = query.ToList();
            return _mapper.Map<List<GroupGetDto>>(groups);
        }

        public PaginatedList<GroupGetDto> GetAllByPage(string? search = null, int page = 1, int size = 10)
        {
            var query = _repository.GetAll(x => search == null || x.No.Contains(search), "Students");
            var paginated = PaginatedList<Group>.Create(query, page, size);
            return new PaginatedList<GroupGetDto>(_mapper.Map<List<GroupGetDto>>(paginated.Items), paginated.TotalPages, page, size);
        }

        public GroupGetDto GetById(int id)
        {

            Group group = _repository.Get(x => x.Id == id && !x.IsDeleted);
            if (group == null)
            {
                throw new RestException(StatusCodes.Status404NotFound, "Group not found by given Id!");
            }

            var result = _mapper.Map<GroupGetDto>(group);

            return result;
        }
    }
}
