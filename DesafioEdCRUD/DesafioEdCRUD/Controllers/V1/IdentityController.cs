using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEdCRUD.Controllers.V1
{    
    public class IdentityController : ControllerBase
    {
        private readonly IidentityService _identityService;
        public IdentityController(IidentityService iidentityService)
        {
            _identityService = iidentityService;
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.Auth)]
        public IActionResult CreateToken([FromBody] Login login)
        {
            bool validUser = _identityService.Authenticate(login);
            
            if (!validUser)
            {
                return Unauthorized();                
            }

            string tokenString = _identityService.BuildJWTToken();

            return Ok(new AuthSuccessResponse
            {
                Token = tokenString
            });
        }        
    }
}
