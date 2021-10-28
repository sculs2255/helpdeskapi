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
    public class InformerController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;

        public InformerController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            DataContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpGet]
        public async Task<ActionResult> List()
        {

            try
            {

                var query = await (from r in _context.Informer
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
                var query = (from inf in _context.Informer
                             join u1 in _context.Users on inf.UserID equals u1.Id into u2
                             from u in u2.DefaultIfEmpty()
                             join c1 in _context.HD_Case on inf.CaseID equals c1.CaseID into c2
                             from c in c2.DefaultIfEmpty()
                             where inf.UserID == u.Id && inf.CaseID == c.CaseID && existingData.CaseID == inf.CaseID
                             join d1 in _context.Workplace on inf.WorkplaceID equals d1.WorkplaceID into cmw
                             from d in cmw.DefaultIfEmpty()

                             // join cm in _context.Users on u.Id equals cm.UserID into cr
                             // from crResult in cr.DefaultIfEmpty()

                             select new
                             {
                                 inf.InformerID,
                                 inf.CaseID,
                                 firstName = u.FirstName,
                                 lastName = u.LastName,
                                 phone = u.PhoneNumber,
                                 email = u.Email,
                                 WorkplaceID = d.WorkplaceID,
                                 

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

    }
}