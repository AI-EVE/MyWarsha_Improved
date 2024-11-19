using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
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
            var predicate = PredicateBuilder.New<Phone>(true);

            if (!string.IsNullOrEmpty(phoneFilters.Number))
            {
                predicate = predicate.And(p => p.Number.Contains(phoneFilters.Number));
            }

            if (phoneFilters.ClientId != null && phoneFilters.ClientId != 0)
            {
                predicate = predicate.And(p => p.ClientId == phoneFilters.ClientId);
            }

            var phones = await _phoneRepository.GetAll(predicate, paginationPropreties);

            return Ok(phones);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var phone = await _phoneRepository.Get(x => x.Id == id);

            if (phone == null)
            {
                return NotFound();
            }

            return Ok(phone);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] PhoneCreateDto phoneCreateDto)
        {
            var phone = new Phone
            {
                Number = phoneCreateDto.Number,
                ClientId = phoneCreateDto.ClientId
            };

            await _phoneRepository.Add(phone);
            await _phoneRepository.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = phone.Id }, new PhoneDto
            {
                Id = phone.Id,
                Number = phone.Number,
                ClientId = phone.ClientId
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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

            _phoneRepository.Update(phone);
            await _phoneRepository.SaveChanges();

            return NoContent();
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
        public  IActionResult Count([FromQuery] PhoneFilters phoneFilters)
        {
            var predicate = PredicateBuilder.New<Phone>();

            if (!string.IsNullOrEmpty(phoneFilters.Number))
            {
                predicate = predicate.And(p => p.Number.Contains(phoneFilters.Number));
            }

            if (phoneFilters.ClientId != null && phoneFilters.ClientId != 0)
            {
                predicate = predicate.And(p => p.ClientId == phoneFilters.ClientId);
            }

            var count = _phoneRepository.Count(predicate);

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