using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyWarsha_DTOs.PhoneDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using MyWarsha_Models.Models;
using Utils.FilteringUtils.PhoneFilters;
using Utils.PageUtils;

namespace MyWarsha_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhonesController : ControllerBase
    {
        private readonly IPhoneRepository _phoneRepository;

        public PhonesController(IPhoneRepository phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationPropreties paginationPropreties, [FromQuery] PhoneFilters phoneFilters)
        {


            var phones = await _phoneRepository.GetAll(phoneFilters, paginationPropreties);

            return Ok(phones);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var phone = await _phoneRepository.GetPhoneDtoById(id);

            if (phone == null)
            {
                return NotFound();
            }

            return Ok(phone);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Create([FromBody] PhoneCreateDto phoneCreateDto)
        {
            var phone = new Phone
            {
                Number = phoneCreateDto.Number,
                ClientId = phoneCreateDto.ClientId
            };

            try
            {
                await _phoneRepository.Add(phone);
                await _phoneRepository.SaveChanges();

                return CreatedAtAction(nameof(GetById), new { id = phone.Id }, new PhoneDto
                {
                    Id = phone.Id,
                    Number = phone.Number,
                    ClientId = phone.ClientId
                });
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return Conflict(new { message = "Phone number already exists" });
            }
        }


        [HttpPost("bulk")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> CreateBulk([FromBody] List<PhoneCreateDto> phoneCreateDto)
        {
            var phones = phoneCreateDto.Select(phone => new Phone
            {
                Number = phone.Number,
                ClientId = phone.ClientId
            }).ToList();

            try
            {
                await _phoneRepository.AddRange(phones);
                await _phoneRepository.SaveChanges();

                return CreatedAtAction(nameof(GetById), new { id = phones.Select(phone => phone.Id) }, phones.Select(phone => new PhoneDto
                {
                    Id = phone.Id,
                    Number = phone.Number,
                    ClientId = phone.ClientId
                }));
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return Conflict(new { message = "Repetetive phone number" });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Update(int id, [FromBody] PhoneUpdateDto phoneUpdateDto)
        {
            var phone = await _phoneRepository.GetById(id);

            if (phone == null)
            {
                return NotFound();
            }

            if (phoneUpdateDto.Number != null)
            {
                phone.Number = phoneUpdateDto.Number;
            }

            try
            {
                _phoneRepository.Update(phone);
                await _phoneRepository.SaveChanges();

                return NoContent();
            }
            catch (DbUpdateException e) when (e.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return Conflict(new { message = "Phone number already exists" });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var phone = await _phoneRepository.GetById(id);

            if (phone == null)
            {
                return NotFound();
            }

            _phoneRepository.Delete(phone);
            await _phoneRepository.SaveChanges();

            return NoContent();
        }

        [HttpGet("count")]
        [ProducesResponseType(200)]
        public IActionResult Count([FromQuery] PhoneFilters phoneFilters)
        {
            var count = _phoneRepository.Count(phoneFilters);
            return Ok(count);
        }

        [HttpGet("count/all")]
        [ProducesResponseType(200)]
        public IActionResult CountAll()
        {
            var count = _phoneRepository.Count();
            return Ok(count);
        }
    }
}