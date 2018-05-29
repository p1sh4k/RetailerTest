using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using RetailerApi.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RetailerApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class GroupsController : BaseController
    {
        private readonly IGroupRepository _groupRepository;

        public GroupsController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpGet]
        public Task<IEnumerable<GroupModel>> Get()
        {
            return GetGroupInternal();
        }

        private async Task<IEnumerable<GroupModel>> GetGroupInternal()
        {
            return await _groupRepository.GetAllGroups();
        }

        // GET api/Groups/id
        [HttpGet("{id}")]
        public Task<GroupModel> Get(string id)
        {
            return GetGroupByIdInternal(id);
        }

        private async Task<GroupModel> GetGroupByIdInternal(string id)
        {
            return await _groupRepository.GetGroup(id) ?? new GroupModel();
        }

        // POST api/Groups
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromForm] GroupViewModel model)
        {
            try
            {
                await _groupRepository.AddGroup(new GroupModel()
                {
                    Name = model.Name,
                    CreateDate = DateTime.Now,
                    ModificationDate = DateTime.Now
                });

                return Ok(model.Name + " Group successfully created");
            }
            catch (Exception ex)
            {
                return Error((int)HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/Groups/id (and name in form)
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] GroupViewModel model)
        {
            try
            {
                await _groupRepository.UpdateGroup(model.GroupId, model.Name);

                return Ok();
            }
            catch (Exception ex)
            {
                return Error((int)HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE api/Groups/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup([FromBody] string groupid)
        {
            try
            {
                await _groupRepository.RemoveGroup(groupid);

                return Ok();
            }
            catch (Exception ex)
            {
                return Error((int)HttpStatusCode.BadRequest, ex);
            }
        }
    }
}