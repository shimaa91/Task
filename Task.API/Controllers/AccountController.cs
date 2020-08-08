using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Services.IdentityUserServices;
using Task.ViewModels.IdentityUserViewModel;

namespace Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IIdentityUserService _identityUserService;

        public AccountController(IIdentityUserService identityUserService)
        {
            _identityUserService = identityUserService;
        }

        //api/account/register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityUserService.RegisterUserAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);//status  code    200
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties is not valid. ");//status code   400   
        }

        //api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityUserService.LoginrUserAsync(model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return BadRequest("Some  properties is not valid. ");

        }
    }
}
