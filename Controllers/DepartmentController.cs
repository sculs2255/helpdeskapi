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
    public class DepartmentController : ControllerBase
    {
        private readonly DataContext _context;
        public  DepartmentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> List([FromQuery] DepartmentFilter filter)
        {
            try
            {

                var query = (from p in _context.Department
                             select new
                             {
                                 DepartmentID = p.DepartmentID,
                                 DepartmentName = p.DepartmentName,
                                 p.BranchID


                             });

                var DbF = Microsoft.EntityFrameworkCore.EF.Functions;

                if (filter.BranchID > 0)
                {
                    query = query.Where(q => q.BranchID == filter.BranchID);
                }

                /*
               if (!String.IsNullOrEmpty(filter.textSearch))
               {
                   query = query.Where(q => DbF.Like(q.Name, "%" + filter.textSearch + "%"));
               }
               */

              

                switch (filter.sortOrder)
                {
                    case "DepartmentName":
                        query = query.OrderBy(q => q.DepartmentName);
                        break;
                    case "DepartmentName_desc":
                        query = query.OrderByDescending(q => q.DepartmentName);
                        break;
                    case "DepartmentID":
                        query = query.OrderBy(q => q.DepartmentID);
                        break;
                    case "DepartmentID_desc":
                        query = query.OrderByDescending(q => q.DepartmentID);
                        break;
                    default:
                        query = query = query.OrderBy(q => q.DepartmentID);
                        break;
                }

                int totalItems = query.Count();
                var data = await query.Skip((filter.pageNumber - 1) * filter.pageSize).Take(filter.pageSize).ToListAsync();

                return Ok(new
                {
                    totalItems = totalItems,
                    data = data,
                    filter=filter,
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
                var existingData = await _context.Department.FindAsync(id);
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
        public async Task<IActionResult> Update(int id, [FromBody] DepartmentRequest request, int DepartmentName)
        {
            try
            {
                var existingData = await _context.Department.FindAsync(id);
                if (existingData == null)
                {
                    return BadRequest(new
                    {

                        message = "Data NotFound",
                        isSuccess = false
                    });
                }
                existingData.DepartmentID = request.DepartmentID;
                existingData.DepartmentName = request.DepartmentName;

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
        public async Task<IActionResult> Create([FromBody] DepartmentRequest request)
        {
            try
            {
                var temp = new Department
                {
                    DepartmentID = request.DepartmentID,
                    DepartmentName = request.DepartmentName,
                    BranchID = request.BranchID 
                };

                _context.Department.Add(temp);
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

                var existingData = await _context.Department.FindAsync(id);

                if (existingData == null)
                {
                    return BadRequest(new
                    {
                        message = "Data NotFound",
                        isSuccess = false
                    });
                }

                _context.Department.Remove(existingData);
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
