using CommerceHub.Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommerceHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {


        //Kullanıcı ID'sini her zaman jwtden al -client(mvc)tarafından almayacak
        protected int GetUserId()
        {
            var claim =User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(claim, out int userId) ? userId : 0;
        }

        protected bool IsAdmin()
            =>User.IsInRole("Admin");

        protected IActionResult Ok<T>(T data,string message="İşlem Başarılı")
            =>base.Ok(ApiResponse<T>.SuccessResult(data, message));

        protected IActionResult Created<T>(T data, string message = "Oluşturma İşlemi Başarılı")
            => base.StatusCode(201, ApiResponse<T>.SuccessResult(data, message));


    }
}
