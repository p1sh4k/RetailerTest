using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmitEvent;

namespace RetailerApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RetailsController : Controller
    {
        private readonly IRetailRepository _retailRepository;
        private readonly IEventLog _eventLog;

        public RetailsController(IRetailRepository retailRepository,
                                                 IEventLog eventLog)
        {
            _retailRepository = retailRepository;
            _eventLog = eventLog;
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
        public void Post([FromForm]string name)
        {
            _retailRepository.AddRetail(new RetailModel() { Name = name, CreateDate = DateTime.Now, ModificationDate = DateTime.Now });
            _eventLog.EventPublish("Create: ", name + " Created");
        }

        // PUT api/Retails/id (and name in form)
        [HttpPut("{id}")]
        public void Put(string id, [FromForm]string name)
        {
            _retailRepository.UpdateRetail(id, name);
            _eventLog.EventPublish("Edit: ",id+ " Updated Name " +name);
        }

        // DELETE api/Retails/id
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _retailRepository.RemoveRetail(id);
            _eventLog.EventPublish("Delete: ", id + " removed");
        }
    }
}