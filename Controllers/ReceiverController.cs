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
using System.Text;

namespace HelpDeskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReceiverController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;

        public ReceiverController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            DataContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> List()
        {

            try
            {
                var query = await (from r in _context.Receiver
                                   select new
                                   {
                                       r.ReceiverID,
                                       r.CaseID,
                                       r.UserID,
                                       r.Description,
                                       r.File

                                   }
                            ).ToListAsync();

                return Ok(new
                {
                    data = query,
                    isSuccess = true
                });
            }
            catch (Exception)
            {
                return Ok(new
                {
                    isSuccess = false
                });

            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(ReceiverRequest request)
        {
            try
            {
                var existingData = await _context.Receiver.FindAsync(request.CaseID);

                if (existingData == null)
                {
                    return BadRequest(new
                    {
                        message = "Data NotFound",
                        isSuccess = false
                    });
                }

                existingData.UserID = request.UserID;
                existingData.Description = request.Description;
                existingData.File = request.File;
                await _context.SaveChangesAsync();


                return Ok(new
                {
                    isSuccess = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex,
                    isSuccess = false
                });
            }
        }


    }
}