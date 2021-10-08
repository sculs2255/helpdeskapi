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
                var query = (from re in _context.Receiver
                             join u1 in _context.Users on re.UserID equals u1.Id into u2
                             from u in u2.DefaultIfEmpty()
                             join c1 in _context.HD_Case on re.CaseID equals c1.CaseID into c2
                             from c in c2.DefaultIfEmpty()
                             where re.UserID == u.Id && re.CaseID == c.CaseID && existingData.CaseID == re.CaseID

                             // join cm in _context.Users on u.Id equals cm.UserID into cr
                             // from crResult in cr.DefaultIfEmpty()

                             select new
                             {
                                 re.ReceiverID,
                                 re.CaseID,
                                 firstName = u.FirstName,
                                 lastName = u.LastName,
                                 phone = u.PhoneNumber,
                                 email = u.Email
                             }
                            );
                var receiver = await query.FirstOrDefaultAsync();
                return Ok(new
                {
                    data = receiver,
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReceiverRequest request)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                var userInfo = await _userManager.FindByEmailAsync(claim[1].Value);

                var existingData = await _context.Receiver.FindAsync(id);

                if (existingData == null)
                {
                    return BadRequest(new
                    {
                        message = "Data NotFound",
                        isSuccess = false
                    });
                }

                var CaseID = existingData.CaseID;
                var existingCase = await _context.HD_Case.FindAsync(CaseID);

                if (existingCase == null)
                {
                    return BadRequest(new
                    {
                        message = "Data NotFound",
                        isSuccess = false
                    });
                }

                if (existingCase.StatusID == 2)
                {
                    existingCase.StatusID = 3;
                    existingData.Description = request.Description;
                    existingData.File = request.File;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    existingCase.StatusID = 2;
                    existingData.UserID = userInfo.Id;

                    await _context.SaveChangesAsync();
                }

                return Ok(new
                {
                    data = CaseID,
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