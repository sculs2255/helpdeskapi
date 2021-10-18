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
         [Route("List")]
        public async Task<ActionResult> List()
        {
            Console.WriteLine("1");
            try
            {

               var user = await (from wp in _context.Workplace
                                  join u in _context.Users on wp.UserID equals u.Id into cmr
                                  from cmResult in cmr.DefaultIfEmpty()
                                  join d1 in _context.Department on wp.DepartmentID equals d1.DepartmentID into cmw
                                  from d in cmw.DefaultIfEmpty()
                                  join b1 in _context.Branch on wp.BranchID equals b1.BranchID into cmb
                                  from b in cmb.DefaultIfEmpty()
                                  join c1 in _context.Country on wp.CountryID equals c1.CountryID into cmc
                                  from c in cmc.DefaultIfEmpty()
                             select new
                             {      
                                 //Workplace join user
                                      wp.WorkplaceID,
                                      UserID = wp.UserID,
                                      wp.DepartmentID,
                                //Department 
                                    BranchID = d.BranchID,
                                    DepartName = d.DepartmentName,
                                
                                //Branch
                                    BranchName= b.BranchName,

                                //country 
                                    CountryName = c.CountryName,

                                    

                                    


                             }).ToListAsync();
                /*
               if (!String.IsNullOrEmpty(filter.textSearch))
               {
                   query = query.Where(q => DbF.Like(q.Name, "%" + filter.textSearch + "%"));
               }
               */

                return Ok(new
                {
                    
                    data = user,
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


        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var user = await (from wp in _context.Workplace
                                  join u in _context.Users on wp.Id equals u.Id into cmr
                                  from cmResult in cmr.DefaultIfEmpty()
                                  where cmResult.Id == id

                                  // join cm in _context.Users on u.Id equals cm.UserID into cr
                                  // from crResult in cr.DefaultIfEmpty()

                                  select new
                                  {
                                      wp.WorkplaceID,
                                      UserID = wp.Id,

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
                existingData.Id = request.Id;
                existingData.DepartmentID = request.DepartmentID;

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
                    Id = request.Id,
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

