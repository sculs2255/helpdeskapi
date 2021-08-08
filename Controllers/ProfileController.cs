using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelpDeskApi.Data;
using HelpDeskApi.Models;
using HelpDeskApi.Models.DTOs.Requests;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HelpDeskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;

        public ProfileController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            DataContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("UserInfo")]
        public async Task<ActionResult> UserInfo()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList(); 
                var userInfo = await _userManager.FindByEmailAsync(claim[1].Value);
                var userRole = await _userManager.GetRolesAsync(userInfo);

                return Ok(new
                {
                    data = new { 
                                user = new { 
                                        firstName=userInfo.FirstName,
                                        lastName=userInfo.LastName,
                                        email = userInfo.Email,
                                        id = userInfo.Id,
                                        phoneNumber = userInfo.PhoneNumber,
                                        userName = userInfo.UserName
                                    }, 
                                role = userRole 
                            },
                    isSuccess = true
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    message = ex,
                    isSuccess = false
                });
            }
        }


       
    }
}
