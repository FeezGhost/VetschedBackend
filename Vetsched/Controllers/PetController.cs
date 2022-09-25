using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vetsched.Data.Dtos.Request;
using Vetsched.Data.Dtos.Response;
using Vetsched.Services.PetSrv;

namespace Vetsched.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _service;
        public PetController(
            IPetService service
            )
        {
            _service = service;
        }

        [HttpGet("Pets")]
        public async Task<ActionResult<List<PetResponseDto>>> GetPets(Guid PetLoverId)
        {
            var res = await _service.GetPets(PetLoverId);
            return Ok(res);
        }
        [HttpGet("PetsMinimal")]
        public async Task<ActionResult<List<PetMinimalResponseDto>>> GetPetsMinimal(Guid PetLoverId)
        {
            var res = await _service.GetPetsMinimal(PetLoverId);
            return Ok(res);
        }
        [HttpGet("Pet")]
        public async Task<ActionResult<PetResponseDto>> GetPet(Guid PetId)
        {
            var res = await _service.GetPet(PetId);
            return Ok(res);
        }
        [HttpDelete("Remove")]
        public async Task<ActionResult<bool>> RemovePet(Guid PetId)
        {
            var res = await _service.DeletePet(PetId);
            return Ok(res);
        }
        [HttpPost("Add")]
        public async Task<ActionResult<PetCreateRequestDto>> CreatePet(PetCreateRequestDto request)
        {
            var res = await _service.CreatePet(request);
            return Ok(res);
        }
        [HttpPut("Update")]
        public async Task<ActionResult<bool>> UpdatePet(PetUpdateRequestDto request)
        {
            var res = await _service.UpdatePet(request);
            return Ok(res);
        }
    }
}
