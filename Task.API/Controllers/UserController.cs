using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Services.UserServices;
using Task.ViewModels.Users;

namespace Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService  userService)
        {
            _userService = userService;
        }
        /// <summary>
        //  /api/User/Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public IActionResult  CreateUser( ManageUserViewModel   model)
        {
            if(ModelState.IsValid)
            {
                var Result = _userService.CreateUser(model);
                if (Result.IsSuccess)
                    return Ok(Result);
               // return BadRequest(Result);
            }
            return BadRequest("Some Proberties isinvalid.   ");
        }

        /// <summary>
        //  /api/User/GetAll
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var Result = _userService.GetAll();
            if (Result.IsSuccess)
                return Ok(Result);
            return BadRequest(Result);
        }

        /// <summary>
        //  /api/User/Get
        /// </summary>
        /// <param name="model">UserId</param>
        /// <returns></returns>
        [HttpGet("Get")]
        public IActionResult Get(int UserId)
        {
            var Result = _userService.GetUserDetails(UserId);
            if (Result.IsSuccess)
                return Ok(Result);
            return BadRequest(Result);
        }

        /// <summary>
        //  /api/User/Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public IActionResult UpdateUser(ManageUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Result = _userService.UpdateUser(model);
                if (Result.IsSuccess)
                    return Ok(Result);
            }
            return BadRequest("Some Proberties isinvalid.   ");
        }

        /// <summary>
        //  /api/User/Delete
        /// </summary>
        /// <param name="model">UserId</param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public IActionResult Delete(int UserId)
        {
            var Result = _userService.DeleteUser(UserId);
            if (Result.IsSuccess)
                return Ok(Result);
            return BadRequest(Result);
        }


    }
}
