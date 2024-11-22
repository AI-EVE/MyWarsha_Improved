using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll([FromQuery] ClientFilters filters, [FromQuery] PaginationPropreties paginationPropreties)
        {


            var clients = await _clientRepository.GetAll(filters, paginationPropreties);

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

            return CreatedAtAction(nameof(GetById), new { id = newClient.Id }, new { ClientId = newClient.Id });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update(int id, [FromBody] ClientUpdateDto clientUpdateDto)
        {
            var clientToUpdate = await _clientRepository.GetById(id);

            if (clientToUpdate == null) return NotFound();

            clientToUpdate.Name = clientUpdateDto.Name ?? clientToUpdate.Name;

            clientToUpdate.Email = clientUpdateDto.Email ?? clientToUpdate.Email;

            _clientRepository.Update(clientToUpdate);
            await _clientRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Delete(int id)
        {
            var clientToDelete = await _clientRepository.GetById(id);

            if (clientToDelete == null) return NotFound();

            _clientRepository.Delete(clientToDelete);

            try
            {
                await _clientRepository.SaveChanges();

                return NoContent();
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && sqlException.Number == 547)
            {
                return Conflict(new { message = "This client is associated with a car or a service, please delete the proprety first." });
            }


        }

        [HttpGet("count")]
        public async Task<IActionResult> Count([FromQuery] ClientFilters filters)
        {
            var count = await _clientRepository.FilterCount(filters);
            return Ok(count);
        }

    }
}