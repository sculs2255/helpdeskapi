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
    public class CancelController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;

        public CancelController(
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

                var query = await (from r in _context.Cancel
                                       //where r.CaseID == CaseID
                                   select r
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var existingData = await _context.HD_Case.FindAsync(id);
                if (existingData.StatusID == 4)
                {

                    var query = (from cl in _context.Cancel
                                 join c1 in _context.HD_Case on cl.CaseID equals c1.CaseID into c2
                                 from c in c2.DefaultIfEmpty()
                                 select cl
                                );


                    var cancel = await query.FirstOrDefaultAsync();
                    return Ok(new
                    {
                        data = cancel,
                        isSuccess = true
                    });
                }
                else
                {
                    return Ok(new
                    {

                        isSuccess = false
                    });
                }
            }
            catch (Exception)
            {
                return Ok(new
                {
                    isSuccess = false
                });

            }

        }

    }
}