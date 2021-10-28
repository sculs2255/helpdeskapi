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
        public async Task<ActionResult> List([FromQuery] WorkplaceFilter filter)
        {
            Console.WriteLine("1");
            try
            {

               var user = (from wp in _context.Workplace
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
                                      UserID = cmResult.Id,
                                      wp.DepartmentID,
                                //Department 
                                BranchID = d.BranchID,
                                DepartName = d.DepartmentName,

                                //Branch
                                BranchName = b.BranchName,

                                //country 
                                CountryName = c.CountryName,

                             });
                var DbF = Microsoft.EntityFrameworkCore.EF.Functions;

                 if (filter.UserID != null)
                {
                   user = user.Where(q => q.UserID == filter.UserID);
                }

                switch (filter.sortOrder)
                {
                    case "WorkplaceID":
                        user= user.OrderBy(q => q.WorkplaceID);
                        break;
                    case "WorkplaceID_desc":
                        user = user.OrderByDescending(q => q.WorkplaceID);
                        break;
                    case "priorityID":
                        user = user.OrderBy(q => q.WorkplaceID);
                        break;
                    case "priorityID_desc":
                        user = user.OrderByDescending(q => q.WorkplaceID);
                        break;   
                        
                    default:
                        user = user = user.OrderBy(q => q.WorkplaceID);
                        break;
                }

                int totalItems = user.Count();
                var data = await user.Skip((filter.pageNumber - 1) * filter.pageSize).Take(filter.pageSize).ToListAsync();

               
              

                
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
                                  join u in _context.Users on wp.UserID equals u.Id into cmr
                                  from cmResult in cmr.DefaultIfEmpty()
                                  where cmResult.Id == id

                                  // join cm in _context.Users on u.Id equals cm.UserID into cr
                                  // from crResult in cr.DefaultIfEmpty()

                                  select new
                                  {
                                      wp.WorkplaceID,
                                      UserID = wp.UserID ,
                                      wp.DepartmentID,
                                      wp.CountryID,
                                      wp.BranchID

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
                existingData.DepartmentID = request.DepartmentID;
                existingData.CountryID = request.CountryID;
                existingData.BranchID = request.BranchID;
                

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
                    UserID  = request.UserID ,
                    DepartmentID = request.DepartmentID,
                    CountryID = request.CountryID,
                    BranchID = request.BranchID
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