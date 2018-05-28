using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrouperApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class GroupsController : Controller
    {
        private readonly IGroupRepository _GroupRepository;

        public GroupsController(IGroupRepository GroupRepository)
        {
            _GroupRepository = GroupRepository;
        }

        [HttpGet]
        public Task<IEnumerable<GroupModel>> Get()
        {
            return GetGroupInternal();
        }

        private async Task<IEnumerable<GroupModel>> GetGroupInternal()
        {
            return await _GroupRepository.GetAllGroups();
        }

        // GET api/Groups/id
        [HttpGet("{id}")]
        public Task<GroupModel> Get(string id)
        {
            return GetGroupByIdInternal(id);
        }

        private async Task<GroupModel> GetGroupByIdInternal(string id)
        {
            return await _GroupRepository.GetGroup(id) ?? new GroupModel();
        }

        // POST api/Groups
        [HttpPost]
        public void Post([FromBody]string value)
        {
            _GroupRepository.AddGroup(new GroupModel() { Name = value, CreateDate = DateTime.Now, ModificationDate = DateTime.Now });
        }

        // PUT api/Groups/id (and name in form)
        [HttpPut("{id}")]
        public void Put(string id, [FromForm]string name)
        {
            _GroupRepository.UpdateGroup(id, name);
        }

        // DELETE api/Groups/id
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _GroupRepository.RemoveGroup(id);
        }
    }
}