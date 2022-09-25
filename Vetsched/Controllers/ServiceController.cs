using Microsoft.AspNetCore.Mvc;
using Vetsched.Data.Dtos.Request;
using Vetsched.Data.Dtos.Response;
using Vetsched.Services.ServicesProviderSrv;

namespace Vetsched.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {
        private readonly IServicesProviderService _servicesProviderServic;
        public ServiceController(
            IServicesProviderService servicesProviderServic
            )
        {
            _servicesProviderServic = servicesProviderServic;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ServicesDto>>> GetAll()
        {
            var res = await _servicesProviderServic.GetAllServices();
            return Ok(res);
        }
        [HttpPost("AddService")]
        public async Task<ActionResult<bool>> AddService(AddServicesDto request)
        {
            var res = await _servicesProviderServic.AddServiceToProfile(request);
            return Ok(res);
        }
        [HttpDelete("RemoveService")]
        public async Task<ActionResult<bool>> RemoveService(Guid ProfileId, Guid ServiceId)
        {
            var res = await _servicesProviderServic.RemoveServiceFromProfile(ServiceId, ProfileId);
            return Ok(res);
        }
        [HttpGet("ProviderServices")]
        public async Task<ActionResult<List<ServicesDto>>> ProviderServices(Guid ProfileId)
        {
            var res = _servicesProviderServic.GetProviderServices(ProfileId);
            return Ok(res);
        }
        [HttpGet("ServicesProvider")]
        public async Task<ActionResult<List<UserBaseResponseDto>>> ServicesProvider(Guid ServiceId)
        {
            var res = _servicesProviderServic.GetServicesProvider(ServiceId);
            return Ok(res);
        }
        [HttpGet("GetSingle")]
        public async Task<ActionResult<List<ServicesDto>>> GetService(Guid ServiceId)
        {
            var res = await _servicesProviderServic.GetService(ServiceId);
            return Ok(res);
        }
    }
}
