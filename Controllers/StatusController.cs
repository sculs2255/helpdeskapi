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
    public class StatusController : ControllerBase
    {
        private readonly DataContext _context;
        public StatusController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> List([FromQuery] StatusFilter filter)
        {
            try
            {

                var query = (from s in _context.Status
                             select new
                             {
                                 StatusID = s.StatusID,
                                 StatusName = s.StatusName

                             });

                var DbF = Microsoft.EntityFrameworkCore.EF.Functions;

                if (filter.StatusID > 0)
                {
                    query = query.Where(q => q.StatusID == filter.StatusID);
                }

                /*
               if (!String.IsNullOrEmpty(filter.textSearch))
               {
                   query = query.Where(q => DbF.Like(q.Name, "%" + filter.textSearch + "%"));
               }
               */

             
                switch (filter.sortOrder)
                {
                    case "statusName":
                        query = query.OrderBy(q => q.StatusName);
                        Console.WriteLine("1");
                        break;
                    case "statusName_desc":
                        query = query.OrderByDescending(q => q.StatusName);
                        Console.WriteLine("2");
                        break;
                    case "statusID":
                        query = query.OrderBy(q => q.StatusID);
                        Console.WriteLine("3");
                        break;
                    case "statusID_desc":
                        query = query.OrderByDescending(q => q.StatusID);
                        Console.WriteLine("4");
                        break;
                    default:
                        query = query.OrderBy(q => q.StatusID);
                        Console.WriteLine("0");
                        break;
                }

                int totalItems = query.Count();
                var data = await query.Skip((filter.pageNumber - 1) * filter.pageSize).Take(filter.pageSize).ToListAsync();

                return Ok(new
                {
                    totalItems = totalItems,
                    data = data,
                    filter = filter,
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
                var existingData = await _context.Status.FindAsync(id);
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
        public async Task<IActionResult> Update(int id, [FromBody] StatusRequest request)
        {
            try
            {
                var existingData = await _context.Status.FindAsync(id);
                if (existingData == null)
                {
                    return BadRequest(new
                    {

                        message = "Data NotFound",
                        isSuccess = false
                    });
                }
                existingData.StatusID = request.StatusID;
                existingData.StatusName = request.StatusName;

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
        public async Task<IActionResult> Create([FromBody] StatusRequest request)
        {
            try
            {
                var temp = new Status
                {
                    StatusID = request.StatusID,
                    StatusName = request.StatusName
                };

                _context.Status.Add(temp);
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

                var existingData = await _context.Status.FindAsync(id);

                if (existingData == null)
                {
                    return BadRequest(new
                    {
                        message = "Data NotFound",
                        isSuccess = false
                    });
                }

                _context.Status.Remove(existingData);
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
