using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vetsched.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        //[HttpPost("Create")]
        //public async Task<ActionResult> Create(AccountRequestDto command)
        //{
        //    return await _mediator.Send(new CreateAccountCommand
        //    {
        //        UserId = GetUserId(),
        //        Principal = HttpContext.User,
        //        Code = command.Code,
        //        Name = command.Name,
        //        Phone = command.Phone,
        //        Url = command.Url,
        //        SwitchCompany = command.SwitchCompany,
        //        VatId = command.VatId,
        //        RoleId = ApplicationRoleConstants.AccountOwner
        //    });
        //}
    }
}
