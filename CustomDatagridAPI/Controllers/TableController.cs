using CustomDatagridAPI.Models;
using CustomDatagridAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CustomDatagridAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TableController : ControllerBase
    {
        private readonly PeopleRepository peopleRepository = new();

        [HttpGet]
        [Route("People")]
        public List<Person> GetPeople(int pageNumber, int itemsPerPage, string? search = "", string sort = "Id", string direction = "ASC")
        {
            return peopleRepository.GetPeople(pageNumber, itemsPerPage, search, sort, direction);
        }

        [HttpGet]
        [Route("TotalPages")]
        public int GetTotalPages(int itemsPerPage, string? search)
        {
            return peopleRepository.GetTotalPages(itemsPerPage, search);
        }
    }
}