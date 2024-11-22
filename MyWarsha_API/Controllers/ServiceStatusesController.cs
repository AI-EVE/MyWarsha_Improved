using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;

namespace MyWarsha_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceStatusesController : ControllerBase
    {
        private readonly IServiceStatusRepository _serviceStatusRepository;

        public ServiceStatusesController(IServiceStatusRepository serviceStatusRepository)
        {
            _serviceStatusRepository = serviceStatusRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetServiceStatuses()
        {
            var serviceStatuses = await _serviceStatusRepository.GetServiceStatuses();
            return Ok(serviceStatuses);
        }

        [HttpPost]
        public async Task<IActionResult> AddServiceStatus(ServiceStatus serviceStatus)
        {
            serviceStatus.Id = 0;
            await _serviceStatusRepository.Add(serviceStatus);

            try
            {
                await _serviceStatusRepository.SaveChanges();

                return CreatedAtAction(nameof(GetServiceStatusById), new { id = serviceStatus.Id }, serviceStatus);
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return Conflict(new { message = "Service status with the same name does already exists" });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> DeleteServiceStatusById(int id)
        {
            var serviceStatus = await _serviceStatusRepository.GetServiceStatusById(id);
            if (serviceStatus == null)
            {
                return NotFound();
            }

            try
            {
                _serviceStatusRepository.Delete(serviceStatus);
                await _serviceStatusRepository.SaveChanges();
                return NoContent();
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && sqlException.Number == 547)
            {
                return Conflict(new { message = "Service status is used in other by an already existing services, so it cant be deleted" });
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetServiceStatusById(int id)
        {
            var serviceStatus = await _serviceStatusRepository.GetServiceStatusById(id);
            if (serviceStatus == null)
            {
                return NotFound();
            }
            return Ok(serviceStatus);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetServiceStatusByName(string name)
        {
            var serviceStatus = await _serviceStatusRepository.GetServiceStatusByName(name);
            if (serviceStatus == null)
            {
                return NotFound();
            }
            return Ok(serviceStatus);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        // update description of service status
        public async Task<IActionResult> UpdateServiceStatus(int id, string description, string colorLight, string colorDark, string name)
        {
            var serviceStatus = await _serviceStatusRepository.GetServiceStatusById(id);
            if (serviceStatus == null)
            {
                return NotFound();
            }


            if (description != null)
            {
                serviceStatus.Description = description;
            }

            if (colorLight != null)
            {
                serviceStatus.ColorLight = colorLight;
            }

            if (colorDark != null)
            {
                serviceStatus.ColorDark = colorDark;
            }

            if (name != null)
            {
                serviceStatus.Name = name;
            }

            try
            {
                _serviceStatusRepository.Update(serviceStatus);
                await _serviceStatusRepository.SaveChanges();
                return NoContent();
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return Conflict(new { message = "Service status with the same name does already exists" });
            }
        }
    }
}