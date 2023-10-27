using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessBuddyApp.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public ActionResult<ICollection<Address>>Get()
        {
            var addresses = _addressService.GetAll();
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> Get([FromRoute] int id)
        {
            var address = await _addressService.Get(id);
            return Ok(address);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Address>> Update([FromBody] Address newAddress, [FromRoute] int id) 
        {
            var address = await _addressService.Update(newAddress, id);
            return Ok(address);
        }

        [HttpPost("{clientId}")]
        public ActionResult<bool> Create([FromBody] Address address, [FromRoute] int clientId)
        {
            var isCreated = _addressService.Create(address, clientId);
            return Ok(isCreated);
        }

    }
}
