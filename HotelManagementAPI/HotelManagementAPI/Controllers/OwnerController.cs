using HotelManagementCommon.Entities;
using HotelManagementCommon.Models;
using HotelManagementService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementAPI.Controllers
{
    [Route("/api/owner")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        private readonly ILogger<OwnerController> _logger;
        public OwnerController(ILogger<OwnerController> logger, IOwnerService ownerService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ownerService = ownerService ?? throw new ArgumentNullException(nameof(ownerService));
        }
        [HttpPost]
        [Route("Login")]
        public  IActionResult Login([FromBody]User user)
        {
            try
            {
               var userDetail=  _ownerService.Login(user);

                return Ok(userDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("AddRooms")]
        [Authorize]
        public async Task<IActionResult> AddRooms(RoomsEntity rooms)
        {
            try
            {
              var result= await _ownerService.AddRooms(rooms);

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("DisplayRooms/{date}")]
        [Authorize]
        public async Task<IActionResult> DisplayRooms([FromRoute] string date)
        {
            try
            {
                CultureInfo culture = CultureInfo.InvariantCulture;
                DateTime convertedDate = DateTime.ParseExact(date, new string[] { "MM.dd.yyyy", "M-d-yyyy", "MM/dd/yyyy" }, culture, DateTimeStyles.None);
                var result=await _ownerService.CheckAvailableRooms(convertedDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
    }
}
