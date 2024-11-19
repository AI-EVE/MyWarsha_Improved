using LinqKit;
using Microsoft.AspNetCore.Mvc;
using MyWarsha_DTOs.ClientDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.ClientFilters;
using Utils.PageUtils;

namespace MyWarsha_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICarRepository _carRepository;
        public ClientsController(IClientRepository clientRepository, ICarRepository carRepository)
        {
            _clientRepository = clientRepository;
            _carRepository = carRepository;
        }

        // [HttpGet]
        // [ProducesResponseType(200)]
        // public async Task<IActionResult> GetAll([FromQuery] PaginationPropreties paginationPropreties)
        // {
        //     var clients = await _clientRepository.GetAll(paginationPropreties);
        //     return Ok(clients);
        // }

        // [HttpGet("filter")]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll([FromQuery] ClientFilters filters, [FromQuery] PaginationPropreties paginationPropreties)
        {
            var predicate = PredicateBuilder.New<Client>(true);

            if (!string.IsNullOrEmpty(filters.Name))
            {
                predicate = predicate.And(c => c.Name.Contains(filters.Name));
            }

            if (!string.IsNullOrEmpty(filters.Email))
            {
                predicate = predicate.And(c => c.Email != null && c.Email.Contains(filters.Email));
            }

            if (!string.IsNullOrEmpty(filters.Phone))
            {
                predicate = predicate.And(c => c.Phones != null && c.Phones.Any(p => p.Number.Contains(filters.Phone)));
            }

            var clients = await _clientRepository.GetAll(predicate, paginationPropreties);
            // get carcount for each client

            foreach (var item in clients)
            {
                item.CarsCount = await _carRepository.CountWithClientId(item.Id);
            }

            return Ok(clients);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _clientRepository.Get(x => x.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Create([FromBody] ClientCreateDto clientCreateDto)
        {
            var newClient = new Client
            {
                Name = clientCreateDto.Name,
                Email = clientCreateDto.Email
            };

            await _clientRepository.Add(newClient);
            await _clientRepository.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = newClient.Id }, new {ClientId = newClient.Id });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update(int id, [FromBody] ClientUpdateDto clientUpdateDto)
        {
            var clientToUpdate = await _clientRepository.GetById(id);

            if (clientToUpdate == null) return NotFound();

            if (clientUpdateDto.Name != null)
            {
                clientToUpdate.Name = clientUpdateDto.Name;
            }

            if (clientUpdateDto.Email != null)
            {
                clientToUpdate.Email = clientUpdateDto.Email;
            }

            _clientRepository.Update(clientToUpdate);
            await _clientRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var clientToDelete = await _clientRepository.GetById(id);

            if (clientToDelete == null) return NotFound();

            _clientRepository.Delete(clientToDelete);
            await _clientRepository.SaveChanges();

            return NoContent();
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count([FromQuery] ClientFilters filters)
        {
            var predicate = PredicateBuilder.New<Client>(true);

            if (!string.IsNullOrEmpty(filters.Name))
            {
                predicate = predicate.And(c => c.Name.Contains(filters.Name));
            }

            if (!string.IsNullOrEmpty(filters.Email))
            {
                predicate = predicate.And(c => c.Email != null && c.Email.Contains(filters.Email));
            }

            if (!string.IsNullOrEmpty(filters.Phone))
            {
                predicate = predicate.And(c => c.Phones != null && c.Phones.Any(p => p.Number.Contains(filters.Phone)));
            }

            var count = await _clientRepository.FilterCount(predicate);
            return Ok(count);
        }

    }
}