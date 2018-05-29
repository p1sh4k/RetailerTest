using Domain.Entities;
using Domain.Interface;
using EmitEvent;
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
    public class RetailsController : BaseController
    {
        private readonly IRetailRepository _retailRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IEventLog _eventLog;

        public RetailsController(
            IRetailRepository retailRepository,
            IGroupRepository groupRepository,
            IEventLog eventLog)
        {
            _retailRepository = retailRepository;
            _eventLog = eventLog;
            _groupRepository = groupRepository;
        }

        [HttpGet]
        public Task<IEnumerable<RetailModel>> Get()
        {
            return GetRetailInternal();
        }

        private async Task<IEnumerable<RetailModel>> GetRetailInternal()
        {
            return await _retailRepository.GetAllRetails();
        }

        // GET api/Retails/id
        [HttpGet("{id}")]
        public Task<RetailModel> Get(string id)
        {
            return GetRetailByIdInternal(id);
        }

        private async Task<RetailModel> GetRetailByIdInternal(string id)
        {
            return await _retailRepository.GetRetail(id) ?? new RetailModel();
        }

        // POST api/Retails
        [HttpPost]
        public async Task<IActionResult> CreateRetail([FromForm] RetailViewModel model)
        {
            try
            {
                if (_groupRepository.GetGroup(model.GroupId) == null) return Error((int)HttpStatusCode.BadRequest);
                await _retailRepository.AddRetail(new RetailModel()
                {
                    Name = model.Name,
                    GroupId = model.GroupId,
                    CreateDate = DateTime.Now,
                    ModificationDate = DateTime.Now
                });
                _eventLog.EventPublish("Create: ", $"{model.RetailId} : {model.Name} at {DateTime.Now}");

                return Ok(model.Name + " Successfully created");
            }
            catch (Exception ex)
            {
                return Error((int)HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/Retails/id (and name in form)
        [HttpPut("{id}")]
        public async Task<IActionResult> EditRetail(string id, [FromForm]RetailViewModel model)
        {
            try
            {
                if (_groupRepository.GetGroup(model.GroupId) == null) return Error((int)HttpStatusCode.BadRequest);
                await _retailRepository.UpdateRetail(model.RetailId, model.Name, model.GroupId);

                _eventLog.EventPublish("Edited : ", $"{model.RetailId} : {model.Name} at {DateTime.Now}");

                return Ok();
            }
            catch (Exception ex)
            {
                return Error((int)HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE api/Retails/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _retailRepository.RemoveRetail(id);
                _eventLog.EventPublish("Delete: ", id + " removed");
                return Ok();
            }
            catch (Exception ex)
            {
                return Error((int)HttpStatusCode.BadRequest, ex);
            }
        }
    }
}