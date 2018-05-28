using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RetailerApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class InitController : Controller
    {
        private readonly IRetailRepository _retailRepository;
        private readonly IGroupRepository _groupRepository;

        public InitController(IRetailRepository retailRepository,
                              IGroupRepository  groupRepository)
        {
            _retailRepository = retailRepository;
            _groupRepository = groupRepository;
        }

        // Call an initialization - api/init/createtest
        [HttpGet("{setting}")]
        public string Get(string setting)
        {
            if (setting != "createtest") return "Unknown";
            _retailRepository.AddRetail(new RetailModel()
            {
                Name = "Test retail 1",
                CreateDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                GroupId = "1"
            });
            _retailRepository.AddRetail(new RetailModel()
            {
                Name = "Test retail 2",
                CreateDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                GroupId = "1"
            });
            _retailRepository.AddRetail(new RetailModel()
            {
                Name = "Test retail 3",
                CreateDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                GroupId = "2"
            });
            _retailRepository.AddRetail(new RetailModel()
            {
                Name = "Test retail 4",
                CreateDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                GroupId = "3"
            });

            _retailRepository.AddRetail(new RetailModel()
            {
                Name = "Test retail 5",
                CreateDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                GroupId = "3"
            });
            _groupRepository.AddGroup(new GroupModel()
            {
                Name = "Test group 1",
                CreateDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                
            });
            _groupRepository.AddGroup(new GroupModel()
            {
                Name = "Test group 2",
                CreateDate = DateTime.Now,
                ModificationDate = DateTime.Now,
               
            });

            _groupRepository.AddGroup(new GroupModel()
            {
                Name = "Test group 3",
                CreateDate = DateTime.Now,
                ModificationDate = DateTime.Now,
            });
            return "Done";

        }
    }
}