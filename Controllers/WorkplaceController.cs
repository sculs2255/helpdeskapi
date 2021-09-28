using HelpDeskApi.Data;
using HelpDeskApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HelpDeskApi.Models.DTOs.Requests;

namespace HelpDeskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkplaceController : ControllerBase
    {
        private readonly DataContext _context;
        public  WorkplaceController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> List([FromQuery] WorkplaceFilter filter)
        {
            try
            {

                var query = (from m in _context.Workplace
                             select new
                             {
                                 WorkplaceID = m.WorkplaceID,   
                                 UserID = m.UserID,
                                 CountryID = m.CountryID,
                                 BranchID = m.BranchID , 
                                 DepartmentID = m.DepartmentID

                             });

                var DbF = Microsoft.EntityFrameworkCore.EF.Functions;

               if (filter.WorkplaceID > 0)
                {
                    query = query.Where(q => q.WorkplaceID == filter.WorkplaceID);
                }

                /*
               if (!String.IsNullOrEmpty(filter.textSearch))
               {
                   query = query.Where(q => DbF.Like(q.Name, "%" + filter.textSearch + "%"));
               }
               */

       

                switch (filter.sortOrder)
                {
                    case "BranchName":
                        query = query.OrderBy(q => q.WorkplaceID);
                        break;
                    case "BranchName_desc":
                        query = query.OrderByDescending(q => q.WorkplaceID);
                        break;
                    case "BranchID":
                        query = query.OrderBy(q => q.WorkplaceID);
                        break;
                    case "BranchID_desc":
                        query = query.OrderByDescending(q => q.WorkplaceID);
                        break;
                    default:
                        query = query = query.OrderBy(q => q.WorkplaceID);
                        break;
                }

                int totalItems = query.Count();
                var data = await query.Skip((filter.pageNumber - 1) * filter.pageSize).Take(filter.pageSize).ToListAsync();

                return Ok(new
                {
                    totalItems = totalItems,
                    data = data,
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
        public async Task<IActionResult> Details(int id)
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

                return Ok(new
                {
                    data = existingData,
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
