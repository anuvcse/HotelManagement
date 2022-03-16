using HotelManagementCommon.Models;
using HotelManagementService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HotelManagementAPI.Controllers
{
    [Route("/api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<OwnerController> _logger;
        public UserController(ILogger<OwnerController> logger, IUserService userService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }


        [HttpPost]
        [Route("BookRooms")]
        public async Task<IActionResult> BookRoom(BookUserRoom rooms)
        {
            try
            {
               var result= await _userService.BookRoom(rooms);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500,"something went wrong");
            }
        }


        [HttpGet]
        [Route("GetUserHistory/{email}")]
        public async Task<IActionResult> GetUserHistory([FromRoute] string email)
        {
            try
            {
               var result= await _userService.GetHistory(email);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500,"something went wrong");
                
            }
        }
    }
}
