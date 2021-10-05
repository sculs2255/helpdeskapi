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
    public class WorkplaceController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;

        public WorkplaceController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            DataContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

      [HttpGet]
        public async Task<IActionResult> ListAll ()
        {
            try
            {
                var user = await (from wp in _context.Workplace
                                  join u in _context.Users on wp.UserID equals u.Id into cmr
                                  from cmResult in cmr.DefaultIfEmpty()
                                  //where cmResult.Id == id 
                                  
                                      // join cm in _context.Users on u.Id equals cm.UserID into cr
                                      // from crResult in cr.DefaultIfEmpty()
                                  
                                  select new
                                  {
                                      wp.WorkplaceID,
                                      UserID = wp.UserID,
                                      wp.CountryID,
                                      wp.BranchID,
                                      wp.DepartmentID
                                  }
                            ).ToListAsync();

                return Ok(new
                {
                    data = user,
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
        public async Task<IActionResult> Details (string id)
        {
            try
            {
                var user = await (from wp in _context.Workplace
                                  join u in _context.Users on wp.UserID equals u.Id into cmr
                                  from cmResult in cmr.DefaultIfEmpty()
                                  where cmResult.Id == id 
                                  
                                      // join cm in _context.Users on u.Id equals cm.UserID into cr
                                      // from crResult in cr.DefaultIfEmpty()
                                  
                                  select new
                                  {
                                      wp.WorkplaceID,
                                      UserID = wp.UserID,
                                      wp.CountryID,
                                      wp.BranchID,
                                      wp.DepartmentID
                                  }
                            ).ToListAsync();

                return Ok(new
                {
                    data = user,
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
        public async Task<IActionResult> Update(int id, [FromBody] WorkplaceRequest request, int WorkplaceID)
        {
            try
            {
                var existingData = await _context.Workplace.FindAsync(id);
                if (existingData == null)
                {
                    return BadRequest(new
                    {

                        message = "Data NotFound",
                        isSuccess = false
                    });
                }
                existingData.WorkplaceID = request.WorkplaceID;
                existingData.UserID = request.UserID;
                existingData.BranchID = request.BranchID;
                existingData.CountryID = request.CountryID;
                existingData.DepartmentID= request.DepartmentID;

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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WorkplaceRequest request)
        {
            try
            {
                var temp = new Workplace
                {  
                    WorkplaceID = request.WorkplaceID,
                    UserID = request.UserID,
                    BranchID = request.BranchID,
                    CountryID = request.CountryID,
                    DepartmentID = request.DepartmentID
                };

                _context.Workplace.Add(temp);
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


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var existingData = await _context.Workplace.FindAsync(id);

                if (existingData == null)
                {
                    return BadRequest(new
                    {
                        message = "Data NotFound",
                        isSuccess = false
                    });
                }

                _context.Workplace.Remove(existingData);
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
